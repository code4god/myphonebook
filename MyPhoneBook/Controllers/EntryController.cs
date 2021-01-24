using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyPhoneBook.API.Model;
using MyPhoneBook.DataLayer.Repository.Interfaces;

namespace MyPhoneBook.API.Controllers
{

    [ApiController]
    public class EntryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EntryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPut]
        [Route("entry/save", Name = "entrySave")]
        public IActionResult Save(Entry entry)
        {
            _unitOfWork.Entries.Add(_mapper.Map<DataLayer.Entity.Entry>(entry));
            var success = _unitOfWork.Complete();
            _unitOfWork.Dispose();

            return Ok(success);
        }

        [HttpGet]
        [Route("entry/get/{id}", Name = "entryGet")]
        public IActionResult Get(int id)
        {
            return Ok(_unitOfWork.Entries.Get(id));
        }

        [HttpPost]
        [Route("entry/delete/{id}", Name = "entryDelete")]
        public IActionResult Delete(Entry entry)
        {
            _unitOfWork.Entries.Remove(_mapper.Map<DataLayer.Entity.Entry>(entry));
            return Ok();
        }

        [HttpGet]
        [Route("entry/getall/{phoneBookId}", Name ="entryGetall")]
        public IActionResult GetAll(int phoneBookId)
        {
            return Ok(_unitOfWork.Entries.GetAll(phoneBookId));
        }
    }
}
