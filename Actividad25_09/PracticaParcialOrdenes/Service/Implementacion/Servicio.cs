using PracticaParcialOrdenes.Datos.Implementacion;
using PracticaParcialOrdenes.Datos.Interfaz;
using PracticaParcialOrdenes.Dominio;
using PracticaParcialOrdenes.Service.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcialOrdenes.Service.Implementacion
{
    public class Servicio : IServicio
    {
        IOrdenesDAO ordenesDAO;

        public List<Material> TraerMateriales()
        {
            ordenesDAO = new OrdenesDAO();
            return ordenesDAO.TraerMateriales();
        }
        public bool ConfirmarOrden(Orden oOrden)
        {
            ordenesDAO = new OrdenesDAO();
            return ordenesDAO.ConfirmarOrden(oOrden);
        }

        public int ObtenerProximaOrden()
        {
            ordenesDAO = new OrdenesDAO();
            return ordenesDAO.ObtenerProximaOrden();
        }


    }
}
