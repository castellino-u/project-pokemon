# 🐱‍👓 Proyecto WinForms Pokémon

Este es mi **primer proyecto con Windows Forms** donde pongo en práctica conceptos de:
- Programación Orientada a Objetos (POO).
- Modelo de dominio.
- Acceso a datos con **ADO.NET**.
- Conexión y consultas a una base de datos SQL Server.
- Manejo de imágenes desde URL en un `PictureBox`.

---

## 📌 Descripción

La aplicación muestra una lista de **Pokémon** almacenados en una base de datos.  
Desde el formulario principal se puede:

- Visualizar en un `DataGridView` los Pokémon (Número, Nombre, Descripción y URL de la imagen).
- Mostrar la imagen del Pokémon seleccionado en un `PictureBox`.
- Practicar cómo conectar WinForms con SQL Server utilizando ADO.NET.

---

## 🛠️ Tecnologías utilizadas

- **Lenguaje:** C#  
- **Framework:** .NET Framework (WinForms)  
- **IDE:** Visual Studio  
- **Base de datos:** SQL Server  
- **Acceso a datos:** ADO.NET  

---

## 📂 Estructura del proyecto

- `Pokemon` → clase de dominio que representa la entidad.  
- `PokemonService` → contiene la lógica para acceder a los datos (consulta a la base de datos).  
- `Form1` → formulario principal que muestra la lista y la imagen.  
- `ScriptDB.sql` → script para crear y poblar la base de datos necesaria.  

---

## ⚙️ Instalación y uso

1. Clonar este repositorio:
   ```bash
   git clone https://github.com/tu-usuario/pokemon-winforms.git
2.Abrir el proyecto en Visual Studio.

3.Ejecutar el script ScriptDB.sql en SQL Server Management Studio para crear la base de datos y las tablas.

4.Verificar la cadena de conexión en el código (clase PokemonService) y ajustarla si es necesario.

5.Ejecutar el proyecto desde Visual Studio (F5).

🚀 Aprendizajes

En este proyecto aprendí a:

Usar WinForms para crear una interfaz gráfica básica.

Trabajar con ADO.NET para acceder a SQL Server.

Implementar un modelo de dominio.

Mostrar imágenes desde una URL en un PictureBox.

Organizar el código separando la lógica de acceso a datos de la UI.

📌 Estado

✅ Proyecto funcional.
🚧 Es solo una práctica inicial, el objetivo es aprender y sentar bases para proyectos más grandes.








