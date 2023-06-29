using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace ProjectPRN221.Pages
{
    
    public class ManagerModel : PageModel
    {
        
        IBookRepository bookRepository = new BookRepository();
        public Book book { get; set; }

        public void OnGet(int? bookId)
        {
            if (HttpContext.Session.GetString("UserRole") == "Manager")
            {
                if (bookId != null)
                {
                    var book = bookRepository.GetBookByID(bookId);
                    ViewData["book"] = book;
                }
            }
            else
            {
                Response.Redirect("Error");
            }
        }

        public void OnPost(Book book)
        {
            
            if (book.BookId != 0)
            {
                if (book.TotalCopies < book.AvailableCopies)
                {
                    var bookFind = bookRepository.GetBookByID(book.BookId);
                    ViewData["book"] = book;
                    ViewData["oversize"] = "oversize";
                }
                else
                {
                    bookRepository.UpdateBook(book);
                    var bookFind = bookRepository.GetBookByID(book.BookId);
                    ViewData["book"] = bookFind;
                    ViewData["infor"] = "updatesuss";
                }
            }
            else
            {
                if (book.TotalCopies < book.AvailableCopies)
                {
                    ViewData["oversize"] = "oversize";
                    ViewData["book"] = book;
                }
                else
                {
                    bookRepository.InsertBook(book);
                    ViewData["infor"] = "insertsuss";
                }
            }
        }
    }
}
