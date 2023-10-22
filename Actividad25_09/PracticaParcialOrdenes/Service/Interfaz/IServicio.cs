using PracticaParcialOrdenes.Datos.Interfaz;
using PracticaParcialOrdenes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcialOrdenes.Service.Interfaz
{
    public interface IServicio
    {
        List<Material> TraerMateriales();

        bool ConfirmarOrden(Orden oOrden);

        int ObtenerProximaOrden();

    }
}
