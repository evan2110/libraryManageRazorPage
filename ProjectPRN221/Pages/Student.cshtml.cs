using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList;
using static System.Reflection.Metadata.BlobBuilder;

namespace ProjectPRN221.Pages
{
    public class StudentModel : PageModel
    {
        IAccountRepository accountRepository = new AccountRepository();
        IBookRepository bookRepository = new BookRepository();
        IBookBorrowRepository bookBorrowRepository = new BookBorrowRepository();
        public IPagedList<Book> PagedBooks { get; set; }
        public void OnGet(int? handler, int? bookId, string? search)
        {
            if (HttpContext.Session.GetString("UserRole") == "Student" || HttpContext.Session.GetString("UserRole") == "Manager")
            {
                
                var pageNumber = handler ?? 1;
                var pageSize = 10;

                var books = bookRepository.GetBooks();
                if (search != null)
                {
                    books = bookRepository.SearchBooksByTitleOrAuthorOrPublicDateOrLocaion(search.Trim());
                }
                PagedBooks = books.ToPagedList(pageNumber, pageSize);
                var totalBooks = (PagedBooks.Count * PagedBooks.PageCount) / 10;
                
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

                ViewData["account"] = accountRepository.CheckReturnBook(accountRepository.GetAccountByEmailAndPass(new Account(HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"))));
                ViewData["totalBooks"] = totalBooks;
                ViewData["search"] = search;
                ViewData["books"] = PagedBooks;
            }
            else
            {
                Response.Redirect("Error");
            }
            
        }
    }
}
