using BookManagementFinalTest.Models;
using BookManagementFinalTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementFinalTest.Controllers
{
    public class BookController : Controller
    {
        #region Variable declarations
        /// <summary>The book repository</summary>
        private readonly IBookRepository _bookRepository;
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="BookController"/> class.</summary>
        /// <param name="bookRepository">The book repository.</param>
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        #endregion

        #region Index method
        // GET: Book
        // Returns all books
        public IActionResult Index(string search = null)
        {
            if (!string.IsNullOrEmpty(search))
                return View(_bookRepository.SearchBook(search));

            return View(_bookRepository.GetAll());
        }
        #endregion

        #region Details method
        // GET: Book/Details
        // Returns details of specific book using id
        public IActionResult Details(int? id)
        {
            var book = _bookRepository.GetBook(id);

            if (book == null)
            {
                Response.StatusCode = 404;
                return View("BookNotFound", id);
            }

            return View(book);
        }
        #endregion

        #region Save method
        // GET: Book/Save
        // Shows form to add new book else shows book to edit
        [HttpGet]
        public IActionResult Save(int id)
        {
            var book = _bookRepository.GetBook(id);
            ViewBag.AuthorId = _bookRepository.GetAuthors();

            if (id == 0)
                book = new Book();

            if (book == null)
            {
                Response.StatusCode = 404;
                return View("BookNotFound", id);
            }

            return View(book);
        }

        // POST: Book/Save
        // Posts book details
        [HttpPost]
        public IActionResult Save(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.SaveBook(book);
                return RedirectToAction("Index");
            }

            return View(book);
        }
        #endregion

        #region Delete method
        // GET: Book/Delete
        // Returns book details to delete
        [HttpGet]
        [ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            var book = _bookRepository.GetBook(id);

            if (book == null)
            {
                Response.StatusCode = 404;
                return View("BookNotFound", id);
            }

            return View(book);
        }

        // POST: Book/Delete
        // Confirms delete
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _bookRepository.DeleteBook(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
