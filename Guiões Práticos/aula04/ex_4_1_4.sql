USE p8g10
GO

CREATE TABLE SNS_Farmacia(
[NIF] [int] NOT NULL IDENTITY PRIMARY KEY,
[Telefone] [int] NOT NULL,
[Nome] [varchar](64) NOT NULL,
[Endereco] [varchar](128) NOT NULL,
)
GO

CREATE TABLE SNS_Paciente(
[Numero_Utente] [int] NOT NULL PRIMARY KEY,
[Nome] [varchar](64) NOT NULL,
[Data_Nascimento] [datetime] NOT NULL, 
[Endereco] [varchar](128) NOT NULL,
)
GO


CREATE TABLE SNS_Medico (
[Numero_ID] [int] NOT NULL PRIMARY KEY,
[Nome] [varchar](64) NOT NULL,
[Especialidade] [varchar](64) NOT NULL, 
)
GO


CREATE TABLE SNS_Prescricao(
[Num_Unico] [int] NOT NULL PRIMARY KEY,
[Data] [datetime] NOT NULL,
[Num_Utente] [int] NOT NULL REFERENCES SNS_Paciente ([Numero_Utente]),
[Num_ID_Medico] [int] NOT NULL REFERENCES SNS_Medico ([Numero_ID]),
[NIF_Farmacia] [int] NOT NULL REFERENCES SNS_Farmacia ([NIF]),
)
GO

CREATE TABLE SNS_Farmaco(
[Nome_Comercial] [varchar](128) NOT NULL PRIMARY KEY,
[Formula] [nvarchar](256) NOT NULL,
[Num_Unico_Prescricao] [int] NOT NULL REFERENCES SNS_Prescricao ([Num_Unico]),
)
GO

CREATE TABLE SNS_Farmaceutica(
[Numero_Registo_Nacional] [int] NOT NULL PRIMARY KEY,
[Nome] [varchar](64) NOT NULL,
[Telefone] [int] NOT NULL, 
[Endereco] [varchar](128) NOT NULL,
)
GO

CREATE TABLE SNS_Produz(
[Numero_Registo_Farmaceutica] [int] NOT NULL REFERENCES SNS_Farmaceutica ([Numero_Registo_Nacional]),
[Nome_Farmaco] [varchar](128) NOT NULL REFERENCES SNS_Farmaco ([Nome_Comercial]),
PRIMARY KEY ([Numero_Registo_Farmaceutica],[Nome_Farmaco]),
)
GO


CREATE TABLE SNS_Comercializa(
[NIF_Farmacia] [int] NOT NULL REFERENCES SNS_Farmacia ([NIF]),
[Nome_Farmaco] [varchar](128) NOT NULL REFERENCES SNS_Farmaco ([Nome_Comercial]),
PRIMARY KEY ([NIF_Farmacia],[Nome_Farmaco]),
)
GO


