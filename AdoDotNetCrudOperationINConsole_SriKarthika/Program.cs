using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;

namespace AdoDotNetCrudOperationINConsole_SriKarthika
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = "Server = LAPTOP-4AQC26NC\\SQLSERVER2022;Database = employee1db;User Id = sa;Password = user123;Trusted_Connection = True;" + 
                    "TrustServerCertificate = True;";
                GetAllEmployees(connectionString);
                int employeeid = 1;
                GetEmployeeById(connectionString, employeeid);
                Console.WriteLine("Enter thr first name:");
                string firstname = Console.ReadLine();
                Console.WriteLine("Enter the last name");
                string lastname = Console.ReadLine();
                Console.WriteLine("Enter the email id");
                string email = Console.ReadLine();
                Console.WriteLine("Enter the street name");
                string street = Console.ReadLine();
                Console.WriteLine("Enter the city name");
                string city = Console.ReadLine();
                Console.WriteLine("Enter the state name");
                string states = Console.ReadLine();
                Console.WriteLine("Enter the post code");
                string postalcode = Console.ReadLine();
                CreateEmployeeWithAddress(connectionString, firstname, lastname, email, street, city, states, postalcode);
                int employeeID = 3;
                firstname = "Rakesh";
                lastname = "Sharma";
                email = "Rakesh@Example.com";
                street = "3456 Patia";
                city = "CTC";
                states = "India";
                postalcode = "755024";
                int addressid = 3;
                UpdateEmployeeWithAddress(connectionString, employeeid, firstname, lastname, email, street, city, states, postalcode, addressid);
                employeeID = 3;
                DeleteEmployee(connectionString, employeeID);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Something went wrong:{ex.Message}");
            }
            //Console.WriteLine("Connection Successfull");
        }
        static void GetAllEmployees(string connectionString)
        {
            Console.WriteLine("GetAllEmployees Stored Procedure Called");
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("getallemployees", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Console.WriteLine($"EmployeeID:{reader["employeeid"]},FirstName:{reader["firstname"]},LastName:{reader["lastname"]},Email:{reader["email"]}");
                    Console.WriteLine($"Address:{reader["street"]},{reader["city"]},{reader["states"]},PostalCode:{reader["postalcode"]}\n");
                }
                reader.Close();
                connection.Close();
            }
        }
        static void GetEmployeeById(string connectionString,int employeeID)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                Console.WriteLine("GetEmployeeById Stored Procedure Called");
                SqlCommand command = new SqlCommand("getemployeeid", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employeeid", employeeID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Console.WriteLine($"Employee:{reader["firstname"]},{reader["lastname"]},Email:{reader["email"]}");
                    Console.WriteLine($"Address:{reader["street"]},{reader["city"]},{reader["states"]},{reader["postalcode"]}");
                }
                reader.Close();
                connection.Close();
            }
        }
        static void CreateEmployeeWithAddress(string connectionString,string firstname,string lastname,string email,string street,string city,string states,string postalcode)
        {
            using( SqlConnection connection = new SqlConnection( connectionString ))
            {
                Console.WriteLine("CreateEmployeeWithAddress Stored Procedure Called");
                SqlCommand command = new SqlCommand("CreateEmployeeWithAddress",connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@firstname", firstname);
                command.Parameters.AddWithValue("@lastname", lastname);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@street", street);
                command.Parameters.AddWithValue("@city", city);
                command.Parameters.AddWithValue("@states", states);
                command.Parameters.AddWithValue("@postalcode", postalcode);
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Employee and Address created successfully");
                connection.Close();
            }
        }
        static void UpdateEmployeeWithAddress(string connectionString,int employeeid,string firstname,string lastname,string email,string street,string city,string states,string postalcode,int addressid)
        {
            using(SqlConnection connection = new SqlConnection(connectionString ))
            {
                Console.WriteLine("UpdateEmployeeWithAddress Stored Procedure Called");
                SqlCommand command = new SqlCommand("updateemployeewithaddress", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employeeid", employeeid);
                command.Parameters.AddWithValue("@firstname", firstname);
                command.Parameters.AddWithValue("@lastname", lastname);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@street", street);
                command.Parameters.AddWithValue("@city", city);
                command.Parameters.AddWithValue("@states", states);
                command.Parameters.AddWithValue("@postalcode", postalcode);
                command.Parameters.AddWithValue("@addressid", addressid);
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Employee and Address updated successfully");
                connection.Close();
            }
        }
        static void DeleteEmployee(string connectionString, int employeeid)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Console.WriteLine("Delete Employee stored procedure called");
                SqlCommand command = new SqlCommand("deleteemployee", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@employeeid", employeeid);
                connection.Open();
                int result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Employee and their address deleted successfully");
                }
                else
                {
                    Console.WriteLine("employee not found");
                }
                connection.Close();
            }

        }
    }
}