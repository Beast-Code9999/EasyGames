using EasyGames.DataAccess.Data;
using EasyGames.DataAccess.Repository;
using EasyGames.DataAccess.Repository.IRepository;
using EasyGames.Models;
using EasyGames.Models.ViewModels;
using EasyGames.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyGames.Areas.Admin.Controllers
{
    [Area("Admin")] // Tell the controller that this controller belongs to Admin
    [Authorize(Roles = SD.Role_Admin)] // Only admin can access

    public class CompanyController : Controller 
    {
        // with Dotnet core we do not have to use legacy code such as
        // ApplicationDbContext name = new ApplicationDbContext();

        // Uses Dependency Injection to access the database via ApplicationDbContext
        private readonly IUnitOfWork _unitOfWork;

        // Inject iwebhost environment to save files in root folder
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index() // action is Index
        {
            // Retrieves all categories from the database as a list
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return View(objCompanyList);
        }

        // Create new company
        public IActionResult Upsert(int? id) // update and insert
        {

            if (id == null || id == 0) // If the company doesn't exist
            {
                return View(new Company());
            }
            else
            {
                // update the company 
                Company companyObj = _unitOfWork.Company.Get(u=>u.Id==id);
                return View(companyObj);
            }
        }
        // Handle Post requests
        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {
            // first check if obj is valid 
            if (ModelState.IsValid)
            {
                // identify whether it is an add or update
                if(companyObj.Id==0)
                {
                    _unitOfWork.Company.Add(companyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(companyObj);
                }

                _unitOfWork.Save();
                // add temporary data to show if successful
                TempData["Success"] = "The company was created successfully!";
                return RedirectToAction("Index"); // redirects back to index
            }
            else // if not validated, ensure that the fields are still populated
            { 
                return View(companyObj);
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

            Company? companyFromDb = _unitOfWork.Company.Get(u => u.Id == id);

            if (companyFromDb == null) // if company is null
            {
                return NotFound();
            }

            return View(companyFromDb);
        }
        // Handle Post requests for Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Company? obj = _unitOfWork.Company.Get(u => u.Id == id);
            // check if null
            if (obj == null)
            {
                return NotFound();
            }
            // if not null
            // Removes company from the Company table
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "The company was deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}
