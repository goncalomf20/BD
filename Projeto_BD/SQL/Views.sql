CREATE VIEW Festival AS
	SELECT * FROM SummerFest.Festival

CREATE VIEW Concertos AS
	SELECT * FROM SummerFest.Concerto

CREATE VIEW Artistas AS
	SELECT * FROM SummerFest.Artista

CREATE VIEW Passes AS
	SELECT * FROM SummerFest.Passe

CREATE VIEW Acampamento AS
	SELECT * FROM SummerFest.Acampamento

CREATE VIEW Barracas AS
	SELECT * FROM SummerFest.Barraca

CREATE VIEW Palco AS
	SELECT * FROM SummerFest.Palco

CREATE VIEW Localizacao AS
	SELECT ID_Localizacao, Lugar FROM SummerFest.Localizacao

CREATE VIEW ViewArtista_Festival AS
	SELECT a1.Nome_Artistico, a1.Estilo_Musical , a1.Idade , a1.Nacionalidade , c2.Numero_Concerto ,c2.Data_do_Concerto
	FROM SummerFest.Artista a1
	INNER JOIN SummerFest.Concerto_DadoPor_Artista c1 ON Artista.ID_Artista = c1.ID_Artista
	INNER JOIN SummerFest.Concerto c2 ON c1.Numero_Concerto = c2.Numero_Concerto
	INNER JOIN SummerFest.Festival f1 ON c1.ID_Festival = f1.ID_Festival

CREATE VIEW ViewArtista_Passe AS
	    SELECT a1.Nome_Artistico, a1.Estilo_Musical , a1.Idade , a1.Nacionalidade , c2.Numero_Concerto ,c2.Data_do_Concerto
	    FROM SummerFest.Artista a1
	    INNER JOIN SummerFest.Concerto_DadoPor_Artista c1 ON Artista.ID_Artista = c1.ID_Artista
	    INNER JOIN SummerFest.Concerto c2 ON c1.Numero_Concerto = c2.Numero_Concerto
        INNER JOIN SummerFest.Festival f1 ON c2.Numero_Concerto = f1.ID_Festival

CREATE VIEW ViewBarracas_Festival AS
        SELECT b1.ID_Barraca , b1.Tipo_de_Alimentacao
        FROM SummerFest.Barraca b1
        INNER JOIN SummerFest.Festival_tem_Barracas fb1 ON fb1.ID_Barraca = b1.ID_Barraca
        INNER JOIN SummerFest.Festival f1 ON f1.ID_Festival = fb1.ID_festival

CREATE VIEW ViewPalcos_Festival AS
        SELECT p1.Nome , p1.Patrocinadorss
		FROM SummerFest.Palco p1
		INNER JOIN SummerFest.Concerto c1 ON c1.ID_Palco = p1.ID_Palco
		INNER JOIN SummerFest.Festival f1 ON f1.ID_Festival = c1.ID_Festival
        
CREATE VIEW ViewPatrocinador_Festival AS
		SELECT pt1.Nome
		FROM SummerFest.Patrocinador pt1
		INNER JOIN SummerFest.Patrocinador pt1 ON pt1.ID_Patrocinador = p1.Palco
		INNER JOIN SummerFest.Concerto c1 ON c1.ID_Palco = p1.ID_Palco
		INNER JOIN SummerFest.Festival f1 ON f1.ID_Festival = c1.ID_Festival

CREATE VIEW ViewArtista_Concerto AS
		SELECT a1.Nome_Artistico, a1.Estilo_Musical , a1.Idade , a1.Nacionalidade , c2.Numero_Concerto ,c2.Data_do_Concerto
		FROM SummerFest.Artista a1
		INNER JOIN SummerFest.Concerto_DadoPor_Artista c1 ON a1.ID_Artista = c1.ID_Artista
		INNER JOIN SummerFest.Concerto c2 ON c1.Numero_Concerto = c2.Numero_Concerto

CREATE VIEW ViewPatrocinador_Barraca AS
        SELECT pt1.ID_Patrocinador , pt1.Nome 
        FROM SummerFest.Patrocinador pt1
        INNER JOIN SummerFest.Barraca b1 ON b1.ID_Patrocinador = pt1.ID_Patrocinador

CREATE VIEW ViewPatrocinador_Palco AS
        SELECT pt1.ID_Patrocinador , pt1.Nome
        FROM SummerFest.Patrocinador pt1
        INNER JOIN SummerFest.Palco p1 ON p1.Patrocinador = pt1.ID_Patrocinador

CREATE VIEW ViewPasses_Acampamentos AS
        SELECT pa1.Numero_De_Serie , pa1.Duracao_Dias , pa1.Preco , pa1.ID_Festival
        FROM SummerFest.Passe pa1
        INNER JOIN SummerFest.Passe_Inclui_Acampamento pia1 ON pa1.Numero_De_Serie = pia1.Numero_De_Serie_Passe
        INNER JOIN SummerFest.Acampamento a1 ON a1.ID_Acampamento = pia1.ID_Acampamento