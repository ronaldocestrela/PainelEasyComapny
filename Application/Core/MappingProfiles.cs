using Application.Bookmakers.DTOs;
using Application.Campaigns.DTOs;
using Application.Projects.DTOs;
using Application.Reports.DTOs;
using AutoMapper;
using Core.Entities;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Bookmaker
        CreateMap<CreateBookmakerDto, Bookmaker>().ReverseMap();
        CreateMap<Bookmaker, ListBookmaker>().ReverseMap();

        // Campaign
        CreateMap<CreateCampaignDto, Campaign>().ReverseMap();
        CreateMap<Campaign, ListCampaignDto>().ReverseMap();

        // Project
        CreateMap<CreateProjectDto, Project>().ReverseMap();
        CreateMap<Project, ListProjectDto>().ReverseMap();

        // Report
        CreateMap<CreateReportDto, Report>().ReverseMap();
        CreateMap<Report, ListReportDto>()
            .ForMember(dest => dest.CampaignName, opt => opt.MapFrom(src => src.Campaign != null ? src.Campaign.Name : string.Empty))
            .ForMember(dest => dest.CampaignDescription, opt => opt.MapFrom(src => src.Campaign != null ? src.Campaign.Description : string.Empty))
            .ForMember(dest => dest.BookmakerName, opt => opt.MapFrom(src => src.Campaign != null && src.Campaign.Bookmaker != null ? src.Campaign.Bookmaker.Name : string.Empty))
            .ForMember(dest => dest.BookmakerWebsite, opt => opt.MapFrom(src => src.Campaign != null && src.Campaign.Bookmaker != null ? src.Campaign.Bookmaker.Website : string.Empty))
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Campaign != null && src.Campaign.Project != null ? src.Campaign.Project.Name : string.Empty))
            .ForMember(dest => dest.ProjectDescription, opt => opt.MapFrom(src => src.Campaign != null && src.Campaign.Project != null ? src.Campaign.Project.Description : string.Empty))
            .ReverseMap();
    }
}
