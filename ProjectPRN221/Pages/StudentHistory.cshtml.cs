using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList;
using ProjectPRN221.BusinessObject3;

namespace ProjectPRN221.Pages
{
    public class StudentHistoryModel : PageModel
    {
        IBookBorrowRepository bookBorrowRepository = new BookBorrowRepository();
        IAccountRepository accountRepository = new AccountRepository();
        IBookRepository bookRepository = new BookRepository();

        public IPagedList<BookBorrowDTO> PagedBookBorrows { get; set; }

        public void OnGet(int? handler, int? id)
        {
            if (HttpContext.Session.GetString("UserRole") == "Student" || HttpContext.Session.GetString("UserRole") == "Manager")
            {
                var pageNumber = handler ?? 1;
                var pageSize = 10;

                var booksBorrow = bookBorrowRepository.GetBooksBorrowByAccountId(accountRepository.GetAccountByEmailAndPass(new Account(HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"))).AccountId, HttpContext.Session.GetString("UserRole"));
                PagedBookBorrows = booksBorrow.ToPagedList(pageNumber, pageSize);
                var totalBooksBorrow = PagedBookBorrows.PageCount;

                if(id != null)
                {
                    var booksBorrowFind = bookBorrowRepository.GetBooksBorrowByID(id);
                    var b = bookRepository.GetBookByID(booksBorrowFind.BookId);
                    b.AvailableCopies++;
                    bookRepository.UpdateBook(b);

                    booksBorrowFind.DateReturn = DateTime.Now;
                    bookBorrowRepository.UpdateBookBorrow(booksBorrowFind);
                    ViewData["id"] = id;
                    Response.Redirect("StudentHistory");
                }


                ViewData["totalBooksBorrow"] = totalBooksBorrow;
                ViewData["booksBorrow"] = PagedBookBorrows;
            }
            else
            {
                Response.Redirect("Error");
            }
        }
    }
}
