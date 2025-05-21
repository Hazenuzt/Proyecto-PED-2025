USE Master;
GO

CREATE DATABASE PlanEatDB;
GO

USE PlanEatDB;
GO
-- Tabla Usuario
CREATE TABLE Usuario (
    ID_Usuario INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    Estatura DECIMAL(5,2) NOT NULL, -- Asumiendo metros o centímetros
    Peso DECIMAL(5,2) NOT NULL,    -- Asumiendo kilogramos
    Username NVARCHAR(50) NOT NULL UNIQUE, -- Username debe ser único
    Password NVARCHAR(255) NOT NULL, -- 
    CantCalorias DECIMAL(10,2), -- Cantidad de calorías diarias objetivo
	Genero NVARCHAR (20) NOT NULL,
    Nivel_Actividad NVARCHAR (75) NOT NULL,
    Objetivo NVARCHAR (75) NOT NULL,
    EstadoFisico NVARCHAR (75) NOT NULL
);

-- Tabla Alimento
CREATE TABLE Alimento (
    ID_Alimento INT PRIMARY KEY,
    NombreAlimento NVARCHAR(255) NOT NULL,
    CaloriasPorPorcion DECIMAL(10,2) NOT NULL,
    ProteinasPorPorcion DECIMAL(10,2) NOT NULL,
    CarbohidratosPorPorcion DECIMAL(10,2) NOT NULL,
    GrasasPorPorcion DECIMAL(10,2) NOT NULL,
    UnidadMedidaBase NVARCHAR(50) NOT NULL, -- e.g., "g", "ml", "unidad"
    TamañoPorcionEstandarGramos DECIMAL(10,2) NULL, -- Puede ser NULL
    TipoAlimento NVARCHAR(100) NOT NULL, -- e.g., "Verdura", "Fruta", "Lácteo"
    RolAlimento NVARCHAR(100) NOT NULL -- e.g., "Base", "Proteina", "Vegetales", "GrasasYExtras"
);

-- Tabla Receta
CREATE TABLE Receta (
    ID_Receta INT PRIMARY KEY IDENTITY(1,1),
    NombreReceta NVARCHAR(255) NOT NULL,
    CaloriasTotales DECIMAL(10,2) NOT NULL
);

CREATE TABLE Receta_Ingrediente (
    ID INT PRIMARY KEY IDENTITY (1,1),
    ID_Receta INT NOT NULL,
    ID_Alimento INT NOT NULL,
    FOREIGN KEY (ID_Receta) REFERENCES Receta(ID_Receta),
    FOREIGN KEY (ID_Alimento) REFERENCES Alimento(ID_Alimento),
    UNIQUE (ID_Receta, ID_Alimento) -- Una receta no tiene el mismo ingrediente duplicado
);

-- Tabla Plan_Receta

CREATE TABLE Plan_Receta (
    ID INT PRIMARY KEY IDENTITY (1,1), -- ID único para cada entrada en esta tabla
    Id_Plan INT NOT NULL, -- ID del plan de comida (asumo que tienes una tabla Plan)
    Id_Receta INT NOT NULL,
    Tiempo_Comida NVARCHAR(50) NOT NULL, -- e.g., "Desayuno", "Almuerzo", "Cena", "Snack"
    Opcion INT NOT NULL, -- Para indicar la opción de receta si hay varias para el mismo tiempo de comida
    FOREIGN KEY (Id_Receta) REFERENCES Receta(ID_Receta)
    -- FOREIGN KEY (Id_Plan) REFERENCES Plan(ID_Plan)
);

--Tabla Plan_Comidas
CREATE TABLE Plan_Comidas (
    Id_Plan INT PRIMARY KEY IDENTITY(1,1),
    Id_Usuario INT NOT NULL,
    Fecha_Generacion DATETIME NOT NULL DEFAULT GETDATE(),
    Calorias_Diarias DECIMAL(10,2) NOT NULL,
    Estado NVARCHAR(50) DEFAULT 'Activo', -- Para marcar si el plan está activo, completado, etc.
    Descripcion NVARCHAR(255) NULL,
    FOREIGN KEY (Id_Usuario) REFERENCES Usuario(ID_Usuario)
);


