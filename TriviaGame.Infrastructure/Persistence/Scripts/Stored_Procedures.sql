CREATE PROCEDURE sp_Usuario_Existe
    @Nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;


    SELECT COUNT(1)
    FROM Usuarios
    WHERE Nombre = @Nombre
      AND Estado = 1;
END;
GO


CREATE PROCEDURE sp_Usuario_Registrar
    @Nombre NVARCHAR(50),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;


    INSERT INTO Usuarios (Nombre, PasswordHash)
    VALUES (@Nombre, @PasswordHash);


    SELECT SCOPE_IDENTITY() AS IdUsuario;
END;
GO



CREATE PROCEDURE sp_Usuario_ObtenerPorNombre
    @Nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;


    SELECT 
        IdUsuario,
        Nombre,
        PasswordHash,
        Estado
    FROM Usuarios
    WHERE Nombre = @Nombre
      AND Estado = 1;
END;
GO



CREATE PROCEDURE sp_Categorias_ListarActivas
AS
BEGIN
    SET NOCOUNT ON;


    SELECT 
        IdCategoria,
        Nombre,
        Descripcion
    FROM Categorias
    WHERE Estado = 1;
END;
GO



CREATE PROCEDURE sp_Partida_Crear
    @UsuarioId INT,
    @CategoriaId INT
AS
BEGIN
    SET NOCOUNT ON;


    INSERT INTO Partidas (UsuarioId, CategoriaId)
    VALUES (@UsuarioId, @CategoriaId);


    SELECT SCOPE_IDENTITY() AS IdPartida;
END;
GO



CREATE PROCEDURE sp_Preguntas_ObtenerAleatoriasPorCategoria
    @CategoriaId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PreguntasSeleccionadas TABLE (Id INT);

    INSERT INTO @PreguntasSeleccionadas (Id)
    SELECT TOP 3 IdPregunta
    FROM Preguntas
    WHERE CategoriaId = @CategoriaId AND Estado = 1
    ORDER BY NEWID();

    SELECT 
        p.IdPregunta,
        p.TextoPregunta,
        r.IdRespuesta,
        r.Respuesta
    FROM Preguntas p
    INNER JOIN @PreguntasSeleccionadas ps ON p.IdPregunta = ps.Id
    INNER JOIN Respuestas r ON r.IdPregunta = p.IdPregunta
    WHERE r.Estado = 1
    ORDER BY p.IdPregunta, NEWID();
END;
GO


CREATE PROCEDURE sp_Respuesta_EsCorrecta
    @RespuestaId INT
AS
BEGIN
    SET NOCOUNT ON;


    SELECT EsCorrecta
    FROM Respuestas
    WHERE IdRespuesta = @RespuestaId;
END;
GO



CREATE PROCEDURE sp_DetallePartida_Insertar
    @PartidaId INT,
    @PreguntaId INT,
    @RespuestaId INT,
    @EsCorrecta BIT
AS
BEGIN
    SET NOCOUNT ON;


    INSERT INTO DetallePartida (
        PartidaId,
        PreguntaId,
        RespuestaId,
        EsCorrecta
    )
    VALUES (
        @PartidaId,
        @PreguntaId,
        @RespuestaId,
        @EsCorrecta
    );
END;
GO



CREATE PROCEDURE sp_Partida_Finalizar
    @PartidaId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Puntaje DECIMAL(5,2);

    SELECT @Puntaje = (COUNT(CASE WHEN EsCorrecta = 1 THEN 1 END) * 10.0) / COUNT(*)
    FROM DetallePartida
    WHERE PartidaId = @PartidaId;

    UPDATE Partidas
    SET 
        FechaFin = GETDATE(),
        Puntaje = @Puntaje
    WHERE IdPartida = @PartidaId;

    SELECT @Puntaje AS PuntajeFinal;
END;
GO

CREATE PROCEDURE sp_Usuario_ObtenerResumen
    @UsuarioId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        COUNT(*) AS PartidasJugadas,
        ISNULL(SUM(Puntaje), 0) AS TotalPuntosAcumulados,
        ISNULL(MAX(Puntaje), 0) AS MejorPuntaje
    FROM Partidas
    WHERE UsuarioId = @UsuarioId;
END;
GO


