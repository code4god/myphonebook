using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPhoneBook.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhoneBook.Web.Controllers
{
    public class EntryController : BaseController
    {
        // GET: EntryController
        public async Task<IActionResult> Index(int phoneBookId)
        {
            var result = await HttpClientGet($"entry/getall/{phoneBookId}");
            if (result == null)
                return View();
            var viewModel = JsonConvert.DeserializeObject<List<EntryViewModel>>(result);
            return View(viewModel);//("~/views/entries/view.cshtml");
        }

        // GET: EntryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EntryController/Create
        public async Task<IActionResult> Create()
        {
            var result = await HttpClientGet($"phonebook/getall");
            if (result == null)
                return View();
            EntryViewModel viewModel = new EntryViewModel();
            viewModel.PhoneBooks = JsonConvert.DeserializeObject<List<PhoneBookViewModel>>(result);
            return View(viewModel);
        }

        // POST: EntryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EntryViewModel viewModel)
        {
            try
            {
                var result = await HttpClientPut($"entry/save", JsonConvert.SerializeObject(viewModel));
                if (result == null)
                    return View();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EntryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EntryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EntryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EntryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
