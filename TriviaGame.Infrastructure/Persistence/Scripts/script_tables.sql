CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE Categorias (
    IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255) NULL,
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE Preguntas (
    IdPregunta INT IDENTITY(1,1) PRIMARY KEY,
    CategoriaId INT NOT NULL,
    TextoPregunta NVARCHAR(500) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Preguntas_Categorias
        FOREIGN KEY (CategoriaId )
        REFERENCES Categorias(IdCategoria )
);

CREATE TABLE Respuestas (
    IdRespuesta INT IDENTITY(1,1) PRIMARY KEY,
    IdPregunta INT NOT NULL,
    Respuesta NVARCHAR(500) NOT NULL,
    EsCorrecta BIT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Respuestas_Preguntas
        FOREIGN KEY (IdPregunta )
        REFERENCES Preguntas(IdPregunta)
);

CREATE TABLE Partidas (
    IdPartida INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    CategoriaId INT NOT NULL,
    FechaInicio DATETIME NOT NULL DEFAULT GETDATE(),
    FechaFin DATETIME NULL,
    Puntaje INT NULL,
    CONSTRAINT FK_Partidas_Usuarios
        FOREIGN KEY (UsuarioId )
        REFERENCES Usuarios(IdUsuario),
    CONSTRAINT FK_Partidas_Categorias
        FOREIGN KEY (CategoriaId)
        REFERENCES Categorias(IdCategoria)
);

CREATE TABLE DetallePartida (
    IdDetalle INT IDENTITY(1,1) PRIMARY KEY,
    PartidaId INT NOT NULL,
    PreguntaId INT NOT NULL,
    RespuestaId INT NOT NULL,
    EsCorrecta BIT NOT NULL,
    CONSTRAINT FK_DetallePartida_Partidas
        FOREIGN KEY (PartidaId)
        REFERENCES Partidas(IdPartida),
    CONSTRAINT FK_DetallePartida_Preguntas
        FOREIGN KEY (PreguntaId)
        REFERENCES Preguntas(IdPregunta),
    CONSTRAINT FK_DetallePartida_Respuestas
        FOREIGN KEY (RespuestaId)
        REFERENCES Respuestas(IdRespuesta)
);

