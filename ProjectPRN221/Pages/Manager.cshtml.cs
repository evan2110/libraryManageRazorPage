using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        }
    }
}
