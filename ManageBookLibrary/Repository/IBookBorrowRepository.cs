using ManageBookLibrary.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBookLibrary.Repository
{
    public interface IBookBorrowRepository
    {
        void InsertBookBorrow(BooksBorrow booksBorrow);

        List<BookBorrowDTO> GetBooksBorrowByAccountId(int accountID, string roleName);

        void UpdateBookBorrow(BooksBorrow booksBorrow);

        BooksBorrow GetBooksBorrowByID(int? booksBorrowId);
    }
}
