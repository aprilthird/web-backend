//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FitGym.WS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Suscription
    {
        public int SuscriptionId { get; set; }
        public int SuscriptionTypeId { get; set; }
        public int GymCompanyId { get; set; }
        public int QMonths { get; set; }
        public System.DateTime StartDate { get; set; }
        public string Status { get; set; }
    
        public virtual GymCompany GymCompany { get; set; }
        public virtual SuscriptionType SuscriptionType { get; set; }
    }
}
