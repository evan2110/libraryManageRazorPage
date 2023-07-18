using Microsoft.EntityFrameworkCore;
using ProjectPRN221.BusinessObject3;

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

        public List<BookBorrowDTO> GetBooksBorrowByAccountId(int? accountID, string roleName)
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

        public void DeleteBookBorrow(int? id)
        {
            try
            {
                BooksBorrow bookborrowFind = GetBooksBorrowByID(id);
                if (bookborrowFind != null)
                {
                    using var context = new DatabaseTestProjectContext();
                    context.BooksBorrows.Remove(bookborrowFind);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The book borrow does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BookBorrowDTO> GetBooksBorrowByBookId(int? bookID)
        {
            List<BookBorrowDTO> listBookBorrow = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                
                    listBookBorrow = (from b in context.Books
                                      join bb in context.BooksBorrows on b.BookId equals bb.BookId
                                      where bb.BookId == bookID
                                      select new BookBorrowDTO
                                      {
                                          BookBorrowId = bb.BookBorrowId,
                                          DateBorrowed = bb.DateBorrowed,
                                          DueDate = bb.DueDate,
                                          DateReturn = bb.DateReturn,
                                          ReceivedBy = bb.ReceivedBy,
                                          Title = b.Title,
                                          Author = b.Author,
                                          ShelfLocation = b.ShelfLocation
                                      }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBookBorrow;
        }

        public List<BooksBorrow> GetBooksBorrows()
        {
            List<BooksBorrow> listBookBorrow = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                listBookBorrow = context.BooksBorrows.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBookBorrow;
        }

        public List<BooksBorrow> SearchBookBorrowByReceivedBy(string search)
        {
            List<BooksBorrow> booksBorrows = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                booksBorrows = context.BooksBorrows.Where(b =>
                b.ReceivedBy.Trim().Contains(search.Trim()) 
            ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return booksBorrows;
        }

        public List<TopBookBorrow> GetTopBookBorrow()
        {
            using var context = new DatabaseTestProjectContext();

            var result = (from bb in context.BooksBorrows
                          join b in context.Books on bb.BookId equals b.BookId
                          group b by new { b.BookId, b.Image } into g
                          orderby g.Count() descending
                          select new TopBookBorrow
                          {
                              BookId = g.Key.BookId,
                              BorrowCount = g.Count(),
                              Image = g.Key.Image
                          }).Take(5).ToList();
            return result;
        }
    }
}
