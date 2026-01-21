INSERT INTO Categorias (Nombre, Descripcion)
VALUES
('Programación', 'Preguntas de programación y desarrollo'),
('Bases de Datos', 'Preguntas sobre SQL y bases de datos');


INSERT INTO Preguntas (CategoriaId, TextoPregunta) VALUES
(1,'¿Qué es una clase en POO?'),
(1,'¿Qué significa SOLID?'),
(1,'¿Qué es una interfaz?'),
(1,'¿Qué palabra se usa para heredar en C#?'),
(1,'¿Qué es un objeto?'),
(1,'¿Qué es un constructor?'),
(1,'¿Qué es encapsulamiento?'),
(1,'¿Qué es polimorfismo?'),
(1,'¿Qué es una variable?'),
(1,'¿Qué es un método?');


INSERT INTO Preguntas (CategoriaId, TextoPregunta) VALUES
(2,'¿Qué es una base de datos?'),
(2,'¿Qué comando SQL obtiene datos?'),
(2,'¿Qué es una clave primaria?'),
(2,'¿Qué es una clave foránea?'),
(2,'¿Qué es un índice?'),
(2,'¿Qué es una tabla?'),
(2,'¿Qué comando elimina registros?'),
(2,'¿Qué es SQL?'),
(2,'¿Qué es una vista?'),
(2,'¿Qué tipo de JOIN devuelve coincidencias?');


INSERT INTO Respuestas (IdPregunta, Respuesta, EsCorrecta) VALUES
(1,'Plantilla para crear objetos',1),(1,'Una variable',0),(1,'Un archivo',0),
(2,'Principios de diseño de software',1),(2,'Un lenguaje',0),(2,'Un framework',0),
(3,'Contrato de métodos',1),(3,'Una clase abstracta',0),(3,'Una BD',0),
(4,':',0),(4,'extends',0),(4,'inherit',1),
(5,'Instancia de una clase',1),(5,'Una función',0),(5,'Un método',0),
(6,'Inicializa un objeto',1),(6,'Destruye datos',0),(6,'Compila código',0),
(7,'Ocultar detalles internos',1),(7,'Duplicar código',0),(7,'Eliminar clases',0),
(8,'Múltiples comportamientos',1),(8,'Herencia múltiple',0),(8,'Uso de variables',0),
(9,'Espacio de memoria',1),(9,'Una clase',0),(9,'Un método',0),
(10,'Función de una clase',1),(10,'Un atributo',0),(10,'Una tabla',0);


INSERT INTO Respuestas (IdPregunta, Respuesta, EsCorrecta) VALUES
(11,'Almacén de datos',1),(11,'Programa',0),(11,'Archivo',0),
(12,'SELECT',1),(12,'INSERT',0),(12,'UPDATE',0),
(13,'Identificador único',1),(13,'Campo opcional',0),(13,'Índice',0),
(14,'Relaciona tablas',1),(14,'Clave primaria',0),(14,'Campo texto',0),
(15,'Mejora búsquedas',1),(15,'Elimina datos',0),(15,'Crea tablas',0),
(16,'Estructura de datos',1),(16,'Vista',0),(16,'Procedimiento',0),
(17,'DELETE',1),(17,'DROP',0),(17,'SELECT',0),
(18,'Lenguaje de consultas',1),(18,'Motor BD',0),(18,'Sistema operativo',0),
(19,'Consulta almacenada',1),(19,'Tabla física',0),(19,'Trigger',0),
(20,'INNER JOIN',1),(20,'LEFT JOIN',0),(20,'FULL JOIN',0);
