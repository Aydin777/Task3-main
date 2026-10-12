using AutoMapper;
using Task3.Dtos;
using Task3.Entities;

namespace Task3.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<BookAddDto, Book>();
        CreateMap<Book, BookExtendDto>()
            .ForMember(d => d.Age, opt => opt.MapFrom<BookAgeResolver>());
    }
}
