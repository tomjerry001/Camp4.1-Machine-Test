using System;

namespace LibraryManagementSystem
{
    // Class representing a book with its details
    public class Book
    {
        public string ISBN { get; set; } // ISBN of the book
        public string Title { get; set; } // Title of the book
        public string Author { get; set; } // Author of the book
        public DateTime PublishedDate { get; set; } // Date when the book was published
    }
}
