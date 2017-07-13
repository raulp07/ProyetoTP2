
--IF EXISTS (SELECT * FROM SYSDATABASES WHERE name = 'GMKT')
--begin
--	drop database PlanMarketing
--end
--create database GMKT

go
use GMKT
go

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'PlanMarketing')
begin
	drop table PlanMarketing
end
go
create table PlanMarketing
(
Id_PlanMarketing int primary key identity,
nombrePanMarketing varchar(100),
descrípcion varchar(max),
fechaIni	datetime,
fechaFin	datetime,
presupuesto	decimal(18, 2),
UsuarioRegistra varchar(100),
MaquinaRegistra varchar(100),
FechaRegistro	datetime,
UsuarioModifica varchar(100),
MaquinaModifica varchar(100),
FechaModifica	datetime
)
go

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'Objetivos')
begin
	drop table Objetivos
end
go
create table Objetivos
(
Id_Objetivo int primary key identity,
NombreObjetivo varchar(50),
DescripcionObjetivo varchar(max),
UsuarioRegistra varchar(100),
MaquinaRegistra varchar(100),
FechaRegistro	datetime,
UsuarioModifica varchar(100),
MaquinaModifica varchar(100),
FechaModifica	datetime
)
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'RubroEstrategia')
begin
	drop table RubroEstrategia
end
go
create table RubroEstrategia
(
idRubroAccion int primary key identity,
NombreRubroEstrategia varchar(50),
PorcentajeImportancia int,
CostoPermitidoRubro decimal(18,2),
UsuarioRegistra varchar(100),
MaquinaRegistra varchar(100),
FechaRegistro	datetime,
UsuarioModifica varchar(100),
MaquinaModifica varchar(100),
FechaModifica	datetime
)

GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'Estrategia')
begin
	drop table Estrategia
end
go
Create table Estrategia
(
Id_Estrategia int primary key identity,
NombreEstrategia varchar(50),
DescripcionEstrategia varchar(max),
EstadoEstrategia int,
Fechacumplimiento datetime,
UsuarioRegistra varchar(100),
MaquinaRegistra varchar(100),
FechaRegistro	datetime,
UsuarioModifica varchar(100),
MaquinaModifica varchar(100),
FechaModifica	datetime
)
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'DatoEstadisticoEstrategia')
begin
	drop table DatoEstadisticoEstrategia
end
go
Create table DatoEstadisticoEstrategia
(
Id_DatoEstadisticoEstrategia int primary key identity,
NombreEstadisticoEstrategia varchar(50),
Puntuacion int,
Porcentaje int,
Fechacumplimiento datetime,
UsuarioRegistra varchar(100),
MaquinaRegistra varchar(100),
FechaRegistro	datetime,
UsuarioModifica varchar(100),
MaquinaModifica varchar(100),
FechaModifica	datetime
)
GO


---- Store PlanMarketing ----------

Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetPlanMarketingAll')
begin
	drop Procedure spGetPlanMarketingAll
end
go
CREATE PROCEDURE [spGetPlanMarketingAll]
AS
BEGIN
SELECT *
FROM
PlanMarketing
END

GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spInsertPlanMarketing')
begin
	drop Procedure spInsertPlanMarketing
end
go
CREATE PROCEDURE [spInsertPlanMarketing]
@Id_PlanMarketing int,
@nombrePanMarketing varchar(100),
@descrípcion varchar(max),
@fechaIni datetime,
@fechaFin datetime,
@presupuesto decimal(18, 2),
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
INSERT INTO 
PlanMarketing(
Id_PlanMarketing,
nombrePanMarketing,
descrípcion,
fechaIni,
fechaFin,
presupuesto,
UsuarioRegistra,
MaquinaRegistra,
FechaRegistro,
UsuarioModifica,
MaquinaModifica,
FechaModifica
)
VALUES (
@Id_PlanMarketing,
@nombrePanMarketing,
@descrípcion,
@fechaIni,
@fechaFin,
@presupuesto,
@UsuarioRegistra,
@MaquinaRegistra,
@FechaRegistro,
@UsuarioModifica,
@MaquinaModifica,
@FechaModifica
)
END

GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdatePlanMarketing')
begin
	drop Procedure spUpdatePlanMarketing
end
go
CREATE PROCEDURE [spUpdatePlanMarketing]
@Id_PlanMarketing int,
@nombrePanMarketing varchar(100),
@descrípcion varchar(max),
@fechaIni datetime,
@fechaFin datetime,
@presupuesto decimal(18,2),
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
UPDATE
PlanMarketing
SET
nombrePanMarketing = @nombrePanMarketing,
descrípcion = @descrípcion,
fechaIni = @fechaIni,
fechaFin = @fechaFin,
presupuesto = @presupuesto,
UsuarioRegistra = @UsuarioRegistra,
MaquinaRegistra = @MaquinaRegistra,
FechaRegistro = @FechaRegistro,
UsuarioModifica = @UsuarioModifica,
MaquinaModifica = @MaquinaModifica,
FechaModifica = @FechaModifica
WHERE
Id_PlanMarketing = @Id_PlanMarketing
END

Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeletePlanMarketing')
begin
	drop Procedure spDeletePlanMarketing
end
go
CREATE PROCEDURE [spDeletePlanMarketing]
@Id_PlanMarketing int
AS
BEGIN
DELETE FROM
PlanMarketing
WHERE
Id_PlanMarketing = @Id_PlanMarketing
END


------ store Objetivos ------------


Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetObjetivosAll')
begin
	drop Procedure spGetObjetivosAll
end
go
CREATE PROCEDURE [spGetObjetivosAll]
AS
BEGIN
SELECT *
FROM
Objetivos
END


GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spInsertObjetivos')
begin
	drop Procedure spInsertObjetivos
end
go
CREATE PROCEDURE [spInsertObjetivos]
@Id_Objetivo int,
@NombreObjetivo varchar(50),
@DescripcionObjetivo varchar(max),
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
INSERT INTO 
Objetivos(
Id_Objetivo,
NombreObjetivo,
DescripcionObjetivo,
UsuarioRegistra,
MaquinaRegistra,
FechaRegistro,
UsuarioModifica,
MaquinaModifica,
FechaModifica
)
VALUES (
@Id_Objetivo,
@NombreObjetivo,
@DescripcionObjetivo,
@UsuarioRegistra,
@MaquinaRegistra,
@FechaRegistro,
@UsuarioModifica,
@MaquinaModifica,
@FechaModifica
)
END

GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateObjetivos')
begin
	drop Procedure spUpdateObjetivos
end
go
CREATE PROCEDURE [spUpdateObjetivos]
@Id_Objetivo int,
@NombreObjetivo varchar(50),
@DescripcionObjetivo varchar(max),
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
UPDATE
Objetivos
SET
NombreObjetivo = @NombreObjetivo,
DescripcionObjetivo = @DescripcionObjetivo,
UsuarioRegistra = @UsuarioRegistra,
MaquinaRegistra = @MaquinaRegistra,
FechaRegistro = @FechaRegistro,
UsuarioModifica = @UsuarioModifica,
MaquinaModifica = @MaquinaModifica,
FechaModifica = @FechaModifica
WHERE
@Id_Objetivo = @Id_Objetivo
END

Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteObjetivos')
begin
	drop Procedure spDeleteObjetivos
end
go
CREATE PROCEDURE [spDeleteObjetivos]
@Id_Objetivo int
AS
BEGIN
DELETE FROM
Objetivos
WHERE
Id_Objetivo = @Id_Objetivo
END



----- store Rubro Estrategia --------------

Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetRubroEstrategiaAll')
begin
	drop Procedure spGetRubroEstrategiaAll
end
go
CREATE PROCEDURE [spGetRubroEstrategiaAll]
AS
BEGIN
SELECT *
FROM
RubroEstrategia
END


GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spInsertRubroEstrategia')
begin
	drop Procedure spInsertRubroEstrategia
end
go
CREATE PROCEDURE [spInsertRubroEstrategia]
@idRubroAccion int,
@NombreRubroEstrategia varchar(50),
@PorcentajeImportancia int,
@CostoPermitidoRubro decimal(18,2),
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
INSERT INTO 
RubroEstrategia(
idRubroAccion,
NombreRubroEstrategia,
PorcentajeImportancia,
CostoPermitidoRubro,
UsuarioRegistra,
MaquinaRegistra,
FechaRegistro,
UsuarioModifica,
MaquinaModifica,
FechaModifica
)
VALUES (
@idRubroAccion,
@NombreRubroEstrategia,
@PorcentajeImportancia,
@CostoPermitidoRubro,
@UsuarioRegistra,
@MaquinaRegistra,
@FechaRegistro,
@UsuarioModifica,
@MaquinaModifica,
@FechaModifica
)
END
GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateRubroEstrategia')
begin
	drop Procedure spUpdateRubroEstrategia
end
go
CREATE PROCEDURE [spUpdateRubroEstrategia]
@idRubroAccion int,
@NombreRubroEstrategia varchar(50),
@PorcentajeImportancia int,
@CostoPermitidoRubro decimal(18,2),
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
UPDATE
RubroEstrategia
SET
NombreRubroEstrategia = @NombreRubroEstrategia,
PorcentajeImportancia = @PorcentajeImportancia,
CostoPermitidoRubro = @CostoPermitidoRubro,
UsuarioRegistra = @UsuarioRegistra,
MaquinaRegistra = @MaquinaRegistra,
FechaRegistro = @FechaRegistro,
UsuarioModifica = @UsuarioModifica,
MaquinaModifica = @MaquinaModifica,
FechaModifica = @FechaModifica
WHERE
idRubroAccion = @idRubroAccion
END
Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteRubroEstrategia')
begin
	drop Procedure spDeleteRubroEstrategia
end
go
CREATE PROCEDURE [spDeleteRubroEstrategia]
@idRubroAccion int
AS
BEGIN
DELETE FROM
RubroEstrategia
WHERE
idRubroAccion = @idRubroAccion
END



----- store Estrategia -----------

Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetEstrategiaAll')
begin
	drop Procedure spGetEstrategiaAll
end
go
CREATE PROCEDURE [spGetEstrategiaAll]
AS
BEGIN
SELECT *
FROM
Estrategia
END

GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spInsertEstrategia')
begin
	drop Procedure spInsertEstrategia
end
go
CREATE PROCEDURE [spInsertEstrategia]
@Id_Estrategia int,
@NombreEstrategia varchar(50),
@DescripcionEstrategia varchar(max),
@EstadoEstrategia int,
@Fechacumplimiento datetime,
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
INSERT INTO 
Estrategia(
Id_Estrategia,
NombreEstrategia,
DescripcionEstrategia,
EstadoEstrategia,
Fechacumplimiento,
UsuarioRegistra,
MaquinaRegistra,
FechaRegistro,
UsuarioModifica,
MaquinaModifica,
FechaModifica
)
VALUES (
@Id_Estrategia,
@NombreEstrategia,
@DescripcionEstrategia,
@EstadoEstrategia,
@Fechacumplimiento,
@UsuarioRegistra,
@MaquinaRegistra,
@FechaRegistro,
@UsuarioModifica,
@MaquinaModifica,
@FechaModifica
)
END


GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateEstrategia')
begin
	drop Procedure spUpdateEstrategia
