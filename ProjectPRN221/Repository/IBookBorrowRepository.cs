using ProjectPRN221.BusinessObject2;

namespace ManageBookLibrary.Repository
{
    public interface IBookBorrowRepository
    {
        void InsertBookBorrow(BooksBorrow booksBorrow);

        List<BookBorrowDTO> GetBooksBorrowByAccountId(int? accountID, string roleName);
        List<BookBorrowDTO> GetBooksBorrowByBookId(int? bookID);


        void UpdateBookBorrow(BooksBorrow booksBorrow);
        void DeleteBookBorrow(int? id);

        BooksBorrow GetBooksBorrowByID(int? booksBorrowId);
        List<BooksBorrow> GetBooksBorrows();
        List<BooksBorrow> SearchBookBorrowByReceivedBy(string? search);
    }
}
