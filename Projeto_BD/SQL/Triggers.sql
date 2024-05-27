CREATE TRIGGER SummerFest.verificar_idade_artista ON SummerFest.Artista
AFTER INSERT
AS
BEGIN
    DELETE FROM SummerFest.Artista 
    WHERE Artista.Idade < 18;
	RAISERROR ('O artista não pode ser menor de idade.', 16, 1);
	RETURN;
END;
GO

CREATE TRIGGER SummerFest.verificar_patrocinador_palco ON SummerFest.Palco
AFTER INSERT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM SummerFest.Patrocinador WHERE ID_Patrocinador IN (SELECT Patrocinador FROM inserted))
    BEGIN
		DELETE FROM SummerFest.Palco
		WHERE ID_Palco = (SELECT Patrocinador FROM inserted);
        RAISERROR ('O patrocinador especificado não existe.', 16, 1);
        RETURN;
    END
END

CREATE TRIGGER SummerFest.verificar_patrocinador_barraca ON SummerFest.Barraca
AFTER INSERT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM SummerFest.Patrocinador WHERE ID_Patrocinador IN (SELECT ID_Patrocinador FROM inserted))
    BEGIN
		DELETE FROM SummerFest.Barraca
		WHERE ID_Barraca = (SELECT ID_Patrocinador FROM inserted);
        RAISERROR ('O patrocinador especificado não existe.', 16, 1);
        RETURN;
    END
END