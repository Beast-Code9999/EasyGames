using EasyGames.DataAccess.Data;
using EasyGames.DataAccess.Repository;
using EasyGames.DataAccess.Repository.IRepository;
using EasyGames.Models;
using EasyGames.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyGames.Areas.Admin.Controllers
{
    [Area("Admin")] // Tell the controller that this controller belongs to Admin
    public class ProductController : Controller 
    {
        // with Dotnet core we do not have to use legacy code such as
        // ApplicationDbContext name = new ApplicationDbContext();

        // Uses Dependency Injection to access the database via ApplicationDbContext
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index() // action is Index
        {
            // Retrieves all categories from the database as a list
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        // Create new product
        public IActionResult Create()
        {
            // Fetches all categories from the database and maps them into a list 
            // of SelectListItem objects for use in a dropdown menu. Then creates 
            // a new ProductVM instance that holds both the category list and an 
            // empty Product object, and finally passes this view model to the View
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            ProductVM productVM = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            return View(productVM);
        }
        // Handle Post requests
        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            // first check if obj is valid 
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                // add temporary data to show if successful
                TempData["Success"] = "The product was created successfully!";
                return RedirectToAction("Index"); // redirects back to index
            }
            else // if not validated, ensure that the fields are still populated
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }); 
                return View(productVM);
            }
                
        }

        // Handle edit/UPDATE
        public IActionResult Edit(int? id) // nullable field
        {
            // check if ID exists
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.Get(u=>u.Id==id);

            // other ways of retrieving product

            //Product? productFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Product? productFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (productFromDb == null) // if product is null
            {
                return NotFound();
            }

            return View(productFromDb);
        }
        // Handle Post requests
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            // first check if obj is valid 
            if (ModelState.IsValid)
            {
                // update the product
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "The product was updated successfully!";
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

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null) // if product is null
            {
                return NotFound();
            }

            return View(productFromDb);
        }
        // Handle Post requests for Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            // check if null
            if (obj == null)
            {
                return NotFound();
            }
            // if not null
            // Removes product from the Product table
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "The product was deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}
