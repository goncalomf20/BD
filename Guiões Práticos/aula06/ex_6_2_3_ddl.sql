CREATE SCHEMA Prescricao;
GO
--CREATE DATABASE Prescricao;

CREATE TABLE Medico(
    numSNS              INT         PRIMARY KEY,
    nome                VARCHAR(45) NOT NULL,
    especialidade       VARCHAR(45),                      
);

CREATE TABLE Paciente(
    numUtente           INT         PRIMARY KEY,
    nome                VARCHAR(45) NOT NULL,
    dataNasc            DATE        NOT NULL,
    endereco            TEXT,
);

CREATE TABLE Farmacia(
    nome                VARCHAR(45) PRIMARY KEY,
    telefone            INT         UNIQUE,
    endereco            TEXT,                      
);

CREATE TABLE Farmaceutica(
    numReg              INT         PRIMARY KEY,
    nome                VARCHAR(45),
    endereco            TEXT,                      
);

CREATE TABLE Farmaco(
    numRegFarm          INT,
    nome                VARCHAR(45),
    formula             TEXT,                      
    PRIMARY KEY (numRegFarm, nome),
	FOREIGN KEY (numRegFarm)REFERENCES Farmaceutica(numReg) ON DELETE NO ACTION ON UPDATE CASCADE,
);

CREATE TABLE Prescricao(
    numPresc            INT         PRIMARY KEY,
    numUtente           INT         NOT NULL REFERENCES Paciente(numUtente) ON DELETE NO ACTION ON UPDATE CASCADE,
    numMedico           INT         NOT NULL REFERENCES Medico(numSNS) ON DELETE NO ACTION ON UPDATE CASCADE,
    farmacia            VARCHAR(45) REFERENCES Farmacia(nome) ON DELETE NO ACTION ON UPDATE CASCADE,
    dataProc            DATE,
);

CREATE TABLE Presc_farmaco(
    numPresc            INT	REFERENCES Prescricao(numPresc) ON DELETE NO ACTION ON UPDATE CASCADE,
    numRegFarm          INT,
    nomeFarmaco         VARCHAR(45),
    FOREIGN KEY (numRegFarm, nomeFarmaco) REFERENCES Farmaco(numRegFarm, nome) ON DELETE NO ACTION ON UPDATE CASCADE,
    PRIMARY KEY (numPresc, numRegFarm, nomeFarmaco),
);