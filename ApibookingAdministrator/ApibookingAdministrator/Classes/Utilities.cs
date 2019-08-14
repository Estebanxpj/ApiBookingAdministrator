using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApibookingAdministrator.Classes
{
    public class Utilities
    {

        public static string GenerateToken()
        {
            try
            {
                return Guid.NewGuid().ToString();
            }
            catch (Exception ex)
            {

                return "";
            }
        }

        public static DateTime SumDate()
        {
            try
            {
                return DateTime.Now.AddHours(1);

            }
            catch (Exception ex)
            {
                return DateTime.Today;
            }
        }

        internal static string GenerateCodeBooking()
        {
            try
            {
                return Guid.NewGuid().ToString().Substring(0, 4);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}