using System;
using System.Linq;
using AutoMapper;

using Issuer.Models;
using Issuer.Models.Api;

namespace Issuer.Infrastructure.AutoMapperProfiles
{
    public class MappingConfigurations : Profile
    {
        public MappingConfigurations()
        {
            CreateMap<ImmunizationResponse, Schema>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => "Vaccination Certificate"))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => "Vaccination Certificate"))
                .ForMember(dest => dest.expirationDate, opt => opt.MapFrom(src => src.NextVaccinationDueDate))
                .ForMember(dest => dest.credential_type, opt => opt.MapFrom(src => "VaccinationEvent"))
                .ForMember(dest => dest.countryOfVaccination, opt => opt.MapFrom(src => src.CountryOfVaccination))
                .ForMember(dest => dest.recipient_type, opt => opt.MapFrom(src => "VaccineRecipient"))
                .ForMember(dest => dest.recipient_fullName, opt => opt.MapFrom(src => src.Patient.FullName))
                .ForMember(dest => dest.recipient_birthDate, opt => opt.MapFrom(src => src.Patient.BirthDate))
                .ForMember(dest => dest.vaccine_type, opt => opt.MapFrom(src => "Vaccine"))
                .ForMember(dest => dest.vaccine_disease, opt => opt.MapFrom(src => src.Vaccine.Disease))
                .ForMember(dest => dest.vaccine_medicinalProductName, opt => opt.MapFrom(src => src.Vaccine.ProductName))
                .ForMember(dest => dest.vaccine_marketingAuthorizationHolder, opt => opt.MapFrom(src => src.Vaccine.Manufacturer))
                .ForMember(dest => dest.vaccine_dateOfVaccination, opt => opt.MapFrom(src => src.DateOfVaccination));

             CreateMap<Credential, CredentialViewModel>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Identifier.Guid));

        }
    }
}
