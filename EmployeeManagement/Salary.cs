using EmployeeManagement.Model.SalaryModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmployeeManagement
{
    public class Salary
    {
        private static SqlConnection ConnectionSetup()
        {
            return new SqlConnection(@"Data Source=(LocalDb)\localdb;Initial Catalog=EmployeeManagement;Integrated Security=True");
        }

        public int UpdateEmployeeSalary(SalaryUpdateModel model)
        {
            SqlConnection SalaryConnection = ConnectionSetup();
            int salary = 0;
            try
            {
                using (SalaryConnection)
                {
                    SalaryDetailModel displayModel = new SalaryDetailModel();
                    SqlCommand command = new SqlCommand("spUpdateEmployeeSalary", SalaryConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", model.SalaryId);
                    command.Parameters.AddWithValue("@month", model.Month);
                    command.Parameters.AddWithValue("@salary", model.EmployeeSalary);
                    command.Parameters.AddWithValue("@EmpId", model.EmployeeId);
                    SalaryConnection.Open();

                    SqlDataReader dr = command.ExecuteReader();

                    //Check if there are Records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            displayModel.EmployeeId = Convert.ToInt32(dr["EmpId"]);
                            displayModel.EmployeeName = dr["ENAME"].ToString();
                            displayModel.jobDiscription = dr["JOB"].ToString();
                            displayModel.EmployeeSalary = Convert.ToInt32(dr["EMPSAL"]);
                            displayModel.Month = dr["SAL_MONTH"].ToString();
                            displayModel.SalaryId = Convert.ToInt32(dr["SALARYID"]);

                            //Display Retrieved Record
                            Console.WriteLine("{0},{1},{2}", displayModel.EmployeeName, displayModel.EmployeeSalary, displayModel.Month);
                            Console.WriteLine("\n");
                            salary = displayModel.EmployeeSalary;
                        }
                    }
                    else
                        Console.WriteLine("No Data Found");

                    //Close Data Reader
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                SalaryConnection.Close();
            }
            return salary;
        }
    }
}