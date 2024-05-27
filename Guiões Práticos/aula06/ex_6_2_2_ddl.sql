CREATE SCHEMA Stocks;
GO

CREATE TABLE tipo_fornecedor(
	codigo INT NOT NULL CHECK(codigo> 0),
	designacao	VARCHAR(20),
	PRIMARY KEY(codigo),
	UNIQUE(codigo),
);

CREATE TABLE fornecedor(
	nif INT NOT NULL CHECK(nif> 0),	
	nome VARCHAR(32) NOT NULL,
	fax INT NOT NULL,	
	endereco VARCHAR(64) NOT NULL,
	condpag VARCHAR(8) NOT NULL,
	tipo INT NOT NULL,
	PRIMARY KEY(nif),
	UNIQUE(nif),
	FOREIGN KEY(tipo) REFERENCES tipo_fornecedor(codigo)
);

CREATE TABLE produto(
	codigo INT NOT NULL CHECK(codigo> 0),	
	nome VARCHAR(32) NOT NULL,
	preco MONEY NOT NULL CHECK(preco> 0),	
	iva INT NOT NULL,
	unidades INT CHECK(unidades> 0) NOT NULL,
	PRIMARY KEY(codigo),
	UNIQUE(codigo)
);

CREATE TABLE encomenda(
	numero INT NOT NULL CHECK(numero> 0),	
	data DATE NOT NULL,
	fornecedor INT NOT NULL,
	PRIMARY KEY(numero),
	UNIQUE(numero),
	FOREIGN KEY(fornecedor) REFERENCES fornecedor(nif)
);

CREATE TABLE item(
	numEnc INT,	
	codProd INT,
	unidades INT,
	FOREIGN KEY(numEnc) REFERENCES encomenda(numero),
	FOREIGN KEY(codProd) REFERENCES produto(codigo),
);