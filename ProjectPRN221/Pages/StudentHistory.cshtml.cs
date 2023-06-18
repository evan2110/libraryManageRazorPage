using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList;

namespace ProjectPRN221.Pages
{
    public class StudentHistoryModel : PageModel
    {
        IBookBorrowRepository bookBorrowRepository = new BookBorrowRepository();
        IAccountRepository accountRepository = new AccountRepository();

        public IPagedList<BookBorrowDTO> PagedBookBorrows { get; set; }

        public void OnGet(int? handler)
        {
            if (HttpContext.Session.GetString("UserRole") == "Student")
            {
                var pageNumber = handler ?? 1;
                var pageSize = 10;

                var booksBorrow = bookBorrowRepository.GetBooksBorrowByAccountId(accountRepository.GetAccountByEmailAndPass(new Account(HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"))).AccountId);
                PagedBookBorrows = booksBorrow.ToPagedList(pageNumber, pageSize);
                var totalBooksBorrow = (PagedBookBorrows.Count * PagedBookBorrows.PageCount) / 10;


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
