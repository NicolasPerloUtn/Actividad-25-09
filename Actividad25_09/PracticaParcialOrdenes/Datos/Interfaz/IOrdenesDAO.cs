using PracticaParcialOrdenes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcialOrdenes.Datos.Interfaz
{
    public interface IOrdenesDAO
    {
        List<Material> TraerMateriales();

        bool ConfirmarOrden(Orden oOrden);

        int ObtenerProximaOrden();
    }
}
