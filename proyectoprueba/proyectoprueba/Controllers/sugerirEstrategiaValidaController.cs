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

        [HttpPost]
        public JsonResult ADDEstrategia(Estrategia BEEstrategia)
        {
            return Json(InsertEstrategia(BEEstrategia), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarRubroEstrategia(RubroEstrategia BERubroEstrategia)
        {
            return Json(GetAllRubroEstrategia(BERubroEstrategia), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ADDDatoEstadisticoEstrategia(List<DatoEstadisticoEstrategia> BEDatoEstadisticoEstrategia)
        {
            bool Resultado = false;
            foreach (DatoEstadisticoEstrategia DatoEstadisticoEstrategia in BEDatoEstadisticoEstrategia)
            {
                Resultado = InsertDatoEstadisticoEstrategia(DatoEstadisticoEstrategia);
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarEstrategia(Estrategia BEEstrategia)
        {
            return Json(GetAllEstrategia(BEEstrategia), JsonRequestBehavior.AllowGet);
        }

        #region "DAO Estrategia"

        private static string Config = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        private static SqlConnection cn = new SqlConnection(Config);
        public List<Estrategia> GetAllEstrategia(Estrategia BEEstrategia)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spGetEstrategiaAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Objetivo", SqlDbType.Int).Value = BEEstrategia.Id_Objetivo;
                List<Estrategia> list = new List<Estrategia>();
                if (cn.State == ConnectionState.Connecting || cn.State == ConnectionState.Open)
                    cn.Close();

                cn.Open();
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
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }

        }

        public string InsertEstrategia(Estrategia BEEstrategia)
        {
            SqlCommand cmd;
            cn.Open();
            string Id_Estrategia = "0";
            try
            {
                using (cmd = new SqlCommand("spInsertEstrategia", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id_Estrategia", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Id_Objetivo", SqlDbType.Int).Value = BEEstrategia.Id_Objetivo;
                    cmd.Parameters.Add("@NombreEstrategia", SqlDbType.VarChar).Value = BEEstrategia.NombreEstrategia;
                    cmd.Parameters.Add("@DescripcionEstrategia", SqlDbType.VarChar).Value = BEEstrategia.DescripcionEstrategia;
                    cmd.Parameters.Add("@EstadoEstrategia", SqlDbType.Int).Value = BEEstrategia.EstadoEstrategia;
                    cmd.Parameters.Add("@Fechacumplimiento", SqlDbType.DateTime).Value = BEEstrategia.Fechacumplimiento;
                    cmd.Parameters.Add("@UsuarioRegistra", SqlDbType.VarChar).Value = Environment.UserName;
                    cmd.Parameters.Add("@MaquinaRegistra", SqlDbType.VarChar).Value = Environment.UserDomainName;
                    cmd.Parameters.Add("@FechaRegistro", SqlDbType.DateTime).Value = DateTime.Today;
                    cmd.Parameters.Add("@UsuarioModifica", SqlDbType.VarChar).Value = Environment.UserName;
                    cmd.Parameters.Add("@MaquinaModifica", SqlDbType.VarChar).Value = Environment.UserDomainName;
                    cmd.Parameters.Add("@FechaModifica", SqlDbType.DateTime).Value = DateTime.Today;
                    cmd.ExecuteNonQuery();
                    Id_Estrategia = Convert.ToString(cmd.Parameters["@Id_Estrategia"].Value.ToString());
                }
                return Id_Estrategia;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
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
            try
            {
                SqlCommand cmd = new SqlCommand("spGetPlanMarketingAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<PlanMarketing> list = new List<PlanMarketing>();
                if (cn.State == ConnectionState.Connecting || cn.State == ConnectionState.Open)
                    cn.Close();

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
                return list;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }

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

        #region "DAO DatoEstadisticoEstrategia"
        public List<DatoEstadisticoEstrategia> GetAllDatoEstadisticoEstrategia()
        {
            SqlCommand cmd = new SqlCommand("spGetDatoEstadisticoEstrategiaAll", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            List<DatoEstadisticoEstrategia> list = new List<DatoEstadisticoEstrategia>();
            cn.Open();
            using (IDataReader dataReader = cmd.ExecuteReader())
            {

                while (dataReader.Read())
                {

                    DatoEstadisticoEstrategia obj = new DatoEstadisticoEstrategia();

                    if (dataReader["Id_DatoEstadisticoEstrategia"] != DBNull.Value) { obj.Id_DatoEstadisticoEstrategia = (int)dataReader["Id_DatoEstadisticoEstrategia"]; }
                    if (dataReader["idRubroAccion"] != DBNull.Value) { obj.idRubroAccion = (int)dataReader["idRubroAccion"]; }
                    if (dataReader["Id_Estrategia"] != DBNull.Value) { obj.Id_Estrategia = (int)dataReader["Id_Estrategia"]; }
                    if (dataReader["NombreEstadisticoEstrategia"] != DBNull.Value) { obj.NombreEstadisticoEstrategia = (string)dataReader["NombreEstadisticoEstrategia"]; }
                    if (dataReader["Puntuacion"] != DBNull.Value) { obj.Puntuacion = (int)dataReader["Puntuacion"]; }
                    if (dataReader["Porcentaje"] != DBNull.Value) { obj.Porcentaje = (int)dataReader["Porcentaje"]; }
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
            cn.Close();
            return list;

        }

        public bool InsertDatoEstadisticoEstrategia(DatoEstadisticoEstrategia BEDatoEstadisticoEstrategia)
        {
            SqlCommand cmd;
            int resultado = 0;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();

                using (cmd = new SqlCommand("spInsertDatoEstadisticoEstrategia", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idRubroAccion", SqlDbType.Int).Value = BEDatoEstadisticoEstrategia.idRubroAccion;
                    cmd.Parameters.Add("@Id_Estrategia", SqlDbType.Int).Value = BEDatoEstadisticoEstrategia.Id_Estrategia;
                    //cmd.Parameters.Add("@NombreEstadisticoEstrategia", SqlDbType.VarChar).Value = BEDatoEstadisticoEstrategia.NombreEstadisticoEstrategia;
                    cmd.Parameters.Add("@Puntuacion", SqlDbType.Int).Value = BEDatoEstadisticoEstrategia.Puntuacion;
                    //cmd.Parameters.Add("@Porcentaje", SqlDbType.Int).Value = BEDatoEstadisticoEstrategia.Porcentaje;
                    cmd.Parameters.Add("@Fechacumplimiento", SqlDbType.DateTime).Value = BEDatoEstadisticoEstrategia.Fechacumplimiento;
                    cmd.Parameters.Add("@UsuarioRegistra", SqlDbType.VarChar).Value = Environment.UserName;
                    cmd.Parameters.Add("@MaquinaRegistra", SqlDbType.VarChar).Value = Environment.UserDomainName;
                    cmd.Parameters.Add("@FechaRegistro", DbType.DateTime).Value = DateTime.Today;
                    cmd.Parameters.Add("@UsuarioModifica", SqlDbType.VarChar).Value = Environment.UserName;
                    cmd.Parameters.Add("@MaquinaModifica", SqlDbType.VarChar).Value = Environment.UserDomainName;
                    cmd.Parameters.Add("@FechaModifica", SqlDbType.DateTime).Value = DateTime.Today;
                    resultado = cmd.ExecuteNonQuery();
                }
                return (resultado == 1);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }

        }



        public bool UpdateDatoEstadisticoEstrategia(DatoEstadisticoEstrategia BEDatoEstadisticoEstrategia)
        {


            SqlCommand cmd;
            try
            {
                cn.Open();
                using (cmd = new SqlCommand("spUpdateDatoEstadisticoEstrategia", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id_DatoEstadisticoEstrategia", SqlDbType.Int).Value = BEDatoEstadisticoEstrategia.Id_DatoEstadisticoEstrategia;
                    cmd.Parameters.Add("@idRubroAccion", SqlDbType.Int).Value = BEDatoEstadisticoEstrategia.idRubroAccion;
                    cmd.Parameters.Add("@Id_Estrategia", SqlDbType.Int).Value = BEDatoEstadisticoEstrategia.Id_Estrategia;
                    cmd.Parameters.Add("@NombreEstadisticoEstrategia", SqlDbType.VarChar).Value = BEDatoEstadisticoEstrategia.NombreEstadisticoEstrategia;
                    cmd.Parameters.Add("@Puntuacion", SqlDbType.Int).Value = BEDatoEstadisticoEstrategia.Puntuacion;
                    cmd.Parameters.Add("@Porcentaje", SqlDbType.Int).Value = BEDatoEstadisticoEstrategia.Porcentaje;
                    cmd.Parameters.Add("@Fechacumplimiento", SqlDbType.DateTime).Value = BEDatoEstadisticoEstrategia.Fechacumplimiento;
                    cmd.Parameters.Add("@UsuarioRegistra", SqlDbType.VarChar).Value = BEDatoEstadisticoEstrategia.UsuarioRegistra;
                    cmd.Parameters.Add("@MaquinaRegistra", SqlDbType.VarChar).Value = BEDatoEstadisticoEstrategia.MaquinaRegistra;
                    cmd.Parameters.Add("@FechaRegistro", DbType.DateTime).Value = BEDatoEstadisticoEstrategia.FechaRegistro;
                    cmd.Parameters.Add("@UsuarioModifica", SqlDbType.VarChar).Value = BEDatoEstadisticoEstrategia.UsuarioModifica;
                    cmd.Parameters.Add("@MaquinaModifica", SqlDbType.VarChar).Value = BEDatoEstadisticoEstrategia.MaquinaModifica;
                    cmd.Parameters.Add("@FechaModifica", SqlDbType.DateTime).Value = BEDatoEstadisticoEstrategia.FechaModifica;
                }

                return (cmd.ExecuteNonQuery() == 1);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }
        }


        #endregion

        #region "DAO Rubro Estrategia"

        public List<RubroEstrategia> GetAllRubroEstrategia(RubroEstrategia BERubroEstrategia)
        {

            SqlCommand cmd = new SqlCommand("spGetRubroEstrategiaAll", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@idRubroAccion", SqlDbType.Int).Value = BERubroEstrategia.idRubroAccion;
            cmd.Parameters.Add("@Id_Objetivo", SqlDbType.Int).Value = BERubroEstrategia.Id_Objetivo;
            List<RubroEstrategia> list = new List<RubroEstrategia>();
            cn.Open();
            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    RubroEstrategia obj = new RubroEstrategia();
                    if (dataReader["idRubroAccion"] != DBNull.Value) { obj.idRubroAccion = (int)dataReader["idRubroAccion"]; }
                    if (dataReader["Id_Objetivo"] != DBNull.Value) { obj.Id_Objetivo = (int)dataReader["Id_Objetivo"]; }
                    if (dataReader["NombreRubroEstrategia"] != DBNull.Value) { obj.NombreRubroEstrategia = (string)dataReader["NombreRubroEstrategia"]; }
                    if (dataReader["PorcentajeImportancia"] != DBNull.Value) { obj.PorcentajeImportancia = (int)dataReader["PorcentajeImportancia"]; }
                    if (dataReader["CostoPermitidoRubro"] != DBNull.Value) { obj.CostoPermitidoRubro = (decimal)dataReader["CostoPermitidoRubro"]; }
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
