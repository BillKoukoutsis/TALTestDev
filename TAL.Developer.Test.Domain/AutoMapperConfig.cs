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
                config.CreateMap<spGroups_GetList_Result, GroupsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.grpId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.grpName));

                config.CreateMap<spGroups_GetById_Result, GroupsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.grpId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.grpName));

                config.CreateMap<spTimezones_GetList_Result, TimezonesModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.tmzId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.tmzName));

                config.CreateMap<spTimezones_GetById_Result, TimezonesModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.tmzId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.tmzName));

                config.CreateMap<spEmployees_GetList_Result, EmployeesModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.empId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.empName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.empUsername))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.empPassword))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.empGroupId))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.grpName))
                .ForMember(dest => dest.TimezoneId, opt => opt.MapFrom(src => src.empTimezoneId))
                .ForMember(dest => dest.TimezoneName, opt => opt.MapFrom(src => src.tmzName))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.empRate));

                config.CreateMap<spEmployees_GetById_Result, EmployeesModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.empId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.empName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.empUsername))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.empPassword))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.empGroupId))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.grpName))
                .ForMember(dest => dest.TimezoneId, opt => opt.MapFrom(src => src.empTimezoneId))
                .ForMember(dest => dest.TimezoneName, opt => opt.MapFrom(src => src.tmzName))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.empRate));

                config.CreateMap<spTimesheets_GetList_Result, TimesheetsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.tshId))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.tshEmployeeId))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.empName))
                .ForMember(dest => dest.TimezoneId, opt => opt.MapFrom(src => src.tshTimezoneId))
                .ForMember(dest => dest.TimezoneName, opt => opt.MapFrom(src => src.tmzName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.tshStartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.tshEndDate))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.tshRate));

                config.CreateMap<spTimesheets_GetById_Result, TimesheetsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.tshId))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.tshEmployeeId))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.empName))
                .ForMember(dest => dest.TimezoneId, opt => opt.MapFrom(src => src.tshTimezoneId))
                .ForMember(dest => dest.TimezoneName, opt => opt.MapFrom(src => src.tmzName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.tshStartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.tshEndDate))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.tshRate));

                config.CreateMap<spTimesheets_ReportByDate_Result, TimesheetsReportModel>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.empId))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.empName))
                .ForMember(dest => dest.EmployeeUsername, opt => opt.MapFrom(src => src.empUsername))
                .ForMember(dest => dest.TimezoneName, opt => opt.MapFrom(src => src.tmzName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.tshStartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.tshEndDate))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.tshRate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.HoursWorked, opt => opt.MapFrom(src => src.HoursWorked))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost));

                config.CreateMap<spEmployees_GetByCredentials_Result, EmployeesModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.empId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.empName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.empUsername))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.empPassword))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.empGroupId))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.grpName))
                .ForMember(dest => dest.TimezoneId, opt => opt.MapFrom(src => src.empTimezoneId))
                .ForMember(dest => dest.TimezoneName, opt => opt.MapFrom(src => src.tmzName))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.empRate));

                _isInitialized = true;
            });
#pragma warning restore CS0618 // Type or member is obsolete

        }
    }
}