end
go
CREATE PROCEDURE [spUpdateEstrategia]
@Id_Estrategia int,
@NombreEstrategia varchar(50),
@DescripcionEstrategia varchar(max),
@EstadoEstrategia int,
@Fechacumplimiento datetime,
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
UPDATE
Estrategia
SET
NombreEstrategia = @NombreEstrategia,
DescripcionEstrategia = @DescripcionEstrategia,
EstadoEstrategia = @EstadoEstrategia,
Fechacumplimiento = @Fechacumplimiento,
UsuarioRegistra = @UsuarioRegistra,
MaquinaRegistra = @MaquinaRegistra,
FechaRegistro = @FechaRegistro,
UsuarioModifica = @UsuarioModifica,
MaquinaModifica = @MaquinaModifica,
FechaModifica = @FechaModifica
WHERE
Id_Estrategia = @Id_Estrategia
END

Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteEstrategia')
begin
	drop Procedure spDeleteEstrategia
end
go
CREATE PROCEDURE [spDeleteEstrategia]
@Id_Estrategia int
AS
BEGIN
DELETE FROM
Estrategia
WHERE
Id_Estrategia = @Id_Estrategia
END

----- store DatoEstadisticoEstrategia ------------

Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetDatoEstadisticoEstrategiaAll')
begin
	drop Procedure spGetDatoEstadisticoEstrategiaAll
end
go
CREATE PROCEDURE [spGetDatoEstadisticoEstrategiaAll]
AS
BEGIN
SELECT *
FROM
DatoEstadisticoEstrategia
END


GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spInsertDatoEstadisticoEstrategia')
begin
	drop Procedure spInsertDatoEstadisticoEstrategia
end
go
CREATE PROCEDURE [spInsertDatoEstadisticoEstrategia]
@Id_DatoEstadisticoEstrategia int,
@NombreEstadisticoEstrategia varchar(50),
@Puntuacion int,
@Porcentaje int,
@Fechacumplimiento datetime,
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
INSERT INTO 
DatoEstadisticoEstrategia(
Id_DatoEstadisticoEstrategia,
NombreEstadisticoEstrategia,
Puntuacion,
Porcentaje,
Fechacumplimiento,
UsuarioRegistra,
MaquinaRegistra,
FechaRegistro,
UsuarioModifica,
MaquinaModifica,
FechaModifica
)
VALUES (
@Id_DatoEstadisticoEstrategia,
@NombreEstadisticoEstrategia,
@Puntuacion,
@Porcentaje,
@Fechacumplimiento,
@UsuarioRegistra,
@MaquinaRegistra,
@FechaRegistro,
@UsuarioModifica,
@MaquinaModifica,
@FechaModifica
)
END

GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateDatoEstadisticoEstrategia')
begin
	drop Procedure spUpdateDatoEstadisticoEstrategia
end
go
CREATE PROCEDURE [spUpdateDatoEstadisticoEstrategia]
@Id_DatoEstadisticoEstrategia int,
@NombreEstadisticoEstrategia varchar(50),
@Puntuacion int,
@Porcentaje int,
@Fechacumplimiento datetime,
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
UPDATE
DatoEstadisticoEstrategia
SET
NombreEstadisticoEstrategia = @NombreEstadisticoEstrategia,
Puntuacion = @Puntuacion,
Porcentaje = @Porcentaje,
Fechacumplimiento = @Fechacumplimiento,
UsuarioRegistra = @UsuarioRegistra,
MaquinaRegistra = @MaquinaRegistra,
FechaRegistro = @FechaRegistro,
UsuarioModifica = @UsuarioModifica,
MaquinaModifica = @MaquinaModifica,
FechaModifica = @FechaModifica
WHERE
Id_DatoEstadisticoEstrategia = @Id_DatoEstadisticoEstrategia
END

Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteDatoEstadisticoEstrategia')
begin
	drop Procedure spDeleteDatoEstadisticoEstrategia
end
go
CREATE PROCEDURE [spDeleteDatoEstadisticoEstrategia]
@Id_DatoEstadisticoEstrategia int
AS
BEGIN
DELETE FROM
DatoEstadisticoEstrategia
WHERE
Id_DatoEstadisticoEstrategia = @Id_DatoEstadisticoEstrategia
END


GO

