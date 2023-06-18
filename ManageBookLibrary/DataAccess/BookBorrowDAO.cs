using ManageBookLibrary.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBookLibrary.DataAccess
{
    public class BookBorrowDAO
    {
        private static BookBorrowDAO instance = null;
        private static readonly object instanceLock = new object();
        public static BookBorrowDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookBorrowDAO();
                    }
                    return instance;
                }
            }
        }

        public BooksBorrow GetBooksBorrowByID(int? bookBorrowID)
        {
            BooksBorrow bookBorrow = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                bookBorrow = context.BooksBorrows.SingleOrDefault(c => c.BookBorrowId == bookBorrowID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bookBorrow;
        }

        public void AddNew(BooksBorrow bookBorrow)
        {
            try
            {
                using var context = new DatabaseTestProjectContext();
                context.BooksBorrows.Add(bookBorrow);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
