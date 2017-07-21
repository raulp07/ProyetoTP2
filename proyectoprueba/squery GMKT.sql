
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
Id_PlanMarketing int,
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
idRubroEstrategia int primary key identity,
Id_Objetivo int,
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
Id_Objetivo int,
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
idRubroEstrategia int,
Id_Estrategia int,
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



IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'RubroAccion')
begin
	drop table RubroAccion
end
go
create table RubroAccion
(
idRubroAccion int primary key identity,
Id_Objetivo int,
nombreRubroAccion varchar(50),
PorcentajeImportancia int,
costoPermitidoRubro decimal(18,2),
UsuarioRegistra varchar(100),
MaquinaRegistra varchar(100),
FechaRegistro	datetime,
UsuarioModifica varchar(100),
MaquinaModifica varchar(100),
FechaModifica	datetime
)

GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'Accion')
begin
	drop table Accion
end
go
Create table Accion
(
Id_Accion int primary key identity,
Id_Estrategia int,
nombreAccion varchar(50),
descipcionAccion varchar(max),
EstadoAccion int,
Fechacumplimiento datetime,
costoAccion decimal(18,2),
UsuarioRegistra varchar(100),
MaquinaRegistra varchar(100),
FechaRegistro	datetime,
UsuarioModifica varchar(100),
MaquinaModifica varchar(100),
FechaModifica	datetime
)
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'DatoEstadisticoAccion')
begin
	drop table DatoEstadisticoAccion
