using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BulkyWeb.Models;
using BulkyWeb.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext applicationDb;

    public CategoryController(ApplicationDbContext applicationDbContext, ILogger<HomeController> logger)
    {
        _logger = logger;
        applicationDb = applicationDbContext;
    }

    //This is will retrive the HTML
    public IActionResult Index()
    {
        List<Category> categories = applicationDb.Categories.ToList();
        return View(categories);
    }

    public IActionResult CreateCategory()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateCategory(Category category)
    {
        if(category.Name == category.DisplayOrder.ToString()){
            ModelState.AddModelError("Name","Name and Display Order Cannot be same");
        }

        if (ModelState.IsValid)
        {
            applicationDb.Categories.Add(category);
            applicationDb.SaveChanges();
            TempData["success"] = "Successfully created the Category";
            return RedirectToAction("Index", "Category");
        }

        return View();
        


    }

    public IActionResult Edit(int? id){
        if(id == null || id==0){
            return NotFound();
        }
        Category? category = applicationDb.Categories.FirstOrDefault(u=>u.Id == id);
        if(category == null){
            return NotFound();
        }
        return View(category);
    }


    //Method Overloading
    [HttpPost]
    public IActionResult Edit(Category category){
        if(ModelState.IsValid){
            applicationDb.Categories.Update(category);
            applicationDb.SaveChanges();
            TempData["success"] = "Successfully Updated the Category";
            return RedirectToAction("Index");
        }
        return View();
    }

    //writing delete method for the category
    public IActionResult Delete(int? id){
        if(id==null || id ==0){
            return NotFound();
        }
        Category? category  = applicationDb.Categories.FirstOrDefault( u => u.Id == id);
        if(category == null){
            return NotFound();
        }

        return View(category);
    }

    [HttpPost,ActionName("Delete")]

    public IActionResult DeletePost(int? id){
        if(id==null || id==0){
            return NotFound();
        }

        Category? category = applicationDb.Categories.FirstOrDefault(u => u.Id == id);
        if(category == null){
            return NotFound();
        }

        applicationDb.Categories.Remove(category);
        applicationDb.SaveChanges();
        TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index");
    }


}
