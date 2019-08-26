using ApibookingAdministrator.Filters;
using ApibookingAdministrator.Models;
using ApibookingAdministrator.Object;
using Newtonsoft.Json;
using System;
using System.Data.Entity.Core.Objects;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApibookingAdministrator.Controllers
{
    [BasicValidateTokenFilter]
    public class HotelController : ApiController
    {
        private Response response;

        [HttpPost]
        [ResponseType(typeof(Response))]
        public IHttpActionResult CreateHotel([FromBody] Request request)
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
                        var hotel = JsonConvert.DeserializeObject<HOTEL>(request.Data.ToString());

                        if (hotel != null && !string.IsNullOrEmpty(hotel.NAME))
                        {
                            ObjectParameter @HOTEL_ID = new ObjectParameter("HOTEL_ID", typeof(Int32));
                            model.SP_CREATE_HOTEL(hotel.LOCATION_ID, hotel.REFERENCE, hotel.NAME, hotel.CAPACITY, hotel.DESCRIPTION, request.User, @HOTEL_ID);

                            if (!string.IsNullOrEmpty(@HOTEL_ID.Value.ToString()))
                            {
                                result = Convert.ToInt32(@HOTEL_ID.Value.ToString());
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

        [HttpPost]
        [ResponseType(typeof(Response))]
        public IHttpActionResult CreateRoom([FromBody] Request request)
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
                        var room = JsonConvert.DeserializeObject<ROOM>(request.Data.ToString());

                        if (room != null && !string.IsNullOrEmpty(room.DESCRIPTION))
                        {
                            ObjectParameter @ROOM_ID = new ObjectParameter("@ROOM_ID", typeof(Int32));
                            model.SP_CREATE_ROOM(room.HOTEL_ID, room.NUMBER, room.FLOOR, room.ROOM_TYPE_ID, room.DESCRIPTION, room.PRICE, @ROOM_ID);

                            if (!string.IsNullOrEmpty(@ROOM_ID.Value.ToString()))
                            {
                                result = Convert.ToInt32(@ROOM_ID.Value.ToString());
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

        [HttpPost]
        [ResponseType(typeof(Response))]
        public IHttpActionResult CreateTax([FromBody] Request request)
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
                        var room_tax = JsonConvert.DeserializeObject<ROOM_TAX>(request.Data.ToString());

                        if (room_tax != null && room_tax.TAX != null)
                        {
                            ObjectParameter @TAX_ID = new ObjectParameter("@TAX_ID", typeof(Int32));
                            model.SP_CREATE_TAX(room_tax.ROOM_ID, room_tax.TAX.NAME, room_tax.TAX.DESCRIPTION, room_tax.TAX.PERCENTAGE, room_tax.TAX.VALUE, @TAX_ID);

                            if (!string.IsNullOrEmpty(@TAX_ID.Value.ToString()))
                            {
                                result = Convert.ToInt32(@TAX_ID.Value.ToString());
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

        [HttpPost]
        [ResponseType(typeof(Response))]
        public IHttpActionResult UpdateHotel([FromBody] Request request)
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
                        var hotel = JsonConvert.DeserializeObject<HOTEL>(request.Data.ToString());

                        if (hotel != null && !string.IsNullOrEmpty(hotel.NAME))
                        {
                            result = model.SP_UPDATE_HOTEL(hotel.HOTEL_ID, hotel.LOCATION_ID, hotel.REFERENCE, hotel.NAME, hotel.CAPACITY, hotel.DESCRIPTION, hotel.STATE);
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

        [HttpPost]
        [ResponseType(typeof(Response))]
        public IHttpActionResult UpdateRoom([FromBody] Request request)
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
                        var room = JsonConvert.DeserializeObject<ROOM>(request.Data.ToString());

                        if (room != null && !string.IsNullOrEmpty(room.DESCRIPTION))
                        {
                            result = model.SP_UPDATE_ROOM(room.ROOM_ID, room.NUMBER, room.FLOOR, room.ROOM_TYPE_ID, room.PRICE, room.DESCRIPTION, room.ROOM_STATE_ID);
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

        [HttpPost]
        [ResponseType(typeof(Response))]
        public IHttpActionResult UpdateTax([FromBody] Request request)
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
                        var room_tax = JsonConvert.DeserializeObject<ROOM_TAX>(request.Data.ToString());

                        if (room_tax != null && room_tax.TAX != null)
                        {
                            model.SP_UPDATE_TAX(room_tax.TAX.TAX_ID, room_tax.TAX.NAME, room_tax.TAX.DESCRIPTION, room_tax.TAX.PERCENTAGE, room_tax.TAX.VALUE);
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
