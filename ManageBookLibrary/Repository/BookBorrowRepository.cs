using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.DataAccess;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBookLibrary.Repository
{
    public class BookBorrowRepository : IBookBorrowRepository
    {
        public List<BookBorrowDTO> GetBooksBorrowByAccountId(int accountId, string roleName) => BookBorrowDAO.Instance.GetBooksBorrowByAccountId(accountId, roleName);

        public BooksBorrow GetBooksBorrowByID(int? booksBorrowId) => BookBorrowDAO.Instance.GetBooksBorrowByID(booksBorrowId);

        public void InsertBookBorrow(BooksBorrow booksBorrow) => BookBorrowDAO.Instance.AddNew(booksBorrow);

        public void UpdateBookBorrow(BooksBorrow booksBorrow) => BookBorrowDAO.Instance.Update(booksBorrow);

    }
}
