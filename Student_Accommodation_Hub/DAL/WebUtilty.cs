using Student_Accommodation_Hub.DAL.SQL_Helper;
using Student_Accommodation_Hub.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.DAL
{
    public class WebUtilty
    {
        public static List<StateModel> GetStatesByCountryId(int countryId)
        {
            List<StateModel> states = new List<StateModel>();

            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                string query = "SELECT StateID, StateName FROM States WHERE CountryID = @CountryID";

                sqlHelper.AddParameter("@CountryID", SqlDbType.Int, countryId);

                using (SqlDataReader reader = sqlHelper.ExecuteQuery(query))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            states.Add(new StateModel
                            {
                                StateID = Convert.ToInt32(reader["StateID"]),
                                StateName = reader["StateName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return states;
        }
        public static List<CountryModel> GetAllCountries()
        {
            List<CountryModel> countries = new List<CountryModel>();

            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                string query = "SELECT CountryID, CountryName FROM Countries";

                using (SqlDataReader reader = sqlHelper.ExecuteQuery(query))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            countries.Add(new CountryModel
                            {
                                CountryID = Convert.ToInt32(reader["CountryID"]),
                                CountryName = reader["CountryName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return countries;
        }
    }
}