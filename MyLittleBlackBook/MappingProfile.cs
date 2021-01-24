using AutoMapper;
using MyLittleBlackBook.API.Model;

namespace MyLittleBlackBook.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DataLayer.Entity.PhoneBook, PhoneBook>().ReverseMap();
            CreateMap<DataLayer.Entity.Entry, Entry>().ReverseMap();
        }

    }
}
