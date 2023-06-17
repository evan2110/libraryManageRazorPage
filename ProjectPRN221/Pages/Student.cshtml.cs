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
        IBookRepository bookRepository = new BookRepository();
        public IPagedList<Book> PagedBooks { get; set; }
        public void OnGet(int? handler)
        {
            if(HttpContext.Session.GetString("UserRole") == "Student")
            {
                var pageNumber = handler ?? 1;
                var pageSize = 10;

                var books = bookRepository.GetBooks();
                PagedBooks = books.ToPagedList(pageNumber, pageSize);
                var totalBooks = (PagedBooks.Count * PagedBooks.PageCount) / 10;

                ViewData["totalBooks"] = totalBooks;
                ViewData["books"] = PagedBooks;
            }
            else
            {
                Response.Redirect("Error");
            }
            
        }
    }
}
