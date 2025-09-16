using EasyGames.DataAccess.Data;
using EasyGames.DataAccess.Repository;
using EasyGames.DataAccess.Repository.IRepository;
using EasyGames.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyGames.Controllers
{
    public class CategoryController : Controller 
    {
        // with Dotnet core we do not have to use legacy code such as
        // ApplicationDbContext name = new ApplicationDbContext();

        // Uses Dependency Injection to access the database via ApplicationDbContext
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository db )
        {
            _categoryRepository = db;
        }
        public IActionResult Index() // action is Index
        {
            // Retrieves all categories from the database as a list
            List<Category> objCategoryList = _categoryRepository.GetAll().ToList();
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
                _categoryRepository.Add(obj);
                _categoryRepository.Save();
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

            Category? categoryFromDb = _categoryRepository.Get(u=>u.Id==id);

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
                _categoryRepository.Update(obj);
                _categoryRepository.Save();
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

            Category? categoryFromDb = _categoryRepository.Get(u => u.Id == id);

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
            Category? obj = _categoryRepository.Get(u => u.Id == id);
            // check if null
            if (obj == null)
            {
                return NotFound();
            }
            // if not null
            // Removes category from the Category table
            _categoryRepository.Remove(obj);
            _categoryRepository.Save();
            TempData["Success"] = "The category was deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}
