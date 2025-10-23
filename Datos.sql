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

-- Sucursales
INSERT INTO Sucursales (Nombre, Direccion) VALUES
('Sucursal Centro', 'Av. Libertador 1234'),
('Sucursal Norte', 'Calle Falsa 5678'),
('Sucursal Sur', 'Calle Real 91011'),
('Sucursal Este', 'Av. Siempre Viva 200'),
('Sucursal Oeste', 'Boulevard Central 45'),
('Sucursal Bioquímica', 'Calle Ciencia 12'),
('Sucursal Tecnológica', 'Paseo de la Tecnología 88'),
('Sucursal Universitaria', 'Av. Campus 1'),
('Sucursal Plaza', 'Plaza Mayor 3'),
('Sucursal Lago', 'Rambla del Lago 5');

-- Usuarios
INSERT INTO Usuarios (Mail, Clave, TipoUsuario) VALUES
('cliente1@example.com', 'clave123', 1),
('cliente2@example.com', 'clave456', 1),
('empleado1@example.com', 'clave789', 2),
('cliente3@example.com', 'clave321', 1),
('cliente4@example.com', 'pass1234', 1),
('cliente5@example.com', 'miClave!', 1),
('empleado2@example.com', 'emple123', 2),
('empleado3@example.com', 'trabajo456', 2),
('cliente6@example.com', 'clave7890', 1),
('admin@example.com', 'adminpass', 2);

-- Clientes
INSERT INTO Clientes (IDUsuario, DNI, Nombre, Apellido, Telefono) VALUES
(1, 12345678, 'Juan', 'Perez', 123456789),
(2, 87654321, 'Ana', 'Gomez', 987654321),
(3, 11223344, 'Luis', 'Martinez', 456789123),
(4, 22334455, 'Marcos', 'Gutiérrez', 111222333),
(5, 33445566, 'Lucía', 'Fernández', 222333444),
(6, 44556677, 'Sofía', 'Ruiz', 333444555),
(1, 55667788, 'Mateo', 'Sosa', 444555666),
(2, 66778899, 'Valentina', 'Rossi', 555666777),
(7, 77889900, 'Diego', 'Vargas', 666777888),
(8, 88990011, 'Clara', 'Mendoza', 777888999);

-- Empleados
INSERT INTO Empleados (IDUsuario, Nombre, Apellido) VALUES
(3, 'Carlos', 'Sanchez'),
(3, 'Maria', 'Lopez'),
(3, 'Pedro', 'Ramirez'),
(4, 'Luciano', 'Ortiz'),
(4, 'Patricia', 'Núñez'),
(5, 'Fernando', 'Ibarra'),
(5, 'Soledad', 'Campos'),
(6, 'Ricardo', 'Pérez'),
(7, 'Verónica', 'Medina'),
(8, 'Andrés', 'Córdoba');

-- Libros
INSERT INTO Libros (IDAutor, IDGenero, IDEditorial, IDSucursal, Titulo, Descripcion, FechaPublicacion, Precio, Paginas, Stock) VALUES
(1, 1, 1, 1, 'Cien anios de soledad', 'Una novela sobre la familia Buendia.', CAST('1967-06-05' AS DATE), 20000, 400, 10),
(2, 2, 2, 2, 'Harry Potter y la piedra filosofal', 'La historia de un joven mago.', CAST('1997-06-26' AS DATE), 15000, 223, 15),
(3, 3, 3, 3, '1984', 'Una novela distopica sobre un futuro totalitario.', CAST('1949-06-08' AS DATE), 14000, 328, 5),
(4, 4, 4, 4, 'La casa de los espíritus', 'Novela familiar con elementos sobrenaturales.', CAST('1982-01-01' AS DATE), 24000, 448, 12),
(5, 2, 5, 5, 'Don Quijote de la Mancha', 'Clásico de la literatura española.', CAST('1605-01-16' AS DATE), 20000, 992, 7),
(6, 3, 6, 6, 'Crimen y castigo', 'Relato sobre culpa y redención.', CAST('1866-01-01' AS DATE), 22000, 671, 9),
(7, 7, 7, 1, 'Kafka en la orilla', 'Novela contemporánea con realismo mágico.', CAST('2002-09-12' AS DATE), 22000, 505, 6),
(1, 1, 1, 2, 'Orgullo y prejuicio', 'Novela romántica clásica.', CAST('1813-01-28' AS DATE), 25000, 279, 14),
(2, 5, 2, 3, 'Las aventuras de Tom Sawyer', 'Novela de aventuras y formación.', CAST('1876-01-01' AS DATE), 15000, 274, 11),
(8, 6, 3, 4, 'Ensayos escogidos', 'Colección de ensayos contemporáneos.', CAST('2010-05-10' AS DATE), 18000, 220, 8);

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
(10, 'ensayos_escogidos.jpg');

-- Deseados
INSERT INTO Deseados (IDCliente, IDLibro) VALUES
(1, 2),
(2, 1),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(1, 7),
(2, 8),
(3, 9),
(4, 10);

-- Carrito
INSERT INTO Carrito (IDCliente, IDLibro) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(1, 7),
(2, 8),
(3, 9),
(4, 10);

-- Stocks
INSERT INTO Stocks (IDLibro, Cantidad) VALUES
(1, 20),
(2, 30),
(3, 10),
(4, 15),
(5, 5),
(6, 9),
(7, 8),
(8, 20),
(9, 13),
(10, 7);

-- Compras
INSERT INTO Compras (FechaCompra, IDCliente, IDLibro, IDSucursal) VALUES
('2023-10-01', 1, 1, 1),
('2023-10-02', 2, 2, 2),
('2023-10-03', 3, 3, 3),
('2023-11-01', 4, 4, 4),
('2023-11-02', 5, 5, 5),
('2023-11-03', 6, 6, 6),
('2023-11-04', 1, 7, 1),
('2023-11-05', 2, 8, 2),
('2023-11-06', 3, 9, 3),
('2023-11-07', 4, 10, 4);

-- Devoluciones
INSERT INTO Devoluciones (IDCliente, IDCompra, IDLibro, Descripcion, FechaDevolucion) VALUES
(1, 1, 1, 'Libro en mal estado', '2023-10-05'),
(2, 2, 2, 'No era lo que esperaba', '2023-10-06'),
(3, 3, 3, 'Cambio de opinion', '2023-10-07'),
(4, 4, 4, 'Daños en la cubierta', '2023-11-10'),
(5, 5, 5, 'Páginas faltantes', '2023-11-11'),
(6, 6, 6, 'Error en el envío', '2023-11-12'),
(1, 7, 7, 'No era el formato esperado', '2023-11-13'),
(2, 8, 8, 'Retraso en entrega', '2023-11-14'),
(3, 9, 9, 'Contenido distinto', '2023-11-15'),
(4, 10, 10, 'Defecto de impresión', '2023-11-16');

-- Consultas finales
SELECT * FROM Usuarios;
SELECT * FROM Clientes;
SELECT * FROM Carrito;
SELECT * FROM Deseados;
