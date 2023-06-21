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
        public void DeleteBookBorrow(int id) => BookBorrowDAO.Instance.DeleteBookBorrow(id);

        public List<BookBorrowDTO> GetBooksBorrowByAccountId(int? accountId, string roleName) => BookBorrowDAO.Instance.GetBooksBorrowByAccountId(accountId, roleName);

        public List<BookBorrowDTO> GetBooksBorrowByBookId(int? bookID) => BookBorrowDAO.Instance.GetBooksBorrowByBookId(bookID);

        public BooksBorrow GetBooksBorrowByID(int? booksBorrowId) => BookBorrowDAO.Instance.GetBooksBorrowByID(booksBorrowId);

        public void InsertBookBorrow(BooksBorrow booksBorrow) => BookBorrowDAO.Instance.AddNew(booksBorrow);

        public void UpdateBookBorrow(BooksBorrow booksBorrow) => BookBorrowDAO.Instance.Update(booksBorrow);

    }
}
