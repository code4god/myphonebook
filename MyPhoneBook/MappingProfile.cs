using AutoMapper;
using MyPhoneBook.API.Model;

namespace MyPhoneBook.API
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
