using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Fields;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.MyClones.Models.Fields;
using MVC.MyClones.ViewModels.Fields;
using Services.Interfaces;

namespace MVC.MyClones.Controllers
{
    public class FieldsController : Controller
    {
        private IFieldService _fieldService;

        public FieldsController(
            IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        // GET: Fields
        public ActionResult Index()
        {
            var fields = _fieldService.GetFields();

            var fieldsList = fields.Select(f => new FieldViewModel(f)).ToList();

            return View(fieldsList);
        }

        // GET: Fields/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddFields()
        {
            var locations = _fieldService.GetHouseLocations();
            ViewData["Locations"] = new SelectList(locations, "LocationId", "LocationName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFields(AddFieldsInputModel input)
        {
            _fieldService.AddFields(input.Count, input.LocationId);

            return RedirectToAction("Index");
        }

        // GET: Fields/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fields/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*IFormCollection collection*/Field field)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Fields/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = new EditFieldModelInput() { Id = id };
            var crops = _fieldService.GetCrops();

            ViewData["Crops"] = new SelectList(crops, "Id", "Name");

            return View(model);
        }

        // POST: Fields/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditFieldModelInput input)
        {
            _fieldService.UpdateField(input.Id, input.CultureId, input.CurrentStage, input.StartDate);
            return RedirectToAction(nameof(Index));
            //try
            //{
            //    _fieldService.UpdateField(input.Id, input.CultureId, input.CurrentStage, input.StartDate);
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Fields/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Fields/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}