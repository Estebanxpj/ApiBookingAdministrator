using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApibookingAdministrator.Object
{
    public class ResponseAuthentication
    {
        /// <summary>
        /// 100--> no existe el objeto
        /// 200--> respuesta Correcta
        /// 300 --> Exception
        /// </summary>
        public int CodeError { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// Token utilizado por todo el login
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Id de la sesion en el api
        /// </summary>
        public object Session { get; set; }

        /// <summary>
        /// Objeto usuario
        /// </summary>
        public object User { get; set; }
    }
}