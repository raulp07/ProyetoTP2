using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using proyectoprueba.Models;
using System.Data.Common;
using System.Configuration;


namespace proyectoprueba.Controllers
{
    public class sugerirEstrategiaValidaController : Controller
    {
        //
        // GET: /sugerirEstrategiaValida/

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult ListarPlanMKT()
        {
            return Json(GetAllPlanMarketing(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarObjetivos(PlanMarketing PMKT)
        {
            return Json(GetAllObjetivos(PMKT), JsonRequestBehavior.AllowGet);
        }

        #region "DAO Estrategia"

        private static string Config = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        private static SqlConnection cn = new SqlConnection(Config);
        public List<Estrategia> GetAllEstrategia()
        {


            SqlCommand cmd = new SqlCommand("spGetEstrategiaAll", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            List<Estrategia> list = new List<Estrategia>();

            using (IDataReader dataReader = cmd.ExecuteReader())
            {

                while (dataReader.Read())
                {

                    Estrategia obj = new Estrategia();

                    if (dataReader["Id_Estrategia"] != DBNull.Value) { obj.Id_Estrategia = (int)dataReader["Id_Estrategia"]; }
                    if (dataReader["NombreEstrategia"] != DBNull.Value) { obj.NombreEstrategia = (string)dataReader["NombreEstrategia"]; }
                    if (dataReader["DescripcionEstrategia"] != DBNull.Value) { obj.DescripcionEstrategia = (string)dataReader["DescripcionEstrategia"]; }
                    if (dataReader["EstadoEstrategia"] != DBNull.Value) { obj.EstadoEstrategia = (int)dataReader["EstadoEstrategia"]; }
                    if (dataReader["Fechacumplimiento"] != DBNull.Value) { obj.Fechacumplimiento = (DateTime)dataReader["Fechacumplimiento"]; }
                    if (dataReader["UsuarioRegistra"] != DBNull.Value) { obj.UsuarioRegistra = (string)dataReader["UsuarioRegistra"]; }
                    if (dataReader["MaquinaRegistra"] != DBNull.Value) { obj.MaquinaRegistra = (string)dataReader["MaquinaRegistra"]; }
                    if (dataReader["FechaRegistro"] != DBNull.Value) { obj.FechaRegistro = (DateTime)dataReader["FechaRegistro"]; }
                    if (dataReader["UsuarioModifica"] != DBNull.Value) { obj.UsuarioModifica = (string)dataReader["UsuarioModifica"]; }
                    if (dataReader["MaquinaModifica"] != DBNull.Value) { obj.MaquinaModifica = (string)dataReader["MaquinaModifica"]; }
                    if (dataReader["FechaModifica"] != DBNull.Value) { obj.FechaModifica = (DateTime)dataReader["FechaModifica"]; }

                    list.Add(obj);

                }

            }

            return list;

        }

        public bool InsertEstrategia(Estrategia BEEstrategia)
        {
            SqlCommand cmd;
            using (cmd = new SqlCommand("spInsertEstrategia", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("Id_Estrategia", SqlDbType.Int).Value = BEEstrategia.Id_Estrategia;
                cmd.Parameters.Add("NombreEstrategia", SqlDbType.VarChar).Value = BEEstrategia.NombreEstrategia;
                cmd.Parameters.Add("DescripcionEstrategia", SqlDbType.VarChar).Value = BEEstrategia.DescripcionEstrategia;
                cmd.Parameters.Add("EstadoEstrategia", SqlDbType.Int).Value = BEEstrategia.EstadoEstrategia;
                cmd.Parameters.Add("Fechacumplimiento", SqlDbType.DateTime).Value = BEEstrategia.Fechacumplimiento;
                cmd.Parameters.Add("UsuarioRegistra", SqlDbType.VarChar).Value = BEEstrategia.UsuarioRegistra;
                cmd.Parameters.Add("MaquinaRegistra", SqlDbType.VarChar).Value = BEEstrategia.MaquinaRegistra;
                cmd.Parameters.Add("FechaRegistro", SqlDbType.DateTime).Value = BEEstrategia.FechaRegistro;
                cmd.Parameters.Add("UsuarioModifica", SqlDbType.VarChar).Value = BEEstrategia.UsuarioModifica;
                cmd.Parameters.Add("MaquinaModifica", SqlDbType.VarChar).Value = BEEstrategia.MaquinaModifica;
                cmd.Parameters.Add("FechaModifica", SqlDbType.DateTime).Value = BEEstrategia.FechaModifica;
            }
            return (cmd.ExecuteNonQuery() == 1);
        }



        public bool UpdateEstrategia(Estrategia BEEstrategia)
        {

            SqlCommand cmd;
            using (cmd = new SqlCommand("spUpdateEstrategia", cn))
            {
                cmd.Parameters.Add("Id_Estrategia", SqlDbType.Int).Value = BEEstrategia.Id_Estrategia;
                cmd.Parameters.Add("NombreEstrategia", SqlDbType.VarChar).Value = BEEstrategia.NombreEstrategia;
                cmd.Parameters.Add("DescripcionEstrategia", SqlDbType.VarChar).Value = BEEstrategia.DescripcionEstrategia;
                cmd.Parameters.Add("EstadoEstrategia", SqlDbType.Int).Value = BEEstrategia.EstadoEstrategia;
                cmd.Parameters.Add("Fechacumplimiento", SqlDbType.DateTime).Value = BEEstrategia.Fechacumplimiento;
                cmd.Parameters.Add("UsuarioRegistra", SqlDbType.VarChar).Value = BEEstrategia.UsuarioRegistra;
                cmd.Parameters.Add("MaquinaRegistra", SqlDbType.VarChar).Value = BEEstrategia.MaquinaRegistra;
                cmd.Parameters.Add("FechaRegistro", SqlDbType.DateTime).Value = BEEstrategia.FechaRegistro;
                cmd.Parameters.Add("UsuarioModifica", SqlDbType.VarChar).Value = BEEstrategia.UsuarioModifica;
                cmd.Parameters.Add("MaquinaModifica", SqlDbType.VarChar).Value = BEEstrategia.MaquinaModifica;
                cmd.Parameters.Add("FechaModifica", SqlDbType.DateTime).Value = BEEstrategia.FechaModifica;
            }
            return (cmd.ExecuteNonQuery() == 1);
        }


        #endregion

        #region "DAO Plan de Marketing"
        public List<PlanMarketing> GetAllPlanMarketing()
        {
            SqlCommand cmd = new SqlCommand("spGetPlanMarketingAll", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            List<PlanMarketing> list = new List<PlanMarketing>();
            cn.Open();
            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    PlanMarketing obj = new PlanMarketing();
                    if (dataReader["Id_PlanMarketing"] != DBNull.Value) { obj.Id_PlanMarketing = (int)dataReader["Id_PlanMarketing"]; }
                    if (dataReader["nombrePanMarketing"] != DBNull.Value) { obj.nombrePanMarketing = (string)dataReader["nombrePanMarketing"]; }
                    if (dataReader["descrípcion"] != DBNull.Value) { obj.descrípcion = (string)dataReader["descrípcion"]; }
                    if (dataReader["fechaIni"] != DBNull.Value) { obj.fechaIni = (DateTime)dataReader["fechaIni"]; }
                    if (dataReader["fechaFin"] != DBNull.Value) { obj.fechaFin = (DateTime)dataReader["fechaFin"]; }
                    if (dataReader["presupuesto"] != DBNull.Value) { obj.presupuesto = (decimal)dataReader["presupuesto"]; }
                    if (dataReader["UsuarioRegistra"] != DBNull.Value) { obj.UsuarioRegistra = (string)dataReader["UsuarioRegistra"]; }
                    if (dataReader["MaquinaRegistra"] != DBNull.Value) { obj.MaquinaRegistra = (string)dataReader["MaquinaRegistra"]; }
                    if (dataReader["FechaRegistro"] != DBNull.Value) { obj.FechaRegistro = (DateTime)dataReader["FechaRegistro"]; }
                    if (dataReader["UsuarioModifica"] != DBNull.Value) { obj.UsuarioModifica = (string)dataReader["UsuarioModifica"]; }
                    if (dataReader["MaquinaModifica"] != DBNull.Value) { obj.MaquinaModifica = (string)dataReader["MaquinaModifica"]; }
                    if (dataReader["FechaModifica"] != DBNull.Value) { obj.FechaModifica = (DateTime)dataReader["FechaModifica"]; }
                    list.Add(obj);
                }
            }
            cn.Close();
            return list;
        }

        #endregion

        #region "DAO Objetivos"
        public List<Objetivos> GetAllObjetivos(PlanMarketing PMKT)
        {
            SqlCommand cmd = new SqlCommand("spGetObjetivosAll", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_PlanMarketing", SqlDbType.Int).Value = PMKT.Id_PlanMarketing;

            List<Objetivos> list = new List<Objetivos>();
            cn.Open();
            using (IDataReader dataReader = cmd.ExecuteReader())
            {

                while (dataReader.Read())
                {

                    Objetivos obj = new Objetivos();

                    if (dataReader["Id_Objetivo"] != DBNull.Value) { obj.Id_Objetivo = (int)dataReader["Id_Objetivo"]; }
                    if (dataReader["Id_PlanMarketing"] != DBNull.Value) { obj.Id_PlanMarketing = (int)dataReader["Id_PlanMarketing"]; }
                    if (dataReader["NombreObjetivo"] != DBNull.Value) { obj.NombreObjetivo = (string)dataReader["NombreObjetivo"]; }
                    if (dataReader["DescripcionObjetivo"] != DBNull.Value) { obj.DescripcionObjetivo = (string)dataReader["DescripcionObjetivo"]; }
                    if (dataReader["UsuarioRegistra"] != DBNull.Value) { obj.UsuarioRegistra = (string)dataReader["UsuarioRegistra"]; }
                    if (dataReader["MaquinaRegistra"] != DBNull.Value) { obj.MaquinaRegistra = (string)dataReader["MaquinaRegistra"]; }
                    if (dataReader["FechaRegistro"] != DBNull.Value) { obj.FechaRegistro = (DateTime)dataReader["FechaRegistro"]; }
                    if (dataReader["UsuarioModifica"] != DBNull.Value) { obj.UsuarioModifica = (string)dataReader["UsuarioModifica"]; }
                    if (dataReader["MaquinaModifica"] != DBNull.Value) { obj.MaquinaModifica = (string)dataReader["MaquinaModifica"]; }
                    if (dataReader["FechaModifica"] != DBNull.Value) { obj.FechaModifica = (DateTime)dataReader["FechaModifica"]; }

                    list.Add(obj);

                }

            }
            cn.Close();
            return list;

        }
        #endregion

    }
}
