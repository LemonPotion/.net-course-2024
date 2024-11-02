using System.Linq.Expressions;
using AutoMapper;
using BankSystem.App.Dto.Employee.Requests;
using BankSystem.App.Dto.Employee.Responses;
using BankSystem.Domain.Models;

namespace BankSystem.App.Mapping;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile()
    {
        CreateMap<CreateEmployeeRequest, Employee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.Contract))
            .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber));

        CreateMap<UpdateEmployeeRequest, Employee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.Contract))
            .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber));
        
        CreateMap<Employee, GetEmployeeByIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.Contract))
            .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber));
        
        CreateMap<EmployeeFilterRequest, Expression<Func<Employee, bool>>>()
            .ConvertUsing((request, _) =>
                x => (string.IsNullOrEmpty(request.FirstName) || x.FirstName == request.FirstName) &&
                     (string.IsNullOrEmpty(request.LastName) || x.LastName == request.LastName) &&
                     (request.BirthDay == default || x.BirthDay == request.BirthDay) &&
                     (string.IsNullOrEmpty(request.PhoneNumber) || x.PhoneNumber == request.PhoneNumber) &&
                     (string.IsNullOrEmpty(request.Email) || x.Email == request.Email) &&
                     (string.IsNullOrEmpty(request.PassportNumber) || x.PassportNumber == request.PassportNumber) &&
                     (string.IsNullOrEmpty(request.Contract) || x.Contract == request.Contract) &&
                     (request.Salary == default || x.Salary == request.Salary));
    }
}