using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyLittleBlackBook.API.Model;
using MyLittleBlackBook.DataLayer.Repository.Interfaces;
using Newtonsoft.Json;
using DBModel = MyLittleBlackBook.DataLayer.Entity;

namespace MyLittleBlackBook.API.Controllers
{

    [ApiController]
    public class PhoneBookController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PhoneBookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

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
