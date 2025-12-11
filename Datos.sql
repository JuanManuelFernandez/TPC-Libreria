GO
USE TPCLibreriaUTN
GO

-- Autores
INSERT INTO Autores (Nombre, Apellido) VALUES
('Gabriel', 'Garcia Marquez'),
('J.K.', 'Rowling'),
('George', 'Orwell'),
('Isabel', 'Allende'),
('Miguel', 'de Cervantes'),
('Fiódor', 'Dostoievski'),
('Haruki', 'Murakami'),
('Jane', 'Austen'),
('Mark', 'Twain'),
('Ángela', 'Lleras');
GO

-- Editoriales
INSERT INTO Editoriales (Nombre) VALUES
('Editorial Planeta'),
('Bloomsbury'),
('Secker & Warburg'),
('Editorial Sudamericana'),
('Alianza Editorial'),
('Editorial Anagrama'),
('Penguin Random House'),
('Editorial Salamandra'),
('Fondo de Cultura Económica'),
('Editorial Siglo XXI');
GO

-- Generos
INSERT INTO Generos (Nombre) VALUES
('Ficcion'),
('Fantastico'),
('Distopia'),
('Realismo mágico'),
('Clásico'),
('Filosófico'),
('Contemporáneo'),
('Romance'),
('Aventura'),
('Ensayo');
GO

-- Usuarios
INSERT INTO Usuarios (Mail, Clave, TipoUsuario) VALUES
('cliente1@example.com', 'Clave1111!', 1),
('cliente2@example.com', 'Clave2222!', 1),
('cliente3@example.com', 'Clave3333!', 1),
('test.libreria.8um4x@silomails.com', 'Clave4444!', 1),
('admin@example.com', 'adminpass', 2);
GO

-- Clientes
INSERT INTO Clientes (IDUsuario, DNI, Nombre, Apellido, Telefono) VALUES
(1, 12345678, 'Juan', 'Perez', 123456789),
(2, 97765432, 'Ana', 'Gomez', 987654321),
(3, 11222333, 'Luis', 'Diaz', 456789123),
(4, 11222333, 'Test', 'Mail', 856789124),
(5, 11333777, 'Mister', 'Master', 000000000);
GO

-- Libros
INSERT INTO Libros (IDAutor, IDGenero, IDEditorial, Titulo, Descripcion, FechaPublicacion, Precio, Paginas, Stock) VALUES
(1, 1, 1, 'Cien anios de soledad', 'Una novela sobre la familia Buendia.', CAST('1967-06-05' AS DATE), 20000, 400, 10),
(2, 2, 2, 'Harry Potter y la piedra filosofal', 'La historia de un joven mago.', CAST('1997-06-26' AS DATE), 15000, 223, 15),
(3, 3, 3, '1984', 'Una novela distopica sobre un futuro totalitario.', CAST('1949-06-08' AS DATE), 14000, 328, 5),
(4, 4, 4, 'La casa de los espíritus', 'Novela familiar con elementos sobrenaturales.', CAST('1982-01-01' AS DATE), 24000, 448, 12),
(5, 2, 5, 'Don Quijote de la Mancha', 'Clásico de la literatura española.', CAST('1605-01-16' AS DATE), 20000, 992, 7),
(6, 3, 6, 'Crimen y castigo', 'Relato sobre culpa y redención.', CAST('1866-01-01' AS DATE), 22000, 671, 9),
(7, 7, 7, 'Kafka en la orilla', 'Novela contemporánea con realismo mágico.', CAST('2002-09-12' AS DATE), 22000, 505, 6),
(1, 1, 1, 'Orgullo y prejuicio', 'Novela romántica clásica.', CAST('1813-01-28' AS DATE), 25000, 279, 14),
(2, 5, 2, 'Las aventuras de Tom Sawyer', 'Novela de aventuras y formación.', CAST('1876-01-01' AS DATE), 15000, 274, 11),
(8, 6, 3, 'Ensayos escogidos', 'Colección de ensayos contemporáneos.', CAST('2010-05-10' AS DATE), 18000, 220, 8),
(3, 3, 3, 'Rebelion en la granja', 'Rebelión de animales contra humanos, corrupción del poder, dictadura de cerdos.', CAST('1945-08-17' AS DATE), 14000, 328, 5);
GO

