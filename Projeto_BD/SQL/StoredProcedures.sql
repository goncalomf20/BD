--CREATE PROCEDURE SummerFest.AdicionarLocalizacao (@ID_Localizacao INT, @Coordenadas VARCHAR(64), @Pais VARCHAR(32), @Cidade VARCHAR(64), @Lugar VARCHAR(64))
--AS

--	BEGIN
--		DECLARE @verification INT;
--		DECLARE @erro VARCHAR(100);
--		SET @verification = (SELECT dbo.checkID_Localizacao(@ID_Localizacao))
--		IF(@verification>=1)
--			RAISERROR ('O ID introduzido já existe, modifique-o para adicionar a localização!', 16,1);
--		ELSE
--			BEGIN
--				BEGIN TRY
--						INSERT INTO SummerFest.Localizacao(ID_Localizacao,Coordenadas,Pais,Cidade,Lugar) VALUES (@ID_Localizacao, @Coordenadas, @Pais, @Cidade, @Lugar);
--				END TRY
--				BEGIN CATCH
--					Rollback TRAN
--					SELECT @erro = ERROR_MESSAGE(); 
--					SET @erro =  'Não foi possível adicionar a localização, algum valor introduzido incorretamente!'
--					RAISERROR (@erro, 16,1);
--				END CATCH
--			END
--	END
--GO

--Festival
CREATE PROCEDURE SummerFest.AdicionarFestival (@ID_Festival INT, @Nome VARCHAR(64), @Data_De_Inicio DATE, @Duracao_Dias INT, @Lotacao_Maxima INT, @Lugar VARCHAR(64))
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
		SET @verification = (SELECT dbo.checkID_Festival(@ID_Festival))
		IF(@verification>=1)
			RAISERROR ('O ID introduzido já existe, modifique-o para adicionar o festival!', 16,1);
		ELSE
			BEGIN
			    DECLARE @ID_Localizacao INT;
				SET @ID_Localizacao = (SELECT dbo.BuscarID_Localizacao(@Lugar));

				BEGIN TRY
					INSERT INTO SummerFest.Festival (ID_Festival, Nome, Data_De_Inicio, Duracao_Dias, Lotacao_Maxima,ID_Localizacao) 
					VALUES (@ID_Festival, @Nome, @Data_De_Inicio, @Duracao_Dias, @Lotacao_Maxima, @ID_Localizacao);
				END TRY
				BEGIN CATCH
					SELECT @erro = ERROR_MESSAGE(); 
					SET @erro =  'Não foi possível adicionar o festival, algum valor introduzido incorretamente!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	END
GO

CREATE PROCEDURE SummerFest.EliminarFestival (@ID_Festival INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkID_Festival(@ID_Festival));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O ID do festival não existe, verifique o ID e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		BEGIN
			BEGIN TRY
				BEGIN TRAN
					DELETE FROM SummerFest.Concerto WHERE ID_Festival = @ID_Festival;
					DELETE FROM SummerFest.Passe WHERE ID_Festival = @ID_Festival;
					DELETE FROM SummerFest.Festival WHERE ID_Festival = @ID_Festival;
				COMMIT TRAN
			END TRY
			BEGIN CATCH
				ROLLBACK TRAN
				SELECT @erro = ERROR_MESSAGE();
				SET @erro = 'Não foi possível eliminar o festival, algum valor introduzido incorretamente!';
				RAISERROR (@erro, 16, 1);
			END CATCH
		END
	END
GO

CREATE PROCEDURE SummerFest.EditarFestival (@ID_Festival INT, @Nome VARCHAR(64), @Data_De_Inicio DATE, @Duracao_Dias INT, @Lotacao_Maxima INT, @ID_Localizacao INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkID_Festival(@ID_Festival));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O ID do festival não existe, verifique o ID e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		SET NOCOUNT ON;
			BEGIN
				BEGIN TRY
					BEGIN TRAN
			UPDATE SummerFest.Festival 
			SET
				Nome = @Nome, Data_De_Inicio = @Data_De_Inicio, Duracao_Dias = @Duracao_Dias, Lotacao_Maxima = @Lotacao_Maxima, ID_Localizacao = @ID_Localizacao
			WHERE
				ID_Festival = @ID_Festival;
		COMMIT TRAN
				END TRY
				BEGIN CATCH
					Rollback TRAN
					SELECT @erro = ERROR_MESSAGE(); 
					SET @erro =  'Não foi possível adicionar o festival, algum valor introduzido incorretamente!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	END
GO

