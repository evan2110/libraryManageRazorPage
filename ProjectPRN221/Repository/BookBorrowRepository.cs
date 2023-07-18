using ManageBookLibrary.DataAccess;
using ProjectPRN221.BusinessObject3;

namespace ManageBookLibrary.Repository
{
    public class BookBorrowRepository : IBookBorrowRepository
    {
        public void DeleteBookBorrow(int? id) => BookBorrowDAO.Instance.DeleteBookBorrow(id);

        public List<BookBorrowDTO> GetBooksBorrowByAccountId(int? accountId, string roleName) => BookBorrowDAO.Instance.GetBooksBorrowByAccountId(accountId, roleName);

        public List<BookBorrowDTO> GetBooksBorrowByBookId(int? bookID) => BookBorrowDAO.Instance.GetBooksBorrowByBookId(bookID);

        public BooksBorrow GetBooksBorrowByID(int? booksBorrowId) => BookBorrowDAO.Instance.GetBooksBorrowByID(booksBorrowId);

        public List<BooksBorrow> GetBooksBorrows() => BookBorrowDAO.Instance.GetBooksBorrows();

        public void InsertBookBorrow(BooksBorrow booksBorrow) => BookBorrowDAO.Instance.AddNew(booksBorrow);

        public List<BooksBorrow> SearchBookBorrowByReceivedBy(string? search) => BookBorrowDAO.Instance.SearchBookBorrowByReceivedBy(search);

        public void UpdateBookBorrow(BooksBorrow booksBorrow) => BookBorrowDAO.Instance.Update(booksBorrow);

    }
}
