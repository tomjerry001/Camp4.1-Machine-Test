using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LibraryManagementSystem
{
    public class LibraryServiceImpl : ILibraryService
    {
        private static Dictionary<string, Book> books = new Dictionary<string, Book>(); // Dictionary to store books with ISBN as the key

        public void AddBook()
        {
            try
            {
                Console.WriteLine("Enter Book details:");

                string isbn;
                while (true) // Loop until a valid ISBN is entered
                {
                    Console.WriteLine("ISBN:");
                    isbn = Console.ReadLine();
                    if (Regex.IsMatch(isbn, @"^\d{13}$")) // Check if ISBN is 13 digits
                    {
                        if (!books.ContainsKey(isbn)) // Check if the book with this ISBN already exists
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("A book with this ISBN already exists.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ISBN. It should be 13 digits.");
                    }
                }

                string title;
                while (true) // Loop until a non-empty title is entered
                {
                    Console.WriteLine("Title:");
                    title = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(title))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Title cannot be empty.");
                    }
                }

                string author;
                while (true) // Loop until a non-empty author is entered
                {
                    Console.WriteLine("Author:");
                    author = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(author))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Author cannot be empty.");
                    }
                }

                DateTime publishedDate;
                while (true) // Loop until a valid date is entered
                {
                    Console.WriteLine("Published Date (YYYY-MM-DD):");
                    if (DateTime.TryParse(Console.ReadLine(), out publishedDate))
                    {
                        if (publishedDate <= DateTime.Now) // Ensure the date is not in the future
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Published date cannot be in the future.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    }
                }

                // Add the book to the dictionary
                books.Add(isbn, new Book
                {
                    ISBN = isbn,
                    Title = title,
                    Author = author,
                    PublishedDate = publishedDate
                });

                Console.WriteLine("Book added successfully."); // Confirmation message
            }
            catch (Exception ex) // Handle any exceptions
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void EditBook(string isbn)
        {
            try
            {
                if (books.TryGetValue(isbn, out Book book)) // Check if the book exists
                {
                    Console.WriteLine("Select the detail to update:");
                    Console.WriteLine("1. Title");
                    Console.WriteLine("2. Author");
                    Console.WriteLine("3. Published Date");
                    Console.WriteLine("4. Exit");

                    if (int.TryParse(Console.ReadLine(), out int choice)) // Read user choice
                    {
                        switch (choice) // Update the chosen detail
                        {
                            case 1:
                                string newTitle;
                                while (true) // Loop until a non-empty title is entered
                                {
                                    Console.WriteLine("Enter new title:");
                                    newTitle = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newTitle))
                                    {
                                        book.Title = newTitle;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Title cannot be empty.");
                                    }
                                }
                                break;
                            case 2:
                                string newAuthor;
                                while (true) // Loop until a non-empty author is entered
                                {
                                    Console.WriteLine("Enter new author:");
                                    newAuthor = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newAuthor))
                                    {
                                        book.Author = newAuthor;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Author cannot be empty.");
                                    }
                                }
                                break;
                            case 3:
                                DateTime newPublishedDate;
                                while (true) // Loop until a valid date is entered
                                {
                                    Console.WriteLine("Enter new published date (YYYY-MM-DD):");
                                    if (DateTime.TryParse(Console.ReadLine(), out newPublishedDate))
                                    {
                                        if (newPublishedDate <= DateTime.Now) // Ensure the date is not in the future
                                        {
                                            book.PublishedDate = newPublishedDate;
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Published date cannot be in the future.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                                    }
                                }
                                break;
                            case 4:
                                return; // Exit the update process
                            default:
                                Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                                break;
                        }
                        Console.WriteLine("Book details updated."); // Confirmation message
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                    }
                }
                else
                {
                    Console.WriteLine("Book not found."); // Book not found message
                }
            }
            catch (Exception ex) // Handle any exceptions
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void RemoveBook(string isbn)
        {
            try
            {
                if (books.Remove(isbn)) // Remove the book if it exists
                {
                    Console.WriteLine("Book removed successfully."); // Confirmation message
                }
                else
                {
                    Console.WriteLine("Book not found."); // Book not found message
                }
            }
            catch (Exception ex) // Handle any exceptions
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void SearchByAuthor(string author)
        {
            try
            {
                // Find books by author
                var foundBooks = books.Values.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
                if (foundBooks.Any()) // If books are found
                {
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.WriteLine("| ISBN        | Title        | Author       | Published Date    |");
                    Console.WriteLine("-----------------------------------------------------------------");
                    foreach (var book in foundBooks)
                    {
                        // Display the found books
                        Console.WriteLine($"| {book.ISBN,-12} | {book.Title,-12} | {book.Author,-12} | {book.PublishedDate:yyyy-MM-dd} |");
                    }
                    Console.WriteLine("-----------------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("No books found for this author."); // No books found message
                }
            }
            catch (Exception ex) // Handle any exceptions
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void SearchByTitle(string title)
        {
            try
            {
                // Find books by title
                var foundBooks = books.Values.Where(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)).ToList();
                if (foundBooks.Any()) // If books are found
                {
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.WriteLine("| ISBN        | Title        | Author       | Published Date    |");
                    Console.WriteLine("-----------------------------------------------------------------");
                    foreach (var book in foundBooks)
                    {
                        // Display the found books
                        Console.WriteLine($"| {book.ISBN,-12} | {book.Title,-12} | {book.Author,-12} | {book.PublishedDate:yyyy-MM-dd} |");
                    }
                    Console.WriteLine("-----------------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("No books found with this title."); // No books found message
                }
            }
            catch (Exception ex) // Handle any exceptions
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void ListAllBooks()
        {
            try
            {
                if (books.Count > 0) // If there are books in the library
                {
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.WriteLine("| ISBN        | Title        | Author       | Published Date    |");
                    Console.WriteLine("-----------------------------------------------------------------");
                    foreach (var book in books.Values)
                    {
                        // Display all books
                        Console.WriteLine($"| {book.ISBN,-12} | {book.Title,-12} | {book.Author,-12} | {book.PublishedDate:yyyy-MM-dd} |");
                    }
                    Console.WriteLine("-----------------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("No books available."); // No books available message
                }
            }
            catch (Exception ex) // Handle any exceptions
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
