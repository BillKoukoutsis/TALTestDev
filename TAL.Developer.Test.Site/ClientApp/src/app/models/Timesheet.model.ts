export interface Timesheet {
  id: number;
  employeeId: number;
  employeeName: string;
  timezoneId: number;
  timezoneName: string;
  startDate: Date;
  formattedStartDate: string;
  endDate: Date;
  formattedEndDate: string;
  rate: number;
}
