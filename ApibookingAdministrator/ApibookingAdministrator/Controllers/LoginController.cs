using ApibookingAdministrator.Classes;
using ApibookingAdministrator.Models;
using ApibookingAdministrator.Object;
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
    public class LoginController : ApiController
    {
        [HttpPost]
        [ResponseType(typeof(ResponseAuthentication))]
        public IHttpActionResult Authenticate([FromBody] RequestAuthentication request)
        {
            var response = new ResponseAuthentication();

            string token = "";

            int idSession = 0;

            int idUser = 0;

            try
            {
                if (request != null && !string.IsNullOrEmpty(request.Email) && !string.IsNullOrEmpty(request.Password))
                {
                    using (var model = new HOSTING_MANAGEREntities())
                    {
                        model.Configuration.ProxyCreationEnabled = false;

                        var user = model.SP_VALIDATE_TRAVEL_AGENT(request.Email, request.Password).FirstOrDefault();

                        if (user != null)
                        {
                            var login = model.SP_VALIDATE_LOGIN_API(user.TRAVEL_AGENT_ID).FirstOrDefault();

                            if (login != null)
                            {
                                token = login.TOKEN;
                                idSession = login.API_LOGIN_LOG_ID;
                                idUser = user.TRAVEL_AGENT_ID;
                            }
                            else
                            {
                                token = Utilities.GenerateToken();
                                if (!string.IsNullOrEmpty(token))
                                {
                                    ObjectParameter @sessionId = new ObjectParameter("LOGINID", typeof(Int32));

                                    model.SP_CREATE_LOGIN_API(user.TRAVEL_AGENT_ID, Utilities.SumDate(), token, @sessionId);

                                    if (@sessionId != null && @sessionId.Value != null)
                                    {
                                        idSession = Convert.ToInt32(@sessionId.Value.ToString());
                                        idUser = user.TRAVEL_AGENT_ID;
                                    }
                                }
                            }
                        }
                    }

                    if (idSession > 0)
                    {
                        response.CodeError = 200;
                        response.Message = "OK";
                        response.Token = token;
                        response.Session = idSession;
                        response.User = idUser;
                    }
                    else
                    {
                        response.CodeError = 100;
                        response.Message = "El usuario no existe o se encuentra desactivado";
                    }
                }
                else
                {
                    response.CodeError = 400;
                    response.Message = "Informacion incorrecta";
                }
            }
            catch (Exception ex)
            {
                response.CodeError = 300;
                response.Message = "Unexpected Error" + ex.ToString();
            }
            return Ok(response);
        }

    }
}
