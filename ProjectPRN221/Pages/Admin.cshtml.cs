using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList;
using ProjectPRN221.BusinessObject3;
using System.Data;

namespace ProjectPRN221.Pages
{
    public class AdminModel : PageModel
    {
        IRoleRepository roleRepository = new RoleRepository();
        IAccountRepository accountRepository = new AccountRepository();
        IBookBorrowRepository borrowRepository = new BookBorrowRepository();
        IBookRepository bookRepository = new BookRepository();
        public IPagedList<Role> PagedRoles { get; set; }
        public IPagedList<Account> PagedAccounts { get; set; }
        public IPagedList<Book> PagedBooks { get; set; }
        public IPagedList<BooksBorrow> PagedBooksBorrows { get; set; }


        public Role role { get; set; }
        public Account acc { get; set; }
        public Book book { get; set; }
        public BooksBorrow bookBorrow { get; set; }


        public void OnGet(string? mode, int? handler, int? id, string? search, int? idDelete)
        {
            if (HttpContext.Session.GetString("UserRole") == "Admin")
            {
                List<Role> listRoles = roleRepository.GetRoles().Where(r => r.RoleName != "Admin").ToList();
                ViewData["listRoles"] = listRoles;
                ViewData["search"] = search;


                if (mode == "deleteRole")
                {
                    List<Account> accounts = accountRepository.GetAccountByRoleId(idDelete);
                    foreach (var a in accounts)
                    {
                        accountRepository.DeleteAccount(a.AccountId);
                    }

                    roleRepository.DeleteRole(idDelete);
                    Response.Redirect("Admin?mode=role");
                }
                if (mode == "createRole")
                {
                    ViewData["mode"] = "createRole";
                }
                if (mode == "editRole")
                {
                    var role = roleRepository.GetRoleById(id);
                    ViewData["mode"] = "createRole";
                    ViewData["role"] = role;
                }
                if (mode == "role")
                {
                    var roles = roleRepository.GetRoles();
                    if (search != null)
                    {
                        roles = roleRepository.SearchRoleByName(search.Trim());
                    }
                    var pageNumber = handler ?? 1;
                    var pageSize = 10;
                    PagedRoles = roles.ToPagedList(pageNumber, pageSize);
                    var totalRoles = PagedRoles.PageCount;
                    ViewData["totalRoles"] = totalRoles;
                    ViewData["roles"] = PagedRoles;
                    ViewData["mode"] = "role";
                }
                if (mode == "acc")
                {
                    var accs = accountRepository.GetAllAccounts();
                    if (search != null)
                    {
                        accs = accountRepository.SearchAccByNameOrEmailOrPhoneOrAddress(search.Trim());
                    }
                    var pageNumber = handler ?? 1;
                    var pageSize = 10;
                    PagedAccounts = accs.ToPagedList(pageNumber, pageSize);
                    var totalAccs = PagedAccounts.PageCount;
                    ViewData["totalAccs"] = totalAccs;
                    ViewData["accs"] = PagedAccounts;
                    ViewData["mode"] = "acc";
                }
                if (mode == "editAcc")
                {
                    var acc = accountRepository.GetAccountById(id);
                    ViewData["mode"] = "createAcc";
                    ViewData["acc"] = acc;
                }
                if (mode == "deleteAcc")
                {
                    List<BookBorrowDTO> bookBorrowDTOs = borrowRepository.GetBooksBorrowByAccountId(idDelete, "Student");
                    foreach (var bb in bookBorrowDTOs)
                    {
                        borrowRepository.DeleteBookBorrow(bb.BookBorrowId);
                    }
                    accountRepository.DeleteAccount(idDelete);
                    Response.Redirect("Admin?mode=acc");
                }
                if (mode == "book")
                {
                    var books = bookRepository.GetBooks();
                    if (search != null)
                    {
                        books = bookRepository.SearchBooksByTitleOrAuthorOrPublicDateOrLocaion(search.Trim());
                    }
                    var pageNumber = handler ?? 1;
                    var pageSize = 10;
                    PagedBooks = books.ToPagedList(pageNumber, pageSize);
                    var totalBooks = PagedBooks.PageCount;
                    ViewData["totalBooks"] = totalBooks;
                    ViewData["books"] = PagedBooks;
                    ViewData["mode"] = "book";
                }
                if (mode == "createBook")
                {
                    ViewData["mode"] = "createBook";
                }
                if (mode == "editBook")
                {
                    var book = bookRepository.GetBookByID(id);
                    ViewData["mode"] = "createBook";
                    ViewData["book"] = book;
                }
                if (mode == "deleteBook")
                {
                    List<BookBorrowDTO> bookBorrowDTO = borrowRepository.GetBooksBorrowByBookId(idDelete);
                    foreach (var b in bookBorrowDTO)
                    {
                        borrowRepository.DeleteBookBorrow(b.BookBorrowId);
                    }
                    bookRepository.DeleteBook(idDelete);
                    Response.Redirect("/Admin?mode=book");
                }
                if (mode == "bookborrow")
                {
                    var bookborrows = borrowRepository.GetBooksBorrows();
                    if (search != null)
                    {
                        bookborrows = borrowRepository.SearchBookBorrowByReceivedBy(search.Trim());
                    }
                    var pageNumber = handler ?? 1;
                    var pageSize = 10;
                    PagedBooksBorrows = bookborrows.ToPagedList(pageNumber, pageSize);
                    var totalBookBorrows = PagedBooksBorrows.PageCount;
                    ViewData["totalBookBorrows"] = totalBookBorrows;
                    ViewData["bookBorrows"] = PagedBooksBorrows;
                    ViewData["mode"] = "bookborrow";
                }
                if (mode == "createBookBorrow")
                {
                    var accounts = accountRepository.GetAllAccounts().Where(a => a.RoleId == 3).ToList();
                    var books = bookRepository.GetBooks().Where(b => b.AvailableCopies > 0).ToList();

                    ViewData["lstAcc"] = accounts;
                    ViewData["lstBook"] = books;

                    ViewData["mode"] = "createBookBorrow";
                }
                if (mode == "editBookBorrow")
                {
                    var bookBorrow = borrowRepository.GetBooksBorrowByID(id);
                    ViewData["mode"] = "createBookBorrow";
                    var accounts = accountRepository.GetAllAccounts().Where(a => a.RoleId == 3).ToList();
                    var books = bookRepository.GetBooks().Where(b => b.AvailableCopies > 0).ToList();

                    ViewData["lstAcc"] = accounts;
                    ViewData["lstBook"] = books;
                    ViewData["bookBorrow"] = bookBorrow;
                }
                if (mode == "deleteBookBorrow")
                {
                    var bo = borrowRepository.GetBooksBorrowByID(idDelete);
                    Book b = bookRepository.GetBookByID(bo.BookId);
                    b.AvailableCopies++;
                    bookRepository.UpdateBook(b);
                    borrowRepository.DeleteBookBorrow(idDelete);
                    Response.Redirect("Admin?mode=bookborrow");
                }
                if (mode == null || mode == "dashboard")
                {
                    ViewData["totalBorrow"] = borrowRepository.GetBooksBorrows().Count;
                    ViewData["totalAcc"] = accountRepository.GetAllAccounts().Count;
                    ViewData["totalBook"] = bookRepository.GetBooks().Count;
                    ViewData["totalReturn"] = borrowRepository.GetBooksBorrows()
                                    .Where(borrow => borrow.DateReturn != null).Count();
                    var lstRecentBorrow = borrowRepository.GetBooksBorrows().Take(10);

                    IOrderedEnumerable<BooksBorrow> result = from s in lstRecentBorrow
                                                             orderby s.DateBorrowed descending
                                                             select s;
                    ViewData["recentBorrow"] = result;

                    var lstTopBorrow = borrowRepository.GetBooksBorrows().Take(10);
                    var accounts = lstTopBorrow.GroupBy(borrow => borrow.AccountId)
                              .Select(group => group.Key);
                    Dictionary<string, int> dict = new Dictionary<string, int>();
                    foreach (var account in accounts)
                    {
                        var lst = borrowRepository.GetBooksBorrowByAccountId(account, "Student").Count();
                        string accountName = accountRepository.GetAccountById(account).FirstName + accountRepository.GetAccountById(account).LastName;
                        dict[accountName] = lst;
                    }
                    ViewData["topBorrow"] = dict.OrderByDescending(pair => pair.Value)
                         .ToDictionary(pair => pair.Key, pair => pair.Value);

                    ViewData["mode"] = "dashboard";
                }
            }
            else
            {
                Response.Redirect("Error");
            }

        }

