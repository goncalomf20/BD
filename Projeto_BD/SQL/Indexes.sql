CREATE INDEX searchID_Festival
ON SummerFest.Festival(ID_Festival)
GO

CREATE INDEX searchLocalizacao_Festival
ON SummerFest.Festival(ID_Localizacao)
GO

CREATE INDEX searchID_Localizacao
ON SummerFest.Localizacao(ID_Localizacao)
GO

CREATE INDEX searchLugar
ON SummerFest.Localizacao(Lugar)
GO
