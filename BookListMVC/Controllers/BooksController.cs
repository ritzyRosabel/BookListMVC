using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Book Book { get; set; }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Upsert( int ? id)
        {
            Book book = new Book();
            if(id == null)
            {
                return View(Book);
            }
            book =  _db.Books.FirstOrDefault(b => b.ID == id);
            if(book== null)
            {
                return NotFound();
            }
            //
            return View(book);
        }

        #region ApiCalls
        [HttpGet]
        
        public IActionResult GetAll()
        {
            var result =  Json(new { data = _db.Books.ToList() });
            return result;
        }
        [HttpDelete]
        [Route("api/DeleteBook")]
        public async Task<IActionResult> Delete(int id)
        {
            var BookFromDb = await _db.Books.FirstOrDefaultAsync(B => B.ID == id);
            if (BookFromDb == null)
            {
                return Json(new { success = false, message = "Failed to delete record." });
            }
            _db.Books.Remove(BookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Successfully deleted" });
        }
        #endregion
    }
}
