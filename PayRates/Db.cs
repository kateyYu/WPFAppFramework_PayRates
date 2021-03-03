/*
Group-4
    Kurian, Eldho,
    Mittal, Tanya,
    Pou, Aileen,
    Yu, Katey,
 */
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PayRates
{
    public class Db
    {
        #region Constants
        private const string CONN_STRING
            = "Server=.\\SQLEXPRESS;Trusted_Connection=True;";
        private static Db db;
        private const string READ_COMMAND
            = "USE Personnel; SELECT * FROM Employee";
        private const string INSERT_COMMAND
            = "USE Personnel; if NOT EXISTS (select * FROM Employee)" +
              "INSERT INTO Employee(Name,Position,HourlyPayRate)" +
              "VALUES" +
                    "('Shreya Bansal','Manager','48.65'), " +
                    "('Monica Kapoor','Developer','19.80'), " +
                    "('Sophia Smith','IT Analyst','15.00'), " +
                    "('Robert Lee','Computer Architect','16.50'), " +
                    "('David Smith','Cloud Consultant','34.00'), " +
                    "('Mike Lee','Developer','19.80'), " +
                    "('Daniel Brown','Developer','19.80')";
        private const string CREATE_COMMAND
            = "IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'Personnel') " +
                "DROP DATABASE Personnel " +
            "CREATE DATABASE Personnel";
        private const string CREATE_TABLE
             = "USE Personnel " +
            "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'Personnel' AND TABLE_NAME = 'Employee')" +
              "CREATE TABLE Employee (EmployeeId INT IDENTITY(9327493,1) PRIMARY KEY, Name nvarchar(50), " +
              "Position nvarchar(50), HourlyPayRate money)";
        private const string HIGHEST_PAY_RATE
            = "select MAX(HourlyPayRate) from Employee";
        private const string LOWEST_PAY_RATE
            = "select MIN(HourlyPayRate) from Employee";
        #endregion

        #region Connection to database
        private readonly SqlConnection conn;
        public static Db GetInstance()
        {
            if (db == null)
                db = new Db();
            return db;
        }
        private Db() // private constructor
        {
            conn = new SqlConnection(CONN_STRING);
            conn.Open();
        }
        #endregion

        #region Creating Database, Table and inserting data
        public void CreateDatabase()
        {
            var cmddb = new SqlCommand(CREATE_COMMAND, conn);
            var cmdtbl = new SqlCommand(CREATE_TABLE, conn);
            var cmdins = new SqlCommand(INSERT_COMMAND, conn);
            cmddb.ExecuteNonQuery(); //sql query to create the database
            cmdtbl.ExecuteNonQuery(); //sql query to create the table 'Employee'
            cmdins.ExecuteNonQuery(); //sql query to insert data inside table 'Employee'          
        }
        #endregion

        #region Maximum and Minimum pay rate query execute
        public decimal MaximumQuery()
        {
            var cmd = new SqlCommand(HIGHEST_PAY_RATE, conn);
            return decimal.Parse(cmd.ExecuteScalar().ToString());
        }
        public decimal MinimumQuery()
        {
            var cmd = new SqlCommand(LOWEST_PAY_RATE, conn);
            return decimal.Parse(cmd.ExecuteScalar().ToString());
        }
        #endregion

        #region Read Data from Table
        public List<Employee> Read()
        {
            var employees = new List<Employee>();
            var cmd = new SqlCommand(READ_COMMAND, conn);
            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var id = rdr.GetOrdinal("EmployeeId");
                var nm = rdr.GetOrdinal("Name");
                var ps = rdr.GetOrdinal("Position");
                var hp = rdr.GetOrdinal("HourlyPayRate");
                employees.Add(new Employee
                {
                    EmployeeId = rdr.IsDBNull(id) ? 0 : rdr.GetInt32(id),
                    Name = rdr.IsDBNull(nm) ? null : rdr.GetString(nm),
                    Position = rdr.IsDBNull(ps) ? null : rdr.GetString(ps),
                    HourlyPayRate = rdr.IsDBNull(hp) ? 0 : rdr.GetDecimal(hp)
                });
            }
            rdr.Close();
            return employees;
        }
        #endregion
    }
}
