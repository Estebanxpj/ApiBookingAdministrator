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
    
    public partial class RIDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RIDER()
        {
            this.BOOKING = new HashSet<BOOKING>();
        }
    
        public int RIDER_ID { get; set; }
        public Nullable<int> DOCUMENT_TYPE_ID { get; set; }
        public string NUMBER_DOCUMENT { get; set; }
        public string NAME { get; set; }
        public Nullable<System.DateTime> BIRTHDAY_DATE { get; set; }
        public string GENDER { get; set; }
        public string EMAIL { get; set; }
        public Nullable<decimal> PHONE { get; set; }
        public string EMERGENCY_NAME { get; set; }
        public Nullable<decimal> EMERGENCY_PHONE { get; set; }
        public Nullable<System.DateTime> CRETE_DATE { get; set; }
        public Nullable<bool> STATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKING> BOOKING { get; set; }
        public virtual DOCUMENT_TYPE DOCUMENT_TYPE { get; set; }
    }
}
