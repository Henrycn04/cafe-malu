CREATE DATABASE [cafeMalu];
USE [cafeMalu];

GO
CREATE TABLE Clientes (
	ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Nombre NVARCHAR(40) NOT NULL,
	ClienteBusqueda NVARCHAR(40),
	Frecuencia INT,
	NumeroTelefonico CHAR(12),
);

CREATE TABLE Cafe (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Nombre NVARCHAR(40) NOT NULL, -- Columna tipo formato: TipoDeCafe Peso Unidades
	Peso DECIMAL(8,2) NOT NULL, -- En kilos
	Especialidad BIT NOT NULL, -- Estandar o especialidad
	PrecioUnitario DECIMAL(12,2),
);

CREATE TABLE Ventas (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	IDCliente INT NOT NULL,
	Fecha DATETIME NOT NULL DEFAULT GETDATE(),
	Semana TINYINT NOT NULL,
	IDCafe INT NOT NULL,
	Cantidad INT NOT NULL,
	PrecioTotal DECIMAL(12,2) NOT NULL,
	Pago DECIMAL(12,2), -- Es igual a ganancia
	Saldo DECIMAL(12,2), -- Se puede trabajar como un saldo total por Cliente
	Debe BIT DEFAULT 0,
	PesoTotal DECIMAL(8,2),
	FOREIGN KEY (IDCliente) REFERENCES Clientes(ID),
	FOREIGN KEY (IDCafe) REFERENCES Cafe(ID)
);



