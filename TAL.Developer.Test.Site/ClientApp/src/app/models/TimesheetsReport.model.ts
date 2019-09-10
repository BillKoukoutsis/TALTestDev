export interface TimesheetsReport {
  employeeId: number;
  employeeName: string;
  employeeUsername: string;
  timezoneName: string;
  startDate: Date;
  formattedStartDate: string;
  endDate: Date;
  formattedEndDate: string;
  rate: number;
  status: string;
  hoursWorked: number;
  cost: number;
}
