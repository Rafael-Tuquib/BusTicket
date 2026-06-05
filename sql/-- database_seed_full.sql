-- database_seed_full.sql
-- Creates tables and seeds municipalities + barangays used by the application seeder.
-- Run on SQL Server. Adjust database context as needed (e.g., USE YourDatabase).

SET XACT_ABORT ON;
BEGIN TRANSACTION;

-- Create tables
IF OBJECT_ID('dbo.Tickets', 'U') IS NOT NULL DROP TABLE dbo.Tickets;
IF OBJECT_ID('dbo.Barangays', 'U') IS NOT NULL DROP TABLE dbo.Barangays;
IF OBJECT_ID('dbo.Municipalities', 'U') IS NOT NULL DROP TABLE dbo.Municipalities;

CREATE TABLE dbo.Municipalities (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL
);

CREATE TABLE dbo.Barangays (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MunicipalityId INT NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    CONSTRAINT FK_Barangays_Municipalities FOREIGN KEY (MunicipalityId) REFERENCES dbo.Municipalities(Id) ON DELETE CASCADE
);

CREATE TABLE dbo.Tickets (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FromBarangayId INT NOT NULL,
    ToBarangayId INT NOT NULL,
    Distance FLOAT NOT NULL,
    Fare DECIMAL(18,2) NOT NULL,
    DateCreated DATETIME2 NOT NULL,
    CONSTRAINT FK_Tickets_FromBarangay FOREIGN KEY (FromBarangayId) REFERENCES dbo.Barangays(Id),
    CONSTRAINT FK_Tickets_ToBarangay FOREIGN KEY (ToBarangayId) REFERENCES dbo.Barangays(Id)
);

-- Insert municipalities (Tagbilaran City + 47 municipalities)
INSERT INTO dbo.Municipalities (Name) VALUES
('Tagbilaran City'),
('Alburquerque'),
('Alicia'),
('Anda'),
('Antequera'),
('Baclayon'),
('Balilihan'),
('Batuan'),
('Bien Unido'),
('Bilar'),
('Buenavista'),
('Calape'),
('Candijay'),
('Carmen'),
('Catigbian'),
('Clarin'),
('Corella'),
('Cortes'),
('Dagohoy'),
('Danao'),
('Dauis'),
('Dimiao'),
('Duero'),
('Garcia Hernandez'),
('Getafe'),
('Guindulman'),
('Inabanga'),
('Jagna'),
('Lila'),
('Loay'),
('Loboc'),
('Loon'),
('Mabini'),
('Maribojoc'),
('Panglao'),
('Pilar'),
('President Carlos P. Garcia'),
('Sagbayan'),
('San Isidro'),
('San Miguel'),
('Sevilla'),
('Sierra Bullones'),
('Sikatuna'),
('Talibon'),
('Trinidad'),
('Tubigon'),
('Ubay'),
('Valencia');

-- Insert barangays for Tagbilaran City
DECLARE @Id INT;

SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Tagbilaran City';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Bool'),
(@Id, 'Cogon'),
(@Id, 'Dao'),
(@Id, 'Tampakes'),
(@Id, 'San Roque'),
(@Id, 'Poblacion I'),
(@Id, 'Poblacion II');

-- Alburquerque
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Alburquerque';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Cambang'),
(@Id, 'Cantagay'),
(@Id, 'Cabacnitan'),
(@Id, 'Pawa');

-- Alicia
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Alicia';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'San Pedro'),
(@Id, 'Bulwang'),
(@Id, 'Hagdan'),
(@Id, 'Magsaysay');

-- Anda
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Anda';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion (Centro)'),
(@Id, 'Napo'),
(@Id, 'Cabatuan'),
(@Id, 'Candabong'),
(@Id, 'Lungsodaan'),
(@Id, 'Taloto');

-- Antequera
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Antequera';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Guintaboan'),
(@Id, 'Poblacion Norte'),
(@Id, 'Poblacion Sur');

-- Baclayon
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Baclayon';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Lotlot'),
(@Id, 'Hicgop'),
(@Id, 'Punta Cruz');

-- Balilihan
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Balilihan';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Cayacap'),
(@Id, 'San Isidro'),
(@Id, 'Guindacpan');

-- Batuan
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Batuan';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Danao'),
(@Id, 'Napo'),
(@Id, 'San Pedro');

-- Bien Unido
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Bien Unido';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Haybay'),
(@Id, 'Bien Unido (Bontulan)'),
(@Id, 'Candabong');

-- Bilar
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Bilar';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Pantudlan'),
(@Id, 'Tiptip'),
(@Id, 'Bunga');

