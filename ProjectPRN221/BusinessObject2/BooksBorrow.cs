using System;
using System.Collections.Generic;

namespace ProjectPRN221.BusinessObject2;

public partial class BooksBorrow
{
    public int AccountId { get; set; }

    public int? BookId { get; set; }

    public DateTime DateBorrowed { get; set; }

    public DateTime DueDate { get; set; }

    public bool? Status { get; set; }

    public DateTime? DateReturn { get; set; }

    public string? ReceivedBy { get; set; }

    public int BookBorrowId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;

    public BooksBorrow(int accountId, int? bookId, DateTime dateBorrowed, DateTime dueDate, bool? status, DateTime? dateReturn, string? receivedBy)
    {
        AccountId = accountId;
        BookId = bookId;
        DateBorrowed = dateBorrowed;
        DueDate = dueDate;
        Status = status;
        DateReturn = dateReturn;
        ReceivedBy = receivedBy;
    }

    public BooksBorrow()
    {
    }

    public BooksBorrow(int accountId, int bookId, DateTime dateBorrowed, DateTime dueDate, bool? status, DateTime? dateReturn, string? receivedBy, int bookBorrowId, Account account, Book book) : this(accountId, bookId, dateBorrowed, dueDate, status, dateReturn, receivedBy)
    {
        BookBorrowId = bookBorrowId;
        Account = account;
        Book = book;
    }

    public BooksBorrow(DateTime dateBorrowed)
    {
        DateBorrowed = dateBorrowed;
    }
}
