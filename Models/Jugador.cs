//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Jugador
    {
        //public Jugador()
        //{
        //    foto_jugador = "../../ImgJugador/default.png";
        //}
        public System.Guid JugadorId { get; set; }
        public string cedula { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public System.Guid EquipoId { get; set; }
        public int Id { get; set; }
        public Nullable<int> carnet { get; set; }
        public string profesion { get; set; }
        public string intruccion { get; set; }
        public string estadocivil { get; set; }
        public string foto_jugador { get; set; }
        //[NotMapped]
        //public HttpPostedFileBase ImageUpload { get; set; }
        public Nullable<System.DateTime> nacimiento { get; set; }
        public Nullable<System.DateTime> afiliacion { get; set; }
    
        public virtual Equipo Equipo { get; set; }
    }
}
