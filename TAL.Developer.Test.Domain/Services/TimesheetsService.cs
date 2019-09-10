using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using TAL.Developer.Test.Domain.Logging;
using TAL.Developer.Test.Domain.Models;
using TAL.Developer.Test.Domain.Repositories;

namespace TAL.Developer.Test.Domain.Services
{
    public class TimesheetsService : ITimesheetsService
    {
        private readonly ILogger _logger;
        private readonly ITimesheetsRepository _timesheetsRepository;

        public TimesheetsService(ILogger logger, ITimesheetsRepository contactsRepository)
        {
            _logger = logger;
            _timesheetsRepository = contactsRepository;
        }

        public void DeleteById(int id)
        {
            try
            {
                _timesheetsRepository.DeleteById(id);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public void DeleteTestData()
        {
            try
            {
                _timesheetsRepository.DeleteTestData();
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public IEnumerable<TimesheetsModel> GetList()
        {
            try
            {
                var list = _timesheetsRepository.GetList();

                foreach (var entry in list)
                {
                    if (entry.TimezoneName != null)
                    {
                        TimeZoneInfo eauZone = TimeZoneInfo.FindSystemTimeZoneById(entry.TimezoneName);
                        if (entry.StartDate != null)
                            entry.StartDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)entry.StartDate, eauZone);
                        if (entry.EndDate != null)
                            entry.EndDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)entry.EndDate, eauZone);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
           
        }

        public IEnumerable<TimesheetsReportModel> ReportByDate(DateTime? date)
        {
            try
            {
                TimeZoneInfo eauReportZone = TimeZoneInfo.FindSystemTimeZoneById("E. Australia Standard Time");
                if (date != null)
                    date = TimeZoneInfo.ConvertTimeFromUtc((DateTime)date, eauReportZone);

                var report = _timesheetsRepository.ReportByDate(date, "E. Australia Standard Time");

                foreach (var row in report)
                {
                    if (row.TimezoneName != null)
                    {
                        TimeZoneInfo eauZone = TimeZoneInfo.FindSystemTimeZoneById(row.TimezoneName);
                        if (row.StartDate != null)
                            row.StartDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)row.StartDate, eauZone);
                        if (row.EndDate != null)
                            row.EndDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)row.EndDate, eauZone);
                    }
                }

                return report;
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }

        }

        public TimesheetsModel GetById(int id)
        {
            try
            {
                var entry = _timesheetsRepository.GetById(id);

                if (entry.TimezoneName != null)
                {
                    TimeZoneInfo eauZone = TimeZoneInfo.FindSystemTimeZoneById(entry.TimezoneName);
                    if (entry.StartDate != null)
                        entry.StartDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)entry.StartDate, eauZone);
                    if (entry.EndDate != null)
                        entry.EndDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)entry.EndDate, eauZone);
                }

                return entry;
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public decimal? Insert(TimesheetsModel model)
        {
            try
            {
                if (model.TimezoneName != null)
                {
                    TimeZoneInfo eauZone = TimeZoneInfo.FindSystemTimeZoneById(model.TimezoneName);
                    if (model.StartDate != null)
                        model.StartDate = TimeZoneInfo.ConvertTimeToUtc((DateTime)model.StartDate, eauZone);
                }

                return _timesheetsRepository.Insert(model);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
        }

        public void UpdateById(TimesheetsModel model)
        {
            try
            {
                if (model.TimezoneName != null)
                {
                    TimeZoneInfo eauZone = TimeZoneInfo.FindSystemTimeZoneById(model.TimezoneName);
                    if (model.StartDate != null)
                    {
                        DateTime startDate = (DateTime)model.StartDate;

                        if (startDate.Kind == DateTimeKind.Unspecified)
                            model.StartDate = TimeZoneInfo.ConvertTimeToUtc((DateTime)model.StartDate, eauZone);
                    }
                    if (model.EndDate != null)
                    {
                        DateTime endDate = (DateTime)model.EndDate;

                        if (endDate.Kind == DateTimeKind.Unspecified)
                            model.EndDate = TimeZoneInfo.ConvertTimeToUtc(endDate, eauZone);
                    }
                }

                _timesheetsRepository.UpdateById(model);
            }
            catch (Exception ex)
            {
                while (ex.Message.IndexOf("See the inner exception for details.") > 0)
                    ex = ex.InnerException;

                _logger.LogError(ex.Message);

                throw ex;
            }
        }

    }
}