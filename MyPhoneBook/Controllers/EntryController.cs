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
            _unitOfWork.Entries.Add(_mapper.Map<DBModel.Entry>(entry));
            var success = _unitOfWork.Complete();
            _unitOfWork.Dispose();

            _cache.Remove($"entry_{entry.Id}");
            _cache.Remove($"entries_all");
            Log.Information($"Save phoneBook: {entry.Id}");
            return Ok(success);
        }

        [HttpGet]
        [Route("entry/get/{id}", Name = "entryGet")]
        public async Task<IActionResult> Get(int id)
        {
            Log.Information($"Get entry: {id}");

            var entry = await _cache.GetCacheValueAsync<Entry>($"entry_{id}");
            if (entry != null)
                return Ok(JsonConvert.SerializeObject(entry));

            entry =  _mapper.Map<Entry>(_unitOfWork.Entries.Get(id));
            await _cache.SetCacheValueAsync($"entry_{id}", entry);

            return Ok(JsonConvert.SerializeObject(entry));
        }

        [HttpPost]
        [Route("entry/delete", Name = "entryDelete")]
        public IActionResult Delete(Entry entry)
        {
            _unitOfWork.Entries.Remove(_mapper.Map<DBModel.Entry>(entry));

            _cache.Remove($"entry_{entry.Id}");
            _cache.Remove($"entries_all");
            Log.Information($"Delete phoneBook: {entry.Id}");
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
            _cache.Remove($"entries_all");

            Log.Information($"Delete entry: {id}");
            return Ok(success);
        }

        [HttpGet]
        [Route("entry/getall/{id}", Name ="entryGetall")]
        public async Task<IActionResult> GetAllByPhoneBook(int id)
        {
            Log.Information("Action: Entries GetAll");
            var entries = await _cache.GetCacheValueAsync<IEnumerable<Entry>>($"entries_all");
            if (entries != null)
                return Ok(JsonConvert.SerializeObject(entries));

            entries = _mapper.Map<List<Entry>>(_unitOfWork.Entries.GetAll(id));

            return Ok(JsonConvert.SerializeObject(entries));
        }
    }
}
