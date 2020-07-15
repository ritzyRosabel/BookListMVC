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
        public IActionResult Index()
        {
            return View();
        }

        #region ApiCalls
        [HttpGet]
        [Route("api/Book")]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.Books.ToList() });
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
