using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MyPhoneBook.API.Extensions;
using MyPhoneBook.API.Model;
using MyPhoneBook.DataLayer.Repository.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;
using DBModel = MyPhoneBook.DataLayer.Entity;

namespace MyPhoneBook.API.Controllers
{

    [ApiController] 
    
    //1. query controller -> search all or % 
    //2. cmd controller -> save and [delete]
    //3. caching 
    //4. swagger
    //5. serilog-> save
    //TblPhone: Name:Siyanda 
    //TblEntry-> Name:Cell Number:0825989, Name:Home Number:021898
    public class PhoneBookController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        public PhoneBookController(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        // cmd controller
        [HttpPut]
        [Route("phonebook/save", Name ="phoneBookSave")]
        public IActionResult Save(PhoneBook phoneBook)
        {
            _unitOfWork.PhoneBooks.Add(_mapper.Map<DBModel.PhoneBook>(phoneBook));
            var success = _unitOfWork.Complete();
            _unitOfWork.Dispose();

            return Ok(success);
        }

        [HttpGet]
        [Route("phonebook/get/{id}", Name = "phoneBookGet")]
        public async Task<IActionResult> Get(int id)
        {
            var phoneBook = await _cache.GetCacheValueAsync<PhoneBook>("phoneBookResult");
            if (phoneBook != null)
                return Ok(JsonConvert.SerializeObject(phoneBook));

            phoneBook = _mapper.Map<PhoneBook>(_unitOfWork.PhoneBooks.Get(id));
            await _cache.SetCacheValueAsync("phoneBookResult", phoneBook);
            return Ok(JsonConvert.SerializeObject(phoneBook));
        }

        // cmd controller
        [HttpPost]
        [Route("phonebook/delete", Name ="phoneBookDelete")]
        public IActionResult Delete(PhoneBook phoneBook)
        {
            _unitOfWork.PhoneBooks.Remove(_mapper.Map<DBModel.PhoneBook>(phoneBook));         

            return Ok(_unitOfWork.Complete());
        }
        
        [HttpGet]
        [Route("phonebook/delete/{id}", Name = "phoneBookDeleteById")]
        public IActionResult Delete(int id)
        {
            DBModel.PhoneBook item = _unitOfWork.PhoneBooks.Get(id); 
            
            _unitOfWork.PhoneBooks.Remove(item);
            var success = _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return Ok(success);
        }

        [HttpGet]
        [Route("phonebook/getall", Name = "phoneBookGetall")]
        public IActionResult GetAll()
        {
            return Ok(JsonConvert.SerializeObject(_unitOfWork.PhoneBooks.GetAll()));
        }
    }
}
