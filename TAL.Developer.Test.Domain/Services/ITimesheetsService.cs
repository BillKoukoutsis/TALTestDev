using TAL.Developer.Test.Domain.Models;
using System;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Services
{
    public interface ITimesheetsService
    {
        void DeleteById(int id);
        void DeleteTestData();
        IEnumerable<TimesheetsModel> GetList();
        IEnumerable<TimesheetsReportModel> ReportByDate(DateTime? date);
        TimesheetsModel GetById(int id);
        decimal? Insert(TimesheetsModel model);
        void UpdateById(TimesheetsModel model);
    }
}