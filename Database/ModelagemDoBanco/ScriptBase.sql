/* ModeloLógico: */

CREATE TABLE Arquivo (
    Cod_Arquivo VARCHAR(250) PRIMARY KEY,
    Nome VARCHAR(MAX),
    Conteudo VARBINARY(MAX),
    Tipo VARCHAR(MAX),
    Cod_Compartilhamento VARCHAR(250)
);

CREATE TABLE Usuario (
    Cod_Usuario INT PRIMARY KEY,
    Nome VARCHAR(MAX),
    Sobrenome VARCHAR(MAX),
    Email VARCHAR(MAX),
    Senha VARCHAR(MAX),
    Perfil INT
);

CREATE TABLE Diretorio (
    Cod_Diretorio INT PRIMARY KEY,
    Nome_Diretorio VARCHAR(MAX),
    Cod_Diretorio_Pai INT
);

CREATE TABLE Compartilhamento (
    Cod_Usuario INT,
    Cod_Compartilhamento VARCHAR(250) PRIMARY KEY,
    Cod_Arquivo VARCHAR(250)
);

CREATE TABLE Arquivo_Usuario (
    fk_Cod_Usuario INT,
    fk_Cod_Arquivo VARCHAR(250)
);

CREATE TABLE Arquivo_Compartilhamento (
    fk_Cod_Arquivo VARCHAR(250)
);

CREATE TABLE Arquivo_Diretorio (
    fk_Cod_Diretorio INT,
    fk_Cod_Arquivo VARCHAR(250)
);
 
ALTER TABLE Arquivo_Usuario ADD CONSTRAINT FK_Arquivo_Usuario_1
    FOREIGN KEY (fk_Cod_Usuario)
    REFERENCES Usuario (Cod_Usuario);
 
ALTER TABLE Arquivo_Usuario ADD CONSTRAINT FK_Arquivo_Usuario_2
    FOREIGN KEY (fk_Cod_Arquivo)
    REFERENCES Arquivo (Cod_Arquivo);
 
ALTER TABLE Arquivo_Compartilhamento ADD CONSTRAINT FK_Arquivo_Compartilhamento_1
    FOREIGN KEY (fk_Cod_Arquivo)
    REFERENCES Arquivo (Cod_Arquivo);
 
ALTER TABLE Arquivo_Diretorio ADD CONSTRAINT FK_Arquivo_Diretorio_1
    FOREIGN KEY (fk_Cod_Diretorio)
    REFERENCES Diretorio (Cod_Diretorio);
 
ALTER TABLE Arquivo_Diretorio ADD CONSTRAINT FK_Arquivo_Diretorio_2
    FOREIGN KEY (fk_Cod_Arquivo)
    REFERENCES Arquivo (Cod_Arquivo);