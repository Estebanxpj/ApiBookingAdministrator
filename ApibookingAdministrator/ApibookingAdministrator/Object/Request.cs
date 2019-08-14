using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApibookingAdministrator.Object
{
    public class Request
    {
        public int Session { get; set; }

        public int User { get; set; }

        public object Data { get; set; }
    }
}