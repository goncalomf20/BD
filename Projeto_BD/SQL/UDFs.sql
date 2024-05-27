CREATE FUNCTION dbo.checkID_Localizacao (@ID_Localizacao INT) RETURNS INT
AS
	BEGIN
		DECLARE @counter INT
		SELECT @counter=COUNT(1) FROM SummerFest.Localizacao WHERE ID_Localizacao=@ID_Localizacao
		RETURN @counter
	END
GO

CREATE FUNCTION dbo.checkID_Festival (@ID_Festival INT) RETURNS INT
AS
	BEGIN
		DECLARE @counter INT
		SELECT @counter=COUNT(1) FROM SummerFest.Festival WHERE ID_Festival=@ID_Festival
		RETURN @counter
	END
GO

CREATE FUNCTION dbo.checkNumero_Concerto (@Numero_Concerto INT) RETURNS INT
AS
	BEGIN
		DECLARE @counter INT
		SELECT @counter=COUNT(1) FROM SummerFest.Concerto WHERE Numero_Concerto=@Numero_Concerto
		RETURN @counter
	END
GO

CREATE FUNCTION dbo.checkNumero_De_Serie_Passe(@Numero_De_Serie INT) RETURNS INT
AS
	BEGIN
		DECLARE @counter INT
		SELECT @counter=COUNT(1) FROM SummerFest.Passe WHERE Numero_De_Serie=@Numero_De_Serie
		RETURN @counter
	END
GO

CREATE FUNCTION dbo.checkID_Acampamento(@ID_Acampamento INT) RETURNS INT
AS
	BEGIN
		DECLARE @counter INT
		SELECT @counter=COUNT(1) FROM SummerFest.Acampamento WHERE ID_Acampamento=@ID_Acampamento
		RETURN @counter
	END
GO

CREATE FUNCTION dbo.checkID_Palco(@ID_Palco INT) RETURNS INT
AS
	BEGIN
		DECLARE @counter INT
		SELECT @counter=COUNT(1) FROM SummerFest.Palco WHERE ID_Palco=@ID_Palco
		RETURN @counter
	END
GO

CREATE FUNCTION dbo.checkID_Barraca(@ID_Barraca INT) RETURNS INT
AS
	BEGIN
		DECLARE @counter INT
		SELECT @counter=COUNT(1) FROM SummerFest.Barraca WHERE ID_Barraca=@ID_Barraca
		RETURN @counter
	END
GO

CREATE FUNCTION dbo.checkID_Artista (@ID_Artista INT) RETURNS INT
AS
	BEGIN
		DECLARE @counter INT
		SELECT @counter=COUNT(1) FROM SummerFest.Artista WHERE ID_Artista=@ID_Artista
		RETURN @counter
	END
GO

CREATE FUNCTION dbo.BuscarID_Localizacao (@Lugar VARCHAR(64)) RETURNS INT
AS
	BEGIN
		DECLARE @ID_Localizacao INT
		SELECT @ID_Localizacao=ID_Localizacao FROM SummerFest.Localizacao WHERE Lugar=@Lugar
		RETURN @ID_Localizacao
	END
GO

CREATE FUNCTION dbo.BuscarLugar (@ID_Localizacao INT) RETURNS VARCHAR(64)
AS
	BEGIN
		DECLARE @Lugar INT
		SELECT @Lugar=Lugar FROM SummerFest.Localizacao WHERE ID_Localizacao=@ID_Localizacao
		RETURN @Lugar
	END
GO