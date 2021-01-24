using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLittleBlackBook.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyLittleBlackBook.Web.Controllers
{
    public class MyPhoneBookController : BaseController
    {
        // GET: MyPhoneBookController
        public async Task<ActionResult> Index()
        {
            var result = await HttpClientGet($"phonebook/getall");
            if (result == null)
                return View();

            var test = JsonConvert.DeserializeObject<List<PhoneBookViewModel>>(result).OrderByDescending(t => t.CreatedDate).ToList();
                //HTTP GET

            return View(test);
        }

        // GET: MyPhoneBookController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await HttpClientGet($"phonebook/get/{id}");
            if (result == null)
                return View();
            var phoneBook = JsonConvert.DeserializeObject<PhoneBookViewModel>(result);
            return View(phoneBook);
        }

        // GET: MyPhoneBookController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyPhoneBookController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhoneBookViewModel viewModel)
        {
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
                var result = await HttpClientPut($"phonebook/save", JsonConvert.SerializeObject(viewModel));
                if (result == null)
                    return View();
                return RedirectToAction(nameof(Index));
            //    }
        }

        // GET: MyPhoneBookController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await HttpClientGet($"phonebook/get/{id}");
            if (result == null)
                return View();
            var phoneBook = JsonConvert.DeserializeObject<PhoneBookViewModel>(result);
            return View(phoneBook);
        }

        // POST: MyPhoneBookController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(PhoneBookViewModel viewModel)
        {
            try
            {
                var result = await HttpClientPut($"phonebook/save", JsonConvert.SerializeObject(viewModel));
                if (result == null)
                    return View();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MyPhoneBookController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await HttpClientGet($"phonebook/get/{id}");
            if (result == null)
                return View();
            var viewModel = JsonConvert.DeserializeObject<PhoneBookViewModel>(result);
            //return View(phoneBook);

             result = await HttpClientPost($"phonebook/delete", JsonConvert.SerializeObject(viewModel));
            if (result == null)
                return View();
            return RedirectToAction(nameof(Index));
        }

        // POST: MyPhoneBookController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, [FromBody]PhoneBookViewModel viewModel)
        {
            try
            {
                var result = await HttpClientPost($"phonebook/delete", JsonConvert.SerializeObject(viewModel));
                if (result == null)
                    return View();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
