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
    
    public partial class HOTEL_TRAVEL_AGENT
    {
        public int HOTEL_TRAVEL_AGENT_ID { get; set; }
        public Nullable<int> HOTEL_ID { get; set; }
        public Nullable<int> TRAVEL_AGENT_ID { get; set; }
        public Nullable<bool> STATE { get; set; }
    
        public virtual HOTEL HOTEL { get; set; }
        public virtual TRAVEL_AGENT TRAVEL_AGENT { get; set; }
    }
}
