using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyPhoneBook.API.Model;
using MyPhoneBook.DataLayer.Repository.Interfaces;
using Newtonsoft.Json;
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
        public PhoneBookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // cmd controller
        [HttpPut]
        [Route("phonebook/save")]
        public IActionResult Save(PhoneBook phoneBook)
        {
            _unitOfWork.PhoneBooks.Add(_mapper.Map<DBModel.PhoneBook>(phoneBook));
            var success = _unitOfWork.Complete();
            _unitOfWork.Dispose();

            return Ok(success);
        }

        [HttpGet]
        [Route("phonebook/get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(JsonConvert.SerializeObject((_unitOfWork.PhoneBooks.Get(id))));
        }

        // cmd controller
        [HttpPost]
        [Route("phonebook/delete")]
        public IActionResult Delete(PhoneBook phoneBook)
        {
            _unitOfWork.PhoneBooks.Remove(_mapper.Map<DBModel.PhoneBook>(phoneBook));         

            return Ok(_unitOfWork.Complete());
        }
        
        [HttpGet]
        [Route("phonebook/delete/{id}")]
        public IActionResult Delete(int id)
        {
            DBModel.PhoneBook item = _unitOfWork.PhoneBooks.Get(id); 
            
            _unitOfWork.PhoneBooks.Remove(item);
            var success = _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return Ok(success);
        }

        [HttpGet]
        [Route("phonebook/getall")]
        public IActionResult GetAll()
        {
            return Ok(JsonConvert.SerializeObject(_unitOfWork.PhoneBooks.GetAll()));
        }
    }
}
