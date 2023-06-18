using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBookLibrary.Repository
{
    public class BookBorrowRepository : IBookBorrowRepository
    {
        public List<BookBorrowDTO> GetBooksBorrowByAccountId(int accountId) => BookBorrowDAO.Instance.GetBooksBorrowByAccountId(accountId);


        public void InsertBookBorrow(BooksBorrow booksBorrow) => BookBorrowDAO.Instance.AddNew(booksBorrow);
    }
}
