//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CasaGaillard.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleAccesorio
    {
        public int ID { get; set; }
        public Nullable<int> AccesorioID { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<int> CaracteristicaAccesorioID { get; set; }
        public Nullable<decimal> Medida { get; set; }
        public Nullable<int> UnidadID { get; set; }
        public string Tipo { get; set; }
    
        public virtual Accesorio Accesorio { get; set; }
        public virtual Unidad Unidad { get; set; }
        public virtual CaracteristicaAccesorio CaracteristicaAccesorio { get; set; }
    }
}
