using ManageBookLibrary.DataAccess;
using ProjectPRN221.BusinessObject3;

namespace ManageBookLibrary.Repository
{
    public class BookRepository : IBookRepository
    {
        public void DeleteBook(int? bookId) => BookDAO.Instance.Remove(bookId);


        public Book GetBookByID(int? bookId) => BookDAO.Instance.GetBookByID(bookId);


        public List<Book> GetBooks() => BookDAO.Instance.GetBookList();


        public void InsertBook(Book book) => BookDAO.Instance.AddNew(book);

        public List<Book> SearchBooksByTitleOrAuthorOrPublicDateOrLocaion(string search) => BookDAO.Instance.SearchBooksByTitleOrAuthorOrPublicDateOrLocaion(search);

        public void UpdateBook(Book book) => BookDAO.Instance.Update(book);
    }
}
