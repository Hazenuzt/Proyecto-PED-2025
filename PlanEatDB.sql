USE Master;
GO



CREATE DATABASE PlanEatDB;
GO

USE PlanEatDB;
GO

-- Tabla Usuario
CREATE TABLE Usuario (
    Id_Usuario INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(50) NOT NULL,
    Edad INT CHECK (Edad > 0) NOT NULL,
    Genero VARCHAR(50) NOT NULL,
    Estatura DECIMAL(10, 2) CHECK (Estatura > 0) NOT NULL,
    Peso DECIMAL(10, 2) CHECK (Peso > 0) NOT NULL,
    Nivel_Actividad VARCHAR(50) NOT NULL,
    Objetivo VARCHAR(75) NOT NULL,
    Username VARCHAR(75) NOT NULL UNIQUE,
    Contraseña VARCHAR(100) NOT NULL,

    CONSTRAINT chk_Genero CHECK (Genero IN ('Masculino', 'Femenino', 'Otro')),
    CONSTRAINT chk_Nivel_Actividad CHECK (Nivel_Actividad IN ('Sedentario', 'Ligero', 'Moderado', 'Intenso')),
    CONSTRAINT chk_Objetivo CHECK (Objetivo IN ('Perder peso', 'Mantenerse', 'Ganar masa muscular'))
);
GO

-- Tabla Alimento (se crea antes que Alimentos_No_Gustan)
CREATE TABLE Alimento (
    Id_Alimento INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(75) NOT NULL,
    Categoria VARCHAR(75) NOT NULL,
    Caloriasx100g DECIMAL(10, 2) NOT NULL,
    Proteinasx100g DECIMAL(10, 2) NOT NULL,
    Carbohidratosx100g DECIMAL(10, 2) NOT NULL,
    Grasasx100g DECIMAL(10, 2) NOT NULL,
    Unidad_Referencia VARCHAR(50) NOT NULL, -- Ej: "pieza", "taza", "gramos"
    Peso_Promedio DECIMAL(10, 2) NOT NULL, -- Peso promedio de la unidad de referencia

    CONSTRAINT chk_Categoria_Alimento CHECK (Categoria IN ('Proteína', 'Carbohidrato', 'Grasa', 'Vegetal', 'Fruta', 'Lácteo', 'Otro'))
);
GO

-- Tabla Preferencias_Usuario
CREATE TABLE Preferencias_Usuario (
    Id_Preferencias INT PRIMARY KEY IDENTITY,
    Id_Usuario INT NOT NULL,
    FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id_Usuario)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

-- Tabla Alimentos_No_Gustan (depende de Alimento y Preferencias_Usuario)
CREATE TABLE Alimentos_No_Gustan (
    Id_Preferencias INT NOT NULL,
    Id_Alimento INT NOT NULL,
    FOREIGN KEY (Id_Preferencias) REFERENCES Preferencias_Usuario(Id_Preferencias)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    FOREIGN KEY (Id_Alimento) REFERENCES Alimento(Id_Alimento)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    PRIMARY KEY (Id_Preferencias, Id_Alimento)
);
GO

-- Tabla Receta
CREATE TABLE Receta (
    Id_Receta INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(255) NOT NULL,
    Descripcion TEXT, -- Texto con el desarrollo de la receta
    CaloriasTotales DECIMAL(10, 2), -- Puede calcularse automáticamente
    TiempoPreparacion INT CHECK (TiempoPreparacion > 0)
);
GO

-- Tabla Receta_Ingrediente (depende de Receta y Alimento)
CREATE TABLE Receta_Ingrediente (
    Id_Receta INT NOT NULL,
    Id_Alimento INT NOT NULL,
    Cantidad DECIMAL(10, 2) NOT NULL, -- Cantidad del ingrediente en la receta
    Unidad VARCHAR(50) NOT NULL, -- Unidad de medida (ej: gramos, tazas, piezas)

    FOREIGN KEY (Id_Receta) REFERENCES Receta(Id_Receta)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    FOREIGN KEY (Id_Alimento) REFERENCES Alimento(Id_Alimento)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    PRIMARY KEY (Id_Receta, Id_Alimento)
);
GO

-- Tabla Plan_Comidas
CREATE TABLE Plan_Comidas (
    Id_Plan INT PRIMARY KEY IDENTITY,
    Id_Usuario INT NOT NULL,
    Fecha_Generacion DATETIME DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id_Usuario)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

-- Tabla Plan_Receta (depende de Plan_Comidas y Receta)
CREATE TABLE Plan_Receta (
    Id_Plan INT NOT NULL,
    Id_Receta INT NOT NULL,
    Tiempo_Comida VARCHAR(50) NOT NULL, -- Ej: Desayuno, Almuerzo, Cena, Snack
    Opcion INT NOT NULL, -- Número de opción (1, 2 o 3)

    FOREIGN KEY (Id_Plan) REFERENCES Plan_Comidas(Id_Plan)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    FOREIGN KEY (Id_Receta) REFERENCES Receta(Id_Receta)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    PRIMARY KEY (Id_Plan, Id_Receta, Tiempo_Comida, Opcion)
);
GO

insert into Usuario values
('Maria', 36, 'Femenino', 1.68, 160.8, 'Ligero', 'Perder peso', 'mariabonita', 'usuario12')--nombre,edad,altura,peso,actividad fisisca,objetivo,usuario,contraseña
go
insert into Usuario values
('Verenice', 20 ,'Femenino', 1.56, 160.8, 'Ligero', 'Perder peso', 'VereNice', 'piolin')--nombre,edad,altura,peso,actividad fisisca,objetivo,usuario,contraseña
go
