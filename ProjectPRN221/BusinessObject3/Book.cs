using System;
using System.Collections.Generic;

namespace ProjectPRN221.BusinessObject3;

public partial class Book
{
    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateTime PublicationDate { get; set; }

    public int AvailableCopies { get; set; }

    public int TotalCopies { get; set; }

    public string ShelfLocation { get; set; } = null!;

    public int BookId { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<BooksBorrow> BooksBorrows { get; set; } = new List<BooksBorrow>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

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

    public Book(string title, string author, DateTime publicationDate, int availableCopies, int totalCopies, string shelfLocation, int bookId, string? image) : this(title, author, publicationDate, availableCopies, totalCopies, shelfLocation)
    {
        BookId = bookId;
        Image = image;
    }

    public Book(string title, string author, DateTime publicationDate, int availableCopies, int totalCopies, string shelfLocation, int bookId, string? image, string? description, ICollection<BooksBorrow> booksBorrows, ICollection<Comment> comments) : this(title, author, publicationDate, availableCopies, totalCopies, shelfLocation, bookId, image)
    {
        Description = description;
        BooksBorrows = booksBorrows;
        Comments = comments;
    }
}
