using ApibookingAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApibookingAdministrator.Classes
{
    public class Repository
    {
        public bool ValidateToken(string token)
        {
            try
            {
                using (var model = new HOSTING_MANAGEREntities())
                {
                    var result = model.SP_VALIDATE_TOKEN(token).FirstOrDefault();
                    if (result.Value > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}