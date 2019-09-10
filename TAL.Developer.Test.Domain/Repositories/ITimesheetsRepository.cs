using TAL.Developer.Test.Domain.Models;
using System;
using System.Collections.Generic;

namespace TAL.Developer.Test.Domain.Repositories
{
    public interface ITimesheetsRepository
    {
        void DeleteById(int id);
        void DeleteTestData();
        TimesheetsModel GetById(int id);
        IEnumerable<TimesheetsModel> GetList();
        IEnumerable<TimesheetsReportModel> ReportByDate(DateTime? date, string timezone);
        decimal? Insert(TimesheetsModel model);
        void UpdateById(TimesheetsModel model);
    }
}