INSERT INTO Alimento (ID_Alimento, NombreAlimento, CaloriasPorPorcion, ProteinasPorPorcion, CarbohidratosPorPorcion, GrasasPorPorcion, UnidadMedidaBase, TamañoPorcionEstandarGramos, TipoAlimento, RolAlimento) VALUES
(1, 'Pechuga de Pollo', 200, 30, 0, 8, 'g', 150.00, 'Carne', 'Proteina'),
(2, 'Huevo Grande', 70, 6, 0.5, 5, 'unidad', NULL, 'Huevo', 'Proteina'),
(3, 'Salmón', 250, 22, 0, 17, 'g', 100.00, 'Pescado', 'Proteina'),
(4, 'Frijoles Negros', 150, 9, 27, 0.5, 'g', 100.00, 'Legumbre', 'Proteina'),
(5, 'Arroz Integral', 130, 3, 28, 1, 'g', 100.00, 'Cereal', 'Base'),
(6, 'Papa Cocida', 80, 2, 18, 0.1, 'g', 100.00, 'Vegetal', 'Base'),
(7, 'Pan Integral', 250, 10, 45, 3, 'g', 100.00, 'Cereal', 'Base'),
(8, 'Pasta Integral', 160, 6, 30, 1, 'g', 100.00, 'Cereal', 'Base'),
(9, 'Brócoli', 55, 3.7, 11, 0.6, 'g', 150.00, 'Verdura', 'Vegetales'),
(10, 'Tomate', 30, 1.5, 6, 0.2, 'g', 200.00, 'Fruta', 'Vegetales'),
(11, 'Espinaca', 23, 2.9, 3.6, 0.4, 'g', 100.00, 'Verdura', 'Vegetales'),
(12, 'Zanahoria', 41, 0.9, 9.6, 0.2, 'g', 100.00, 'Vegetal', 'Vegetales'),
(13, 'Aguacate', 160, 2, 9, 15, 'g', 50.00, 'Fruta', 'GrasasYExtras'),
(14, 'Aceite de Oliva', 900, 0, 0, 100, 'ml', 10.00, 'Grasa', 'GrasasYExtras'),
(15, 'Queso Cheddar', 400, 25, 1, 33, 'g', 30.00, 'Lácteo', 'GrasasYExtras'),
(16, 'Mantequilla de Cacahuete', 588, 25, 20, 50, 'g', 32.00, 'Nuez', 'GrasasYExtras'),
(17, 'Yogur Griego', 150, 17, 8, 5, 'g', 150.00, 'Lácteo', 'Proteina'),
(18, 'Manzana', 95, 0.5, 25, 0.3, 'unidad', NULL, 'Fruta', 'Vegetales'),
(19, 'Avena', 389, 13, 67, 7, 'g', 50.00, 'Cereal', 'Base'),
(20, 'Ternera Magra', 250, 35, 0, 10, 'g', 150.00, 'Carne Roja', 'Proteina'),
(21, 'Lentejas', 120, 9, 20, 0.5, 'g', 100.00, 'Legumbre', 'Proteina'),
(22, 'Tofu Firme', 80, 8, 2, 5, 'g', 100.00, 'Legumbre', 'Proteina'),
(23, 'Camarones', 85, 18, 0, 1.5, 'g', 100.00, 'Mariscos', 'Proteina'),
(24, 'Queso Cottage', 98, 11, 3, 4, 'g', 100.00, 'Lácteo', 'Proteina'),
(25, 'Quinoa', 120, 4, 21, 2, 'g', 80.00, 'Cereal', 'Base'),
(26, 'Tortilla de Maíz', 210, 5, 45, 3, 'g', 60.00, 'Cereal', 'Base'),
(27, 'Batata Cocida', 90, 2, 21, 0.2, 'g', 100.00, 'Vegetal', 'Base'),
(28, 'Pan de Centeno', 250, 9, 48, 2, 'g', 100.00, 'Cereal', 'Base'),
(29, 'Cuscús', 112, 4, 23, 0.5, 'g', 70.00, 'Cereal', 'Base'),
(30, 'Pepino', 15, 0.7, 3.6, 0.1, 'g', 100.00, 'Verdura', 'Vegetales'),
(31, 'Pimiento Rojo', 31, 1, 6, 0.3, 'g', 100.00, 'Verdura', 'Vegetales'),
(32, 'Lechuga Romana', 17, 1.2, 3.3, 0.3, 'g', 100.00, 'Verdura', 'Vegetales'),
(33, 'Champiñones', 22, 3.1, 3.3, 0.3, 'g', 100.00, 'Hongo', 'Vegetales'),
(34, 'Coliflor', 25, 1.9, 5, 0.3, 'g', 100.00, 'Verdura', 'Vegetales'),
(35, 'Almendras', 579, 21, 22, 50, 'g', 30.00, 'Fruto Seco', 'GrasasYExtras'),
(36, 'Semillas de Chía', 486, 17, 42, 31, 'g', 15.00, 'Semilla', 'GrasasYExtras'),
(37, 'Aceitunas Negras', 115, 0.8, 6, 11, 'g', 20.00, 'Fruta', 'GrasasYExtras'),
(38, 'Hummus', 166, 7.9, 14, 9.6, 'g', 50.00, 'Legumbre', 'GrasasYExtras'),
(39, 'Salsa Pesto', 450, 6, 4, 45, 'g', 30.00, 'Salsa', 'GrasasYExtras'),
(40, 'Leche Entera', 61, 3.2, 4.8, 3.3, 'ml', 200.00, 'Lácteo', 'GrasasYExtras'),
(41, 'Fresas', 32, 0.7, 7.7, 0.3, 'g', 150.00, 'Fruta', 'Vegetales'),
(42, 'Plátano', 105, 1.3, 27, 0.3, 'unidad', NULL, 'Fruta', 'Base'),
(43, 'Granola', 471, 10, 68, 20, 'g', 50.00, 'Cereal', 'Base'),
(44, 'Barra de Cereal', 150, 3, 25, 5, 'unidad', NULL, 'Snack', 'Base'),
(45, 'Pavo Feteado', 110, 20, 0, 3, 'g', 50.00, 'Carne', 'Proteina'),
(46, 'Nueces', 654, 15, 14, 65, 'g', 30.00, 'Fruto Seco', 'GrasasYExtras'),
(47, 'Espinaca_Duplicate', 23, 2.9, 3.6, 0.4, 'g', 100.00, 'Verdura', 'Vegetales'),
(48, 'Queso Fresco', 250, 18, 2, 18, 'g', 50.00, 'Lácteo', 'Proteina'),
(49, 'Pan Árabe', 260, 9, 50, 3, 'g', 70.00, 'Cereal', 'Base');
GO

