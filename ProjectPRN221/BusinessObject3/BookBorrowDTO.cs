namespace ProjectPRN221.BusinessObject3
{
    public class BookBorrowDTO
    {
        public int BookBorrowId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public bool Gender { get; set; }

        public DateTime DateBorrowed { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? DateReturn { get; set; }

        public string? ReceivedBy { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string ShelfLocation { get; set; } = null!;

        public BookBorrowDTO() { }
    }
}
