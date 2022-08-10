using AutoMapper;
using Cooperchip.ITDeveloper.Application.ViewModels;
using Cooperchip.ITDeveloper.Application.ViewModels.Farmacia;
using Cooperchip.ITDeveloper.Domain.Entities;
using Cooperchip.ITDeveloper.Domain.ValueObjects;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;

namespace Cooperchip.ITDeveloper.Application.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Paciente, PacienteViewModel>().ReverseMap();

            CreateMap<Tags, TagsViewModel>().ReverseMap();

            CreateMap<Author, AuthorViewModel>()
                .ForMember(rd => rd.Facebook, o => o.MapFrom(s => s.RedesSociais.Facebook))
                .ForMember(rd => rd.Twitter, o => o.MapFrom(s => s.RedesSociais.Twitter))
                .ForMember(rd => rd.Linkedin, o => o.MapFrom(s => s.RedesSociais.Linkedin));

            CreateMap<AuthorViewModel, Author>()
                .ConstructUsing(p =>
                    new Author(p.Name, p.LastName, 
                        p.EmailAddress, p.WebSite, 
                          new RedesSociais(p.Facebook, p.Twitter, p.Linkedin)));

            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();

        }
    }
}
