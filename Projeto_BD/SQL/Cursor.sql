CREATE PROCEDURE SummerFest.PassesDisponiveis (@ID_Festival INT, @PassesDisponiveis INT OUTPUT)
AS
BEGIN
    DECLARE @Passe_Contador INT;

    DECLARE PasseCursor CURSOR FOR
    SELECT COUNT(*) AS passe_contador
    FROM SummerFest.Passe
    WHERE ID_Festival = @ID_Festival;

    OPEN PasseCursor;

    FETCH NEXT FROM PasseCursor INTO @Passe_Contador;

    IF @@FETCH_STATUS = 0
    BEGIN
        SET @PassesDisponiveis = @Passe_Contador;
    END

    CLOSE PasseCursor;

    DEALLOCATE PasseCursor;
END

