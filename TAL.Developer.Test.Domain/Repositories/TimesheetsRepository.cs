using AutoMapper;
using TAL.Developer.Test.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TAL.Developer.Test.Domain.Repositories
{
    public class TimesheetsRepository : ITimesheetsRepository
    {
        TALTestEntities _context;

        private TimesheetsRepository() { } // hide default constructor

        public TimesheetsRepository(TALTestEntities context)
        {
            _context = context;
        }

        public void DeleteById(int id)
        {
            _context.spTimesheets_DeleteById(id);
        }

        public void DeleteTestData()
        {
            _context.spTimesheets_DeleteTestData();
        }

        public IEnumerable<TimesheetsModel> GetList()
        {
            return Mapper.Map<IEnumerable<TimesheetsModel>>(_context.spTimesheets_GetList());
        }

        public IEnumerable<TimesheetsReportModel> ReportByDate(DateTime? date, string timezone)
        {
            return Mapper.Map<IEnumerable<TimesheetsReportModel>>(_context.spTimesheets_ReportByDate(date, timezone));
        }

        public TimesheetsModel GetById(int id)
        {
            return Mapper.Map<TimesheetsModel>(_context.spTimesheets_GetById(id).FirstOrDefault());
        }

        public decimal? Insert(TimesheetsModel model)
        {
            return _context.spTimesheets_Insert
                (
                    model.EmployeeId,
                    model.TimezoneId,
                    model.StartDate,
                    model.Rate
                ).FirstOrDefault();
        }

        public void UpdateById(TimesheetsModel model)
        {
            _context.spTimesheets_UpdateById(model.Id, model.TimezoneId, model.StartDate, model.EndDate, model.Rate);
        }

    }
}