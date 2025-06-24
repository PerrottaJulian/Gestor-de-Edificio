---

## ℹ️ Sobre este Proyecto

> Este desarrollo es parte de la cursada de la materia **"Técnicas Avanzadas de Programación"** de la carrera **"Licenciatura en Informática"** de la **Universidad Metropolitana Para la Educación y el Trabajo (UMET)**.  
> Todo lo propuesto a continuación es en base a un **contexto ficticio propuesto también por mi persona**, pero el sistema busca ser **100% funcional**.  
> Previo al desarrollo de código se realizó un **análisis funcional y no funcional completo** de todas las partes del proyecto**.  
> Yo mismo hice la función completa de un equipo completo, incluyendo **Project Manager, Scrum Master, Diseñador y Programador Full-Stack**.  
> El total de tiempo implementado en este sistema es de actualmente **un cuatrimestre completo**.

---

# 🏢 RED BELGRANO

![Gestor de Edificio Web](https://github.com/user-attachments/assets/a77150df-8d62-4327-a432-08004a367689)

---

## 📚 Tabla de Contenidos
- [Contexto del Proyecto](#contexto-del-proyecto)
- [Entrevista de Licitación de Requerimientos](#entrevista-de-licitación-de-requerimientos)
- [Análisis de la Entrevista](#análisis-de-la-entrevista)
- [Partes del Sistema](#partes-del-sistema)
- [Stack Tecnológico](#stack-tecnológico)
- [🔝 Volver arriba](#-red-belgrano)

---

## 📌 Contexto del Proyecto

> "En el centro de la Ciudad de Buenos Aires se viene construyendo un edificio hace ya un par de años, y vendió la idea de ser uno de los más modernos y novedosos de toda la ciudad. Este edifico se llama la **Torre Belgrano**, y está a unos 6 meses de entrar en funcionamiento.  
> Esta no busca ser solo un edifico cualquiera, es algo así como un producto de lujo, y se le está dando mucha propaganda. Para estar a la altura de las expectativas, los dueños buscan 2 cosas:  
> • Incluir mucha tecnología, para ser vistos como el “edificio del futuro”.  
> • Formar una comunidad unida entre las personas que vivan en él.  
> Una de las tantas ideas que se formó para cumplir estos objetivos es la de tener su propio sistema virtual de gestión y comunicación, donde se concentre todo el trabajo y se forme esa comunidad que tanto quieren.  
> Los dueños contactaron a nuestro equipo de desarrollo para construir este sistema antes de que la torre abra sus puertas."

---

## 🗣️ Entrevista de Licitación de Requerimientos

- **¿Qué problemática detectas en cuanto a la comunicación entre residentes?**  
  > La comunicación es informal y anticuada, únicamente por grupos de WhatsApp que nadie lee o cuando se cruzan en el edificio. Muchas veces, el encargado termina actuando como vía de comunicación entre ellos. El único proceso formal que existe son las reuniones de consorcio.

- **¿Y entre los residentes con los administrativos del edificio?**  
  > Hay veces que llega a ser nula, teniendo que pasar de boca en boca, como un teléfono descompuesto. Esto también hace que la administración no dimensione los problemas de convivencia que están ocurriendo en el momento.

- **¿Vos, como encargado, sentirías que unificar la comunicación en un único sistema mejoraría la vida en común del edificio?**  
  > Por supuesto, necesitamos algo que ayude a que el edificio se sienta como una comunidad y no como un grupo de desconocidos. Además, se mueve mucha información en un lugar así, tener todo en el mismo espacio sería muy cómodo.

- **¿Quiénes conviven en el edificio?**  
  > El edificio se divide principalmente en Inquilinos y Propietarios, que englobando todo serían los residentes. Los propietarios tienen más “poder” que los inquilinos, en cuestiones de toma de decisiones. Luego estamos los que trabajamos para el edificio, como el encargado, que puede ser con o sin vivienda, servicios de limpieza, de mantenimiento, servicios de cuidado personal. Y, por último, está la administración, que no suele vivir en el edificio, pero obviamente es parte de este.

- **¿A qué tipo de usuario tendría que estar dedicado el uso del sistema?**  
  > No se puede saber qué clase de persona vive en un edificio, varían las edades, profesiones, estudios, culturas. Así que lo mejor sería asumir que el usuario no tiene conocimiento alguno. Otra cuestión es la administración, que seguro sí maneja más herramientas informáticas.

---

## 🧠 Análisis de la Entrevista

- Los usuarios del edificio son:  
  **Residentes** (Propietarios e Inquilinos), **Encargados**, y **Administración**.

- El sistema debe ofrecer **medios de comunicación entre todas las partes**.

- El sistema debe ser de **uso muy sencillo**, ya que **no se puede asumir conocimiento técnico** por parte de los usuarios.

---

## 🧩 Partes del Sistema

- **🎫 Tickets para el Encargado**  
  Permite a los residentes generar pedidos con detalles. El encargado puede visualizar estos tickets, cambiar su estado y llevar un registro.

- **💬 Espacio de Mensajería entre Residentes**  
  Canal de comunicación interna entre los propios residentes.

- **📣 Mensajería de Administración**  
  - Notificaciones de la administración hacia los residentes.  
  - Quejas o reclamos de los residentes hacia la administración.  
  - Posibilidad de incluir al encargado en las conversaciones.

- **📅 Reservas de Espacios Comunes**  
  Sistema para organizar el uso de áreas compartidas y evitar conflictos.

- **💰 Gestión Financiera**  
  Permite a la administración manejar las finanzas del edificio de forma automatizada.

- **👥 Gestión de Residentes**  
  Sección para visualizar y administrar la información de los usuarios del edificio.

- **🗒️ Espacio Personal**  
  Área privada donde los usuarios pueden guardar notas personales.

---

## 🛠️ Stack Tecnológico

### 🔧 Backend & Frontend
- [.NET 8](https://dotnet.microsoft.com/) con **ASP.NET Core MVC**
- **Entity Framework** integrado a ASP.NET
- Base de datos **SQL Server**
- Hosting en **Microsoft Azure**

### 🧰 Herramientas de Desarrollo
- **Git** para control de versiones  
- **GitHub** para alojamiento del código  
- **Jira** para gestión del Product Backlog  
- **Visual Studio** como entorno de desarrollo

---

[🔝 Volver arriba](#-red-belgrano)
