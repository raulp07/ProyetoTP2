using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace proyectoprueba.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }

    public class PlanMarketing
    {

        ///

        /// Gets or Sets Id_PlanMarketing
        ///

        public int Id_PlanMarketing
        {
            get { return _Id_PlanMarketing; }
            set { _Id_PlanMarketing = value; }
        }
        private int _Id_PlanMarketing;

        ///

        /// Gets or Sets nombrePanMarketing
        ///

        public string nombrePanMarketing
        {
            get { return _nombrePanMarketing; }
            set { _nombrePanMarketing = value; }
        }
        private string _nombrePanMarketing;

        ///

        /// Gets or Sets descrípcion
        ///

        public string descrípcion
        {
            get { return _descrípcion; }
            set { _descrípcion = value; }
        }
        private string _descrípcion;

        ///

        /// Gets or Sets fechaIni
        ///

        public DateTime fechaIni
        {
            get { return _fechaIni; }
            set { _fechaIni = value; }
        }
        private DateTime _fechaIni;

        ///

        /// Gets or Sets fechaFin
        ///

        public DateTime fechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }
        private DateTime _fechaFin;

        ///

        /// Gets or Sets presupuesto
        ///

        public decimal presupuesto
        {
            get { return _presupuesto; }
            set { _presupuesto = value; }
        }
        private decimal _presupuesto;

        ///

        /// Gets or Sets UsuarioRegistra
        ///

        public string UsuarioRegistra
        {
            get { return _UsuarioRegistra; }
            set { _UsuarioRegistra = value; }
        }
        private string _UsuarioRegistra;

        ///

        /// Gets or Sets MaquinaRegistra
        ///

        public string MaquinaRegistra
        {
            get { return _MaquinaRegistra; }
            set { _MaquinaRegistra = value; }
        }
        private string _MaquinaRegistra;

        ///

        /// Gets or Sets FechaRegistro
        ///

        public DateTime FechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        private DateTime _FechaRegistro;

        ///

        /// Gets or Sets UsuarioModifica
        ///

        public string UsuarioModifica
        {
            get { return _UsuarioModifica; }
            set { _UsuarioModifica = value; }
        }
        private string _UsuarioModifica;

        ///

        /// Gets or Sets MaquinaModifica
        ///

        public string MaquinaModifica
        {
            get { return _MaquinaModifica; }
            set { _MaquinaModifica = value; }
        }
        private string _MaquinaModifica;

        ///

        /// Gets or Sets FechaModifica
        ///

        public DateTime FechaModifica
        {
            get { return _FechaModifica; }
            set { _FechaModifica = value; }
        }
        private DateTime _FechaModifica;

    }


    public class Objetivos
    {

        ///

        /// Gets or Sets Id_Objetivo
        ///

        public int Id_Objetivo
        {
            get { return _Id_Objetivo; }
            set { _Id_Objetivo = value; }
        }
        private int _Id_Objetivo;

        ///

        /// Gets or Sets NombreObjetivo
        ///

        public string NombreObjetivo
        {
            get { return _NombreObjetivo; }
            set { _NombreObjetivo = value; }
        }
        private string _NombreObjetivo;

        ///

        /// Gets or Sets DescripcionObjetivo
        ///

        public string DescripcionObjetivo
        {
            get { return _DescripcionObjetivo; }
            set { _DescripcionObjetivo = value; }
        }
        private string _DescripcionObjetivo;

        ///

        /// Gets or Sets UsuarioRegistra
        ///

        public string UsuarioRegistra
        {
            get { return _UsuarioRegistra; }
            set { _UsuarioRegistra = value; }
        }
        private string _UsuarioRegistra;

        ///

        /// Gets or Sets MaquinaRegistra
        ///

        public string MaquinaRegistra
        {
            get { return _MaquinaRegistra; }
            set { _MaquinaRegistra = value; }
        }
        private string _MaquinaRegistra;

        ///

        /// Gets or Sets FechaRegistro
        ///

        public DateTime FechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        private DateTime _FechaRegistro;

        ///

        /// Gets or Sets UsuarioModifica
        ///

        public string UsuarioModifica
        {
            get { return _UsuarioModifica; }
            set { _UsuarioModifica = value; }
        }
        private string _UsuarioModifica;

        ///

        /// Gets or Sets MaquinaModifica
        ///

        public string MaquinaModifica
        {
            get { return _MaquinaModifica; }
            set { _MaquinaModifica = value; }
        }
        private string _MaquinaModifica;

        ///

        /// Gets or Sets FechaModifica
        ///

        public DateTime FechaModifica
        {
            get { return _FechaModifica; }
            set { _FechaModifica = value; }
        }
        private DateTime _FechaModifica;

    }

    public class RubroEstrategia
    {

        ///

        /// Gets or Sets idRubroAccion
        ///

        public int idRubroAccion
        {
            get { return _idRubroAccion; }
            set { _idRubroAccion = value; }
        }
        private int _idRubroAccion;

        ///

        /// Gets or Sets NombreRubroEstrategia
        ///

        public string NombreRubroEstrategia
        {
            get { return _NombreRubroEstrategia; }
            set { _NombreRubroEstrategia = value; }
        }
        private string _NombreRubroEstrategia;

        ///

        /// Gets or Sets PorcentajeImportancia
        ///

        public int PorcentajeImportancia
        {
            get { return _PorcentajeImportancia; }
            set { _PorcentajeImportancia = value; }
        }
        private int _PorcentajeImportancia;

        ///

        /// Gets or Sets CostoPermitidoRubro
        ///

        public decimal CostoPermitidoRubro
        {
            get { return _CostoPermitidoRubro; }
            set { _CostoPermitidoRubro = value; }
        }
        private decimal _CostoPermitidoRubro;

        ///

        /// Gets or Sets UsuarioRegistra
        ///

        public string UsuarioRegistra
        {
            get { return _UsuarioRegistra; }
            set { _UsuarioRegistra = value; }
        }
        private string _UsuarioRegistra;

        ///

        /// Gets or Sets MaquinaRegistra
        ///

        public string MaquinaRegistra
        {
            get { return _MaquinaRegistra; }
            set { _MaquinaRegistra = value; }
        }
        private string _MaquinaRegistra;

        ///

        /// Gets or Sets FechaRegistro
        ///

        public DateTime FechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        private DateTime _FechaRegistro;

        ///

        /// Gets or Sets UsuarioModifica
        ///

        public string UsuarioModifica
        {
            get { return _UsuarioModifica; }
            set { _UsuarioModifica = value; }
        }
        private string _UsuarioModifica;

        ///

        /// Gets or Sets MaquinaModifica
        ///

        public string MaquinaModifica
        {
            get { return _MaquinaModifica; }
            set { _MaquinaModifica = value; }
        }
        private string _MaquinaModifica;

        ///

        /// Gets or Sets FechaModifica
        ///

        public DateTime FechaModifica
        {
            get { return _FechaModifica; }
            set { _FechaModifica = value; }
        }
        private DateTime _FechaModifica;

    }

    public class Estrategia
    {

        ///

        /// Gets or Sets Id_Estrategia
        ///

        public int Id_Estrategia
        {
            get { return _Id_Estrategia; }
            set { _Id_Estrategia = value; }
        }
        private int _Id_Estrategia;

        ///

        /// Gets or Sets NombreEstrategia
        ///

        public string NombreEstrategia
        {
            get { return _NombreEstrategia; }
            set { _NombreEstrategia = value; }
        }
        private string _NombreEstrategia;

        ///

        /// Gets or Sets DescripcionEstrategia
        ///

        public string DescripcionEstrategia
        {
            get { return _DescripcionEstrategia; }
            set { _DescripcionEstrategia = value; }
        }
        private string _DescripcionEstrategia;

        ///

        /// Gets or Sets EstadoEstrategia
        ///

        public int EstadoEstrategia
        {
            get { return _EstadoEstrategia; }
            set { _EstadoEstrategia = value; }
        }
        private int _EstadoEstrategia;

        ///

        /// Gets or Sets Fechacumplimiento
        ///

        public DateTime Fechacumplimiento
        {
            get { return _Fechacumplimiento; }
            set { _Fechacumplimiento = value; }
        }
        private DateTime _Fechacumplimiento;

        ///

        /// Gets or Sets UsuarioRegistra
        ///

        public string UsuarioRegistra
        {
            get { return _UsuarioRegistra; }
            set { _UsuarioRegistra = value; }
        }
        private string _UsuarioRegistra;

        ///

        /// Gets or Sets MaquinaRegistra
        ///

        public string MaquinaRegistra
        {
            get { return _MaquinaRegistra; }
            set { _MaquinaRegistra = value; }
        }
        private string _MaquinaRegistra;

        ///

        /// Gets or Sets FechaRegistro
        ///

        public DateTime FechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        private DateTime _FechaRegistro;

        ///

        /// Gets or Sets UsuarioModifica
        ///

        public string UsuarioModifica
        {
            get { return _UsuarioModifica; }
            set { _UsuarioModifica = value; }
        }
        private string _UsuarioModifica;

        ///

        /// Gets or Sets MaquinaModifica
        ///

        public string MaquinaModifica
        {
            get { return _MaquinaModifica; }
            set { _MaquinaModifica = value; }
        }
        private string _MaquinaModifica;

        ///

        /// Gets or Sets FechaModifica
        ///

        public DateTime FechaModifica
        {
            get { return _FechaModifica; }
            set { _FechaModifica = value; }
        }
        private DateTime _FechaModifica;

    }

    public class DatoEstadisticoEstrategia
    {

        ///

        /// Gets or Sets Id_DatoEstadisticoEstrategia
        ///

        public int Id_DatoEstadisticoEstrategia
        {
            get { return _Id_DatoEstadisticoEstrategia; }
            set { _Id_DatoEstadisticoEstrategia = value; }
        }
        private int _Id_DatoEstadisticoEstrategia;

        ///

        /// Gets or Sets NombreEstadisticoEstrategia
        ///

        public string NombreEstadisticoEstrategia
        {
            get { return _NombreEstadisticoEstrategia; }
            set { _NombreEstadisticoEstrategia = value; }
        }
        private string _NombreEstadisticoEstrategia;

        ///

        /// Gets or Sets Puntuacion
        ///

        public int Puntuacion
        {
            get { return _Puntuacion; }
            set { _Puntuacion = value; }
        }
        private int _Puntuacion;

        ///

        /// Gets or Sets Porcentaje
        ///

        public int Porcentaje
        {
            get { return _Porcentaje; }
            set { _Porcentaje = value; }
        }
        private int _Porcentaje;

        ///

        /// Gets or Sets Fechacumplimiento
        ///

        public DateTime Fechacumplimiento
        {
            get { return _Fechacumplimiento; }
            set { _Fechacumplimiento = value; }
        }
        private DateTime _Fechacumplimiento;

        ///

        /// Gets or Sets UsuarioRegistra
        ///

        public string UsuarioRegistra
        {
            get { return _UsuarioRegistra; }
            set { _UsuarioRegistra = value; }
        }
        private string _UsuarioRegistra;

        ///

        /// Gets or Sets MaquinaRegistra
        ///

        public string MaquinaRegistra
        {
            get { return _MaquinaRegistra; }
            set { _MaquinaRegistra = value; }
        }
        private string _MaquinaRegistra;

        ///

        /// Gets or Sets FechaRegistro
        ///

        public DateTime FechaRegistro
        {
            get { return _FechaRegistro; }
            set { _FechaRegistro = value; }
        }
        private DateTime _FechaRegistro;

        ///

        /// Gets or Sets UsuarioModifica
        ///

        public string UsuarioModifica
        {
            get { return _UsuarioModifica; }
            set { _UsuarioModifica = value; }
        }
        private string _UsuarioModifica;

        ///

        /// Gets or Sets MaquinaModifica
        ///

        public string MaquinaModifica
        {
            get { return _MaquinaModifica; }
            set { _MaquinaModifica = value; }
        }
        private string _MaquinaModifica;

        ///

        /// Gets or Sets FechaModifica
        ///

        public DateTime FechaModifica
        {
            get { return _FechaModifica; }
            set { _FechaModifica = value; }
        }
        private DateTime _FechaModifica;

    }
}
