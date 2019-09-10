using BookManagementFinalTest.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagementFinalTest.Repositories
{
    #region Author interface
    public interface IAuthorRepository
    {
        /// <summary>Gets all.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        IEnumerable<Author> GetAll(int page, int pageSize);

        /// <summary>Gets the author.</summary>
        /// <param name="id">The identifier.</param>
        Author GetAuthor(int? id);

        /// <summary>Saves the author.</summary>
        /// <param name="author">The author.</param>
        Author SaveAuthor(Author author);

        /// <summary>Deletes the author.</summary>
        /// <param name="id">The identifier.</param>
        Author DeleteAuthor(int? id);

        Author GetEmail(string email);

        Author GetContact(string contact);
    }
    #endregion

    #region Author repository
    /// <summary></summary>
    /// <seealso cref="BookManagementFinalTest.Repositories.IAuthorRepository" />
    public class AuthorRepository : IAuthorRepository
    {
        #region Variable declarations
        /// <summary>The context</summary>
        private readonly AppDbContext _context;
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="AuthorRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AuthorRepository(AppDbContext context) => this._context = context;
        #endregion

        #region Get all
        // Gets all the authors with pagination
        public IEnumerable<Author> GetAll(int page, int pageSize)
        {
            PagedList<Author> author = new PagedList<Author>(_context.Authors, page, pageSize);
            return author;
        }
        #endregion

        #region Gets single author
        // Gets single author with specified id
        public Author GetAuthor(int? id) => _context.Authors.Where(e => e.Id == id).SingleOrDefault();
        #endregion

        #region Save author
        // Saves or edits author details
        public Author SaveAuthor(Author author)
        {
            if (author != null)
            {
                if (author.Id == 0)
                {
                    author.RegisterDate = DateTime.Today;
                    _context.Add(author);
                }
                else
                {
                    var oldAuthor = _context.Authors.Where(e => e.Id == author.Id)?.SingleOrDefault();
                    if (oldAuthor != null)
                    {
                        oldAuthor.Firstname = author.Firstname;
                        oldAuthor.Lastname = author.Lastname;
                        oldAuthor.Email = author.Email;
                        oldAuthor.Contact = author.Contact;
                        oldAuthor.RegisterDate = oldAuthor.RegisterDate;
                    }
                }
                _context.SaveChanges();
            }

            return author;
        }
        #endregion

        #region Delete author
        // Deletes author with specifies id
        public Author DeleteAuthor(int? id)
        {
            Author author = _context.Authors.Where(e => e.Id == id).SingleOrDefault();

            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }

            return author;
        }
        #endregion

        public Author GetEmail(string email)
        {
            return _context.Authors.Where(w => w.Email == email).SingleOrDefault();
        }

        public Author GetContact(string contact)
        {
            return _context.Authors.Where(w => w.Contact == contact).SingleOrDefault();
        }
    }
    #endregion
}
