using AutoMapper;
using TAL.Developer.Test.Domain.Models;

namespace TAL.Developer.Test.Domain
{
    public class AutoMapperConfig
    {
        private static bool _isInitialized = false;

        public static bool IsInitialized { get { return _isInitialized; } }

        public static void Initialize()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            Mapper.Initialize((config) =>
            {
                config.CreateMap<spOccupationRatings_GetList_Result, OccupationRatingsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.oraId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.oraName))
                .ForMember(dest => dest.Factor, opt => opt.MapFrom(src => src.oraFactor));

                config.CreateMap<spOccupationRatings_GetById_Result, OccupationRatingsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.oraId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.oraName))
                .ForMember(dest => dest.Factor, opt => opt.MapFrom(src => src.oraFactor));

                config.CreateMap<spOccupations_GetList_Result, OccupationsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.occId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.occName))
                .ForMember(dest => dest.OccupationRating, opt => opt.MapFrom(src => src));
                config.CreateMap<spOccupations_GetList_Result, OccupationRatingsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.occOccupationRatingId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.oraName))
                .ForMember(dest => dest.Factor, opt => opt.MapFrom(src => src.oraFactor));

                config.CreateMap<spOccupations_GetById_Result, OccupationsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.occId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.occName))
                .ForMember(dest => dest.OccupationRating, opt => opt.MapFrom(src => src));
                config.CreateMap<spOccupations_GetById_Result, OccupationRatingsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.occOccupationRatingId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.oraName))
                .ForMember(dest => dest.Factor, opt => opt.MapFrom(src => src.oraFactor));

                config.CreateMap<spMembers_GetList_Result, MembersModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.memId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.memName))
                .ForMember(dest => dest.DOB, opt => opt.MapFrom(src => src.memDOB))
                .ForMember(dest => dest.SumInsured, opt => opt.MapFrom(src => src.memSumInsured))
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src));
                config.CreateMap<spMembers_GetList_Result, OccupationsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.memOccupationId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.occName))
                .ForMember(dest => dest.OccupationRating, opt => opt.MapFrom(src => src));
                config.CreateMap<spMembers_GetList_Result, OccupationRatingsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.occOccupationRatingId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.oraName))
                .ForMember(dest => dest.Factor, opt => opt.MapFrom(src => src.oraFactor));

                config.CreateMap<spMembers_GetById_Result, MembersModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.memId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.memName))
                .ForMember(dest => dest.DOB, opt => opt.MapFrom(src => src.memDOB))
                .ForMember(dest => dest.SumInsured, opt => opt.MapFrom(src => src.memSumInsured))
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src));
                config.CreateMap<spMembers_GetById_Result, OccupationsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.memOccupationId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.occName))
                .ForMember(dest => dest.OccupationRating, opt => opt.MapFrom(src => src));
                config.CreateMap<spMembers_GetById_Result, OccupationRatingsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.occOccupationRatingId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.oraName))
                .ForMember(dest => dest.Factor, opt => opt.MapFrom(src => src.oraFactor));

                _isInitialized = true;
            });
#pragma warning restore CS0618 // Type or member is obsolete

        }
    }
}