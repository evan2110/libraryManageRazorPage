using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectPRN221.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        IBookBorrowRepository bookBorrowRepository = new BookBorrowRepository();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ViewData["topBookBorrow"] = bookBorrowRepository.GetTopBookBorrow();
        }
    }
}