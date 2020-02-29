using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class SubirImagen
    {
        public string confirmmacion { get; set; }
        public Exception error { get; set; }
        public void Subirarchivo (string ruta, HttpPostedFileBase file)
        {
            try
            {
                file.SaveAs(ruta);
                this.confirmmacion = "Imagen guardada";

            }
            catch (Exception ex)
            {

                this.error = ex;
            }
        }
    }
}