using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MyPhoneBook.API.Model;
using MyPhoneBook.DataLayer.Repository.Interfaces;
using Serilog;
using DBModel = MyPhoneBook.DataLayer.Entity;

namespace MyPhoneBook.API.Controllers
{

    [ApiController]
    public class EntryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public EntryController(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
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
        [Route("entry/delete", Name = "entryDelete")]
        public IActionResult Delete(Entry entry)
        {
            _unitOfWork.Entries.Remove(_mapper.Map<DataLayer.Entity.Entry>(entry));
            return Ok();
        }

        [HttpPost]
        [Route("entry/delete/{id}", Name = "entryDeleteById")]
        public IActionResult Delete(int id)
        {
            DBModel.Entry item = _unitOfWork.Entries.Get(id);

            _unitOfWork.Entries.Remove(item);
            var success = _unitOfWork.Complete();
            _unitOfWork.Dispose();

            _cache.Remove($"entryResult_{id}");
            _cache.Remove($"entriesResult_all");

            Log.Information($"Delete entry: {id}");
            return Ok(success);
        }

        [HttpGet]
        [Route("entry/getall/{id}", Name ="entryGetall")]
        public IActionResult GetAllByPhoneBook(int id)
        {
            return Ok(_unitOfWork.Entries.GetAll(id));
        }
    }
}
