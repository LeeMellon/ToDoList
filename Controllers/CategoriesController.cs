using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{

  public class CategoriesController : Controller
  {

    [HttpGet("/categories/index")]
    public ActionResult Categories()
    {
      List<Category> allCategories = Category.GetAll();
      return View(allCategories);
    }

    [HttpGet("/categories/new")]
    public ActionResult CreateForm()
    {
      return View();
    }

    [HttpPost("/categories")]
    public ActionResult Create()
    {
      Category newCategory = new Category(Request.Form["category-name"]);

      List<Category> allCategories = Category.GetAll();
      return View("Categories", allCategories);
    }
    [HttpGet("/categories/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Category selectedCategory = Category.Find(id);
      List<Item> categoryItems = selectedCategory.GetItems();
      model.Add("category", selectedCategory);
      model.Add("items", categoryItems);
      return View(model);
    }


    [HttpPost("/items")]
    public ActionResult CreateItem()
    {
      Dictionary<string, object> model = new Dictionary<string, object>();

      Category foundCategory = Category.Find(Int32.Parse(Request.Form["category-id"]));
      List<Item> categoryItems = foundCategory.GetItems();
      string itemDescription = Request.Form["item-description"];
      Item newItem = new Item(itemDescription);
      categoryItems.Add(newItem);
      model.Add("items", categoryItems);
      model.Add("category", foundCategory);

      return View("Details", model);
    }


  }


}