-- Buenavista
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Buenavista';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Cabbac'),
(@Id, 'San Jose'),
(@Id, 'Santa Cruz');

-- Calape
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Calape';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Cangmating'),
(@Id, 'Caticugan'),
(@Id, 'Looc');

-- Candijay
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Candijay';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Can-uba'),
(@Id, 'San Isidro'),
(@Id, 'Cambangay');

-- Carmen
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Carmen';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Carpenter'),
(@Id, 'Tagbilaran (Carmen)'),
(@Id, 'Looc'),
(@Id, 'Sagbayan');

-- Catigbian
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Catigbian';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Handig'),
(@Id, 'Caganhao'),
(@Id, 'Kamang');

-- Clarin
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Clarin';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Cabilao (island)'),
(@Id, 'Langtad'),
(@Id, 'Cagting');

-- Corella
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Corella';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion (Corella)'),
(@Id, 'Tigbao'),
(@Id, 'Can-apoy');

-- Cortes
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Cortes';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Santol'),
(@Id, 'Looc');

-- Dagohoy
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Dagohoy';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'New Dagohoy'),
(@Id, 'Cansuso');

-- Danao
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Danao';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Lubang'),
(@Id, 'Aguisan');

-- Dauis
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Dauis';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Tabalong'),
(@Id, 'San Vicente'),
(@Id, 'Punta Cruz');

-- Dimiao
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Dimiao';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Can-uba'),
(@Id, 'Guintatona');

-- Duero
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Duero';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Looc'),
(@Id, 'Catagbacan');

-- Garcia Hernandez
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Garcia Hernandez';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Cabano'),
(@Id, 'Imelda');

-- Getafe
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Getafe';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Mabini'),
(@Id, 'Santa Cruz'),
(@Id, 'Lungsodaan');

-- Guindulman
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Guindulman';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Cambugason'),
(@Id, 'Cansologo');

-- Inabanga
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Inabanga';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Danao'),
(@Id, 'Cambante'),
(@Id, 'San Antonio');

-- Jagna
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Jagna';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Can-uba'),
(@Id, 'Guinarona'),
(@Id, 'Cabungaan');

-- Lila
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Lila';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Agsupa'),
(@Id, 'Napo');

-- Loay
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Loay';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Quinapondan'),
(@Id, 'Naic');

-- Loboc
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Loboc';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'La Paz'),
(@Id, 'Fatima');

-- Loon
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Loon';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Ahan'),
(@Id, 'Calape'),
(@Id, 'Cabilao');

-- Mabini
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Mabini';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Haguimit'),
(@Id, 'Can-uba');

-- Maribojoc
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Maribojoc';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Guinsaan'),
(@Id, 'San Roque');

-- Panglao
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Panglao';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Doljo'),
(@Id, 'Danao'),
(@Id, 'Tawala'),
(@Id, 'Poblacion'),
(@Id, 'Laum'),
(@Id, 'Bohol');

-- Pilar
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Pilar';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Panagsama'),
(@Id, 'Canlao');

-- President Carlos P. Garcia
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'President Carlos P. Garcia';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Loma'),
(@Id, 'Tugas');

-- Sagbayan
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Sagbayan';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Paye'),
(@Id, 'Tagbilaran');

-- San Isidro
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'San Isidro';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Guindulman'),
(@Id, 'Cabuloan');

-- San Miguel
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'San Miguel';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Can-uba'),
(@Id, 'Cabungaan');

-- Sevilla
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Sevilla';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Tugas'),
(@Id, 'Lungsodaan');

-- Sierra Bullones
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Sierra Bullones';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Guindulman'),
(@Id, 'Cansalo');

-- Sikatuna
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Sikatuna';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Canman'),
(@Id, 'Tagbilaran');

-- Talibon
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Talibon';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'San Isidro'),
(@Id, 'Looc'),
(@Id, 'Atabay');

-- Trinidad
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Trinidad';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Sikatuna'),
(@Id, 'Danao'),
(@Id, 'Poblacion');

-- Tubigon
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Tubigon';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Suba'),
(@Id, 'Looc'),
(@Id, 'San Isidro');

-- Ubay
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Ubay';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Dagohoy'),
(@Id, 'Guindacpan'),
(@Id, 'San Pascual');

-- Valencia
SELECT @Id = Id FROM dbo.Municipalities WHERE Name = 'Valencia';
INSERT INTO dbo.Barangays (MunicipalityId, Name) VALUES
(@Id, 'Poblacion'),
(@Id, 'Bagacay'),
(@Id, 'Valencia East');

COMMIT TRANSACTION;

PRINT 'Database created and seeded successfully.';