INSERT INTO Receta (NombreReceta, CaloriasTotales) VALUES
('Pollo al Curry con Arroz y Brócoli', 450.0),
('Ensalada de Atún y Aguacate', 380.0),
('Omelette de Espinacas y Queso', 320.0),
('Pasta con Lentejas y Tomate', 550.0),
('Tacos de Frijoles y Verduras', 420.0),
('Salmón al Horno con Patatas Asadas y Coliflor', 500.0),
('Sopa de Lentejas y Verduras', 300.0),
('Desayuno Completo: Huevos Revueltos, Pan y Aguacate', 400.0),
('Yogur con Avena y Frutas', 350.0),
('Bowl de Quinoa con Tofu y Brócoli', 480.0),
('Bowl de Desayuno con Yogur, Avena y Frutas del Bosque', 380.0),
('Tostadas Integrales con Aguacate y Huevo', 350.0),
('Batido de Plátano, Avena y Leche', 300.0),
('Ensalada de Quinoa con Pollo y Vegetales Frescos', 480.0),
('Burrito de Frijoles Negros y Arroz con Pimiento', 450.0),
('Salmón a la Plancha con Espinacas y Batata', 520.0),
('Curry de Lentejas con Arroz Integral y Brócoli', 470.0),
('Sandwich de Pavo y Queso Fresco en Pan de Centeno', 360.0),
('Ensalada César con Pechuga de Pollo', 400.0),
('Pasta con Pesto y Champiñones', 580.0),
('Desayuno Mediterráneo: Pan Árabe, Hummus y Pepino', 310.0),
('Bowl de Proteínas: Ternera, Quinoa y Coliflor Asada', 550.0),
('Sopa Fría de Tomate y Pepino', 150.0),
('Pizza de Tortilla Integral con Vegetales y Queso', 410.0),
('Avena Cocida con Manzana y Canela', 280.0),
('Ensalada de Camarones con Aguacate y Lechuga', 390.0),
('Omelette de Verduras Variadas', 290.0),
('Tazón de Yogur Griego con Almendras y Miel', 330.0),
('Wrap de Pollo y Vegetales', 400.0),
('Pasta de Trigo Integral con Salsa de Tomate y Lentejas', 510.0),
('Snack Energético: Plátano con Mantequilla de Cacahuete', 280.0),
('Tostadas de Centeno con Queso Cottage y Tomate', 290.0),
('Ensalada de Garbanzos (Frijoles Negros) y Pimiento', 320.0),
('Huevos Duros con Espinacas y Aceitunas', 250.0),
('Salmón Asado con Cuscús y Espárragos (imaginar espárragos)', 530.0),
('Bowl de Avena con Fresas y Semillas de Chía', 310.0),
('Sandwich Vegetal con Hummus y Lechuga', 300.0),
('Curry de Tofu y Vegetales Mixtos', 460.0),
('Batata Asada con Frijoles y Queso Cheddar', 440.0),
('Pechuga de Pollo Rellena de Espinacas y Queso', 470.0),
('Sopa de Champiñones y Arroz', 280.0),
('Tortilla Española (con Papa y Huevo)', 370.0),
('Ensalada Griega con Pepino, Tomate y Aceitunas', 200.0),
('Desayuno Proteico: Yogur Griego y Pavo Feteado', 260.0),
('Bowl de Frutas con Granola y Yogur', 320.0),
('Ternera Salteada con Brócoli y Zanahoria', 420.0),
('Ensalada Caprese (Tomate, Queso Fresco, Albahaca)', 280.0),
('Lentejas Estofadas con Verduras de Raíz', 380.0),
('Huevos a la Ranchera (con Frijoles y Tortilla)', 390.0),
('Pasta con Camarones y Salsa Ligera de Tomate', 500.0),
('Crema de Espinacas y Champiñones', 180.0),
('Barra de Cereal Casera con Frutos Secos', 220.0),
('Pan Integral Tostado con Mermelada (No incluida)', 200.0),
('Bowl de Arroz con Pollo Desmenuzado y Verduras', 460.0),
('Tofu Salteado con Pimientos y Cebolla', 390.0),
('Desayuno Rápido: Queso Cottage y Fresas', 180.0),
('Pescado Blanco (no en lista, usar Salmón) con Cuscús y Zanahoria', 490.0),
('Wrap de Hummus y Verduras', 330.0),
('Ensalada de Pasta con Atún y Olivas', 560.0),
('Bowl de Batata Asada con Almendras y Canela', 300.0),
('Omelette de Queso y Champiñones', 340.0),
('Tostadas de Pan de Centeno con Aguacate y Tomate', 310.0),
('Smoothie Verde (Espinaca, Plátano, Agua/Leche)', 250.0),
('Pechuga de Pollo al Horno con Verduras Asadas', 480.0),
('Bowl de Lentejas y Verduras', 350.0),
('Mini Sandwiches de Pan Integral con Pavo y Lechuga', 300.0),
('Receta de Papa Rellena con Queso Cottage', 330.0),
('Tazón de Quinua con Camarones y Pepino', 400.0),
('Desayuno Americano (Huevos, Pan, Queso Cheddar)', 450.0),
('Ensalada de Pasta Fría con Pollo y Verduras', 490.0),
('Bowl de Avena con Nueces y Plátano', 390.0);

