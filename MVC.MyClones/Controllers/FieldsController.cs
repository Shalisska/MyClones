using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using Domain.Entities.Fields;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.MyClones.Extensions;
using MVC.MyClones.Models;
using MVC.MyClones.Models.Fields;
using MVC.MyClones.Utils;
using MVC.MyClones.ViewModels.Fields;
using Services.Contracts.Fields;

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

        public IActionResult Index()
        {
            var loadActionName = "GetFields";
            //var updateActionName = "UpdateField";
            //var createActionName = "CreateField";
            //var deleteActionName = "DeleteField";

            var source = new DEGridTableDataSource
            {
                Type = "createStore",
                Key = "id",
                LoadUrl = Url.Action(loadActionName),
                //UpdateUrl = Url.Action(updateActionName),
                //CreateUrl = Url.Action(createActionName),
                //DeleteUrl = Url.Action(deleteActionName)
            };

            var columns = new List<DEGridTableColumn>();
            columns.Add(new DEGridTableColumn("id"));
            columns.Add(new DEGridTableColumn("culture") { Caption = "Культура"});

            var gridTable = new DEGridTable
            {
                DataSource = source,
                Columns = columns
            };

            return View("DevExpressTmpl/_GridTable", gridTable);
        }

        [HttpGet("getfields")]
        public object GetFields(DataSourceLoadOptions loadOptions)
        {
            var fields = _fieldService.GetFields();
            var fieldsList = fields.Select(f => new FieldViewModel(f)).ToList();

            loadOptions.PrimaryKey = new[] { "Id" };
            loadOptions.PaginateViaPrimaryKey = true;

            return DataSourceLoader.Load(fieldsList, loadOptions);
        }

        [HttpPut("update-field")]
        public IActionResult UpdateField(int key, string values)
        {
            //var res = _stockManagementService.GetStock(key);
            //if (res == null)
            //    return StatusCode(409, "Stock not found");

            //JsonConvert.PopulateObject(values, res);

            //if (!TryValidateModel(res))
            //    return BadRequest(ModelState.ToFullErrorString());

            //_stockManagementService.UpdateStock(res);

            return Ok();
        }

        //[HttpPost("create-field")]
        //public IActionResult CreateField(string values)
        //{
        //    var stock = new StockManagementModel();
        //    JsonConvert.PopulateObject(values, stock);

        //    if (!TryValidateModel(stock))
        //        return BadRequest(ModelState.ToFullErrorString());

        //    _stockManagementService.CreateStock(stock);

        //    return Json(stock.Id);
        //}

        //[HttpDelete("delete-field")]
        //public IActionResult DeleteField(int key)
        //{
        //    var stock = _stockManagementService.GetStock(key);
        //    if (stock == null)
        //        return StatusCode(409, "Stock not found");

        //    _stockManagementService.DeleteStock(key);

        //    return Ok();
        //}

        // GET: Fields
        public ActionResult Index1()
        {
            var fields = _fieldService.GetFields();

            var fieldsList = fields.Select(f => new FieldViewModel(f)).ToList();

            return View("Index1", fieldsList);
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