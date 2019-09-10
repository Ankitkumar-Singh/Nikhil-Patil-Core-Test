using BookManagementFinalTest.Models;
using BookManagementFinalTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementFinalTest.Controllers
{
    public class AuthorController : Controller
    {
        #region Variable declarations
        /// <summary>The author repository</summary>
        private readonly IAuthorRepository _AuthorRepository;
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="AuthorController"/> class.</summary>
        /// <param name="AuthorRepository">The author repository.</param>
        public AuthorController(IAuthorRepository AuthorRepository)
        {
            _AuthorRepository = AuthorRepository;
        }
        #endregion

        #region Index method
        // GET: Author
        // Returns all author with pagination
        public IActionResult Index(int page = 1, int pageSize = 3)
        {
            return View(_AuthorRepository.GetAll(page, pageSize));
        }
        #endregion

        #region Details method
        // GET: Author/Details
        // Returns author details with matching id
        public IActionResult Details(int? id)
        {
            var author = _AuthorRepository.GetAuthor(id);

            if (author == null)
            {
                Response.StatusCode = 404;
                return View("AuthorNotFound", id);
            }

            return View(author);
        }
        #endregion

        #region Save method
        // GET: Author/Save
        // Saves or edits author details
        public IActionResult Save(int id)
        {
            var author = _AuthorRepository.GetAuthor(id);

            if (author == null)
                author = new Author();

            return View(author);
        }

        // POST: Book/Save
        // Saves or edits author details
        [HttpPost]
        public IActionResult Save(Author author)
        {
            if (author.Id == 0)
            {
                var isEmailAvailable = _AuthorRepository.GetEmail(author.Email);
                var isContactAvailable = _AuthorRepository.GetContact(author.Contact);

                if (isEmailAvailable != null)
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View(author);
                }
                else if (isContactAvailable != null)
                {
                    ModelState.AddModelError("Contact", "Contact already exists.");
                    return View(author);
                }
            }

            if (ModelState.IsValid)
            {
                _AuthorRepository.SaveAuthor(author);
                return RedirectToAction("Index");
            }
            return View(author);
        }
        #endregion

        #region Delete method
        // GET: Book/Delete
        // Deletes book with specified id
        public IActionResult Delete(int id)
        {
            var author = _AuthorRepository.GetAuthor(id);

            if (author == null)
            {
                Response.StatusCode = 404;
                return View("AuthorNotFound", id);
            }

            return View(author);
        }

        // POST: Book/Delete
        // Confirms delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _AuthorRepository.DeleteAuthor(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