--Concerto
CREATE PROCEDURE SummerFest.AdicionarConcerto (@Numero_Concerto INT, @Data_do_Concerto DATE, @Duracao_Minutos INT, @ID_Palco INT, @ID_Festival INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
		SET @verification = (SELECT dbo.checkNumero_Concerto(@Numero_Concerto))
		IF (@verification >= 1)
			RAISERROR ('O número de concerto introduzido já existe, modifique-o para adicionar o concerto!', 16, 1);
		ELSE
		BEGIN
			BEGIN TRY
				INSERT INTO SummerFest.Concerto (Numero_Concerto, Data_do_Concerto, Duracao_Minutos, ID_Palco, ID_Festival) VALUES (@Numero_Concerto, @Data_do_Concerto, @Duracao_Minutos, @ID_Palco, @ID_Festival);
			END TRY
			BEGIN CATCH
			SELECT @erro = ERROR_MESSAGE(); 
				SET @erro = 'Não foi possível adicionar o concerto, algum valor foi introduzido incorretamente!';
				RAISERROR (@erro, 16, 1);
			END CATCH
		END
	END
GO

CREATE PROCEDURE SummerFest.EliminarConcerto(@Numero_Concerto INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkNumero_Concerto(@Numero_Concerto));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O numero do concerto não existe, verifique o numero e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		BEGIN
			BEGIN TRY
				BEGIN TRAN
				    DELETE FROM SummerFest.Concerto_DadoPor_Artista WHERE Numero_Concerto = @Numero_Concerto;
					DELETE FROM SummerFest.Concerto WHERE Numero_Concerto =@Numero_Concerto
				COMMIT TRAN
			END TRY
			BEGIN CATCH
				ROLLBACK TRAN
				SELECT @erro = ERROR_MESSAGE();
				SET @erro = 'Não foi possível eliminar o concerto, algum valor introduzido incorretamente!';
				RAISERROR (@erro, 16, 1);
			END CATCH
		END
	END
GO

CREATE PROCEDURE SummerFest.EditarConcerto (@Numero_Concerto INT, @Data_do_Concerto DATE, @Duracao_Minutos INT, @ID_Palco INT, @ID_Festival INT)
AS
BEGIN
    DECLARE @verification INT;
    DECLARE @erro VARCHAR(100);
    
    SET @verification = (SELECT dbo.checkNumero_Concerto(@Numero_Concerto));
    
    IF (@verification = 0)
    BEGIN
        SET @erro = 'O numero do concerto não existe, verifique o numero e tente novamente!';
        RAISERROR (@erro, 16, 1);
    END
    ELSE
	SET NOCOUNT ON;
    BEGIN
        BEGIN TRY
            BEGIN TRAN
            UPDATE SummerFest.Concerto
            SET 
                Data_do_Concerto = @Data_do_Concerto, 
                Duracao_Minutos = @Duracao_Minutos,
				ID_Festival = @ID_Festival,
				ID_Palco = @ID_Palco
            WHERE
                Numero_Concerto = @Numero_Concerto
            COMMIT TRAN
        END TRY
        BEGIN CATCH
            rollback TRAN
            SELECT @erro = ERROR_MESSAGE(); 
            SET @erro =  'Não foi possível editar o concerto, algum valor introduzido incorretamente!';
            RAISERROR (@erro, 16, 1);
        END CATCH
		END
	END
GO

--Artistas

CREATE PROCEDURE SummerFest.AdicionarArtista (@ID_Artista INT,@Nome_Artistico VARCHAR(128), @Estilo_Musical VARCHAR(64), @Nome_Verdadeiro VARCHAR(128), @Idade INT, @Premios VARCHAR(128),@Nacionalidade VARCHAR(128))
AS

	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
		SET @verification = (SELECT dbo.checkID_Artista(@ID_Artista))
		IF(@verification>=1)
			RAISERROR ('O ID introduzido já existe, modifique-o para adicionar o artista!', 16,1);
		ELSE
			BEGIN
				BEGIN TRY
					INSERT INTO SummerFest.Artista(ID_Artista, Nome_Artistico, Estilo_Musical, Nome_Verdadeiro,Idade, Premios, Nacionalidade) 
					VALUES (@ID_Artista, @Nome_Artistico, @Estilo_Musical, @Nome_Verdadeiro, @Idade, @Premios, @Nacionalidade);
				END TRY
				BEGIN CATCH
					SELECT @erro = ERROR_MESSAGE(); 
					SET @erro =  'Não foi possível adicionar o artista, algum valor introduzido incorretamente!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	END
GO

CREATE PROCEDURE SummerFest.EliminarArtista (@ID_Artista INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkID_Artista(@ID_Artista));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O ID do artista não existe, verifique o ID e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		BEGIN
			BEGIN TRY
					DELETE FROM SummerFest.Artista WHERE ID_Artista = @ID_Artista;
				END TRY
			BEGIN CATCH
				ROLLBACK TRAN
				SELECT @erro = ERROR_MESSAGE();
				SET @erro = 'Não foi possível eliminar o artista, algum valor introduzido incorretamente!';
				RAISERROR (@erro, 16, 1);
			END CATCH
		END
	END
GO

CREATE PROCEDURE SummerFest.EditarArtista (@ID_Artista INT, @Nome_Artistico VARCHAR(128), @Estilo_Musical VARCHAR(64), @Nome_Verdadeiro VARCHAR(128), @Idade INT, @Premios VARCHAR(128),@Nacionalidade VARCHAR(128))
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkID_Artista(@ID_Artista));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O ID do artista não existe, verifique o ID e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		SET NOCOUNT ON;
			BEGIN
				BEGIN TRY
					BEGIN TRAN
			UPDATE SummerFest.Artista
			SET
				Nome_Artistico=@Nome_Artistico, Estilo_Musical=@Estilo_Musical, Nome_Verdadeiro=@Nome_Verdadeiro, Idade=@Idade, Premios=@Premios, Nacionalidade=@Nacionalidade
			WHERE
				ID_Artista = @ID_Artista;
		COMMIT TRAN
				END TRY
				BEGIN CATCH
					Rollback TRAN
					SELECT @erro = ERROR_MESSAGE(); 
					SET @erro =  'Não foi possível adicionar o artista, algum valor introduzido incorretamente!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	END
