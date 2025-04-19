using AjaxControlToolkit;
using Student_Accommodation_Hub.DAL;
using Student_Accommodation_Hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Student_Accommodation_Hub.SampleServices
{
    /// <summary>
    /// Summary description for SampleServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class SampleServices : System.Web.Services.WebService
    {

        [WebMethod]
        public CascadingDropDownNameValue[] GetRoomNumbersByBlock(string knownCategoryValues, string category)
        {
            string blockNumber = knownCategoryValues.Split(':')[1]; // Extract block value
            List<RoomModel> rooms = GetRoomNumbers(blockNumber.Split(';')[0]); // Fetch room numbers and ids for the selected block
            List<CascadingDropDownNameValue> roomItems = new List<CascadingDropDownNameValue>();
            foreach (var room in rooms)
            {
                roomItems.Add(new CascadingDropDownNameValue { name = room.RoomNumber, value = room.RoomId.ToString() }
                );
            }
            return roomItems.ToArray();
        }

        private List<RoomModel> GetRoomNumbers(string block)
        {
            List<RoomModel> rooms = new List<RoomModel>();
            // Example: Fetch room numbers based on the selected block
            if (!string.IsNullOrEmpty(block) && block != "-1")
                rooms = Room.GetRoomsByBlock(block);
            // Add more blocks and room numbers as needed

            return rooms;
        }
        [WebMethod]
        public CascadingDropDownNameValue[] GetStatesByCountryId(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> stateItems = new List<CascadingDropDownNameValue>();
            string countryId = knownCategoryValues.Split(':')[1];

            List<StateModel> states = GetStates(countryId.Split(';')[0]); // Fetch room numbers and ids for the selected block

            foreach (var state in states)
            {
                stateItems.Add(new CascadingDropDownNameValue { name = state.StateName, value = state.StateID.ToString() }
                );
            }
            return stateItems.ToArray();
        }

        private List<StateModel> GetStates(string countryId)
        {
            List<StateModel> states = new List<StateModel>();
            // Example: Fetch room numbers based on the selected block
            if (!string.IsNullOrEmpty(countryId) && countryId != "-1")
                states = WebUtilty.GetStatesByCountryId(int.Parse(countryId));
            // Add more blocks and room numbers as needed

            return states;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public  string[] GetStudentNames(string prefixText, int count)
        {
            List<string> studentNames = new List<string>();
            try
            {

                if (prefixText != null)
                {
                    studentNames = Student_Accommodation_Hub.DAL.Student.GetStudentNameByPrefix(prefixText);
                }
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                System.Diagnostics.Debug.WriteLine("Error in GetStudentNames: " + ex.Message);

                // Return empty list to prevent crashes
                return new List<string> { "Error: " + ex.Message }.ToArray();
            }
            return studentNames.ToArray();
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetRommNumbers(string prefixText, int count)
        {
            List<string> RoomNumbers = new List<string>();
            try
            {

                if (prefixText != null)
                {
                    RoomNumbers = Room.GetRoomNoByPrefix(prefixText);
                }
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                System.Diagnostics.Debug.WriteLine("Error in GetStudentNames: " + ex.Message);

                // Return empty list to prevent crashes
                return new List<string> { "Error: " + ex.Message }.ToArray();
            }
            return RoomNumbers.ToArray();
        }
    }
}
