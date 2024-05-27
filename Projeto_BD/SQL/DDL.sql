CREATE TABLE SummerFest.Localizacao(
ID_Localizacao INT PRIMARY KEY NOT NULL,
Coordenadas VARCHAR(64) NOT NULL,
Pais VARCHAR(32),
Cidade VARCHAR(64),
Lugar VARCHAR(64) NOT NULL
)

CREATE TABLE SummerFest.Festival(
ID_Festival INT PRIMARY KEY NOT NULL,
Nome VARCHAR(64),
Data_De_Inicio DATE NOT NULL,
Duracao_Dias INT NOT NULL,
Lotacao_Maxima INT NOT NULL,
ID_Localizacao INT REFERENCES SummerFest.Localizacao(ID_Localizacao)
)

CREATE TABLE SummerFest.Patrocinador(
ID_Patrocinador INT PRIMARY KEY NOT NULL,
Nome VARCHAR(128),
)

CREATE TABLE SummerFest.Palco(
ID_Palco INT PRIMARY KEY NOT NULL,
Nome VARCHAR(64),
Patrocinador INT REFERENCES SummerFest.Patrocinador(ID_Patrocinador)
)

CREATE TABLE SummerFest.Concerto(
Numero_Concerto INT PRIMARY KEY NOT NULL,
Data_do_Concerto DATE NOT NULL,
Duracao_Minutos INT,
ID_Palco INT REFERENCES SummerFest.Palco(ID_Palco),
ID_Festival INT REFERENCES SummerFest.Festival(ID_Festival)
)

CREATE TABLE SummerFest.Artista(
ID_Artista INT PRIMARY KEY NOT NULL,
Nome_Artistico VARCHAR(128),
Estilo_Musical VARCHAR(64),
Nome_Verdadeiro VARCHAR(128),
Idade INT,
Premios VARCHAR(128),
Nacionalidade VARCHAR(128),
)

CREATE TABLE SummerFest.Acampamento(
ID_Acampamento INT PRIMARY KEY NOT NULL,
Espaco_Disponivel INT,
Nome VARCHAR(128),
Duracao_Dias INT NOT NULL,
Acomodidades VARCHAR(64),
ID_Localizacao INT REFERENCES SummerFest.Localizacao(ID_Localizacao)
)

CREATE TABLE SummerFest.Passe(
Numero_De_Serie INT PRIMARY KEY NOT NULL,
Duracao_Dias INT NOT NULL,
Preco INT NOT NULL,
ID_Festival INT REFERENCES SummerFest.Festival(ID_Festival),
)

CREATE TABLE SummerFest.Barraca(
ID_Barraca INT PRIMARY KEY NOT NULL,
Tipo_de_Alimentacao VARCHAR(128),
ID_Patrocinador INT REFERENCES SummerFest.Patrocinador(ID_Patrocinador)
)

CREATE TABLE SummerFest.Concerto_DadoPor_Artista(
ID_Artista INT REFERENCES SummerFest.Artista(ID_Artista) NOT NULL,
Numero_Concerto INT REFERENCES SummerFest.Concerto(Numero_Concerto) NOT NULL,
PRIMARY KEY(ID_Artista, Numero_Concerto)
)

CREATE TABLE SummerFest.Festival_tem_Barracas(
ID_Festival INT REFERENCES SummerFest.Festival(ID_Festival) NOT NULL,
ID_Barraca INT REFERENCES SummerFest.Barraca(ID_Barraca) NOT NULL,
PRIMARY KEY(ID_Festival, ID_Barraca)
)

CREATE TABLE SummerFest.Passe_Inclui_Acampamento(
ID_Acampamento INT REFERENCES SummerFest.Acampamento(ID_Acampamento) NOT NULL,
Numero_De_Serie_Passe INT REFERENCES SummerFest.Passe(Numero_De_Serie) NOT NULL,
PRIMARY KEY(ID_Acampamento, Numero_De_Serie_Passe)
)

CREATE TABLE SummerFest.Nacionalidade_Artista(
ID_Artista INT REFERENCES SummerFest.Artista(ID_Artista) NOT NULL,
Nacionalidade VARCHAR(128) NOT NULL,
PRIMARY KEY(ID_Artista, Nacionalidade)
)

CREATE TABLE SummerFest.Premios_Artista(
ID_Artista INT REFERENCES SummerFest.Artista(ID_Artista) NOT NULL,
Premios VARCHAR(128) NOT NULL,
PRIMARY KEY(ID_Artista, Premios)
)