        public void OnPost(Role? role, Account? acc, Book? book, BooksBorrow? booksBorrow)
        {
            if (role.RoleName != null)
            {
                
                if (role.RoleId != 0)
                {
                    var roleFind = roleRepository.GetRoleById(role.RoleId);

                    if (roleRepository.GetRoles().Where(r => r.RoleName != roleFind.RoleName).
                        FirstOrDefault(r => r.RoleName == role.RoleName) != null)
                    {
                        ViewData["roleExist"] = "suss";
                    }
                    else
                    {
                        roleRepository.UpdateRole(role);
                        ViewData["updateRoleSuss"] = "suss";
                    }
                    ViewData["mode"] = "createRole";
                    ViewData["role"] = role;

                }
                else
                {
                    if (roleRepository.GetRoles().
                        FirstOrDefault(r => r.RoleName == role.RoleName) != null)
                    {
                        ViewData["roleExist"] = "suss";
                        ViewData["role"] = role;
                    }
                    else
                    {
                        roleRepository.InsertRole(role);
                        ViewData["createRoleSuss"] = "suss";
                    }
                }

                ViewData["mode"] = "createRole";
            }
            else if (acc.FirstName != null)
            {
                if (acc.AccountId != 0)
                {
                    Account accFind = accountRepository.GetAccountById(acc.AccountId);
                    acc.RoleId = accFind.RoleId;
                    acc.Email = accFind.Email;
                    acc.Phone = accFind.Phone;
                    acc.CreatedTime = accFind.CreatedTime;
                    acc.Status = accFind.Status;
                    acc.UpdateTime = DateTime.Now;
                    acc.Password = accFind.Password;
                    accountRepository.UpdateAccount(acc);
                }

                List<Role> listRoles = roleRepository.GetRoles().Where(r => r.RoleName != "Admin").ToList();
                ViewData["listRoles"] = listRoles;
                ViewData["mode"] = "createAcc";
                var accResult = accountRepository.GetAccountById(acc.AccountId);
                ViewData["acc"] = accResult;
                ViewData["updateAccSuss"] = "suss";

            }
            else if (book.Title != null)
            {
                if (book.BookId != 0)
                {
                    if(book.TotalCopies >= book.AvailableCopies)
                    {
                        bookRepository.UpdateBook(book);
                        ViewData["updateBookSuss"] = "suss";
                    }
                    else
                    {
                        ViewData["totalLess"] = "suss";
                    }
                    ViewData["book"] = book;
                }
                else
                {
                    if(book.TotalCopies >= book.AvailableCopies)
                    {
                        bookRepository.InsertBook(book);
                        ViewData["createBookSuss"] = "suss";
                    }
                    else
                    {
                        ViewData["totalLess"] = "suss";
                        ViewData["book"] = book;
                    }
                }
                
                ViewData["mode"] = "createBook";


            }
            else if(booksBorrow.ReceivedBy != null)
            {
                if (booksBorrow.BookBorrowId != 0)
                {
                    if (booksBorrow.DateBorrowed > booksBorrow.DueDate || booksBorrow.DateBorrowed > booksBorrow.DateReturn)
                    {
                        if (booksBorrow.DateBorrowed > booksBorrow.DueDate)
                        {
                            ViewData["dueDateError"] = "suss";
                        }
                        if (booksBorrow.DateBorrowed > booksBorrow.DateReturn)
                        {
                            ViewData["dateReturnError"] = "suss";
                        }
                    }
                    else
                    {
                        booksBorrow.Status = true;
                        borrowRepository.UpdateBookBorrow(booksBorrow);
                        ViewData["updateBorrowSuss"] = "suss";
                    }
                    ViewData["bookBorrow"] = booksBorrow;
                }
                else
                {
                    if(booksBorrow.DateBorrowed > booksBorrow.DueDate || booksBorrow.DateBorrowed > booksBorrow.DateReturn)
                    {
                        if (booksBorrow.DateBorrowed > booksBorrow.DueDate)
                        {
                            ViewData["dueDateError"] = "suss";
                            ViewData["bookBorrow"] = booksBorrow;
                        }
                        if (booksBorrow.DateBorrowed > booksBorrow.DateReturn)
                        {
                            ViewData["dateReturnError"] = "suss";
                            ViewData["bookBorrow"] = booksBorrow;
                        }
                    }
                    else
                    {
                        booksBorrow.Status = true;
                        Book b = bookRepository.GetBookByID(booksBorrow.BookId);
                        b.AvailableCopies--;
                        bookRepository.UpdateBook(b);
                        borrowRepository.InsertBookBorrow(booksBorrow);
                        ViewData["createBorrowSuss"] = "suss";
                    }
                }
                var accounts = accountRepository.GetAllAccounts();
                var books = bookRepository.GetBooks();

                ViewData["lstAcc"] = accounts;
                ViewData["lstBook"] = books;
                ViewData["mode"] = "createBookBorrow";

            }

        }
    }
}
