using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPhoneBook.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyPhoneBook.Web.Controllers
{
    public class MyPhoneBookController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            var result = await HttpClientGet($"phonebook/getall");
            if (string.IsNullOrWhiteSpace(result))
                return View();

            List<PhoneBookViewModel> phoneBookViewModels = new List<PhoneBookViewModel>();
            phoneBookViewModels = JsonConvert.DeserializeObject<List<PhoneBookViewModel>>(result).OrderBy(t => t.Name).ToList();
            if (phoneBookViewModels != null)
                await SetPhoneBookTotalEntries(phoneBookViewModels);

            return View(phoneBookViewModels);
        }

        private async Task SetPhoneBookTotalEntries(List<PhoneBookViewModel> phoneBookViewModels)
        {
            foreach (var book in phoneBookViewModels)
            {
                var bookResult = await HttpClientGet($"entry/getall/{book.Id}"); // will check cache first.
                if (!string.IsNullOrWhiteSpace(bookResult))
                    book.TotalEntries = JsonConvert.DeserializeObject<List<PhoneBookViewModel>>(bookResult).Count();
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            var result = await HttpClientGet($"phonebook/get/{id}");
            if (result == null)
                return View();
            var phoneBook = JsonConvert.DeserializeObject<PhoneBookViewModel>(result);
            return View(phoneBook);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PhoneBookViewModel viewModel)
        {
            var result = await HttpClientPut($"phonebook/save", JsonConvert.SerializeObject(viewModel));
            if (result == null)
                return View();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var result = await HttpClientPost($"phonebook/delete/{id}", string.Empty);
            if (result == null)
                return View();
            return RedirectToAction(nameof(Index));
        }
    }
}
