using System;
using System.Collections.Generic;

namespace ManageBookLibrary.BusinessObject;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateTime PublicationDate { get; set; }

    public int AvailableCopies { get; set; }

    public int TotalCopies { get; set; }

    public string ShelfLocation { get; set; } = null!;

    public virtual ICollection<BooksBorrow> BooksBorrows { get; set; } = new List<BooksBorrow>();
}