GO

INSERT INTO Receta_Ingrediente (ID_Receta, ID_Alimento) VALUES
-- Original Recipes (linking by name)
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pollo al Curry con Arroz y Brócoli'), 1),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pollo al Curry con Arroz y Brócoli'), 5),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pollo al Curry con Arroz y Brócoli'), 9),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pollo al Curry con Arroz y Brócoli'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Atún y Aguacate'), 3),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Atún y Aguacate'), 13),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Atún y Aguacate'), 10),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Atún y Aguacate'), 32),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Omelette de Espinacas y Queso'), 2),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Omelette de Espinacas y Queso'), 11),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Omelette de Espinacas y Queso'), 15),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta con Lentejas y Tomate'), 8),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta con Lentejas y Tomate'), 21),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta con Lentejas y Tomate'), 10),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta con Lentejas y Tomate'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tacos de Frijoles y Verduras'), 4),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tacos de Frijoles y Verduras'), 26),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tacos de Frijoles y Verduras'), 31),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tacos de Frijoles y Verduras'), 30),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Salmón al Horno con Patatas Asadas y Coliflor'), 3),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Salmón al Horno con Patatas Asadas y Coliflor'), 6),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Salmón al Horno con Patatas Asadas y Coliflor'), 34),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Salmón al Horno con Patatas Asadas y Coliflor'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sopa de Lentejas y Verduras'), 21),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sopa de Lentejas y Verduras'), 10),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sopa de Lentejas y Verduras'), 12),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sopa de Lentejas y Verduras'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Completo: Huevos Revueltos, Pan y Aguacate'), 2),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Completo: Huevos Revueltos, Pan y Aguacate'), 7),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Completo: Huevos Revueltos, Pan y Aguacate'), 13),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Yogur con Avena y Frutas'), 17),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Yogur con Avena y Frutas'), 19),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Yogur con Avena y Frutas'), 18),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Yogur con Avena y Frutas'), 41),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Quinoa con Tofu y Brócoli'), 25),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Quinoa con Tofu y Brócoli'), 22),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Quinoa con Tofu y Brócoli'), 9),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Quinoa con Tofu y Brócoli'), 14),

