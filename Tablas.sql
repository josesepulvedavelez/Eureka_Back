CREATE DATABASE Eureka;
USE Eureka;

CREATE TABLE Cliente
(
	Cc_Nit Varchar(20),
	Nombre_RazonSocial Varchar(100),
	Direccion VARCHAR(MAX),
	Telefono VARCHAR(50),
	Estado BIT,
	ClienteId INT IDENTITY(1, 1) PRIMARY KEY
);

CREATE TABLE Factura
(
	No VARCHAR(5),
	Fecha DATE,
	Total MONEY,

	ClienteId INT FOREIGN KEY REFERENCES Cliente(ClienteId),
	FacturaId INT IDENTITY(1, 1) PRIMARY KEY
);

CREATE TABLE Articulo
(
	Codigo VARCHAR(50),
	Descripcion VARCHAR(100),
	Costo MONEY,
	Porcentaje FLOAT,
	PrecioSugerido MONEY,
	Cantidad INT,
	Estado BIT,
	ArticuloId INT IDENTITY(1, 1) PRIMARY KEY
);

CREATE TABLE FacturaDetalle
(
	Precio MONEY,
	Cantidad INT,
	SubTotal MONEY,

	FacturaId INT FOREIGN KEY REFERENCES Factura(FacturaId),
	ArticuloId INT FOREIGN KEY REFERENCES Articulo(ArticuloId),
	FacturaDetalleId INT IDENTITY(1, 1) PRIMARY KEY
);
