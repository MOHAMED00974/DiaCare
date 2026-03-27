using AutoMapper;
using DiaCare.Application.DTOS;
using DiaCare.Domain.DTOS;
using DiaCare.Domain.Entities;

namespace DiaCare.Application.Profiles
{
    public class PredictionProfile : Profile
    {
        public PredictionProfile()
        {
            CreateMap<PredictionInputDto, HealthProfile>();
            CreateMap<PredictionResult, PredictionResultDto>().ReverseMap();
            CreateMap<Article, ArticleDto>().ReverseMap();
        }
    }
}