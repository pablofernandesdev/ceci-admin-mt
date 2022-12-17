using AutoMapper;
using CeciAdminMT.Domain.DTO.Address;
using CeciAdminMT.Domain.DTO.Auth;
using CeciAdminMT.Domain.DTO.Company;
using CeciAdminMT.Domain.DTO.Role;
using CeciAdminMT.Domain.DTO.User;
using CeciAdminMT.Domain.DTO.ViaCep;
using CeciAdminMT.Domain.Entities;

namespace CeciAdminMT.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<User, UserAddDTO>().ReverseMap();

            CreateMap<User, UserImportDTO>().ReverseMap();

            CreateMap<User, UserResultDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            CreateMap<User, UserUpdateDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<User, UserUpdateRoleDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            #endregion

            #region Auth
            CreateMap<User, AuthResultDTO>()
              .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Email))
              .ReverseMap();

            CreateMap<RefreshToken, AuthResultDTO>()
              .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.Token))
              .ReverseMap();
            #endregion

            #region Role
            CreateMap<Role, RoleAddDTO>().ReverseMap();

            CreateMap<Role, RoleResultDTO>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Role, RoleUpdateDTO>()
                 .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
                 .ReverseMap();

            CreateMap<Role, RoleUpdateDTO>().ReverseMap();
            #endregion

            #region Address
            CreateMap<ViaCepAddressResponseDTO, AddressResultDTO>()
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Cep))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.Bairro))
                .ForMember(dest => dest.Locality, opt => opt.MapFrom(src => src.Localidade))
                .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Complemento))
            .ReverseMap();

            CreateMap<Address, AddressAddDTO>().ReverseMap();

            CreateMap<Address, AddressUpdateDTO>().ReverseMap();

            CreateMap<Address, AddressResultDTO>().ReverseMap();

            #endregion

            #region Company
            CreateMap<Company, CompanyAddDTO>().ReverseMap();

            CreateMap<Company, CompanyResultDTO>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Company, CompanyUpdateDTO>()
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            #endregion
        }
    }
}
