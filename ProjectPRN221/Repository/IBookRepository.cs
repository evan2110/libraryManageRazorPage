using ManageBookLibrary.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBookLibrary.Repository
{
    public interface IBookRepository
    {
        List<Book> GetBooks();
        Book GetBookByID(int? bookId);
        void InsertBook(Book book);
        void DeleteBook(int? bookId);
        void UpdateBook(Book book);
        List<Book> SearchBooksByTitleOrAuthorOrPublicDateOrLocaion(string search);
    }
}
