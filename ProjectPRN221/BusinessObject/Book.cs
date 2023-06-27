using System;
using System.Collections.Generic;

namespace ManageBookLibrary.BusinessObject;

public partial class Book
{
    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateTime PublicationDate { get; set; }

    public int AvailableCopies { get; set; }

    public int TotalCopies { get; set; }

    public string ShelfLocation { get; set; } = null!;

    public int BookId { get; set; }

    public virtual ICollection<BooksBorrow> BooksBorrows { get; set; } = new List<BooksBorrow>();

    public Book(string title, string author, DateTime publicationDate, int availableCopies, int totalCopies, string shelfLocation)
    {
        Title = title;
        Author = author;
        PublicationDate = publicationDate;
        AvailableCopies = availableCopies;
        TotalCopies = totalCopies;
        ShelfLocation = shelfLocation;
    }

    public Book() { }

    public Book(string title, string author, DateTime publicationDate, int availableCopies, int totalCopies, string shelfLocation, int bookId, ICollection<BooksBorrow> booksBorrows) : this(title, author, publicationDate, availableCopies, totalCopies, shelfLocation)
    {
        BookId = bookId;
        BooksBorrows = booksBorrows;
    }
}
