GO
USE TPCLibreriaUTN
GO

-- Autores
INSERT INTO Autores (Nombre, Apellido) VALUES 
('Gabriel', 'Garc�a M�rquez'),
('J.K.', 'Rowling'),
('George', 'Orwell');

-- Editoriales
INSERT INTO Editoriales (Nombre) VALUES 
('Editorial Planeta'),
('Bloomsbury'),
('Secker & Warburg');

-- Generos
INSERT INTO Generos (Nombre) VALUES 
('Ficci�n'),
('Fant�stico'),
('Distop�a');

-- Sucursales
INSERT INTO Sucursales (Nombre, Direccion) VALUES 
('Sucursal Centro', 'Av. Libertador 1234'),
('Sucursal Norte', 'Calle Falsa 5678'),
('Sucursal Sur', 'Calle Real 91011');

-- Usuarios
INSERT INTO Usuarios (Mail, Clave, TipoUsuario) VALUES 
('cliente1@example.com', 'clave123', 1),
('cliente2@example.com', 'clave456', 1),
('empleado1@example.com', 'clave789', 2);

-- Clientes
INSERT INTO Clientes (IDUsuario, DNI, Nombre, Apellido, Telefono) VALUES 
(1, 12345678, 'Juan', 'P�rez', 123456789),
(2, 87654321, 'Ana', 'G�mez', 987654321),
(3, 11223344, 'Luis', 'Mart�nez', 456789123);

-- Empleados
INSERT INTO Empleados (IDUsuario, Nombre, Apellido) VALUES 
(3, 'Carlos', 'S�nchez'),
(3, 'Mar�a', 'L�pez'),
(3, 'Pedro', 'Ram�rez');

-- Libros
INSERT INTO Libros (IDAutor, IDGenero, IDEditorial, IDSucursal, Titulo, Descripcion, FechaPublicacion, Precio, Paginas, Stock) VALUES 
(1, 1, 1, 1, 'Cien a�os de soledad', 'Una novela sobre la familia Buend�a.', '1967-06-05', 29.99, 400, 10),
(2, 2, 2, 2, 'Harry Potter y la piedra filosofal', 'La historia de un joven mago.', '1997-06-26', 19.99, 223, 15),
(3, 3, 3, 3, '1984', 'Una novela dist�pica sobre un futuro totalitario.', '1949-06-08', 14.99, 328, 5);

-- Portadas
INSERT INTO Portadas (IDLibro, Imagen) VALUES 
(1, 'cien_anos_soledad.jpg'),
(2, 'harry_potter.jpg'),
(3, '1984.jpg');

-- Deseados
INSERT INTO Deseados (IDCliente, IDLibro) VALUES 
(1, 2),
(2, 1),
(3, 3);

-- Carrito
INSERT INTO Carrito (IDCliente, IDLibro) VALUES 
(1, 1),
(2, 2),
(3, 3);

-- Stocks
INSERT INTO Stocks (IDLibro, Cantidad) VALUES 
(1, 20),
(2, 30),
(3, 10);

-- Compras
INSERT INTO Compras (FechaCompra, IDCliente, IDLibro, IDSucursal) VALUES 
('2023-10-01', 1, 1, 1),
('2023-10-02', 2, 2, 2),
('2023-10-03', 3, 3, 3);

-- Devoluciones
INSERT INTO Devoluciones (IDCliente, IDCompra, IDLibro, Descripcion, FechaDevolucion) VALUES 
(1, 1, 1, 'Libro en mal estado', '2023-10-05'),
(2, 2, 2, 'No era lo que esperaba', '2023-10-06'),
(3, 3, 3, 'Cambio de opini�n', '2023-10-07');