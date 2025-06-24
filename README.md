Este desarrollo es parte de la cursada de la materia 'Tecnicas Avanzadas de Programacion' de la carrera 'Licenciatura en Informatica' de la Universidad Metropolitana Para la Educacion y el Trabajo (UMET).
Todo lo propuesto a continuacion es en base a un contexto ficticio propuesto tambien por mi persona, pero el sistema busca ser 100% funcional.
Previo al desarrollo de codigo se realizo un analisis funcional y no funcional completo de todas las partes del proyecto. Yo mismo hice la funcion completa de un equipo completo, incluyendo Proyect Manager, Scrum Master, Diseñador y Programador Full-Stack
El total de tiempo implementado en este sistema es de actualmente un cuatrimestre completo.

---------------------------------------------------------------------------------------- RED BELGRANO ----------------------------------------------------------------------------------------

![Gestor de Edificio Web](https://github.com/user-attachments/assets/a77150df-8d62-4327-a432-08004a367689)

Contexto del proyecto
" En el centro de la Ciudad de Buenos Aires se viene construyendo un edificio hace ya un par de años, y vendió la idea de ser uno de los más modernos y novedosos de toda la ciudad. Este edifico se llama la Torre Belgrano, y está a unos 6 meses de entrar en funcionamiento.
Esta no busca ser solo un edifico cualquiera, es algo así como un producto de lujo, y se le está dando mucha propaganda. Para estar a la altura de las expectativas, los dueños buscan 2 cosas: Incluir mucha tecnología, para ser vistos como el “edificio del futuro”, y formar una comunidad unida entre las personas que vivan en él. 
Una de las tantas ideas que se formó para cumplir estos objetivos es la de tener su propio sistema virtual de gestión y comunicación, donde se concentre todo el trabajo y se forme esa comunidad que tanto quieren.
Los dueños contactaron a nuestro equipo de desarrollo para construir este sistema antes de que la torre abra sus puertas. "

Entrevista de Licitacion de Requerimientos:
"
•	¿Qué problemática detectas en cuanto a la comunicación entre residentes?
La comunicación es informal y anticuada, únicamente por grupos de WhatsApp que nadie lee o cuando se cruzan en el edificio. Muchas veces, el encargado termina actuando como vía de comunicación entre ellos. El único proceso formal que existe son las reuniones de consorcio.
•	¿Y entre los residentes con los administrativos del edificio?
Hay veces que llega a ser nula, teniendo que pasar de boca en boca, como un teléfono descompuesto. Esto también hace que la administración no dimensione los problemas de convivencia que están ocurriendo en el momento.
•	¿Vos, como encargado, sentirías que unificar la comunicación en un único sistema mejoraría la vida en común del edificio?
Por supuesto, necesitamos algo que ayude a que el edificio se sienta como una comunidad y no como un grupo de desconocidos. Además, se mueve mucha información en un lugar así, tener todo en el mismo espacio sería muy cómodo.
•	¿Quiénes conviven en el edificio?
El edificio se divide principalmente en Inquilinos y Propietarios, que englobando todo serían los residentes. Los propietarios tienen más “poder” que los inquilinos, en cuestiones de toma de decisiones. Luego estamos los que trabajamos para el edifico, como el encargado, que puede ser con o sin vivienda, servicios de limpieza, de mantenimiento, servicios de cuidado personal. Y, por último, está la administración, que no suele vivir en el edificio, pero obviamente es parte de este.
•	¿A qué tipo de usuario tendría que estar dedicado el uso del sistema?
No se puede saber qué clase de persona vive en un edificio, varían las edades, profesiones, estudios, culturas. Así que lo mejor sería asumir que el usuario no tiene conocimiento alguno. Otra cuestión es la administración, que seguro si manejan más herramientas informáticas.
"

Análisis de Entrevista
De la entrevista realizada, podemos sacar que las personas que conviven en un edificio son los residentes, que se dividen en inquilinos y propietarios, encargados y la administración. 
También, sacamos que un supuesto sistema de gestión de un edifico debería ofrecer medios de comunicación entre todas estas personas. 
Por último, entendemos que este sistema debería ser de un uso muy sencillo, ya que no podemos asumir el conocimiento informático del usuario.

Partes del sistema:
•	Tickets para el Encargado: Un ticket es un pedido, detallado con la información de quien lo pide, y que es lo que necesita. En este caso, los residentes pedirían los servicios del encargado. Del lado del encargado, el ticket tiene un estado, y los tickets quedan registrados.
•	Espacio de mensajería entre Residentes: Espacio donde los residentes pueden comunicarse entre ellos.
•	Mensajería de Administración: Sección para que los residentes y la administración se comuniquen. Puede ser en forma de notificaciones (de la Administración a los Residentes) y quejas (De los Residentes a la Administración). También se puede incluir al encargado en la cuestión. Con este sistema se completaría la comunicación entre todas las partes, haciendo distinción en cómo se comunican unos con otros.
•	Reservas de Espacios comunes: Un sistema para mejorar la organización de los espacios compartidos, necesario para seguir mejorando la comunicación entre residentes y evitar la mayor cantidad de conflictos.
•	Gestión Financiera: Sección donde la administración maneje las cuestiones financieras del edificio. Buscamos que se automaticen la mayor cantidad de tareas.
•	Gestión de Residentes: Sección donde se vea la información de los residentes, para facilitar la búsqueda de esta información al máximo.
•	Espacio personal: Es una sección donde cualquier usuario pueda colocar sus notas personales y que nadie más las lea.

Stack Tecnologico:
Para el desarrollo y posterior puesta en producción de este proyecto vamos a utilizar el entorno de .NET 8. Más específicamente:
•	ASP.NET Core, plantilla MVC, para el desarrollo Full Stack
•	Entity Framework integrado a ASP.NET
•	Base de Datos SQL y Hosting en Azure

A eso le sumamos las siguientes tecnologías:
•	GIT para el control de versiones
•	GitHub para el alojamiento de código
•	Jira para el Product Backlog
•	Visual Studio como editor de código

