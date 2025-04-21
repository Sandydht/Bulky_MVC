using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    
    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public IActionResult Index()
    {
        List<Category> objectCategoryList = _categoryRepository.GetAll().ToList();
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
            _categoryRepository.Add(obj);
            _categoryRepository.Save();
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

        Category? categoryFromDB = _categoryRepository.Get(u => u.Id == id);

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
            _categoryRepository.Update(obj);
            _categoryRepository.Save();
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

        Category? categoryFromDB = _categoryRepository.Get(u => u.Id == id);

        if (categoryFromDB == null)
        {
            return NotFound();
        }

        return View(categoryFromDB);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        Category? obj = _categoryRepository.Get(u => u.Id == id);

        if (obj == null)
        {
            return NotFound();
        }

        _categoryRepository.Remove(obj);
        _categoryRepository.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}