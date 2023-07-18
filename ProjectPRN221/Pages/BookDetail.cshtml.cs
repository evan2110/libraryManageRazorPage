using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221.BusinessObject3;
using ProjectPRN221.Repository;

namespace ProjectPRN221.Pages
{
    public class BookDetailModel : PageModel
    {
        IAccountRepository accountRepository = new AccountRepository();
        IBookRepository bookRepository = new BookRepository();
        ICommentRepository commentRepository = new CommentRepository();
        public Book book { get; set; }
        public List<Comment> comments { get; set; }

        public string nameComment { get; set; }

        public void OnGet(string? mode, int? bookId)
        {
            book = bookRepository.GetBookByID(bookId);
            comments = commentRepository.GetAllComments().Where(e => e.BookId == bookId).ToList();
        }
    }
}
