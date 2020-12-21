using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_Asp.net.Models;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace WebApi_Asp.net.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DataTable table = new DataTable();
            //  string query = @" select DepartmentID ,DepartmentName from departments ";
            string query = "Usp_GetDipartment";

            using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new MySqlCommand(query, con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                //cmd.CommandType = CommandType.Text;
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Department dep)
        {
            try
            {
                DataTable table = new DataTable();
            //  string query = @" insert into departments(DepartmentName) values('"+dep.DepartmentName + @"')";
                string query = "Usp_DipartmentInsert";

                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    //cmd.CommandType = CommandType.Text;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("DepartmentName", dep.DepartmentName);
                    da.Fill(table);
                }
                return "Added Successfully";
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public string Put(Department dep)
        {
            try
            {
                DataTable table = new DataTable();
                // string query = @" insert into departments(DepartmentName) values('"+dep.DepartmentName + @"')";
                string query = "Usp_Department_Update";

                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                   // cmd.CommandType = CommandType.Text;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("d_DepartmentID", dep.DepartmentID);
                    cmd.Parameters.AddWithValue("d_DepartmentName", dep.DepartmentName);
                    da.Fill(table);
                }
                return "Updated Successfully";
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public string Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                // string query = @" insert into departments(DepartmentName) values('"+dep.DepartmentName + @"')";
                string query = "Usp_Department_Delete";

                using (var con = new MySqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new MySqlCommand(query, con))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    // cmd.CommandType = CommandType.Text;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("d_DepartmentID", id);
                    da.Fill(table);
                }
                return "Delete Successfully";
            }
            catch (Exception )
            {
                return "Failed to Delete";

            }
        }


    }
}