end
go
Create table DatoEstadisticoAccion
(
Id_DatoEstadisticoAccion int primary key identity,
idRubroAccion int,
Id_Accion int,
nombreDatoEstadisticoAccion varchar(50),
Puntuacion int,
Porcentaje int,
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
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetPlanMarketingPresupuesto')
begin
	drop Procedure spGetPlanMarketingPresupuesto
end
go
CREATE PROCEDURE [spGetPlanMarketingPresupuesto]
(
@Id_PlanMarketing int =0
)
AS
BEGIN
SELECT PM.presupuesto, sum(A.costoAccion) as 'costoAccionGeneral' FROM PlanMarketing PM 
inner join Objetivos O on PM.Id_PlanMarketing = O.Id_PlanMarketing
inner join Estrategia E on E.Id_Objetivo = O.Id_Objetivo
inner join Accion A on A.Id_Estrategia = E.Id_Estrategia
where (PM.Id_PlanMarketing = @Id_PlanMarketing)
group by PM.presupuesto

END



Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetPlanMarketingAll')
begin
	drop Procedure spGetPlanMarketingAll
end
go
CREATE PROCEDURE [spGetPlanMarketingAll]
(
@Id_PlanMarketing int =0
)
AS
BEGIN
SELECT *
FROM
PlanMarketing
where (@Id_PlanMarketing = 0 or Id_PlanMarketing = @Id_PlanMarketing)
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
(
@Id_PlanMarketing int =0
)
AS
BEGIN
SELECT *
FROM
Objetivos
where
(Id_PlanMarketing=0 or Id_PlanMarketing=@Id_PlanMarketing)
END


GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spInsertObjetivos')
begin
	drop Procedure spInsertObjetivos
end
go
CREATE PROCEDURE [spInsertObjetivos]
@Id_Objetivo int,
@Id_PlanMarketing int,
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
Id_PlanMarketing,
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
@Id_PlanMarketing,
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
@Id_PlanMarketing int,
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
Id_PlanMarketing = @Id_PlanMarketing,
NombreObjetivo = @NombreObjetivo,
DescripcionObjetivo = @DescripcionObjetivo,
UsuarioRegistra = @UsuarioRegistra,
MaquinaRegistra = @MaquinaRegistra,
FechaRegistro = @FechaRegistro,
UsuarioModifica = @UsuarioModifica,
MaquinaModifica = @MaquinaModifica,
FechaModifica = @FechaModifica
WHERE
Id_Objetivo = @Id_Objetivo
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
(
@idRubroEstrategia int =0,
@Id_Objetivo int =0
)
AS
BEGIN
SELECT *
FROM
RubroEstrategia
where 
(@idRubroEstrategia = 0 or idRubroEstrategia = @idRubroEstrategia)
and (@Id_Objetivo = 0 or Id_Objetivo = @Id_Objetivo)
END


GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spInsertRubroEstrategia')
begin
	drop Procedure spInsertRubroEstrategia
end
go
CREATE PROCEDURE [spInsertRubroEstrategia]
@Id_Objetivo int,
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
Id_Objetivo,
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
@Id_Objetivo,
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
@idRubroEstrategia int,
@Id_Objetivo int,
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
Id_Objetivo = @Id_Objetivo,
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
idRubroEstrategia = @idRubroEstrategia
END
Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spDeleteRubroEstrategia')
begin
	drop Procedure spDeleteRubroEstrategia
end
go
CREATE PROCEDURE [spDeleteRubroEstrategia]
@idRubroEstrategia int
AS
BEGIN
DELETE FROM
RubroEstrategia
WHERE
idRubroEstrategia = @idRubroEstrategia
END



----- store Estrategia -----------

Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetEstrategiaAll')
begin
	drop Procedure spGetEstrategiaAll
end
go
CREATE PROCEDURE [spGetEstrategiaAll]
(
@Id_Estrategia int =0,
@Id_Objetivo int =0
)
AS
BEGIN
SELECT *
FROM
Estrategia
where 
(@Id_Estrategia =0 or Id_Estrategia=@Id_Estrategia)
AND (@Id_Objetivo= 0 or Id_Objetivo = @Id_Objetivo)
END

GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spInsertEstrategia')
begin
	drop Procedure spInsertEstrategia
end
go
CREATE PROCEDURE [spInsertEstrategia]
@Id_Estrategia int out,
@Id_Objetivo int,
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
Id_Objetivo,
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
@Id_Objetivo,
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
set @Id_Estrategia = @@IDENTITY
END



GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateEstrategia')
begin
	drop Procedure spUpdateEstrategia
end
go
CREATE PROCEDURE [spUpdateEstrategia]
@Id_Estrategia int,
@Id_Objetivo int,
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
	Id_Objetivo = @Id_Objetivo,
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

	delete from DatoEstadisticoEstrategia where Id_Estrategia = @Id_Estrategia

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
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetDatoEstadisticoEstrategiaObjetivos')
begin
	drop Procedure spGetDatoEstadisticoEstrategiaObjetivos
end
go
CREATE PROCEDURE [spGetDatoEstadisticoEstrategiaObjetivos]
(
@Id_Objetivo int 
)
AS
BEGIN
SELECT DEE.*,E.NombreEstrategia
FROM
DatoEstadisticoEstrategia DEE
inner join Estrategia E on DEE.Id_Estrategia = E.Id_Estrategia
where E.Id_Objetivo = @Id_Objetivo
END


Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetDatoEstadisticoEstrategiaAll')
begin
	drop Procedure spGetDatoEstadisticoEstrategiaAll
end
go

CREATE PROCEDURE [spGetDatoEstadisticoEstrategiaAll]
(
@Id_DatoEstadisticoEstrategia int =0,
@Id_Estrategia int =0
)
AS
BEGIN
SELECT *
FROM
DatoEstadisticoEstrategia
where 
(@Id_DatoEstadisticoEstrategia = 0 or Id_DatoEstadisticoEstrategia = @Id_DatoEstadisticoEstrategia)
AND (@Id_Estrategia = 0 or Id_Estrategia = @Id_Estrategia)
END


GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spInsertDatoEstadisticoEstrategia')
begin
	drop Procedure spInsertDatoEstadisticoEstrategia
end
go
CREATE PROCEDURE [spInsertDatoEstadisticoEstrategia]
@idRubroEstrategia int,
@Id_Estrategia int,
@NombreEstadisticoEstrategia varchar(50)='',
@Puntuacion int,
@Porcentaje int = 0,
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
idRubroEstrategia,
Id_Estrategia,
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
@idRubroEstrategia,
@Id_Estrategia,
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



--- store rubro accion


IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetRubroAccionAll')
begin
	drop Procedure spGetRubroAccionAll
end
Go
CREATE PROCEDURE [spGetRubroAccionAll]
(
@idRubroAccion int =0,
@Id_Objetivo int =0
)
AS
BEGIN
SELECT *
FROM
RubroAccion
where 
(@idRubroAccion = 0 or idRubroAccion = @idRubroAccion)
and (@Id_Objetivo = 0 or Id_Objetivo = @Id_Objetivo)
END




GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spInsertRubroAccion')
begin
	drop Procedure spInsertRubroAccion
end
Go
CREATE PROCEDURE [spInsertRubroAccion]
@Id_Objetivo int,
@nombreRubroAccion varchar(50),
@PorcentajeImportancia int,
@costoPermitidoRubro decimal(18,2),
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
INSERT INTO 
RubroAccion(
Id_Objetivo,
nombreRubroAccion,
PorcentajeImportancia,
costoPermitidoRubro,
UsuarioRegistra,
MaquinaRegistra,
FechaRegistro,
UsuarioModifica,
MaquinaModifica,
FechaModifica
)
VALUES (
@Id_Objetivo,
@nombreRubroAccion,
@PorcentajeImportancia,
@costoPermitidoRubro,
@UsuarioRegistra,
@MaquinaRegistra,
@FechaRegistro,
@UsuarioModifica,
@MaquinaModifica,
@FechaModifica
)
END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateRubroAccion')
begin
	drop Procedure spUpdateRubroAccion
end
Go
CREATE PROCEDURE [spUpdateRubroAccion]
@idRubroAccion int,
@Id_Objetivo int,
@nombreRubroAccion varchar(50),
@PorcentajeImportancia int,
@costoPermitidoRubro decimal(18,2),
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
UPDATE
RubroAccion
SET
Id_Objetivo = @Id_Objetivo,
nombreRubroAccion = @nombreRubroAccion,
PorcentajeImportancia = @PorcentajeImportancia,
costoPermitidoRubro = @costoPermitidoRubro,
UsuarioRegistra = @UsuarioRegistra,
MaquinaRegistra = @MaquinaRegistra,
FechaRegistro = @FechaRegistro,
UsuarioModifica = @UsuarioModifica,
MaquinaModifica = @MaquinaModifica,
FechaModifica = @FechaModifica
WHERE
idRubroAccion = @idRubroAccion
END

----- store Accion

Go

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetAccionAll')
begin
	drop Procedure spGetAccionAll
end
Go
CREATE PROCEDURE [spGetAccionAll]
(
@Id_Accion int= 0,
@Id_Estrategia int =0
)
AS
BEGIN
SELECT *
FROM
Accion
where
(@Id_Accion = 0 or Id_Accion = @Id_Accion)
and (@Id_Estrategia = 0 or Id_Estrategia = @Id_Estrategia)
END

GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spInsertAccion')
begin
	drop Procedure spInsertAccion
end
Go
CREATE PROCEDURE [spInsertAccion]
@Id_Accion int out,
@Id_Estrategia int,
@nombreAccion varchar(50),
@descipcionAccion varchar(max),
@EstadoAccion int,
@Fechacumplimiento datetime,
@costoAccion decimal(18,2),
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
INSERT INTO 
Accion(
Id_Estrategia,
nombreAccion,
descipcionAccion,
EstadoAccion,
Fechacumplimiento,
costoAccion,
UsuarioRegistra,
MaquinaRegistra,
FechaRegistro,
UsuarioModifica,
MaquinaModifica,
FechaModifica
)
VALUES (
@Id_Estrategia,
@nombreAccion,
@descipcionAccion,
@EstadoAccion,
@Fechacumplimiento,
@costoAccion,
@UsuarioRegistra,
@MaquinaRegistra,
@FechaRegistro,
@UsuarioModifica,
@MaquinaModifica,
@FechaModifica
)
set @Id_Accion = @@IDENTITY
END


GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateAccion')
begin
	drop Procedure spUpdateAccion
end
Go
CREATE PROCEDURE [spUpdateAccion]
@Id_Accion int,
@Id_Estrategia int,
@nombreAccion varchar(50),
@descipcionAccion varchar(max),
@EstadoAccion int,
@Fechacumplimiento datetime,
@costoAccion decimal(18,2),
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
UPDATE
Accion
SET
Id_Estrategia = @Id_Estrategia,
nombreAccion = @nombreAccion,
descipcionAccion = @descipcionAccion,
EstadoAccion = @EstadoAccion,
Fechacumplimiento = @Fechacumplimiento,
costoAccion = @costoAccion,
UsuarioRegistra = @UsuarioRegistra,
MaquinaRegistra = @MaquinaRegistra,
FechaRegistro = @FechaRegistro,
UsuarioModifica = @UsuarioModifica,
MaquinaModifica = @MaquinaModifica,
FechaModifica = @FechaModifica
WHERE
Id_Accion = @Id_Accion

delete from DatoEstadisticoAccion where Id_Accion = @Id_Accion

END

--- store dato rubro accion



Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetDatoEstadisticoAccionObjetivos')
begin
	drop Procedure spGetDatoEstadisticoAccionObjetivos
end
go
CREATE PROCEDURE [spGetDatoEstadisticoAccionObjetivos]
(
@Id_Estrategia int 
)
AS
BEGIN
SELECT DEA.*,A.nombreAccion
FROM
DatoEstadisticoAccion DEA
inner join Accion A on DEA.Id_Accion = A.Id_Accion
where A.Id_Estrategia = @Id_Estrategia
END


Go
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spGetDatoEstadisticoAccionAll')
begin
	drop Procedure spGetDatoEstadisticoAccionAll
end
go

CREATE PROCEDURE [spGetDatoEstadisticoAccionAll]
(
@Id_DatoEstadisticoAccion int =0,
@Id_Accion int =0
)
AS
BEGIN
SELECT *
FROM
DatoEstadisticoAccion
where 
(@Id_DatoEstadisticoAccion = 0 or Id_DatoEstadisticoAccion = @Id_DatoEstadisticoAccion)
AND (@Id_Accion = 0 or Id_Accion = @Id_Accion)
END


GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spInsertDatoEstadisticoAccion')
begin
	drop Procedure spInsertDatoEstadisticoAccion
end
Go
CREATE PROCEDURE [spInsertDatoEstadisticoAccion]
@idRubroAccion int,
@Id_Accion int,
@nombreDatoEstadisticoAccion varchar(50)='',
@Puntuacion int,
@Porcentaje int=0,
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
INSERT INTO 
DatoEstadisticoAccion(
idRubroAccion,
Id_Accion,
nombreDatoEstadisticoAccion,
Puntuacion,
Porcentaje,
UsuarioRegistra,
MaquinaRegistra,
FechaRegistro,
UsuarioModifica,
MaquinaModifica,
FechaModifica
)
VALUES (
@idRubroAccion,
@Id_Accion,
@nombreDatoEstadisticoAccion,
@Puntuacion,
@Porcentaje,
@UsuarioRegistra,
@MaquinaRegistra,
@FechaRegistro,
@UsuarioModifica,
@MaquinaModifica,
@FechaModifica
)
END

GO
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'spUpdateDatoEstadisticoAccion')
begin
	drop Procedure spUpdateDatoEstadisticoAccion
