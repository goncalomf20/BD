CREATE TABLE Stocks_Armazem(
	[Codigo] [varchar] (8) NOT NULL PRIMARY KEY,
	[Morada] [varchar] (64) NOT NULL,
)
GO

CREATE TABLE Stocks_Produto(
	[Armazem_Codigo] [varchar] (8) NOT NULL REFERENCES [Stocks_Armazem] ([Codigo]),
	[Codigo] [varchar] (8) NOT NULL PRIMARY KEY,
	[Nome] [varchar] (32) NOT NULL,
	[IVA] [float] NOT NULL,
	[Preço] [money] NOT NULL, 
)
GO

CREATE TABLE Stocks_TipoFornecedor(
	[Codigo] [varchar] (8) NOT NULL PRIMARY KEY,
	[Nome] [varchar] (32) NOT NULL,
)
GO

CREATE TABLE Stocks_CondicoesPagamento(
	[Codigo] [varchar] (8) NOT NULL PRIMARY KEY,
	[Nome] [varchar] (32) NOT NULL,
	[Prazo] [datetime] NOT NULL,
)
GO

CREATE TABLE Stocks_Fornecedor(
	[TipoFornecedor_Codigo] [varchar] (8) NOT NULL REFERENCES [Stocks_TipoFornecedor]([Codigo]),
	[CondicoesPagamento_Codigo] [varchar] (8) NOT NULL REFERENCES [Stocks_CondicoesPagamento]([Codigo]),
	[Codigo] [varchar] (8) NOT NULL PRIMARY KEY,
	[Nome] [varchar] (32) NOT NULL,
	[Fax] [int] NOT NULL,
	[NIF] [int] NOT NULL,
	[Morada] [varchar] (64) NOT NULL,
)
GO

CREATE TABLE Stocks_Encomenda(
	[Fornecedor_Codigo] [varchar] (8) NOT NULL REFERENCES [Stocks_Fornecedor]([Codigo]),
	[Numero] [int] NOT NULL PRIMARY KEY,
	[Date] [datetime] NOT NULL,
	[Quantidade] [int] NOT NULL,
)
GO

CREATE TABLE Stocks_Tem(
	[Produto_Codigo] [varchar] (8) NOT NULL REFERENCES [Stocks_Produto]([Codigo]),
	[Encomenda_Numero] [int] NOT NULL REFERENCES [Stocks_Encomenda]([Numero]),
)
GO