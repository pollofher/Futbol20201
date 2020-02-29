using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class SubirArchivo
    {
        public void SubirImagen(string ruta, HttpPostedFileBase file)
        {
            try
            {
                file.SaveAs(ruta);
            }
            catch (Exception)
            {

            }
        }
    }
}