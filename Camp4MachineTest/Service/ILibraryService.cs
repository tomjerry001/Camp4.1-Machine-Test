using System;

namespace LibraryManagementSystem
{
    // Interface defining the operations for the library service
    public interface ILibraryService
    {
        // Method to add a new book
        void AddBook();

        // Method to edit an existing book using its ISBN
        void EditBook(string isbn);

        // Method to remove a book using its ISBN
        void RemoveBook(string isbn);

        // Method to search for books by author
        void SearchByAuthor(string author);

        // Method to search for books by title
        void SearchByTitle(string title);

        // Method to list all books in the library
        void ListAllBooks();
    }
}
