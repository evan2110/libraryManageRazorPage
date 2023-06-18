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

        public void Update(BooksBorrow booksBorrow)
        {
            try
            {
                BooksBorrow booksBorrowFind = GetBooksBorrowByID(booksBorrow.BookBorrowId);
                if (booksBorrowFind != null)
                {
                        using var context = new DatabaseTestProjectContext();
                        context.BooksBorrows.Update(booksBorrow);
                        context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BookBorrowDTO> GetBooksBorrowByAccountId(int accountID, string roleName)
        {
            List<BookBorrowDTO> listBookBorrow = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                if(roleName == "Student")
                {
                    listBookBorrow = (from a in context.Accounts
                                      join bb in context.BooksBorrows on a.AccountId equals bb.AccountId
                                      join b in context.Books on bb.BookId equals b.BookId
                                      where a.AccountId == accountID
                                      select new BookBorrowDTO
                                      {
                                          BookBorrowId = bb.BookBorrowId,
                                          FirstName = a.FirstName,
                                          LastName = a.LastName,
                                          Email = a.Email,
                                          Phone = a.Phone,
                                          Address = a.Address,
                                          Gender = a.Gender,
                                          DateBorrowed = bb.DateBorrowed,
                                          DueDate = bb.DueDate,
                                          DateReturn = bb.DateReturn,
                                          ReceivedBy = bb.ReceivedBy,
                                          Title = b.Title,
                                          Author = b.Author,
                                          ShelfLocation = b.ShelfLocation
                                      }).ToList();
                }
                else
                {
                    listBookBorrow = (from a in context.Accounts
                                      join bb in context.BooksBorrows on a.AccountId equals bb.AccountId
                                      join b in context.Books on bb.BookId equals b.BookId
                                      select new BookBorrowDTO
                                      {
                                          BookBorrowId = bb.BookBorrowId,
                                          FirstName = a.FirstName,
                                          LastName = a.LastName,
                                          Email = a.Email,
                                          Phone = a.Phone,
                                          Address = a.Address,
                                          Gender = a.Gender,
                                          DateBorrowed = bb.DateBorrowed,
                                          DueDate = bb.DueDate,
                                          DateReturn = bb.DateReturn,
                                          ReceivedBy = bb.ReceivedBy,
                                          Title = b.Title,
                                          Author = b.Author,
                                          ShelfLocation = b.ShelfLocation
                                      }).ToList();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBookBorrow;
        }
    }
}