GO


--Passes

CREATE PROCEDURE SummerFest.AdicionarPasse (@Numero_De_Serie INT, @Duracao_Dias INT, @Preco INT, @ID_Festival INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
		SET @verification = (SELECT dbo.checkNumero_De_Serie_Passe(@Numero_De_Serie))
		IF (@verification >= 1)
			RAISERROR ('O número de série do passe introduzido já existe, modifique-o para adicionar o passe!', 16, 1);
		ELSE
		BEGIN
			BEGIN TRY
				INSERT INTO SummerFest.Passe (Numero_De_Serie , Duracao_Dias , Preco , ID_Festival ) VALUES (@Numero_De_Serie , @Duracao_Dias , @Preco , @ID_Festival );
			END TRY
			BEGIN CATCH
				SELECT @erro = ERROR_MESSAGE(); 
				SET @erro = 'Não foi possível adicionar o passe, algum valor foi introduzido incorretamente!';
				RAISERROR (@erro, 16, 1);
			END CATCH
		END
	END
GO

CREATE PROCEDURE SummerFest.EliminarPasse(@Numero_De_Serie INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkNumero_De_Serie_Passe(@Numero_De_Serie));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O numero de série do passe não existe, verifique o numero e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		BEGIN
			BEGIN TRY
				BEGIN TRAN
					DELETE FROM SummerFest.Passe WHERE Numero_De_Serie =@Numero_De_Serie
				COMMIT TRAN
			END TRY
			BEGIN CATCH
				ROLLBACK TRAN
				SELECT @erro = ERROR_MESSAGE();
				SET @erro = 'Não foi possível eliminar o passe, algum valor introduzido incorretamente!';
				RAISERROR (@erro, 16, 1);
			END CATCH
		END
	END
GO

CREATE PROCEDURE SummerFest.EditarPasse (@Numero_De_Serie INT, @Duracao_Dias INT, @Preco INT, @ID_Festival INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkNumero_De_Serie_Passe(@Numero_De_Serie));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O numero de série do passe não existe, verifique o numero e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		SET NOCOUNT ON;
			BEGIN
				BEGIN TRY
					BEGIN TRAN
			UPDATE SummerFest.Passe
			SET
				Duracao_Dias = @Duracao_Dias, Preco=@Preco, ID_Festival = @ID_Festival
			WHERE
				Numero_De_Serie=@Numero_De_Serie
		COMMIT TRAN
				END TRY
				BEGIN CATCH
					Rollback TRAN
					SELECT @erro = ERROR_MESSAGE(); 
					SET @erro =  'Não foi possível editar o passe, algum valor introduzido incorretamente!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	END
GO

--Acampamento

CREATE PROCEDURE SummerFest.AdicionarAcampamento (@ID_Acampamento INT,@Espaco_Disponivel INT,@Nome VARCHAR(128),@Duracao_Dias INT,@Acomodidades VARCHAR(64),@ID_Localizacao INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
		SET @verification = (SELECT dbo.checkID_Acampamento(@ID_Acampamento))
		IF(@verification>=1)
			RAISERROR ('O ID introduzido já existe, modifique-o para adicionar o acampamento!', 16,1);
		ELSE
			BEGIN
				BEGIN TRY
					INSERT INTO SummerFest.Acampamento (ID_Acampamento ,Espaco_Disponivel ,Nome,Duracao_Dias ,Acomodidades,ID_Localizacao) VALUES (@ID_Acampamento,@Espaco_Disponivel,@Nome,@Duracao_Dias,@Acomodidades ,@ID_Localizacao );
				END TRY
				BEGIN CATCH
					SELECT @erro = ERROR_MESSAGE(); 
					SET @erro =  'Não foi possível adicionar o acampamento, algum valor introduzido incorretamente!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	END
GO

CREATE PROCEDURE SummerFest.EliminarAcampamento (@ID_Acampamento INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkID_Acampamento(@ID_Acampamento));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O ID do acampamento não existe, verifique o ID e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		BEGIN
			BEGIN TRY
				BEGIN TRAN
					DELETE FROM SummerFest.Acampamento WHERE ID_Acampamento = @ID_Acampamento;
				COMMIT TRAN
			END TRY
			BEGIN CATCH
				ROLLBACK TRAN
				SELECT @erro = ERROR_MESSAGE();
				SET @erro = 'Não foi possível eliminar o acampamento, algum valor introduzido incorretamente!';
				RAISERROR (@erro, 16, 1);
			END CATCH
		END
	END
GO

CREATE PROCEDURE SummerFest.EditarAcampamento (@ID_Acampamento INT,@Espaco_Disponivel INT,@Nome VARCHAR(128),@Duracao_Dias INT,@Acomodidades VARCHAR(64),@ID_Localizacao INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkID_Acampamento(@ID_Acampamento));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O ID do acampamento não existe, verifique o ID e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		SET NOCOUNT ON;
			BEGIN
				BEGIN TRY
					BEGIN TRAN
			UPDATE SummerFest.Acampamento 
			SET
				Espaco_Disponivel = @Espaco_Disponivel, Nome = @Nome, Duracao_Dias = @Duracao_Dias, Acomodidades = @Acomodidades, ID_Localizacao = @ID_Localizacao
			WHERE
				ID_Acampamento = @ID_Acampamento
		COMMIT TRAN
				END TRY
				BEGIN CATCH
					Rollback TRAN
					SELECT @erro = ERROR_MESSAGE(); 
					SET @erro =  'Não foi possível adicionar o acampamento, algum valor introduzido incorretamente!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	END
GO

--Palco

CREATE PROCEDURE SummerFest.AdicionarPalco (@ID_Palco INT, @Nome VARCHAR(64),@Patrocinador INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
		SET @verification = (SELECT dbo.checkID_Palco(@ID_Palco))
		IF(@verification>=1)
			RAISERROR ('O ID introduzido já existe, modifique-o para adicionar o palco!', 16,1);
		ELSE
			BEGIN
				BEGIN TRY
					INSERT INTO SummerFest.Palco (ID_Palco ,Nome , Patrocinador) VALUES (@ID_Palco,@Nome,@Patrocinador);
				END TRY
				BEGIN CATCH
					SELECT @erro = ERROR_MESSAGE(); 
					SET @erro =  'Não foi possível adicionar o palco, algum valor introduzido incorretamente!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	END
GO

CREATE PROCEDURE SummerFest.EliminarPalco (@ID_Palco INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkID_Palco(@ID_Palco));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O ID do palco não existe, verifique o ID e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		BEGIN
			BEGIN TRY
				BEGIN TRAN
					DELETE FROM SummerFest.Palco WHERE ID_Palco = @ID_Palco;
				COMMIT TRAN
			END TRY
			BEGIN CATCH
				ROLLBACK TRAN
				SELECT @erro = ERROR_MESSAGE();
				SET @erro = 'Não foi possível eliminar o palco, algum valor introduzido incorretamente!';
				RAISERROR (@erro, 16, 1);
			END CATCH
		END
	END
GO

CREATE PROCEDURE SummerFest.EditarPalco (@ID_Palco INT, @Nome VARCHAR(64),@Patrocinador INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkID_Palco(@ID_Palco));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O ID do palco não existe, verifique o ID e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		SET NOCOUNT ON;
			BEGIN
				BEGIN TRY
					BEGIN TRAN
			UPDATE SummerFest.Palco 
			SET
				Nome = @Nome, Patrocinador = @Patrocinador
			WHERE
				ID_Palco = @ID_Palco
		COMMIT TRAN
				END TRY
				BEGIN CATCH
					Rollback TRAN
					SELECT @erro = ERROR_MESSAGE(); 
					SET @erro =  'Não foi possível adicionar o palco, algum valor introduzido incorretamente!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	END
GO

--Barracas

CREATE PROCEDURE SummerFest.AdicionarBarraca (@ID_Barraca INT, @Tipo_de_Alimentacao VARCHAR(128), @ID_Patrocinador INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
		SET @verification = (SELECT dbo.checkID_Barraca(@ID_Barraca))
		IF(@verification>=1)
			RAISERROR ('O ID introduzido já existe, modifique-o para adicionar a barraca!', 16,1);
		ELSE
			BEGIN
				BEGIN TRY
					INSERT INTO SummerFest.Barraca (ID_Barraca ,Tipo_de_Alimentacao ,ID_Patrocinador) VALUES (@ID_Barraca ,@Tipo_de_Alimentacao ,@ID_Patrocinador);
				END TRY
				BEGIN CATCH
					SELECT @erro = ERROR_MESSAGE(); 
					SET @erro =  'Não foi possível adicionar a barraca, algum valor introduzido incorretamente!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	END
GO

CREATE PROCEDURE SummerFest.EliminarBarraca (@ID_Barraca INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkID_Barraca(@ID_Barraca));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O ID da barraca não existe, verifique o ID e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		BEGIN
			BEGIN TRY
				BEGIN TRAN
					DELETE FROM SummerFest.Barraca WHERE ID_Barraca = @ID_Barraca;
				COMMIT TRAN
			END TRY
			BEGIN CATCH
				ROLLBACK TRAN
				SELECT @erro = ERROR_MESSAGE();
				SET @erro = 'Não foi possível eliminar a barraca, algum valor introduzido incorretamente!';
				RAISERROR (@erro, 16, 1);
			END CATCH
		END
	END
GO

CREATE PROCEDURE SummerFest.EditarBarraca (@ID_Barraca INT, @Tipo_de_Alimentacao VARCHAR(128), @ID_Patrocinador INT)
AS
	BEGIN
		DECLARE @verification INT;
		DECLARE @erro VARCHAR(100);
    
		SET @verification = (SELECT dbo.checkID_Barraca(@ID_Barraca));
    
		IF (@verification = 0)
		BEGIN
			SET @erro = 'O ID da barraca não existe, verifique o ID e tente novamente!';
			RAISERROR (@erro, 16, 1);
		END
		ELSE
		SET NOCOUNT ON;
			BEGIN
				BEGIN TRY
					BEGIN TRAN
			UPDATE SummerFest.Barraca 
			SET
				Tipo_de_Alimentacao = @Tipo_de_Alimentacao, ID_Patrocinador = @ID_Patrocinador
			WHERE
				ID_Barraca = @ID_Barraca
		COMMIT TRAN
				END TRY
				BEGIN CATCH
					Rollback TRAN
					SELECT @erro = ERROR_MESSAGE(); 
					SET @erro =  'Não foi possível adicionar a barraca, algum valor introduzido incorretamente!'
					RAISERROR (@erro, 16,1);
				END CATCH
			END
	END
GO
----------------------------------------------------------
--CREATE PROCEDURE SummerFest.AdicionarArtista_Festival
--    @ID_Artista INT,
--    @Numero_Concerto INT
--AS
--	BEGIN
--		DECLARE @verification_artista INT;
--		DECLARE @verification_concerto INT;
--		DECLARE @erro VARCHAR(100);

--		SET @verification_artista = (SELECT dbo.checkID_Artista(@ID_Artista))

--		IF (@verification_artista = 0)
--			RAISERROR ('O id de artista fornecido não existe!', 16, 1);
--		ELSE

--		SET @verification_concerto = (SELECT dbo.checkNumero_Concerto(@Numero_Concerto))
--		IF (@verification_concerto = 0)
--			RAISERROR ('O id de artista fornecido não existe!', 16, 1);
--		ELSE

--		BEGIN TRY
--			INSERT INTO SummerFest.Concerto_DadoPor_Artista (ID_Artista, Numero_Concerto) VALUES (@ID_Artista, @Numero_Concerto);
--		END TRY
--		BEGIN CATCH
--			SET @erro = 'Não foi possível adicionar o artista ao festival!';
--			RAISERROR (@erro, 16, 1);
--		END CATCH
--	END
--GO