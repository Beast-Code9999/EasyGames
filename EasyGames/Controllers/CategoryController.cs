using EasyGames.Data;
using EasyGames.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyGames.Controllers
{
    public class CategoryController : Controller 
    {
        // with Dotnet core we do not have to use legacy code such as
        // ApplicationDbContext name = new ApplicationDbContext();

        // Uses Dependency Injection to access the database via ApplicationDbContext
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db )
        {
            _db = db;
        }
        public IActionResult Index() // action is Index
        {
            // Retrieves all categories from the database as a list
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        // Create new category
        public IActionResult Create()
        {
            return View();
        }
        // Handle Post requests
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // first check if obj is valid 
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                // add temporary data to show if successful
                TempData["Success"] = "The category was created successfully!";
                return RedirectToAction("Index"); // redirects back to index
            }
            return View();
        }

        // Handle edit/UPDATE
        public IActionResult Edit(int? id) // nullable field
        {
            // check if ID exists
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id);

            // other ways of retrieving category

            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (categoryFromDb == null) // if category is null
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        // Handle Post requests
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            // first check if obj is valid 
            if (ModelState.IsValid)
            {
                // update the category
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "The category was updated successfully!";
                return RedirectToAction("Index"); // redirects back to index
            }
            return View();
        }


        // Handle Delete
        public IActionResult Delete(int? id) // nullable field
        {
            // check if ID exists
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null) // if category is null
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }
        // Handle Post requests for Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            // check if null
            if (obj == null)
            {
                return NotFound();
            }
            // if not null
            // Removes category from the Category table
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "The category was deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}
