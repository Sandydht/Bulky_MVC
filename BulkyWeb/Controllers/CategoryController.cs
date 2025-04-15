using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    
    public CategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IActionResult Index()
    {
        List<Category> objectCategoryList = _dbContext.Categories.ToList();
        return View(objectCategoryList);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
        }

        if (ModelState.IsValid)
        {
            _dbContext.Categories.Add(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? categoryFromDB = _dbContext.Categories.Find(id);

        if (categoryFromDB == null)
        {
            return NotFound();
        }

        return View(categoryFromDB);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
        }

        if (ModelState.IsValid)
        {
            _dbContext.Categories.Update(obj);
            _dbContext.SaveChanges();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? categoryFromDB = _dbContext.Categories.Find(id);

        if (categoryFromDB == null)
        {
            return NotFound();
        }

        return View(categoryFromDB);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        Category? obj = _dbContext.Categories.Find(id);

        if (obj == null)
        {
            return NotFound();
        }

        _dbContext.Categories.Remove(obj);
        _dbContext.SaveChanges();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}