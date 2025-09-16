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

        // Inject iwebhost environment to save files in root folder
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index() // action is Index
        {
            // Retrieves all categories from the database as a list
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        // Create new product
        public IActionResult Upsert(int? id) // update and insert
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

            if (id == null || id == 0) // If the product doesn't exist
            {
                return View(productVM);
            }
            else
            {
                // update the product 
                productVM.Product = _unitOfWork.Product.Get(u=>u.Id==id);
                return View(productVM);
            }
        }
        // Handle Post requests
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            // first check if obj is valid 
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                // check if file is not null, then if not save it to the folder
                if (file != null)
                {
                    // craete random string for file
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    // save image
                    using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // save in model too

                    productVM.Product.ImageUrl = @"\images\product\" + filename;
                }

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
