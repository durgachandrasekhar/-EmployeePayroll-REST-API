﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Model.SalaryModel
{
    class SalaryDetailModel
    {
        //Geeter Setter
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string jobDiscription { get; set; }
        public string Month { get; set; }
        public int EmployeeSalary { get; set; }
        public int SalaryId { get; set; }
    }
}