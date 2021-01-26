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
    public class MyEntryController : BaseController
    {
        public async Task<IActionResult> Index(int id)
        {
            var result = await HttpClientGet($"entry/getall/{id}");
            if (result == null)
                return View();

            EntryViewModel viewModel = new EntryViewModel();
            viewModel.Entries = JsonConvert.DeserializeObject<List<Entry>>(result);
            viewModel.PhoneBook = new PhoneBook();
            result = await HttpClientGet($"phonebook/get/{id}");

            if(result != null)
                viewModel.PhoneBook = JsonConvert.DeserializeObject<PhoneBook>(result);

            return View(viewModel);
        }

        public async Task<IActionResult> InitializeCreate(int id)
        {
            Entry model = new Entry();
            var result = await HttpClientGet($"phonebook/get/{id}");

            if (result != null)
            {
                model.PhoneBook = JsonConvert.DeserializeObject<PhoneBook>(result);
                model.phoneBookId = id;
            }
            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Entry model)
        {
            var result = await HttpClientPut($"entry/save/", JsonConvert.SerializeObject(model));
            if (result == null)
                return View();

            return RedirectToAction(nameof(Index), new { id = model.phoneBookId });
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id, int phoneBookId)
        {
            var result = await HttpClientPost($"entry/delete/{id}", string.Empty);
            if (result == null)
                return View();
            return RedirectToAction(nameof(Index), new { id = phoneBookId });
        }
    }
}
