using PracticaParcialOrdenes.Service.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcialOrdenes.Service
{
    public abstract class FabricaServicio
    {
        public abstract IServicio CrearServicio();
    }
}