-- New Recipes (linking by name)
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Desayuno con Yogur, Avena y Frutas del Bosque'), 17),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Desayuno con Yogur, Avena y Frutas del Bosque'), 19),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Desayuno con Yogur, Avena y Frutas del Bosque'), 41),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Desayuno con Yogur, Avena y Frutas del Bosque'), 36),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tostadas Integrales con Aguacate y Huevo'), 7),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tostadas Integrales con Aguacate y Huevo'), 13),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tostadas Integrales con Aguacate y Huevo'), 2),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Batido de Plátano, Avena y Leche'), 42),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Batido de Plátano, Avena y Leche'), 19),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Batido de Plátano, Avena y Leche'), 40),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Quinoa con Pollo y Vegetales Frescos'), 25),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Quinoa con Pollo y Vegetales Frescos'), 1),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Quinoa con Pollo y Vegetales Frescos'), 31),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Quinoa con Pollo y Vegetales Frescos'), 30),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Quinoa con Pollo y Vegetales Frescos'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Burrito de Frijoles Negros y Arroz con Pimiento'), 4),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Burrito de Frijoles Negros y Arroz con Pimiento'), 5),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Burrito de Frijoles Negros y Arroz con Pimiento'), 26),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Burrito de Frijoles Negros y Arroz con Pimiento'), 31),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Salmón a la Plancha con Espinacas y Batata'), 3),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Salmón a la Plancha con Espinacas y Batata'), 11),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Salmón a la Plancha con Espinacas y Batata'), 27),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Salmón a la Plancha con Espinacas y Batata'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Curry de Lentejas con Arroz Integral y Brócoli'), 21),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Curry de Lentejas con Arroz Integral y Brócoli'), 5),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Curry de Lentejas con Arroz Integral y Brócoli'), 9),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sandwich de Pavo y Queso Fresco en Pan de Centeno'), 45),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sandwich de Pavo y Queso Fresco en Pan de Centeno'), 48),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sandwich de Pavo y Queso Fresco en Pan de Centeno'), 28),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada César con Pechuga de Pollo'), 1),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada César con Pechuga de Pollo'), 32),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada César con Pechuga de Pollo'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta con Pesto y Champiñones'), 8),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta con Pesto y Champiñones'), 39),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta con Pesto y Champiñones'), 33),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Mediterráneo: Pan Árabe, Hummus y Pepino'), 49),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Mediterráneo: Pan Árabe, Hummus y Pepino'), 38),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Mediterráneo: Pan Árabe, Hummus y Pepino'), 30),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Proteínas: Ternera, Quinoa y Coliflor Asada'), 20),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Proteínas: Ternera, Quinoa y Coliflor Asada'), 25),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Proteínas: Ternera, Quinoa y Coliflor Asada'), 34),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Proteínas: Ternera, Quinoa y Coliflor Asada'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sopa Fría de Tomate y Pepino'), 10),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sopa Fría de Tomate y Pepino'), 30),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pizza de Tortilla Integral con Vegetales y Queso'), 26),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pizza de Tortilla Integral con Vegetales y Queso'), 10),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pizza de Tortilla Integral con Vegetales y Queso'), 31),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pizza de Tortilla Integral con Vegetales y Queso'), 15),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Avena Cocida con Manzana y Canela'), 19),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Avena Cocida con Manzana y Canela'), 18),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Camarones con Aguacate y Lechuga'), 23),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Camarones con Aguacate y Lechuga'), 13),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Camarones con Aguacate y Lechuga'), 32),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Camarones con Aguacate y Lechuga'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Omelette de Verduras Variadas'), 2),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Omelette de Verduras Variadas'), 9),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Omelette de Verduras Variadas'), 10),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Omelette de Verduras Variadas'), 31),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tazón de Yogur Griego con Almendras y Miel'), 17),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tazón de Yogur Griego con Almendras y Miel'), 35),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Wrap de Pollo y Vegetales'), 1),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Wrap de Pollo y Vegetales'), 49),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Wrap de Pollo y Vegetales'), 32),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Wrap de Pollo y Vegetales'), 12),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Wrap de Pollo y Vegetales'), 31),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta de Trigo Integral con Salsa de Tomate y Lentejas'), 8),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta de Trigo Integral con Salsa de Tomate y Lentejas'), 10),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta de Trigo Integral con Salsa de Tomate y Lentejas'), 21),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Snack Energético: Plátano con Mantequilla de Cacahuete'), 42),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Snack Energético: Plátano con Mantequilla de Cacahuete'), 16),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tostadas de Centeno con Queso Cottage y Tomate'), 28),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tostadas de Centeno con Queso Cottage y Tomate'), 24),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tostadas de Centeno con Queso Cottage y Tomate'), 10),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Garbanzos (Frijoles Negros) y Pimiento'), 4),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Garbanzos (Frijoles Negros) y Pimiento'), 31),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Garbanzos (Frijoles Negros) y Pimiento'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Huevos Duros con Espinacas y Aceitunas'), 2),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Huevos Duros con Espinacas y Aceitunas'), 11),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Huevos Duros con Espinacas y Aceitunas'), 37),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Salmón Asado con Cuscús y Espárragos (imaginar espárragos)'), 3),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Salmón Asado con Cuscús y Espárragos (imaginar espárragos)'), 29),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Salmón Asado con Cuscús y Espárragos (imaginar espárragos)'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Avena con Fresas y Semillas de Chía'), 19),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Avena con Fresas y Semillas de Chía'), 41),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Avena con Fresas y Semillas de Chía'), 36),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sandwich Vegetal con Hummus y Lechuga'), 7),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sandwich Vegetal con Hummus y Lechuga'), 38),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sandwich Vegetal con Hummus y Lechuga'), 32),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Curry de Tofu y Vegetales Mixtos'), 22),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Curry de Tofu y Vegetales Mixtos'), 9),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Curry de Tofu y Vegetales Mixtos'), 12),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Curry de Tofu y Vegetales Mixtos'), 34),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Curry de Tofu y Vegetales Mixtos'), 5),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Batata Asada con Frijoles y Queso Cheddar'), 27),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Batata Asada con Frijoles y Queso Cheddar'), 4),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Batata Asada con Frijoles y Queso Cheddar'), 15),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pechuga de Pollo Rellena de Espinacas y Queso'), 1),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pechuga de Pollo Rellena de Espinacas y Queso'), 11),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pechuga de Pollo Rellena de Espinacas y Queso'), 15),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sopa de Champiñones y Arroz'), 33),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Sopa de Champiñones y Arroz'), 5),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tortilla Española (con Papa y Huevo)'), 6),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tortilla Española (con Papa y Huevo)'), 2),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tortilla Española (con Papa y Huevo)'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada Griega con Pepino, Tomate y Aceitunas'), 30),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada Griega con Pepino, Tomate y Aceitunas'), 10),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada Griega con Pepino, Tomate y Aceitunas'), 37),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Proteico: Yogur Griego y Pavo Feteado'), 17),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Proteico: Yogur Griego y Pavo Feteado'), 45),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Frutas con Granola y Yogur'), 18),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Frutas con Granola y Yogur'), 41),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Frutas con Granola y Yogur'), 43),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Frutas con Granola y Yogur'), 17),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ternera Salteada con Brócoli y Zanahoria'), 20),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ternera Salteada con Brócoli y Zanahoria'), 9),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ternera Salteada con Brócoli y Zanahoria'), 12),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ternera Salteada con Brócoli y Zanahoria'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada Caprese (Tomate, Queso Fresco, Albahaca)'), 10),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada Caprese (Tomate, Queso Fresco, Albahaca)'), 48),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada Caprese (Tomate, Queso Fresco, Albahaca)'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Lentejas Estofadas con Verduras de Raíz'), 21),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Lentejas Estofadas con Verduras de Raíz'), 12),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Lentejas Estofadas con Verduras de Raíz'), 27),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Huevos a la Ranchera (con Frijoles y Tortilla)'), 2),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Huevos a la Ranchera (con Frijoles y Tortilla)'), 4),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Huevos a la Ranchera (con Frijoles y Tortilla)'), 26),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta con Camarones y Salsa Ligera de Tomate'), 8),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta con Camarones y Salsa Ligera de Tomate'), 23),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta con Camarones y Salsa Ligera de Tomate'), 10),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pasta con Camarones y Salsa Ligera de Tomate'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Crema de Espinacas y Champiñones'), 11),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Crema de Espinacas y Champiñones'), 33),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Crema de Espinacas y Champiñones'), 40),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Barra de Cereal Casera con Frutos Secos'), 44),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Barra de Cereal Casera con Frutos Secos'), 35),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Barra de Cereal Casera con Frutos Secos'), 46),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pan Integral Tostado con Mermelada (No incluida)'), 7),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Arroz con Pollo Desmenuzado y Verduras'), 5),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Arroz con Pollo Desmenuzado y Verduras'), 1),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Arroz con Pollo Desmenuzado y Verduras'), 9),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Arroz con Pollo Desmenuzado y Verduras'), 12),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tofu Salteado con Pimientos y Cebolla'), 22),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tofu Salteado con Pimientos y Cebolla'), 31),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tofu Salteado con Pimientos y Cebolla'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Rápido: Queso Cottage y Fresas'), 24),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Rápido: Queso Cottage y Fresas'), 41),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pescado Blanco (no en lista, usar Salmón) con Cuscús y Zanahoria'), 3),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pescado Blanco (no en lista, usar Salmón) con Cuscús y Zanahoria'), 29),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pescado Blanco (no en lista, usar Salmón) con Cuscús y Zanahoria'), 12),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Wrap de Hummus y Verduras'), 49),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Wrap de Hummus y Verduras'), 38),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Wrap de Hummus y Verduras'), 30),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Wrap de Hummus y Verduras'), 31),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Wrap de Hummus y Verduras'), 11),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Pasta con Atún y Olivas'), 8),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Pasta con Atún y Olivas'), 3),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Pasta con Atún y Olivas'), 37),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Batata Asada con Almendras y Canela'), 27),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Batata Asada con Almendras y Canela'), 35),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Omelette de Queso y Champiñones'), 2),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Omelette de Queso y Champiñones'), 15),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Omelette de Queso y Champiñones'), 33),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tostadas de Pan de Centeno con Aguacate y Tomate'), 28),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tostadas de Pan de Centeno con Aguacate y Tomate'), 13),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tostadas de Pan de Centeno con Aguacate y Tomate'), 10),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Smoothie Verde (Espinaca, Plátano, Agua/Leche)'), 11),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Smoothie Verde (Espinaca, Plátano, Agua/Leche)'), 42),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Smoothie Verde (Espinaca, Plátano, Agua/Leche)'), 40),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pechuga de Pollo al Horno con Verduras Asadas'), 1),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pechuga de Pollo al Horno con Verduras Asadas'), 9),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pechuga de Pollo al Horno con Verduras Asadas'), 12),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pechuga de Pollo al Horno con Verduras Asadas'), 34),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Pechuga de Pollo al Horno con Verduras Asadas'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Lentejas y Verduras'), 21),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Lentejas y Verduras'), 10),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Lentejas y Verduras'), 31),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Lentejas y Verduras'), 30),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Mini Sandwiches de Pan Integral con Pavo y Lechuga'), 7),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Mini Sandwiches de Pan Integral con Pavo y Lechuga'), 45),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Mini Sandwiches de Pan Integral con Pavo y Lechuga'), 32),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Receta de Papa Rellena con Queso Cottage'), 6),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Receta de Papa Rellena con Queso Cottage'), 24),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tazón de Quinua con Camarones y Pepino'), 25),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tazón de Quinua con Camarones y Pepino'), 23),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Tazón de Quinua con Camarones y Pepino'), 30),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Americano (Huevos, Pan, Queso Cheddar)'), 2),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Americano (Huevos, Pan, Queso Cheddar)'), 7),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Desayuno Americano (Huevos, Pan, Queso Cheddar)'), 15),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Pasta Fría con Pollo y Verduras'), 8),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Pasta Fría con Pollo y Verduras'), 1),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Pasta Fría con Pollo y Verduras'), 10),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Pasta Fría con Pollo y Verduras'), 31),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Ensalada de Pasta Fría con Pollo y Verduras'), 14),

((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Avena con Nueces y Plátano'), 19),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Avena con Nueces y Plátano'), 46),
((SELECT ID_Receta FROM Receta WHERE NombreReceta = 'Bowl de Avena con Nueces y Plátano'), 42);

GO

INSERT INTO Usuario VALUES
('Juan', 'Perez', 21, 185, 85, 'JuanPi', 'juanperez', 22200, 'Masculino', 'Actividad_ligera', 'Perder_grasa', 'Normal' )
go

select * from Alimento
select * from Plan_Receta
select * from Receta
select * from RecetaIngrediente
select * from Usuario
go

