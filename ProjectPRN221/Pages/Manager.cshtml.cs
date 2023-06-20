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
            if(bookId != null)
            {
                var book = bookRepository.GetBookByID(bookId);
                ViewData["book"] = book;
            }
        }

        public void OnPost(Book book)
        {
            Console.WriteLine(book.BookId);
            if (book.BookId != 0)
            {
                bookRepository.UpdateBook(book);
                var bookFind = bookRepository.GetBookByID(book.BookId);
                ViewData["book"] = bookFind;
                ViewData["infor"] = "updatesuss";
                Response.Redirect("Manager");
            }
            else
            {
                bookRepository.InsertBook(book);
                ViewData["infor"] = "insertsuss";
                Response.Redirect("Manager");
            }
        }
    }
}
