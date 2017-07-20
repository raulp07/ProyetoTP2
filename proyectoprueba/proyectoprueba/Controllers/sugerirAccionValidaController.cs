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
    public class sugerirAccionValidaController : Controller
    {
        //
        // GET: /sugerirAccionValida/

        public ActionResult Index()
        {
            return View();
        }


        private static string Config = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        private static SqlConnection cn = new SqlConnection(Config);


        [HttpPost]
        public JsonResult ListarAccion(Accion BEAccion)
        {
            return Json(GetAllAccion(BEAccion), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarRubroAccion(RubroAccion BERubroAccion)
        {
            return Json(GetAllRubroAccion(BERubroAccion), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ADDAccion(Accion BEAccion)
        {
            return Json(InsertAccion(BEAccion), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ADDDatoEstadisticoAccion(List<DatoEstadisticoAccion> BEDatoEstadisticoAccion)
        {
            bool Resultado = false;
            foreach (DatoEstadisticoAccion DatoEstadisticoAccion in BEDatoEstadisticoAccion)
            {
                Resultado = InsertDatoEstadisticoAccion(DatoEstadisticoAccion);
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarDatoEstadisticoAccion(DatoEstadisticoAccion BEDatoEstadisticoAccion)
        {
            return Json(GetAllDatoEstadisticoAccion(BEDatoEstadisticoAccion), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UPDAccion(Accion BEAccion, List<DatoEstadisticoAccion> BEDatoEstadisticoAccion)
        {
            bool Resultado = false;
            if (UpdateAccion(BEAccion))
            {
                foreach (DatoEstadisticoAccion DatoEstadisticoAccion in BEDatoEstadisticoAccion)
                {
                    Resultado = InsertDatoEstadisticoAccion(DatoEstadisticoAccion);
                }
            }

            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarDatoEstadisticoAccionEstrategia(Accion BEAccion)
        {
            return Json(GetAllDatoEstadisticoAccionEstrategia(BEAccion), JsonRequestBehavior.AllowGet);
        }

        #region "DAO Estrategia"

        public List<Accion> GetAllAccion(Accion BEAccion)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spGetAccionAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Accion", SqlDbType.Int).Value = BEAccion.Id_Accion;
                cmd.Parameters.Add("@Id_Estrategia", SqlDbType.Int).Value = BEAccion.Id_Estrategia;
                List<Accion> list = new List<Accion>();

                cn.Open();
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Accion obj = new Accion();
                        if (dataReader["Id_Accion"] != DBNull.Value) { obj.Id_Accion = (int)dataReader["Id_Accion"]; }
                        if (dataReader["Id_Estrategia"] != DBNull.Value) { obj.Id_Estrategia = (int)dataReader["Id_Estrategia"]; }
                        if (dataReader["nombreAccion"] != DBNull.Value) { obj.nombreAccion = (string)dataReader["nombreAccion"]; }
                        if (dataReader["descipcionAccion"] != DBNull.Value) { obj.descipcionAccion = (string)dataReader["descipcionAccion"]; }
                        if (dataReader["EstadoAccion"] != DBNull.Value) { obj.EstadoAccion = (int)dataReader["EstadoAccion"]; }
                        if (dataReader["Fechacumplimiento"] != DBNull.Value) { obj.Fechacumplimiento = (DateTime)dataReader["Fechacumplimiento"]; }
                        if (dataReader["costoAccion"] != DBNull.Value) { obj.costoAccion = (decimal)dataReader["costoAccion"]; }
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

        public string InsertAccion(Accion BEAccion)
        {
            SqlCommand cmd;
            cn.Open();
            string Id_Estrategia = "0";
            try
            {
                using (cmd = new SqlCommand("spInsertAccion", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id_Accion", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Id_Estrategia", SqlDbType.Int).Value = BEAccion.Id_Estrategia;
                    cmd.Parameters.Add("@nombreAccion", SqlDbType.VarChar).Value = BEAccion.nombreAccion;
                    cmd.Parameters.Add("@descipcionAccion", SqlDbType.VarChar).Value = BEAccion.descipcionAccion;
                    cmd.Parameters.Add("@EstadoAccion", SqlDbType.Int).Value = BEAccion.EstadoAccion;
                    cmd.Parameters.Add("@Fechacumplimiento", SqlDbType.DateTime).Value = BEAccion.Fechacumplimiento;
                    cmd.Parameters.Add("@costoAccion", SqlDbType.Decimal).Value = BEAccion.costoAccion;
                    cmd.Parameters.Add("@UsuarioRegistra", SqlDbType.VarChar).Value = Environment.UserName;
                    cmd.Parameters.Add("@MaquinaRegistra", SqlDbType.VarChar).Value = Environment.UserDomainName;
                    cmd.Parameters.Add("@FechaRegistro", SqlDbType.DateTime).Value = DateTime.Today;
                    cmd.Parameters.Add("@UsuarioModifica", SqlDbType.VarChar).Value = Environment.UserName;
                    cmd.Parameters.Add("@MaquinaModifica", SqlDbType.VarChar).Value = Environment.UserDomainName;
                    cmd.Parameters.Add("@FechaModifica", SqlDbType.DateTime).Value = DateTime.Today;
                    cmd.ExecuteNonQuery();
                    Id_Estrategia = Convert.ToString(cmd.Parameters["@Id_Accion"].Value.ToString());
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



        public bool UpdateAccion(Accion BEAccion)
        {
            try
            {
                cn.Open();
                SqlCommand cmd;
                using (cmd = new SqlCommand("spUpdateAccion", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id_Accion", SqlDbType.Int).Value = BEAccion.Id_Accion;
                    cmd.Parameters.Add("@Id_Estrategia", SqlDbType.Int).Value = BEAccion.Id_Estrategia;
                    cmd.Parameters.Add("@nombreAccion", SqlDbType.VarChar).Value = BEAccion.nombreAccion;
                    cmd.Parameters.Add("@descipcionAccion", SqlDbType.VarChar).Value = BEAccion.descipcionAccion;
                    cmd.Parameters.Add("@EstadoAccion", SqlDbType.Int).Value = BEAccion.EstadoAccion;
                    cmd.Parameters.Add("@Fechacumplimiento", SqlDbType.DateTime).Value = BEAccion.Fechacumplimiento;
                    cmd.Parameters.Add("@costoAccion", SqlDbType.Decimal).Value = BEAccion.costoAccion;
                    cmd.Parameters.Add("@UsuarioRegistra", SqlDbType.VarChar).Value = Environment.UserName;
                    cmd.Parameters.Add("@MaquinaRegistra", SqlDbType.VarChar).Value = Environment.UserDomainName; ;
                    cmd.Parameters.Add("@FechaRegistro", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@UsuarioModifica", SqlDbType.VarChar).Value = Environment.UserName;
                    cmd.Parameters.Add("@MaquinaModifica", SqlDbType.VarChar).Value = Environment.UserDomainName; ;
                    cmd.Parameters.Add("@FechaModifica", SqlDbType.DateTime).Value = DateTime.Now;
                }
                cmd.ExecuteNonQuery();
                return true;
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


        #region "DAO DatoEstadisticoEstrategia"

        public List<DatoEstadisticoAccion> GetAllDatoEstadisticoAccionEstrategia(Accion BEAccion)
        {
            SqlCommand cmd = new SqlCommand("spGetDatoEstadisticoAccionObjetivos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_Estrategia", SqlDbType.Int).Value = BEAccion.Id_Estrategia;
            List<DatoEstadisticoAccion> list = new List<DatoEstadisticoAccion>();
            cn.Open();
            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    DatoEstadisticoAccion obj = new DatoEstadisticoAccion();
                    if (dataReader["Id_DatoEstadisticoAccion"] != DBNull.Value) { obj.Id_DatoEstadisticoAccion = (int)dataReader["Id_DatoEstadisticoAccion"]; }
                    if (dataReader["idRubroAccion"] != DBNull.Value) { obj.idRubroAccion = (int)dataReader["idRubroAccion"]; }
                    if (dataReader["Id_Accion"] != DBNull.Value) { obj.Id_Accion = (int)dataReader["Id_Accion"]; }
                    if (dataReader["nombreDatoEstadisticoAccion"] != DBNull.Value) { obj.nombreDatoEstadisticoAccion = (string)dataReader["NombreAccion"]; }
                    if (dataReader["Puntuacion"] != DBNull.Value) { obj.Puntuacion = (int)dataReader["Puntuacion"]; }
                    if (dataReader["Porcentaje"] != DBNull.Value) { obj.Porcentaje = (int)dataReader["Porcentaje"]; }
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

        public List<DatoEstadisticoAccion> GetAllDatoEstadisticoAccion(DatoEstadisticoAccion BEDatoEstadisticoAccion)
        {
            SqlCommand cmd = new SqlCommand("spGetDatoEstadisticoAccionAll", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id_DatoEstadisticoAccion", SqlDbType.Int).Value = BEDatoEstadisticoAccion.Id_DatoEstadisticoAccion;
            cmd.Parameters.Add("@Id_Accion", SqlDbType.Int).Value = BEDatoEstadisticoAccion.Id_Accion;
            List<DatoEstadisticoAccion> list = new List<DatoEstadisticoAccion>();
            cn.Open();
            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    DatoEstadisticoAccion obj = new DatoEstadisticoAccion();
                    if (dataReader["Id_DatoEstadisticoAccion"] != DBNull.Value) { obj.Id_DatoEstadisticoAccion = (int)dataReader["Id_DatoEstadisticoAccion"]; }
                    if (dataReader["idRubroAccion"] != DBNull.Value) { obj.idRubroAccion = (int)dataReader["idRubroAccion"]; }
                    if (dataReader["Id_Accion"] != DBNull.Value) { obj.Id_Accion = (int)dataReader["Id_Accion"]; }
                    if (dataReader["nombreDatoEstadisticoAccion"] != DBNull.Value) { obj.nombreDatoEstadisticoAccion = (string)dataReader["nombreDatoEstadisticoAccion"]; }
                    if (dataReader["Puntuacion"] != DBNull.Value) { obj.Puntuacion = (int)dataReader["Puntuacion"]; }
                    if (dataReader["Porcentaje"] != DBNull.Value) { obj.Porcentaje = (int)dataReader["Porcentaje"]; }
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

        public bool InsertDatoEstadisticoAccion(DatoEstadisticoAccion BEDatoEstadisticoAccion)
        {
            SqlCommand cmd;
            int resultado = 0;
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();

                using (cmd = new SqlCommand("spInsertDatoEstadisticoAccion", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idRubroAccion", SqlDbType.Int).Value = BEDatoEstadisticoAccion.idRubroAccion;
                    cmd.Parameters.Add("@Id_Accion", SqlDbType.Int).Value = BEDatoEstadisticoAccion.Id_Accion;
                    //cmd.Parameters.Add("@NombreEstadisticoEstrategia", SqlDbType.VarChar).Value = BEDatoEstadisticoEstrategia.NombreEstadisticoEstrategia;
                    cmd.Parameters.Add("@Puntuacion", SqlDbType.Int).Value = BEDatoEstadisticoAccion.Puntuacion;
                    //cmd.Parameters.Add("@Porcentaje", SqlDbType.Int).Value = BEDatoEstadisticoEstrategia.Porcentaje;
                    //cmd.Parameters.Add("@Fechacumplimiento", SqlDbType.DateTime).Value = BEDatoEstadisticoAccion.Fechacumplimiento;
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

        public bool UpdateDatoEstadisticoAccion(DatoEstadisticoAccion BEDatoEstadisticoAccion)
        {


            try
            {
                SqlCommand cmd;
                cn.Open();
                using (cmd = new SqlCommand("spUpdateDatoEstadisticoAccion", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id_DatoEstadisticoAccion", SqlDbType.Int).Value = BEDatoEstadisticoAccion.Id_DatoEstadisticoAccion;
                    cmd.Parameters.Add("@idRubroAccion", SqlDbType.Int).Value = BEDatoEstadisticoAccion.idRubroAccion;
                    cmd.Parameters.Add("@Id_Accion", SqlDbType.Int).Value = BEDatoEstadisticoAccion.Id_Accion;
                    cmd.Parameters.Add("@nombreDatoEstadisticoAccion", SqlDbType.VarChar).Value = BEDatoEstadisticoAccion.nombreDatoEstadisticoAccion;
                    cmd.Parameters.Add("@Puntuacion", SqlDbType.Int).Value = BEDatoEstadisticoAccion.Puntuacion;
                    cmd.Parameters.Add("@Porcentaje", SqlDbType.Int).Value = BEDatoEstadisticoAccion.Porcentaje;
                    //cmd.Parameters.Add("@Fechacumplimiento", SqlDbType.DateTime).Value = BEDatoEstadisticoAccion.Fechacumplimiento;
                    cmd.Parameters.Add("@UsuarioRegistra", SqlDbType.VarChar).Value = BEDatoEstadisticoAccion.UsuarioRegistra;
                    cmd.Parameters.Add("@MaquinaRegistra", SqlDbType.VarChar).Value = BEDatoEstadisticoAccion.MaquinaRegistra;
                    cmd.Parameters.Add("@FechaRegistro", DbType.DateTime).Value = BEDatoEstadisticoAccion.FechaRegistro;
                    cmd.Parameters.Add("@UsuarioModifica", SqlDbType.VarChar).Value = BEDatoEstadisticoAccion.UsuarioModifica;
                    cmd.Parameters.Add("@MaquinaModifica", SqlDbType.VarChar).Value = BEDatoEstadisticoAccion.MaquinaModifica;
                    cmd.Parameters.Add("@FechaModifica", SqlDbType.DateTime).Value = BEDatoEstadisticoAccion.FechaModifica;
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


        #region "DAO Rubro Accion"

        public List<RubroAccion> GetAllRubroAccion(RubroAccion BERubroAccion)
        {

            SqlCommand cmd = new SqlCommand("spGetRubroAccionAll", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@idRubroAccion", SqlDbType.Int).Value = BERubroAccion.idRubroAccion;
            cmd.Parameters.Add("@Id_Objetivo", SqlDbType.Int).Value = BERubroAccion.Id_Objetivo;
            List<RubroAccion> list = new List<RubroAccion>();
            cn.Open();
            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    RubroAccion obj = new RubroAccion();
                    if (dataReader["idRubroAccion"] != DBNull.Value) { obj.idRubroAccion = (int)dataReader["idRubroAccion"]; }
                    if (dataReader["Id_Objetivo"] != DBNull.Value) { obj.Id_Objetivo = (int)dataReader["Id_Objetivo"]; }
                    if (dataReader["nombreRubroAccion"] != DBNull.Value) { obj.nombreRubroAccion = (string)dataReader["nombreRubroAccion"]; }
                    if (dataReader["PorcentajeImportancia"] != DBNull.Value) { obj.PorcentajeImportancia = (int)dataReader["PorcentajeImportancia"]; }
                    if (dataReader["CostoPermitidoRubro"] != DBNull.Value) { obj.costoPermitidoRubro = (decimal)dataReader["CostoPermitidoRubro"]; }
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
