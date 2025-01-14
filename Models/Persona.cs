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
    
    public partial class Persona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Persona()
        {
            this.TelefonosPersona = new HashSet<TelefonoPersona>();
        }
    
        public int ID { get; set; }
        public string NombrePersona { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string NIF { get; set; }
        public Nullable<int> DireccionID { get; set; }
    
        public virtual Direccion Direccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TelefonoPersona> TelefonosPersona { get; set; }
    }
}
