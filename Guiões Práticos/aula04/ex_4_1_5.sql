CREATE TABLE Conferencias_Conferencia(
	[Codigo] [varchar] (8) NOT NULL PRIMARY KEY,
)
GO

CREATE TABLE Conferencias_Artigos(
	[Conferencia_Codigo] [varchar] (8) NOT NULL REFERENCES [Conferencias_Conferencia] ([Codigo]),
	[Codigo] [varchar] (8) NOT NULL PRIMARY KEY,
	[Titulo] [varchar] (128) NOT NULL,
	[Numero_Registo] [int] NOT NULL,
)
GO

CREATE TABLE Conferencias_Instituicao(
	[Codigo] [varchar] (8) NOT NULL PRIMARY KEY,
	[Nome] [varchar] (64) NOT NULL,
	[Morada] [varchar] (128) NOT NULL,
)
GO

CREATE TABLE Conferencias_Pessoa(
	[Instituicao_Codigo] [varchar] (8) NOT NULL REFERENCES [Conferencias_Instituicao] ([Codigo]),
	[Codigo] [varchar] (8) NOT NULL PRIMARY KEY,
	[Nome] [varchar] (64) NOT NULL,
	[Email] [varchar] (32) NOT NULL,
)
GO

CREATE TABLE Conferencias_Autor(
	[Codigo] [varchar] (8) NOT NULL PRIMARY KEY REFERENCES [Conferencias_Pessoa] ([Codigo]),
)
GO

CREATE TABLE Conferencias_Participante(
	[Codigo] [varchar] (8) NOT NULL PRIMARY KEY REFERENCES [Conferencias_Pessoa] ([Codigo]),
	[Morada] [varchar] (128) NOT NULL,
	[Data_Inscricao] [datetime] NOT NULL,
)
GO

CREATE TABLE Conferencias_Estudante(
	[Codigo] [varchar] (8) NOT NULL REFERENCES [Conferencias_Participante] ([Codigo]),
	[Localizacao_Eletronica_Comprovativo] [varchar](128) NOT NULL,
	PRIMARY KEY ([Codigo], [Localizacao_Eletronica_Comprovativo])
)
GO

CREATE TABLE Conferencias_NaoEstudante(
	[Codigo] [varchar] (8) NOT NULL REFERENCES [Conferencias_Participante] ([Codigo]),
	[Referencia_Bancaria_Inscricao] [varchar] (32) NOT NULL,
	PRIMARY KEY ([Codigo], [Referencia_Bancaria_Inscricao])
)
GO

CREATE TABLE Conferencias_Tem(
	[Autor_Codigo] [varchar] (8) NOT NULL REFERENCES [Conferencias_Autor] ([Codigo]),
	[Artigos_Codigo] [varchar] (8) NOT NULL REFERENCES [Conferencias_Artigos] ([Codigo]),
	PRIMARY KEY ([Autor_Codigo], [Artigos_Codigo])
)
GO