using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Student_Accommodation_Hub.DAL.SQL_Helper
{
    public class SqlHelper
    {
        private readonly string _connectionString;
        private readonly List<SqlParameter> _parameters;

        public SqlHelper()
        {
            // Fetch the connection string from Web.config or App.config
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionStringName"].ConnectionString;
            _parameters = new List<SqlParameter>();
        }

        #region ExecuteStoredProcedure
        // Executes a stored procedure and returns a SqlDataReader
        // Accepts stored procedure name and output parameter name
        public SqlDataReader ExecuteStoredProcedure(string procedureName,string parameter, out SqlCommand command)
        {
            SqlDataReader reader = null;
            command=null;  // Default result value in case of failure
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);
                
                    SqlCommand cmd = new SqlCommand(procedureName, conn);
                    
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters to command
                        AddParametersToCommand(cmd);
                        
                        // Add output parameter
                        SqlParameter outputParam = new SqlParameter(parameter, SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        conn.Open();
                        reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        command = cmd;
                // ✅ Move the output parameter retrieval **after** the reader is read in GetStudents()
                //result = Convert.ToInt32(outputParam.Value);


            }
            
            catch (Exception)
            {
                throw;
            }
            return reader;
        }
        #endregion

        #region ExecuteQuery
        // Executes a general query (e.g., SELECT) and returns a SqlDataReader
        public SqlDataReader ExecuteQuery(string query)
        {
            SqlDataReader reader = null;
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);

                SqlCommand cmd = new SqlCommand(query, conn);
                    
                        cmd.CommandType = CommandType.Text;
                        AddParametersToCommand(cmd); // Add parameters to command
                        conn.Open();
                        reader = cmd.ExecuteReader();
                _parameters.Clear();
                       
                    
                
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
            }
            return reader;
        }
        #endregion

        #region ExecuteNonQuery
        // Executes an SQL command (INSERT, UPDATE, DELETE) and returns the number of rows affected
        public int ExecuteNonQuery(string query)
        {
            int affectedRows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        AddParametersToCommand(cmd); // Add parameters to command
                        conn.Open();
                        affectedRows = cmd.ExecuteNonQuery();
                    }
                }
            }
            
            catch (Exception)
            {
              throw;
            }
            return affectedRows;
        }
        #endregion

        #region ExecuteScalar
        // Executes a scalar query (e.g., COUNT, MAX) and returns a single value
        public object ExecuteScalar(string query)
        {
            object result = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        AddParametersToCommand(cmd); // Add parameters to command
                        conn.Open();
                        result = cmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                HandleSqlException(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
            }
            return result;
        }
        #endregion

        #region AddParameter
        // Adds a parameter to the list of parameters for the SQL command
        public void AddParameter(string parameterName, SqlDbType sqlDbType, object value)
        {
            if (value == null)
            {
                value = DBNull.Value; // ✅ Replace null with DBNull.Value
            }

            SqlParameter param = new SqlParameter(parameterName, sqlDbType)
            {
                Value = value
            };
            _parameters.Add(param);
        }
        #endregion

        #region AddParametersToCommand
        // Adds all parameters stored in the class to the SQL command
        private void AddParametersToCommand(SqlCommand cmd)
        {
            foreach (var parameter in _parameters)
            {
                cmd.Parameters.Add(parameter);
            }
        }
        #endregion

        #region HandleSqlException
        // Handles SQL exceptions and logs them
        private void HandleSqlException(SqlException ex)
        {
            foreach (SqlError error in ex.Errors)
            {
                Console.WriteLine($"SQL Error: {error.Message}");
                // Log the SQL errors to a file or use a logging framework (e.g., NLog, log4net)
            }
        }
        #endregion
    }
}
