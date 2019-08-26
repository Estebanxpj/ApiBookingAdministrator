using ApibookingAdministrator.Classes;
using ApibookingAdministrator.Filters;
using ApibookingAdministrator.Models;
using ApibookingAdministrator.Object;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApibookingAdministrator.Controllers
{
    [BasicValidateTokenFilter]
    public class TravelAgentController : ApiController
    {
        private Response response;

        [HttpPost]
        [ResponseType(typeof(Response))]
        public IHttpActionResult GetBookings([FromBody] Request request)
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
                        var result = model.SP_GET_BOOKINGS(request.User);
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
    }
}