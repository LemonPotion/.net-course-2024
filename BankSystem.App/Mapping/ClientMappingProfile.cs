using System.Linq.Expressions;
using AutoMapper;
using BankSystem.App.Dto.Client.Requests;
using BankSystem.App.Dto.Client.Responses;
using BankSystem.Domain.Models;

namespace BankSystem.App.Mapping;

public class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        CreateMap<CreateClientRequest, Client>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BankAccountNumber, opt => opt.MapFrom(src => src.BankAccountNumber))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber));

        CreateMap<UpdateClientRequest, Client>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BankAccountNumber, opt => opt.MapFrom(src => src.BankAccountNumber))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber));
        
        CreateMap<Client, GetClientByIdResponse>()
            .ForMember(dest => dest.BankAccountNumber, opt => opt.MapFrom(src => src.BankAccountNumber))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        
        CreateMap<ClientFilterRequest, Expression<Func<Client, bool>>>()
            .ConvertUsing((request, _) =>
                x => (string.IsNullOrEmpty(request.FirstName) || x.FirstName == request.FirstName) &&
                     (string.IsNullOrEmpty(request.LastName) || x.LastName == request.LastName) &&
                     (request.BirthDay == default || x.BirthDay == request.BirthDay) &&
                     (string.IsNullOrEmpty(request.PhoneNumber) || x.PhoneNumber == request.PhoneNumber) &&
                     (string.IsNullOrEmpty(request.Email) || x.Email == request.Email) &&
                     (string.IsNullOrEmpty(request.PassportNumber) || x.PassportNumber == request.PassportNumber) &&
                     (string.IsNullOrEmpty(request.BankAccountNumber) || x.BankAccountNumber == request.BankAccountNumber));
    }
}