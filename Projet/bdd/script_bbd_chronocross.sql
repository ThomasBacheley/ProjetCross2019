---------------------------------------------------------------------------------
IF EXISTS(SELECT name FROM sys.databases WHERE name = 'Chronocross')
DROP DATABASE Chronocross;
GO

---------------------------------------------------------------------------------
create database [Chronocross]
GO
---------------------------------------------------------------------------------
USE [Chronocross]
GO

CREATE TABLE [dbo].[table_categorie](
	[id_categorie] [int] IDENTITY(1,1) NOT NULL,
	[categorie] [varchar](15) NOT NULL,
	[annee_min] [int] NOT NULL,
	[annee_max] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_categorie] ASC
)
) ON [PRIMARY]
GO
---------------------------------------------------------------------------------
CREATE TABLE [dbo].[table_classe](
	[id_classe] [int] IDENTITY(1,1) NOT NULL,
	[classe] [varchar](15) NOT NULL,
	[unite_pedagogique] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_classe] ASC
)
) ON [PRIMARY]
GO
---------------------------------------------------------------------------------
CREATE TABLE [dbo].[table_eleve](
	[id_eleve] [int] IDENTITY(1,1) NOT NULL,
	[idcategorie] [int] NOT NULL,
	[idclasse] [int] NOT NULL,
	[nom] [varchar](50) NOT NULL,
	[prenom] [varchar](50) NOT NULL,
	[date_naissance] [date] NOT NULL,
	[genre] [varchar](1) NOT NULL,
	[temps] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_eleve] ASC
)
) ON [PRIMARY]
GO
---------------------------------------------------------------------------------
CREATE TABLE [dbo].[table_inscrit](
	[id_inscrit] [int] IDENTITY(1,1) NOT NULL,
	[ideleve] [int] NOT NULL,
	[idtranspondeur] [int] NOT NULL,
	[numero_dossard] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_inscrit] ASC
)
) ON [PRIMARY]
GO
---------------------------------------------------------------------------------
CREATE TABLE [dbo].[table_transpondeur](
	[id_transpondeur] [int] IDENTITY(1,1) NOT NULL,
	[tag] [varchar](10) NOT NULL,
	[rendu] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_transpondeur] ASC
)
) ON [PRIMARY]
GO
---------------------------------------------------------------------------------
ALTER TABLE [dbo].[table_transpondeur] ADD  DEFAULT ((1)) FOR [rendu]
GO
---------------------------------------------------------------------------------
ALTER TABLE [dbo].[table_eleve]  WITH CHECK ADD FOREIGN KEY([idcategorie])
REFERENCES [dbo].[table_categorie] ([id_categorie])
GO
---------------------------------------------------------------------------------
ALTER TABLE [dbo].[table_eleve]  WITH CHECK ADD FOREIGN KEY([idclasse])
REFERENCES [dbo].[table_classe] ([id_classe])
GO
---------------------------------------------------------------------------------
ALTER TABLE [dbo].[table_inscrit]  WITH CHECK ADD FOREIGN KEY([ideleve])
REFERENCES [dbo].[table_eleve] ([id_eleve])
GO
---------------------------------------------------------------------------------
ALTER TABLE [dbo].[table_inscrit]  WITH CHECK ADD FOREIGN KEY([idtranspondeur])
REFERENCES [dbo].[table_transpondeur] ([id_transpondeur])
GO
---------------------------------------------------------------------------------
ALTER TABLE [dbo].[table_eleve]  WITH CHECK ADD CHECK  (([genre]='M' OR [genre]='F'))
GO
---------------------------------------------------------------------------------
ALTER TABLE [dbo].[table_classe]  WITH CHECK ADD CHECK  (([unite_pedagogique]='LA' OR [unite_pedagogique]='LGT' OR [unite_pedagogique]='LP' OR [unite_pedagogique]='BTS' OR [unite_pedagogique]='UFA' OR [unite_pedagogique]='Personnel'))
GO
---------------------------------------------------------------------------------
USE [master]
GO
ALTER DATABASE [Chronocross] SET  READ_WRITE 
GO
---------------------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'Cross')
DROP LOGIN [Cross]
GO

