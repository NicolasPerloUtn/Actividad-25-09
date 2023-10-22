using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcialOrdenes.Dominio
{
    public class DetalleOrden
    {
        public int idDetalle { get; set; }
        public Material materialDetalle { get; set; }
        public int cantidadDetalle { get; set; }

        public DetalleOrden()
        {
            idDetalle = 0;
            materialDetalle = new Material();
            cantidadDetalle = 0;
        }

        public DetalleOrden(Material mat, int cant)
        {
            materialDetalle = mat;
            cantidadDetalle = cant;
        }

        public override string ToString()
        {
            return idDetalle + " " + materialDetalle;
        }


    }
}
