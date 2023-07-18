using ProjectPRN221.BusinessObject2;

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
