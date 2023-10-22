﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcialOrdenes.Dominio
{
    public class Orden
    {
        public int nroOrden { get; set; }
        public DateTime fechaOrden { get; set; }
        public string responsableOrden { get; set; }
        public List<DetalleOrden> listaDetalles { get; set; }

        public Orden()
        {
            nroOrden = 0;
            fechaOrden = DateTime.Now;
            responsableOrden = string.Empty;
            listaDetalles = new List<DetalleOrden>();
        }

        public Orden(int nroOrden, DateTime fechaOrden, string responsableOrden)
        {
            this.nroOrden = nroOrden;
            this.fechaOrden = fechaOrden;
            this.responsableOrden = responsableOrden;
            this.listaDetalles = new List<DetalleOrden>();
        }

        public void AgregarDetalle(DetalleOrden det)
        {
            listaDetalles.Add(det);
        }

        public void QuitarDetalle(int nroDet)
        {
            listaDetalles.RemoveAt(nroDet);
        }

    }
}
