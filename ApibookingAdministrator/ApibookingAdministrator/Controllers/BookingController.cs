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
                if (request.Session > 0 && request.User > 0)
                {
                    var result = DbConection.GetListHoteles();
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
                        result =  DbConection.InsertBooking(JsonConvert.DeserializeObject<RIDER>(request.Data.ToString()));
                        
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
