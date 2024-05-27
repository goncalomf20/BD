USE p8g10
GO

CREATE TABLE ATL_Pessoa(
[Numero_CC] [varchar](32) NOT NULL PRIMARY KEY,
[Telefone] [int] NOT NULL,
[Nome] [varchar](64) NOT NULL,
[Morada] [varchar](128) NOT NULL,
[Email] [varchar](128) NOT NULL,
[Data_Nascimento] [datetime] NOT NULL,
)
GO

CREATE TABLE ATL_Professor(
[Numero_CC] [varchar](32) NOT NULL REFERENCES ATL_Pessoa ([Numero_CC]),
[Numero_Funcionario] [int] NOT NULL PRIMARY KEY,
)
GO

CREATE TABLE ATL_Encarregado(
[Numero_CC] [varchar](32) NOT NULL PRIMARY KEY REFERENCES ATL_Pessoa ([Numero_CC]),
)
GO

CREATE TABLE ATL_Responsavel(
[Numero_CC] [varchar](32) NOT NULL PRIMARY KEY REFERENCES ATL_Encarregado ([Numero_CC]),
)
GO




CREATE TABLE ATL_Turma(
[ID_Turma] [int] NOT NULL PRIMARY KEY,
[ID_Professor] [int] NOT NULL REFERENCES ATL_Professor ([Numero_Funcionario]),
[Ano] [int] NOT NULL,
[Classe] [int] NOT NULL ,
[Max_Alunos] [int] NOT NULL,
)
GO

CREATE TABLE ATL_Atividade(
[ID_Atividade] [int] NOT NULL PRIMARY KEY,
[Designacao] [varchar](128) NOT NULL,
[Custo] [int] NOT NULL, 
)
GO

CREATE TABLE ATL_Aluno(
[Numero_CC] [varchar](32) NOT NULL PRIMARY KEY REFERENCES ATL_Pessoa ([Numero_CC]),
[Data_Nascimento] [datetime] NOT NULL, 
[Morada] [varchar](128) NOT NULL,
[Nome] [varchar](64) NOT NULL ,
[ID_Professor_Responsavel] [int] NOT NULL REFERENCES ATL_Professor ([Numero_Funcionario]),
)
GO

CREATE TABLE ATL_Realiza(
[ID_Atibidade] [int] NOT NULL REFERENCES ATL_Atividade ([Id_Atividade]),
[ID_Tutma] [int] NOT NULL REFERENCES ATL_Turma ([ID_Turma]),
[ID_Professor] [int] NOT NULL REFERENCES ATL_Professor ([Numero_Funcionario]),

)
GO



