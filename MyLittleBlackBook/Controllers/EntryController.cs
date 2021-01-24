using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyLittleBlackBook.API.Model;
using MyLittleBlackBook.DataLayer.Repository.Interfaces;

namespace MyLittleBlackBook.API.Controllers
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
        [Route("entry/save")]
        public IActionResult Save(Entry entry)
        {
            _unitOfWork.Entries.Add(_mapper.Map<DataLayer.Entity.Entry>(entry));
            var success = _unitOfWork.Complete();
            _unitOfWork.Dispose();

            return Ok(success);
        }

        [HttpGet]
        [Route("entry/get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_unitOfWork.Entries.Get(id));
        }

        [HttpGet]
        [Route("entry/delete/{id}")]
        public IActionResult Delete(Entry entry)
        {
            _unitOfWork.Entries.Remove(_mapper.Map<DataLayer.Entity.Entry>(entry));
            return Ok();
        }

        [HttpGet]
        [Route("entry/getall")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Entries.GetAll());
        }
    }
}
