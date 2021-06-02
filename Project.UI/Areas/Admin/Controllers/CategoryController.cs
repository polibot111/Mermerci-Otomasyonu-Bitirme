﻿using Project.BLL.DesingPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.UI.Areas.AdminArea.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository _crep;

        public CategoryController()
        {
            _crep = new CategoryRepository();
        }

        // GET: Admin/Category
        public ActionResult CategoryList(int? id)
        {
            CategoryVM cvm = id == null ? new CategoryVM
            {
                Categories = _crep.GetActives()
            } : new CategoryVM { Category = _crep.Find(id.Value) };


            return View(cvm);
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            _crep.Add(category);
            return RedirectToAction("CategoryList");
        }

        public ActionResult UpdateCategory(int id)
        {
            CategoryVM cvm = new CategoryVM
            {
                Category = _crep.Find(id)
            };

            return View(cvm);

        }

        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            _crep.Update(category);
            return RedirectToAction("CategoryList");
        }

        public ActionResult DeleteCategory(int id)
        {
            _crep.Delete(_crep.Find(id));
            return RedirectToAction("CategoryList");
        }
    }
}