CREATE LOGIN [Cross] WITH PASSWORD=N'Password1234', DEFAULT_DATABASE=[Chronocross], DEFAULT_LANGUAGE=[Français], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO

ALTER LOGIN [Cross] ENABLE
GO

USE [Chronocross]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
USE [Chronocross]
CREATE USER [Cross] FOR LOGIN [Cross]
EXEC sp_addrolemember 'db_datareader', 'Cross'
EXEC sp_addrolemember 'db_datawriter', 'Cross'
GO

GRANT EXECUTE TO [Cross];
GO

---------------------------------------------------------------------------------
use Chronocross
INSERT INTO table_categorie(categorie,annee_min,annee_max) VALUES ('Seniors-Masters',YEAR(getDate()) - 65,YEAR(getDate()) - 23);
INSERT INTO table_categorie(categorie,annee_min,annee_max) VALUES ('Espoirs',YEAR(getDate()) - 22,YEAR(getDate()) - 20);
INSERT INTO table_categorie(categorie,annee_min,annee_max) VALUES ('Juniors',YEAR(getDate()) - 19,YEAR(getDate()) - 18);
INSERT INTO table_categorie(categorie,annee_min,annee_max) VALUES ('Cadets',YEAR(getDate()) - 17,YEAR(getDate()) - 16);
INSERT INTO table_categorie(categorie,annee_min,annee_max) VALUES ('Minimes',YEAR(getDate()) - 15,YEAR(getDate()) - 14);
---
INSERT INTO table_classe(classe,unite_pedagogique) VALUES ('Personnel','Personnel');
GO
---------------------------------------------------------------------------------
use Chronocross
GO

IF EXISTS (SELECT * from sys.objects WHERE type = 'P' AND name ='enregistrement_eleve')
DROP PROCEDURE enregistrement_eleve
GO

CREATE PROCEDURE [dbo].[enregistrement_eleve]
	@nom varchar(50),
	@prenom varchar(50),
	@genre varchar(1),
	@date_n date,
	@classe varchar(15),
	@up varchar(10)
AS
---
DECLARE
@id_cl int,
@id_cat int

BEGIN TRANSACTION 
SELECT @id_cl=id_classe FROM table_classe WHERE classe = @classe
IF(@@ROWCOUNT=0)
	BEGIN
		INSERT INTO table_classe (classe,unite_pedagogique) VALUES (@classe,@up);
		SELECT @id_cl=id_classe FROM table_classe WHERE classe = @classe
		IF(@@ROWCOUNT=0)
			BEGIN
				ROLLBACK
				return 1 -- la classe n'a pas été insérée
			END
	END
---
SELECT @id_cat=id_categorie from table_categorie WHERE YEAR(@date_n) BETWEEN annee_min AND annee_max
IF(@@ROWCOUNT=0)
	BEGIN
		ROLLBACK
		return 2 -- la catégorie n'a pas été trouvée
	END
---
SELECT id_eleve FROM table_eleve WHERE nom=@nom AND prenom=@prenom AND date_naissance=@date_n
IF(@@ROWCOUNT!=0)
	BEGIN
		ROLLBACK
		return 3 -- l'élève existe déja
	END
---
INSERT INTO table_eleve (idcategorie,idclasse,nom,prenom,date_naissance,genre) VALUES (@id_cat,@id_cl,@nom,@prenom,@date_n,@genre);
SELECT id_eleve FROM table_eleve WHERE nom=@nom AND prenom=@prenom AND date_naissance=@date_n
IF(@@ROWCOUNT=0)
	BEGIN
		ROLLBACK
		return 4 -- l'élève n'a pas été ajouté
	END
COMMIT
return 0 -- tout à marcher
GO
---------------------------------------------------------------------------------
use Chronocross
GO

IF EXISTS (SELECT * from sys.objects WHERE type = 'P' AND name ='inscription')
DROP PROCEDURE inscription
GO

CREATE PROCEDURE [dbo].[inscription]
	@id_e int,
	@num_dos int,
	@tag varchar(10)
AS
DECLARE
	@id_t int
