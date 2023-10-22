using PracticaParcialOrdenes.Dominio;
using PracticaParcialOrdenes.Service;
using PracticaParcialOrdenes.Service.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaParcialOrdenes
{
    public partial class Form1 : Form
    {
        IServicio servicioDatos;
        Orden oOrden;
        public Form1(FabricaServicioImp fabrica)
        {
            InitializeComponent();
            servicioDatos = fabrica.CrearServicio();
            oOrden = new Orden();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboMateriales.DataSource = servicioDatos.TraerMateriales();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                MessageBox.Show("Debe ingresar una cantidad", "Error", MessageBoxButtons.OK);
                return;
            }
            foreach (DataGridViewRow r in dgvDetalles.Rows)
            {
                Material mat = (Material)cboMateriales.SelectedItem;
                if (r.Cells["ColumnaMaterial"].Value.ToString() == mat.nombreMaterial)
                {
                    MessageBox.Show("Ese Material ya esta utilizado!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            Material item = (Material)cboMateriales.SelectedItem;

            int cant = int.Parse(txtCantidad.Text);

            DetalleOrden detalle = new DetalleOrden(item,cant);
            oOrden.AgregarDetalle(detalle);
            dgvDetalles.Rows.Add(new object[] {detalle.idDetalle, detalle.materialDetalle.nombreMaterial, detalle.materialDetalle.stockMaterial, detalle.cantidadDetalle, "Quitar" });

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dtpFecha.Value > DateTime.Now)
            {
                MessageBox.Show("Debe ingresar una fecha valida");
                return;
            }

            if (string.IsNullOrEmpty(txtResponsable.Text)) 
            {
                MessageBox.Show("Debe ingresar un responsable");
                return;
            }

            oOrden.fechaOrden = dtpFecha.Value;
            oOrden.responsableOrden = txtResponsable.Text;

            if (servicioDatos.ConfirmarOrden(oOrden))
            {
                MessageBox.Show("Se ingreso la orden");
                this.Dispose();
            }
            else
            {
                MessageBox.Show("No se ingreso la orden");
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro que desea cancelar?","Cancelar",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return;
            }
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvDetalles.CurrentCell.ColumnIndex == 4) 
            {
                oOrden.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentRow.Index);
            }
        }
    }
}
