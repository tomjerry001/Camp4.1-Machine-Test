using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LibraryManagementSystem
{
    public class LibraryManagement
    {
        private readonly ILibraryService _libraryService; // Dependency on ILibraryService

        public LibraryManagement(ILibraryService libraryService) // Constructor to inject the library service
        {
            _libraryService = libraryService;
        }

        public static void Main(string[] args)  // Main entry point of the application
        {
            var libraryApp = new LibraryManagement(new LibraryServiceImpl()); // Creating an instance of LibraryManagement

            while (true) // Infinite loop to keep the application running
            {
                Console.WriteLine("\nLibrary Management System");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Edit Book");
                Console.WriteLine("3. Remove Book");
                Console.WriteLine("4. Search By Author");
                Console.WriteLine("5. Search By Title");
                Console.WriteLine("6. List All Books");
                Console.WriteLine("7. Exit");
                Console.WriteLine("Enter your choice");

                // Read user input and parse it as an integer choice
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 7)
                {
                    Console.WriteLine("Invalid Choice. Please try again."); // Handle invalid choices
                    continue;
                }

                switch (choice) // Execute different actions based on the user's choice
                {
                    case 1:
                        libraryApp._libraryService.AddBook(); // Call the method to add a new book
                        break;
                    case 2:
                        Console.WriteLine("Enter ISBN of the book to update:");
                        string isbnToUpdate = Console.ReadLine(); // Read the ISBN for the book to update
                        libraryApp._libraryService.EditBook(isbnToUpdate); // Call the method to edit the book
                        break;
                    case 3:
                        Console.WriteLine("Enter ISBN of the book to remove:");
                        string isbnToRemove = Console.ReadLine(); // Read the ISBN for the book to remove
                        libraryApp._libraryService.RemoveBook(isbnToRemove); // Call the method to remove the book
                        break;
                    case 4:
                        Console.WriteLine("Enter Author name to search:");
                        string authorToSearch = Console.ReadLine(); // Read the author's name to search
                        libraryApp._libraryService.SearchByAuthor(authorToSearch); // Call the method to search books by author
                        break;
                    case 5:
                        Console.WriteLine("Enter Title to search:");
                        string titleToSearch = Console.ReadLine(); // Read the title to search
                        libraryApp._libraryService.SearchByTitle(titleToSearch); // Call the method to search books by title
                        break;
                    case 6:
                        libraryApp._libraryService.ListAllBooks(); // Call the method to list all books
                        break;
                    case 7:
                        return; // Exit the application
                }
            }
        }
    }
}
