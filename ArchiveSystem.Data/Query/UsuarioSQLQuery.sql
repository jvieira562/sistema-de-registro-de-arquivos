CREATE TABLE Usuario (

	Id int IDENTITY (1,1) PRIMARY KEY,
	Nome VARCHAR(75),
	Sobrenome VARCHAR(75),
	Email VARCHAR(75),
	Senha VARCHAR(30),
	Perfil INT,
);

INSERT INTO Usuario VALUES ('Jair Rodrigues', 'De Assis', 'jrodrigues@vunesp.com.br', 'jair1234', 2);
SELECT * FROM Usuario;