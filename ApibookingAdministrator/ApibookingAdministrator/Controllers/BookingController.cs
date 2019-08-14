using ApibookingAdministrator.Classes;
using ApibookingAdministrator.Models;
using ApibookingAdministrator.Object;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApibookingAdministrator.Controllers
{
    public class BookingController : ApiController
    {
        private Response response;

        [HttpPost]
        [ResponseType(typeof(Response))]
        public IHttpActionResult GetAvailableHotels([FromBody] Request request)
        {
            response = new Response();
            response.Data = null;

            try
            {
                using (var model = new HOSTING_MANAGEREntities())
                {
                    model.Configuration.ProxyCreationEnabled = false;
                    if (request.Session > 0 && request.User > 0)
                    {
                        var result = model.SP_GET_HOTELES();
                        if (result != null)
                        {
                            response.Data = result;
                        }
                    }
                    if (response.Data != null)
                    {
                        response.CodeError = 200;
                        response.Message = "OK";
                    }
                    else
                    {
                        response.CodeError = 100;
                        response.Message = "No se logro insertar el log";
                        response.Data = false;
                    }
                }
            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = "Unexpected Error" + ex.ToString();
                response.Data = false;
            }
            return Ok(response);
        }

        [HttpPost]
        [ResponseType(typeof(Response))]
        public IHttpActionResult CreateBooking([FromBody] Request request)
        {
            response = new Response();

            int result = 0;

            try
            {
                using (var model = new HOSTING_MANAGEREntities())
                {
                    model.Configuration.ProxyCreationEnabled = false;
                    if (request.Session > 0 && request.User > 0 && request.Data != null)
                    {
                        var rider = JsonConvert.DeserializeObject<RIDER>(request.Data.ToString());

                        if (rider != null && !string.IsNullOrEmpty(rider.NUMBER_DOCUMENT))
                        {
                            ObjectParameter @RIDER_ID = new ObjectParameter("RIDER_ID", typeof(Int32));
                            model.SP_CREATE_RIDER(rider.DOCUMENT_TYPE_ID, rider.NUMBER_DOCUMENT, rider.NAME, 
                                rider.BIRTHDAY_DATE, rider.GENDER, rider.EMAIL, rider.PHONE, rider.EMERGENCY_NAME, 
                                rider.EMERGENCY_PHONE, @RIDER_ID);

                            if (!string.IsNullOrEmpty(@RIDER_ID.Value.ToString()))
                            {
                                result = Convert.ToInt32(@RIDER_ID.Value.ToString());

                                if (rider.BOOKING !=null)
                                {
                                    var booking = rider.BOOKING.FirstOrDefault();
                                    ObjectParameter @BOOKING_ID = new ObjectParameter("BOOKING_ID", typeof(Int32));
                                    string codeBooking = Utilities.GenerateCodeBooking();
                                    model.SP_CREATE_BOOKING(booking.ROOM_ID, result, codeBooking, booking.CHEK_IN, booking.CHEK_OUNT, booking.NUMBER_PEOPLE, booking.DESCRIPTION,booking.STATE_BOOKING_ID, @BOOKING_ID);

                                    if (!string.IsNullOrEmpty(@BOOKING_ID.Value.ToString()))
                                    {
                                        result = Convert.ToInt32(@BOOKING_ID.Value.ToString());
                                    }
                                }
                            }
                        }
                    }
                    if (result == 0)
                    {
                        response.CodeError = 200;
                        response.Message = "OK";
                        response.Data = result;
                    }
                    else
                    {
                        response.CodeError = 100;
                        response.Message = "No se logro insertar el log";
                        response.Data = false;
                    }
                }
            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = "Unexpected Error" + ex.ToString();
                response.Data = false;
            }
            return Ok(response);
        }
    }
}
