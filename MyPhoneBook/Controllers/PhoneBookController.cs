using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MyPhoneBook.API.Extensions;
using MyPhoneBook.API.Model;
using MyPhoneBook.DataLayer.Repository.Interfaces;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using DBModel = MyPhoneBook.DataLayer.Entity;

namespace MyPhoneBook.API.Controllers
{

    [ApiController]
    public class PhoneBookController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        //private readonly ILogger _logger;
        public PhoneBookController(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
            //_logger = logger;
        }
                
        [HttpPut]
        [Route("phonebook/save", Name ="phoneBookSave")]
        public IActionResult Save(PhoneBook phoneBook)
        {
            _unitOfWork.PhoneBooks.Add(_mapper.Map<DBModel.PhoneBook>(phoneBook));
            var success = _unitOfWork.Complete();
            _unitOfWork.Dispose();
            
            _cache.Remove($"phoneBookResult_{phoneBook.Id}"); //invalidate cache
            _cache.Remove($"phoneBookResult_all");
            Log.Information($"Save phoneBook: {phoneBook.Id}");
            return Ok(success);
        }

        [HttpGet]
        [Route("phonebook/get/{id}", Name = "phoneBookGet")]
        public async Task<IActionResult> Get(int id)
        {
            Log.Information($"Get phoneBook: {id}");

            var phoneBook = await _cache.GetCacheValueAsync<PhoneBook>($"phoneBookResult_{id}");
            if (phoneBook != null)
                return Ok(JsonConvert.SerializeObject(phoneBook)); 

            phoneBook = _mapper.Map<PhoneBook>(_unitOfWork.PhoneBooks.Get(id));
            await _cache.SetCacheValueAsync($"phoneBookResult_{id}", phoneBook);            

            return Ok(JsonConvert.SerializeObject(phoneBook));
        }
        
        [HttpPost]
        [Route("phonebook/delete", Name ="phoneBookDelete")]
        public IActionResult Delete(PhoneBook phoneBook)
        {            
            _unitOfWork.PhoneBooks.Remove(_mapper.Map<DBModel.PhoneBook>(phoneBook));
            _cache.Remove($"phoneBookResult_{phoneBook.Id}");
            Log.Information($"Delete phoneBook: {phoneBook.Id}");
            return Ok(_unitOfWork.Complete());
        }
        
        [HttpPost]
        [Route("phonebook/delete/{id}", Name = "phoneBookDeleteById")]
        public IActionResult Delete(int id)
        {
            DBModel.PhoneBook item = _unitOfWork.PhoneBooks.Get(id); 
            
            _unitOfWork.PhoneBooks.Remove(item);
            var success = _unitOfWork.Complete();
            _unitOfWork.Dispose();

            _cache.Remove($"phoneBookResult_{id}");
            _cache.Remove($"phoneBookResult_all");
            
            Log.Information($"Delete phoneBook: {id}");
            return Ok(success);
        }

        [HttpGet]
        [Route("phonebook/getall", Name = "phoneBookGetall")]
        public async Task<IActionResult> GetAll()
        {
            Log.Information("Action: PhoneBook GetAll");
            var phoneBooks = await _cache.GetCacheValueAsync<IEnumerable<PhoneBook>>($"phoneBookResult_all");
            if (phoneBooks != null)
                return Ok(JsonConvert.SerializeObject(phoneBooks));

            phoneBooks = _mapper.Map<IEnumerable<PhoneBook>>(_unitOfWork.PhoneBooks.GetAll());
            await _cache.SetCacheValueAsync($"phoneBookResult_all", phoneBooks);
            //Log.Information("{@phoneBooks}", phoneBooks);
            
            return Ok(phoneBooks == null ? string.Empty : JsonConvert.SerializeObject(phoneBooks));
        }
    }
}
