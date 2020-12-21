using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_Asp.net.Models;

namespace WebApi_Asp.net.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            string query = @" select EmployeeID,EmployeeName,Department,MailID, curdate() as DOJ FROM EMPLOYEES
                              ";

            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new MySqlCommand(query, con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Employee emp)
        {
            try
            {
                DataTable table = new DataTable();
               // string doj = emp.DOJ.ToString().Split('')[0];
                string query = @" insert into employees(EmployeeName,Department,MailID,DOJ)
                                  values('" + emp.EmployeeName + @"',
                                         '" + emp.Department + @"',
                                         '" + emp.MailID + @"',
                                         '" + Convert.ToDateTime(emp.DOJ).ToString("yyyy-MM-dd") + @"')";

               // string query = "Usp_EmployeeInsert";
            
                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("EmployeeName",emp.EmployeeName);
                    //cmd.Parameters.AddWithValue("Department", emp.Department);
                    //cmd.Parameters.AddWithValue("MailID", emp.MailID);
                    //cmd.Parameters.AddWithValue("DOJ",);
                    da.Fill(table);
                }
                return "Added Successfully";
            }
            catch (Exception )
            {
               return "Failed to Add";
            }
        }

        public string Put(Employee emp)
        {
            try
            {
                DataTable table = new DataTable();
                string query = @" update employees set
                                  EmployeeName = '" + emp.EmployeeName + @"', 
                                  Department = '" + emp.Department + @"',
                                  MailID = '" + emp.MailID + @"',
                                  DOJ = '" + Convert.ToDateTime(emp.DOJ).ToString("yyyy-MM-dd") + @"'
                                  where EmployeeID = " + emp.EmployeeID + @"
                                ";
                                  

                 //string query = "Usp_EmployeeInsert";

                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("EmployeeName",emp.EmployeeName);
                    //cmd.Parameters.AddWithValue("Department", emp.Department);
                    //cmd.Parameters.AddWithValue("MailID", emp.MailID);
                    //cmd.Parameters.AddWithValue("DOJ",);
                    da.Fill(table);
                }
                return "Udpated Successfully";
            }
            catch (Exception)
            {
                return "Failed to Updated";
            }
        }


        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                //string query = @" delete from employees 
                //                  where EmployeeID = " + id;

                string query = "Usp_EmployeeDelete";

                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                   // cmd.CommandType = CommandType.Text;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("e_EmployeeID", id);
                    
                    da.Fill(table);
                }
                return "Delete Successfully";
            }
            catch (Exception)
            {
                return "Failed to Delete";
            }
        }


    }
}
