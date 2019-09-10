using BookManagementFinalTest.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagementFinalTest.Repositories
{
    #region Book interface
    public interface IBookRepository
    {
        /// <summary>Gets all.</summary>
        IEnumerable<Book> GetAll();

        /// <summary>Gets the book.</summary>
        /// <param name="id">The identifier.</param>
        Book GetBook(int? id);

        /// <summary>Saves the book.</summary>
        /// <param name="book">The book.</param>
        Book SaveBook(Book book);

        /// <summary>Deletes the book.</summary>
        /// <param name="id">The identifier.</param>
        Book DeleteBook(int? id);

        /// <summary>Searches the book.</summary>
        /// <param name="search">The search.</param>
        IEnumerable<Book> SearchBook(string search);

        List<SelectListItem> GetAuthors();
    }
    #endregion

    #region Book repository
    public class BookRepository : IBookRepository
    {
        #region Variable declarations
        // <summary>The context</summary>
        private readonly AppDbContext _context;
        #endregion

        #region Constructor
        // <summary>Initializes a new instance of the <see cref="BookRepository"/> class.</summary>
        // <param name="context">The context.</param>
        public BookRepository(AppDbContext context) => this._context = context;
        #endregion

        #region Get all
        // Returns all books to controller
        public IEnumerable<Book> GetAll() => _context.Books.Include(e => e.Author).OrderBy(o => o.RegisterDate);
        #endregion

        #region Get single
        // Returns single book with id
        public Book GetBook(int? id) => _context.Books.Include(e => e.Author).Where(e => e.Id == id).SingleOrDefault();
        #endregion

        #region Save book
        // Saves new book or edits ols book details
        public Book SaveBook(Book book)
        {
            if (book != null)
            {
                if (book.Id == 0)
                {
                    book.RegisterDate = DateTime.Today;
                    _context.Add(book);
                }
                else
                {
                    var oldBook = _context.Books.Include(e => e.Author).Where(e => e.Id == book.Id)?.SingleOrDefault();
                    if (oldBook != null)
                    {
                        oldBook.Name = book.Name;
                        oldBook.Price = book.Price;
                        oldBook.Description = book.Description;
                        oldBook.AuthorId = book.AuthorId;
                        oldBook.NumOfPages = book.NumOfPages;
                        oldBook.Language = book.Language;
                        oldBook.Category = book.Category;
                        oldBook.RegisterDate = oldBook.RegisterDate;
                    }
                }
                _context.SaveChanges();
            }

            return book;
        }
        #endregion

        #region Delete book
        // Deletes book with id
        public Book DeleteBook(int? id)
        {
            Book book = _context.Books.Include(e => e.Author).Where(e => e.Id == id).SingleOrDefault();

            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }

            return book;
        }
        #endregion

        #region Search book
        // Searches books with matching search string 
        public IEnumerable<Book> SearchBook(string search)
        {
            return _context.Books.Include(e => e.Author)
                .Where(w => w.Name.Contains(search) ||
                        w.Description.Contains(search) ||
                        w.Author.Firstname.Contains(search) ||
                        w.Author.Lastname.Contains(search)).ToList();
        }
        #endregion

        #region Get authors
        public List<SelectListItem> GetAuthors()
        {
            return _context.Authors.Select(e => new SelectListItem()
            {
                Value = e.Id.ToString(),
                Text = string.Concat(e.Firstname, " ", e.Lastname)
            }).ToList();
        }
        #endregion
    }
    #endregion
}
