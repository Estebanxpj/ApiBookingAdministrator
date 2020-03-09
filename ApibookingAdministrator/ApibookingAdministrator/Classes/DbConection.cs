using ApibookingAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace ApibookingAdministrator.Classes
{
    public static class DbConection
    {
        public static ObjectResult<SP_GET_HOTELES_Result> GetListHoteles()
        {
            try
            {

                using (var model = new HOSTING_MANAGEREntities())
                {
                    model.Configuration.ProxyCreationEnabled = false;
                    var result = model.SP_GET_HOTELES();
                    if (result != null && result.Count() > 0)
                    {
                        return result;
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public static int InsertBooking(RIDER rider)
        {
            try
            {
                using (var model = new HOSTING_MANAGEREntities())
                {
                    model.Configuration.ProxyCreationEnabled = false;
                    if (rider != null && !string.IsNullOrEmpty(rider.NUMBER_DOCUMENT))
                    {
                        ObjectParameter @RIDER_ID = new ObjectParameter("RIDER_ID", typeof(Int32));
                        model.SP_CREATE_RIDER(rider.DOCUMENT_TYPE_ID, rider.NUMBER_DOCUMENT, rider.NAME,
                            rider.BIRTHDAY_DATE, rider.GENDER, rider.EMAIL, rider.PHONE, rider.EMERGENCY_NAME,
                            rider.EMERGENCY_PHONE, @RIDER_ID);

                        if (!string.IsNullOrEmpty(@RIDER_ID.Value.ToString()))
                        {
                            var result = Convert.ToInt32(@RIDER_ID.Value.ToString());

                            if (rider.BOOKING != null)
                            {
                                var booking = rider.BOOKING.FirstOrDefault();
                                ObjectParameter @BOOKING_ID = new ObjectParameter("BOOKING_ID", typeof(Int32));
                                string codeBooking = Utilities.GenerateCodeBooking();
                                model.SP_CREATE_BOOKING(booking.ROOM_ID, result, codeBooking, booking.CHEK_IN, booking.CHEK_OUNT, booking.NUMBER_PEOPLE, booking.DESCRIPTION, booking.STATE_BOOKING_ID, @BOOKING_ID);

                                if (!string.IsNullOrEmpty(@BOOKING_ID.Value.ToString()))
                                {
                                    return Convert.ToInt32(@BOOKING_ID.Value.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return 0;
        }
    }
}