using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList;
using ProjectPRN221.BusinessObject3;
using ProjectPRN221.Repository;
using System.Net;
using System.Web.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace ProjectPRN221.Pages
{
    public class BookDetailModel : PageModel
    {
        IAccountRepository accountRepository = new AccountRepository();
        IBookRepository bookRepository = new BookRepository();
        ICommentRepository commentRepository = new CommentRepository();
        public IPagedList<Comment> PagedComments { get; set; }

        public Comment comment { get; set; }

        public void OnGet(int? handler, int? bookId, int? commentIdDelete)
        {
            if (HttpContext.Session.GetString("UserRole") == "Student" || HttpContext.Session.GetString("UserRole") == "Manager")
            {
                if(commentIdDelete != null)
                {
                    commentRepository.DeleteComment(commentIdDelete);
                }
                var pageNumber = handler ?? 1;
                var pageSize = 2;
                var book = bookRepository.GetBookByID(bookId);
                var totalComment = commentRepository.GetAllComments().Where(e => e.BookId == bookId).Count();
                PagedComments = commentRepository.GetAllComments().Where(e => e.BookId == bookId).ToPagedList(pageNumber, pageSize);

                ViewData["book"] = book;
                ViewData["totalComment"] = totalComment;
                ViewData["PagedComments"] = PagedComments;
            }
            else
            {
                Response.Redirect("Error");
            }
        }

        public void OnPost(Comment comment)
        {
            Comment newComment = new Comment();
            newComment.BookId = comment.BookId;
            newComment.AccountId = accountRepository.GetAccountByEmailAndPass(new Account(HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password"))).AccountId;
            newComment.Content = comment.Content;
            newComment.DateComment = DateTime.Now;
            commentRepository.InsertComment(newComment);

            var pageNumber = 1;
            var pageSize = 2;
            var book = bookRepository.GetBookByID(comment.BookId);
            var totalComment = commentRepository.GetAllComments().Where(e => e.BookId == comment.BookId).Count();
            PagedComments = commentRepository.GetAllComments().Where(e => e.BookId == comment.BookId).ToPagedList(pageNumber, pageSize);

            ViewData["book"] = book;
            ViewData["totalComment"] = totalComment;
            ViewData["PagedComments"] = PagedComments;
        }
    }
}