---
BEGIN TRANSACTION 
SELECT id_inscrit FROM table_inscrit WHERE ideleve=@id_e
IF(@@ROWCOUNT!=0)
	BEGIN
		ROLLBACK
		return 1 -- l'eleve est deja inscrit
	END
---
SELECT @id_t=id_transpondeur FROM table_transpondeur WHERE tag=@tag
IF(@@ROWCOUNT=0)
	BEGIN
		INSERT INTO table_transpondeur (tag,rendu) VALUES (@tag,0);
		SELECT @id_t=id_transpondeur FROM table_transpondeur WHERE tag=@tag
		IF(@@ROWCOUNT=0)
			BEGIN
				ROLLBACK
				return 2 -- le bagde na pas été ajouté
			END
	END
UPDATE table_transpondeur set rendu=0 WHERE id_transpondeur=@id_t;
---
SELECT numero_dossard FROM table_inscrit WHERE numero_dossard=@num_dos
IF(@@ROWCOUNT!=0)
	BEGIN
		ROLLBACK
		return 3 -- le numero de dossard est déja pris
	END
---
INSERT INTO table_inscrit (ideleve,idtranspondeur,numero_dossard) VALUES (@id_e,@id_t,@num_dos);
SELECT id_inscrit FROM table_inscrit WHERE ideleve=@id_e AND idtranspondeur=@id_t AND numero_dossard=@num_dos;
IF(@@ROWCOUNT=0)
	BEGIN
		ROLLBACK
		return 4 -- l'inscription n'a pas aboutie
	END
COMMIT
return 0 -- tout à marcher
GO
---------------------------------------------------------------------------------
use Chronocross
GO

IF EXISTS (SELECT * from sys.objects WHERE type = 'P' AND name ='update_temps')
DROP PROCEDURE update_temps
GO

CREATE PROCEDURE [dbo].[update_temps]
@tag varchar(10),
@t time
AS
DECLARE
	@id_t int,
	@id_e int
---
BEGIN TRANSACTION 
SELECT @id_t=id_transpondeur FROM table_transpondeur WHERE tag=@tag
IF(@@ROWCOUNT=0)
	BEGIN
		ROLLBACK
		return 1 -- le badge n'est pas répétorié
	END
---
SELECT @id_e=ideleve from table_inscrit WHERE idtranspondeur=@id_t
IF(@@ROWCOUNT=0)
	BEGIN
		ROLLBACK
		return 2 -- l'eleve n'a pas était retrouvé
	END
---
UPDATE table_eleve SET temps=@t WHERE id_eleve=@id_e
IF(@@ROWCOUNT=0)
	BEGIN
		ROLLBACK
		return 3 -- le temps n'a pas était mis a jour
	END
COMMIT
return 0 -- tout à marcher
GO
---------------------------------------------------------------------------------
use Chronocross
GO

IF EXISTS (SELECT * from sys.objects WHERE type = 'P' AND name ='clear_table')
DROP PROCEDURE clear_table
GO

CREATE PROCEDURE [dbo].[clear_table]
AS
---
BEGIN TRANSACTION 
---
BEGIN TRY
---
DELETE from table_inscrit;
DELETE from table_eleve;
DELETE from table_classe WHERE classe!='Personnel';
---
UPDATE table_categorie
SET annee_min = YEAR(getDate()) - 65, annee_max = YEAR(getDate()) - 23 WHERE categorie='Seniors-Masters';
UPDATE table_categorie
SET annee_min = YEAR(getDate()) - 22, annee_max = YEAR(getDate()) - 20 WHERE categorie='Espoirs';
UPDATE table_categorie
SET annee_min = YEAR(getDate()) - 19, annee_max = YEAR(getDate()) - 18 WHERE categorie='Juniors';
UPDATE table_categorie
SET annee_min = YEAR(getDate()) - 17, annee_max = YEAR(getDate()) - 16 WHERE categorie='Cadets';
UPDATE table_categorie
SET annee_min = YEAR(getDate()) - 15, annee_max = YEAR(getDate()) - 14 WHERE categorie='Minimes';
COMMIT
END TRY
BEGIN CATCH
	ROLLBACK
END CATCH
GO
---------------------------------------------------------------------------------