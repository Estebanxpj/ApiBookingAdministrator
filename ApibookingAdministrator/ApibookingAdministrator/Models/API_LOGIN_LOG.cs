//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApibookingAdministrator.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class API_LOGIN_LOG
    {
        public int API_LOGIN_LOG_ID { get; set; }
        public Nullable<int> TRAVEL_AGENT_ID { get; set; }
        public string TOKEN { get; set; }
        public Nullable<System.DateTime> DATE_BEGIN { get; set; }
        public Nullable<System.DateTime> DATE_END { get; set; }
        public Nullable<bool> STATE { get; set; }
    
        public virtual TRAVEL_AGENT TRAVEL_AGENT { get; set; }
    }
}
