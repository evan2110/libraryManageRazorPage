using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using PagedList;
using ProjectPRN221.BusinessObject2;
using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace ProjectPRN221.Pages
{
    public class StudentModel : PageModel
    {
        IAccountRepository accountRepository = new AccountRepository();
        IBookRepository bookRepository = new BookRepository();
        IBookBorrowRepository bookBorrowRepository = new BookBorrowRepository();
        public IPagedList<Book> PagedBooks { get; set; }
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public StudentModel(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public void OnGet(int? handler, int? bookId, string? search, int? bookIdDelete, string? fromDate, string? toDate)
        {
            
            if (HttpContext.Session.GetString("UserRole") == "Student" || HttpContext.Session.GetString("UserRole") == "Manager")
            {
                var pageNumber = handler ?? 1;
                var pageSize = 10;

                var books = bookRepository.GetBooks();

                if (fromDate != null && toDate == null && search == null)
                {
                    books = bookRepository.GetBooks().Where(b => b.PublicationDate >= DateTime.Parse(fromDate)).ToList();
                }else if(toDate != null && fromDate == null & search == null)
                {
                    books = bookRepository.GetBooks().Where(b => b.PublicationDate <= DateTime.Parse(toDate)).ToList();

                }else if(search != null && fromDate == null && toDate == null)
                {
                    books = bookRepository.GetBooks().Where(e => e.Title.Contains(search.Trim()) || e.Author.Contains(search.Trim()) || e.ShelfLocation.Contains(search.Trim())).ToList();
                }else if(fromDate != null && toDate != null && search == null)
                {
                    books = bookRepository.GetBooks().Where(b => b.PublicationDate <= DateTime.Parse(toDate) && b.PublicationDate >= DateTime.Parse(fromDate)).ToList();
                }else if(search != null && fromDate != null && toDate == null)
                {
                    books = bookRepository.GetBooks().Where(e => e.Title.Contains(search.Trim()) || e.Author.Contains(search.Trim()) || e.ShelfLocation.Contains(search.Trim()) && e.PublicationDate >= DateTime.Parse(fromDate)).ToList();
                }
                else if(search != null && toDate != null && fromDate == null)
                {
                    books = bookRepository.GetBooks().Where(e => e.Title.Contains(search.Trim()) || e.Author.Contains(search.Trim()) || e.ShelfLocation.Contains(search.Trim()) && e.PublicationDate <= DateTime.Parse(toDate)).ToList();
                }else if(search != null && fromDate != null && toDate != null)
                {
                    books = bookRepository.GetBooks().Where(e => e.Title.Contains(search.Trim()) || e.Author.Contains(search.Trim()) || e.ShelfLocation.Contains(search.Trim()) && e.PublicationDate <= DateTime.Parse(toDate) && e.PublicationDate >= DateTime.Parse(fromDate)).ToList();
                }

                PagedBooks = books.ToPagedList(pageNumber, pageSize);
                var totalBooks = PagedBooks.PageCount;
                
                if(bookId != null)
                {
                    Book book = bookRepository.GetBookByID(bookId);
                    book.AvailableCopies--;
                    bookRepository.UpdateBook(book);

                    BooksBorrow booksBorrow = new BooksBorrow(accountRepository.GetAccountByEmailAndPass(new Account(HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"))).AccountId,
                                                                bookId, DateTime.Now, DateTime.Now.AddDays(7),  true, null, HttpContext.Session.GetString("Email"));

                    bookBorrowRepository.InsertBookBorrow(booksBorrow);
                    Response.Redirect($"Student?handler={PagedBooks.PageNumber}");

                }

                if(bookIdDelete != null)
                {
                    List<BookBorrowDTO> bookBorrowDTO = bookBorrowRepository.GetBooksBorrowByBookId(bookIdDelete);
                    foreach (var b in bookBorrowDTO)
                    {
                        bookBorrowRepository.DeleteBookBorrow(b.BookBorrowId);
                    }
                    bookRepository.DeleteBook(bookIdDelete);
                    
                    Response.Redirect("Student");
                }

                if (fromDate != null)
                {

                }

                ViewData["account"] = accountRepository.CheckReturnBook(accountRepository.GetAccountByEmailAndPass(new Account(HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"))));
                ViewData["totalBooks"] = totalBooks;
                ViewData["search"] = search;
                ViewData["fromDate"] = fromDate;
                ViewData["toDate"] = toDate;
                ViewData["books"] = PagedBooks;
            }
            else
            {
                Response.Redirect("Error");
            }
            
        }

        [Required(ErrorMessage = "Chọn một file")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif")]
        [Display(Name = "Chọn file upload")]
        [BindProperty]
        public IFormFile FileUpload { get; set; }
        public async Task OnPostAsync()
        {
            var filePath = string.Empty;

            if (FileUpload != null)
            {
                var uploadsFolder = Path.Combine(_environment.ContentRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                filePath = Path.Combine(uploadsFolder, FileUpload.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(stream);
                }
                var absoluteFilePath = Path.GetFullPath(filePath);

                var extension = absoluteFilePath.Split('.');
                if (extension[1] == "xlsx")
                {
                    using (var package = new ExcelPackage(new FileInfo(absoluteFilePath)))
                    {
                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet != null)
                        {
                            int rowCount = worksheet.Dimension.Rows;
                            List<Book> lstbooks = new List<Book>();

                            for (int row = 2; row <= rowCount; row++) // Bắt đầu từ dòng 2, bỏ qua dòng tiêu đề
                            {
                                var book = new Book();
                                book.Title = worksheet.Cells[row, 1].Value?.ToString();
                                book.Author = worksheet.Cells[row, 2].Value?.ToString();
                                book.PublicationDate = DateTime.Parse(worksheet.Cells[row, 3].Value?.ToString());
                                book.AvailableCopies = int.Parse(worksheet.Cells[row, 4].Value?.ToString());
                                book.TotalCopies = int.Parse(worksheet.Cells[row, 5].Value?.ToString());
                                book.ShelfLocation = worksheet.Cells[row, 6].Value?.ToString();
                                if(book.Title == null || book.Author == null || book.PublicationDate == null ||
                                    book.AvailableCopies == null || book.TotalCopies == null || book.ShelfLocation == null
                                    || book.TotalCopies < book.AvailableCopies)
                                {
                                    continue;
                                }
                                lstbooks.Add(book);
                            }

                            // Lưu trữ danh sách sách vào database hoặc thực hiện các xử lý khác tùy thuộc vào yêu cầu của bạn

                            // Ví dụ: Lưu vào database
                            foreach (var book in lstbooks)
                            {
                                bookRepository.InsertBook(book);
                            }
                        }
                    }
                }
            }
            else
            {

            }
            var books = bookRepository.GetBooks();
            var pageNumber = 1;
            var pageSize = 10;
            PagedBooks = books.ToPagedList(pageNumber, pageSize);
            var totalBooks = PagedBooks.PageCount;
            ViewData["account"] = accountRepository.CheckReturnBook(accountRepository.GetAccountByEmailAndPass(new Account(HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"))));
            ViewData["books"] = PagedBooks;
        }
    }
}
