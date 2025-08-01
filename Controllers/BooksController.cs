using BooksManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using BooksManagementSystem.Models;
using static BooksManagementSystem.Services.BookService;

namespace BooksManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            List<Books> Books = _service.GetAllBooks();
            return View(Books);
        }


        //This is a Get Api
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        //This is a Post Api
        [HttpPost]
        public IActionResult Create(Books Books)
        {
            if (ModelState.IsValid)
            {
                _service.AddBook(Books);
                return RedirectToAction("Index");
            }
            return View(Books);
        }


        // GET: Employee/Delete/5
        public IActionResult Delete(int id)
        {
            var Book = _service.GetAllBooks().FirstOrDefault(e => e.Id == id);
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var Book = _service.GetAllBooks().FirstOrDefault(e => e.Id == id);
            if (Book == null)
                return NotFound();

            return View("Update", Book);
        }

        [HttpPost]
        public IActionResult Edit(Books updatedBooks)
        {
            if (!ModelState.IsValid)
                return View(updatedBooks);

            _service.UpdateBook(updatedBooks);
            return RedirectToAction("Index");
        }


    }
}
