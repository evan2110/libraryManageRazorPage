using ManageBookLibrary.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBookLibrary.DataAccess
{
    public class BookDAO
    {
        private static BookDAO instance = null;
        private static readonly object instanceLock = new object();
        public static BookDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Book> GetBookList()
        {
            List<Book> listBook = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                listBook = context.Books.Select(b => new Book
                {
                    BookId = b.BookId,
                    Author = b.Author,
                    AvailableCopies = b.AvailableCopies,
                    PublicationDate = b.PublicationDate,
                    ShelfLocation = b.ShelfLocation,
                    Title = b.Title,
                    TotalCopies = b.TotalCopies,
                })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listBook;
        }

        public Book GetBookByID(int? bookID)
        {
            Book book = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                book = context.Books.SingleOrDefault(c => c.BookId == bookID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return book;
        }

        public void AddNew(Book book)
        {
            try
            {
                Book bookFind = GetBookByID(book.BookId);
                if (bookFind == null)
                {
                    if (book.AvailableCopies > book.TotalCopies)
                    {
                        throw new Exception("The Available Copies can not greater than Total Copies.");
                    }
                    else
                    {
                        using var context = new DatabaseTestProjectContext();
                        context.Books.Add(book);
                        context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The book is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Book book)
        {
            try
            {
                Book bookFind = GetBookByID(book.BookId);
                if (bookFind != null)
                {
                    if (book.AvailableCopies > book.TotalCopies)
                    {
                        throw new Exception("The Available Copies can not greater than Total Copies.");
                    }
                    else
                    {
                        using var context = new DatabaseTestProjectContext();
                        context.Books.Update(book);
                        context.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("The book does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int bookID)
        {
            try
            {
                Book bookbookFind = GetBookByID(bookID);
                if (bookbookFind != null)
                {
                    using var context = new DatabaseTestProjectContext();
                    context.Books.Remove(bookbookFind);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The book does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Book> SearchBooksByTitleOrAuthorOrPublicDateOrLocaion(string search)
        {
            List<Book> books = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                books = context.Books.Where(b =>
                b.Title.Contains(search) ||
                b.Author.Contains(search) ||
                b.ShelfLocation.Contains(search)
            ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return books;
        }
    }
}
