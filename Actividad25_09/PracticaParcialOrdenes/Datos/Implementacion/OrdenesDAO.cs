using PracticaParcialOrdenes.Datos.Interfaz;
using PracticaParcialOrdenes.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParcialOrdenes.Datos.Implementacion
{
    public class OrdenesDAO : IOrdenesDAO
    {
        public List<Material> TraerMateriales()
        {
            DataTable dt = HelperDB.ObtenerInstancia().Consultar("SP_CONSULTAR_MATERIALES");
            List<Material> lMaterial = new List<Material>();

            foreach (DataRow r in dt.Rows)
            {
                int nro = int.Parse(r["id_material"].ToString());
                string nom = r["nom_material"].ToString();
                int stock = int.Parse(r["stock_material"].ToString());
                lMaterial.Add(new Material (nro, nom, stock));
            }
            return lMaterial;
        }


        public bool ConfirmarOrden(Orden oOrden)
        {
            SqlConnection conexion = HelperDB.ObtenerInstancia().ObtenerConexion();
            SqlTransaction transaction = null;
            bool ok = true;

            try
            {
                conexion.Open();
                transaction = conexion.BeginTransaction();
                SqlCommand cmdMaestro = new SqlCommand("SP_INSERTAR_ORDEN",conexion,transaction);
                cmdMaestro.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@prox_orden",SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmdMaestro.Parameters.Add(param);
                cmdMaestro.Parameters.AddWithValue("@fecha",oOrden.fechaOrden);
                cmdMaestro.Parameters.AddWithValue("@responsable", oOrden.responsableOrden);
                cmdMaestro.ExecuteNonQuery();

                int nroOrden = (int)param.Value;
                int count = 1;

                foreach (DetalleOrden detalle in oOrden.listaDetalles)
                {
                    SqlCommand cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE",conexion,transaction);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@id_orden", nroOrden);
                    cmdDetalle.Parameters.AddWithValue("@id_detalle", count);
                    cmdDetalle.Parameters.AddWithValue("@material", detalle.materialDetalle.codigoMaterial);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", detalle.cantidadDetalle);
                    cmdDetalle.ExecuteNonQuery();
                    count++;
                }
                transaction.Commit();
            }
            catch
            {
                if (conexion != null)
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open) 
                {
                    conexion.Close();
                }
            }
            return ok;
        }

        public int ObtenerProximaOrden()
        {
            return HelperDB.ObtenerInstancia().ProximaOrden("SP_PROXIMA_ORDEN", "@next");
        }


    }
}