-- Portadas
INSERT INTO Portadas (IDLibro, Imagen) VALUES
(1, 'cien_anos_soledad.jpg'),
(2, 'harry_potter.jpg'),
(3, '1984.jpg'),
(4, 'casa_espiritus.jpg'),
(5, 'don_quijote.jpg'),
(6, 'crimen_castigo.jpg'),
(7, 'kafka_orilla.jpg'),
(8, 'orgullo_prejuicio.jpg'),
(9, 'tom_sawyer.jpg'),
(10, 'ensayos_escogidos.jpg'),
(11, 'rebelion_en_la_granja.jpg');
GO

-- Deseados
INSERT INTO Deseados (IDCliente, IDLibro) VALUES
(1, 2),
(1, 6),
(1, 3),
(2, 4),
(2, 5),
(2, 6),
(3, 7),
(3, 8),
(3, 9);

-- Carritos
INSERT INTO Carritos (IDCliente) VALUES
(1),
(2),
(3);
GO

-- Libros x Carrito
INSERT INTO LibrosPorCarrito (IDCarrito, IDLibro, Cantidad, PrecioUnitario) VALUES
(1, 6, 1, 22000),
(1, 2, 1, 15000),
(1, 3, 1, 14000),
(2, 4, 1, 24000),
(2, 5, 1, 20000),
(2, 6, 1, 22000),
(3, 7, 1, 22000),
(3, 8, 1, 25000),
(3, 9, 1, 15000);
GO


-- Compras
INSERT INTO Compras (FechaCompra, IDCliente, Mail, Nombre, Apellido, DFacturacion, Localidad, Codigo, Telefono, Total) VALUES
('2023-10-01', 1, 'cliente1@example.com', 'Juan', 'Perez', 'Av. Corrientes 1234', 'Buenos Aires', '1043', '123456789', 20000),
('2023-10-02', 1, 'cliente1@example.com', 'Juan', 'Perez', 'Av. Corrientes 1234', 'Buenos Aires', '1043', '123456789', 15000),
('2023-10-03', 1, 'cliente1@example.com', 'Juan', 'Perez', 'Av. Corrientes 1234', 'Buenos Aires', '1043', '123456789', 14000),
('2023-11-01', 2, 'cliente2@example.com', 'Ana', 'Gomez', 'Calle San Martin 567', 'Cordoba', '5000', '987654321', 24000),
('2023-11-02', 2, 'cliente2@example.com', 'Ana', 'Gomez', 'Calle San Martin 567', 'Cordoba', '5000', '987654321', 20000),
('2023-11-03', 2, 'cliente2@example.com', 'Ana', 'Gomez', 'Calle San Martin 567', 'Cordoba', '5000', '987654321', 22000),
('2023-11-04', 3, 'cliente3@example.com', 'Luis', 'Diaz', 'Belgrano 890', 'Rosario', '2000', '456789123', 22000),
('2023-11-05', 3, 'cliente3@example.com', 'Luis', 'Diaz', 'Belgrano 890', 'Rosario', '2000', '456789123', 25000),
('2023-11-06', 3, 'cliente3@example.com', 'Luis', 'Diaz', 'Belgrano 890', 'Rosario', '2000', '456789123', 15000);
GO

-- LibrosPorCompra
INSERT INTO LibrosPorCompra (IDCompra, IDLibro, Cantidad, PrecioUnitario) VALUES
(1, 1, 1, 20000),
(2, 2, 1, 15000),
(3, 3, 1, 14000),
(4, 4, 1, 24000),
(5, 5, 1, 20000),
(6, 6, 1, 22000),
(7, 7, 1, 22000),
(8, 8, 1, 25000),
(9, 9, 1, 15000);
GO

-- Consultas de prueba
SELECT * FROM Libros;
SELECT * FROM Portadas;

SELECT * FROM Clientes;
SELECT * FROM Usuarios;

SELECT * FROM Carrito;
SELECT * FROM Deseados;

SELECT * FROM Stocks;
SELECT * FROM Compras;
SELECT * FROM LibrosPorCompra;

SELECT * FROM Opiniones;
SELECT * FROM Autores;
SELECT * FROM Generos;
SELECT * FROM Editoriales;
