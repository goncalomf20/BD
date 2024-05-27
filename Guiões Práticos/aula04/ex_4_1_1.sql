USE p8g10
GO

CREATE TABLE RentACar_Balcao (
	[Numero] [int] NOT NULL PRIMARY KEY,
	[Nome] [varchar](256) NOT NULL,
	[Endereco] [varchar](1024) NOT NULL, 
)
GO

CREATE TABLE RentACar_Cliente(
[NIF] [int] NOT NULL PRIMARY KEY,
[Nome] [varchar](256) NOT NULL,
[Endereco] [varchar](1024) NOT NULL, 
[Num_Carta] [varchar](32) NOT NULL,
)
GO

CREATE TABLE RentACar_TipoVeiculo(
[Codigo] [int] NOT NULL IDENTITY PRIMARY KEY,
[Designacao] [varchar](128) NOT NULL,
[ArCondicionado] [bit] NOT NULL,
)
GO

CREATE TABLE RentACar_Veiculo(
[Matricula] [varchar](16) NOT NULL PRIMARY KEY,
[Ano] [int] NOT NULL,
[Marca] [varchar](128) NOT NULL, 
[TipoVeiculo_Código] [int] NOT NULL REFERENCES RentACar_TipoVeiculo ([Codigo]),
)
GO

CREATE TABLE RentACar_Aluguer(
[Numero] [int] NOT NULL PRIMARY KEY,
[Duracao] [int] NOT NULL,
[Data] [datetime] NOT NULL, 
[Cliente_NIF] [int] NOT NULL REFERENCES RentACar_Cliente ([NIF]), 
[Balcao_Num] [int] NOT NULL REFERENCES RentACar_Balcao ([Numero]),
[Vei_Matricula] [varchar](16) NOT NULL REFERENCES RentACar_Veiculo ([Matricula]),
)
GO

CREATE TABLE RentACar_Similaridade(
[Codigo1] [int] NOT NULL REFERENCES [RentACar_TipoVeiculo] ([Codigo]),
[Codigo2] [int] NOT NULL REFERENCES [RentACar_TipoVeiculo] ([Codigo]),
PRIMARY KEY ([Codigo1],[Codigo2])
)
GO

CREATE TABLE RentACar_Ligeiro(
[Codigo] [int] NOT NULL PRIMARY KEY REFERENCES [RentACar_TipoVeiculo] ([Codigo]),
[Combustivel] [varchar] (32) NOT NULL,
[Portas] [int] NOT NULL,
[Lugares] [int] NOT NULL,
)
GO

CREATE TABLE RentACar_Pesado(
[Codigo] [int] NOT NULL PRIMARY KEY REFERENCES [RentACar_TipoVeiculo] ([Codigo]),
[Passageiros] [int] NOT NULL,
[Peso] [int] NOT NULL,
)
GO