end
Go
CREATE PROCEDURE [spUpdateDatoEstadisticoAccion]
@Id_DatoEstadisticoAccion int,
@idRubroAccion int,
@Id_Accion int,
@nombreDatoEstadisticoAccion varchar(50),
@Puntuacion int,
@Porcentaje int,
@UsuarioRegistra varchar(100),
@MaquinaRegistra varchar(100),
@FechaRegistro datetime,
@UsuarioModifica varchar(100),
@MaquinaModifica varchar(100),
@FechaModifica datetime
AS
BEGIN
UPDATE
DatoEstadisticoAccion
SET
idRubroAccion = @idRubroAccion,
Id_Accion = @Id_Accion,
nombreDatoEstadisticoAccion = @nombreDatoEstadisticoAccion,
Puntuacion = @Puntuacion,
Porcentaje = @Porcentaje,
UsuarioRegistra = @UsuarioRegistra,
MaquinaRegistra = @MaquinaRegistra,
FechaRegistro = @FechaRegistro,
UsuarioModifica = @UsuarioModifica,
MaquinaModifica = @MaquinaModifica,
FechaModifica = @FechaModifica
WHERE
Id_DatoEstadisticoAccion = @Id_DatoEstadisticoAccion
END





insert into PlanMarketing (nombrePanMarketing,descrípcion) values ('Plan Marketing 1','Plan Marketing 1')
insert into PlanMarketing (nombrePanMarketing,descrípcion) values ('Plan Marketing 2','Plan Marketing 2')
insert into PlanMarketing (nombrePanMarketing,descrípcion) values ('Plan Marketing 3','Plan Marketing 3')
insert into PlanMarketing (nombrePanMarketing,descrípcion) values ('Plan Marketing 4','Plan Marketing 4')
insert into PlanMarketing (nombrePanMarketing,descrípcion) values ('Plan Marketing 5','Plan Marketing 5')


insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (1,'Objetivo 1 Planmkt 1','Objetivo 1')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (1,'Objetivo 2 Planmkt 1','Objetivo 2')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (1,'Objetivo 3 Planmkt 1','Objetivo 3')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (1,'Objetivo 4 Planmkt 1','Objetivo 4')


insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (2,'Objetivo 1 Planmkt 2','Objetivo 1')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (2,'Objetivo 2 Planmkt 2','Objetivo 2')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (2,'Objetivo 3 Planmkt 2','Objetivo 3')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (2,'Objetivo 4 Planmkt 2','Objetivo 4')

insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (3,'Objetivo 1 Planmkt 3','Objetivo 1')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (3,'Objetivo 2 Planmkt 3','Objetivo 2')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (3,'Objetivo 3 Planmkt 3','Objetivo 3')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (3,'Objetivo 4 Planmkt 3','Objetivo 4')

insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (4,'Objetivo 1 Planmkt 4','Objetivo 1')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (4,'Objetivo 2 Planmkt 4','Objetivo 2')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (4,'Objetivo 3 Planmkt 4','Objetivo 3')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (4,'Objetivo 4 Planmkt 4','Objetivo 4')

insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (5,'Objetivo 1 Planmkt 5','Objetivo 1')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (5,'Objetivo 2 Planmkt 5','Objetivo 2')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (5,'Objetivo 3 Planmkt 5','Objetivo 3')
insert into Objetivos (Id_PlanMarketing,NombreObjetivo,DescripcionObjetivo) values (5,'Objetivo 4 Planmkt 5','Objetivo 4')

insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (1,'rubro 1 objetivo 1',3,5)
insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (1,'rubro 2 objetivo 1',3,5)
insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (1,'rubro 3 objetivo 1',3,5)
insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (1,'rubro 4 objetivo 1',3,5)

insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (2,'rubro 1 objetivo 2',3,5)
insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (2,'rubro 2 objetivo 2',3,5)
insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (2,'rubro 3 objetivo 2',3,5)
insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (2,'rubro 4 objetivo 2',3,5)

insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (3,'rubro 1 objetivo 3',3,5)
insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (3,'rubro 2 objetivo 3',3,5)
insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (3,'rubro 3 objetivo 3',3,5)
insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (3,'rubro 4 objetivo 3',3,5)

insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (4,'rubro 1 objetivo 4',3,5)
insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (4,'rubro 2 objetivo 4',3,5)
insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (4,'rubro 3 objetivo 4',3,5)
insert into RubroEstrategia (Id_Objetivo,NombreRubroEstrategia,PorcentajeImportancia,CostoPermitidoRubro) values (4,'rubro 4 objetivo 4',3,5)



insert into RubroAccion(Id_Objetivo,nombreRubroAccion,PorcentajeImportancia,CostoPermitidoRubro) values (1,'rubro 1 objetivo 1',3,5)
insert into RubroAccion (Id_Objetivo,nombreRubroAccion,PorcentajeImportancia,CostoPermitidoRubro) values (1,'rubro 2 objetivo 1',3,5)
insert into RubroAccion (Id_Objetivo,nombreRubroAccion,PorcentajeImportancia,CostoPermitidoRubro) values (1,'rubro 4 objetivo 1',3,5)

insert into RubroAccion (Id_Objetivo,nombreRubroAccion,PorcentajeImportancia,CostoPermitidoRubro) values (2,'rubro 1 objetivo 2',3,5)
insert into RubroAccion (Id_Objetivo,nombreRubroAccion,PorcentajeImportancia,CostoPermitidoRubro) values (2,'rubro 2 objetivo 2',3,5)
insert into RubroAccion (Id_Objetivo,nombreRubroAccion,PorcentajeImportancia,CostoPermitidoRubro) values (2,'rubro 3 objetivo 2',3,5)

insert into RubroAccion (Id_Objetivo,nombreRubroAccion,PorcentajeImportancia,CostoPermitidoRubro) values (3,'rubro 1 objetivo 3',3,5)
insert into RubroAccion (Id_Objetivo,nombreRubroAccion,PorcentajeImportancia,CostoPermitidoRubro) values (3,'rubro 2 objetivo 3',3,5)

insert into RubroAccion (Id_Objetivo,nombreRubroAccion,PorcentajeImportancia,CostoPermitidoRubro) values (4,'rubro 1 objetivo 4',3,5)
insert into RubroAccion (Id_Objetivo,nombreRubroAccion,PorcentajeImportancia,CostoPermitidoRubro) values (4,'rubro 2 objetivo 4',3,5)
insert into RubroAccion (Id_Objetivo,nombreRubroAccion,PorcentajeImportancia,CostoPermitidoRubro) values (4,'rubro 3 objetivo 4',3,5)
insert into RubroAccion (Id_Objetivo,nombreRubroAccion,PorcentajeImportancia,CostoPermitidoRubro) values (4,'rubro 4 objetivo 4',3,5)


