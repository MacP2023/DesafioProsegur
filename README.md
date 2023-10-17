# DesafioProsegur
# Gestion de Pedidos Desafio Prosegur
# Gestion de Pedidos Desafio Prosegur especificaciones de la estructura de proyectos, como levantar a aplicacion
# - Levantar la solucion general que se encuentra dentro de la subcarpeta DesafioProsegur
# - Dentro de la solucion se encuentran 5 proyectos
#  1.Proyecto Apis para gestion de usuarios proyecto ApiUsuarios
#  2.Proyecto Apis para gestion de las ordenes/pedidos proyecto ApiGestionPedidos
#  3.Proyecto Front-end GestiondePedidosSB
#  4.Proyecto para Test GestiondePedidosSB_Test
#  5.Proyecto de Utilidades
# - Levantar/ejecutar los proyectos API
# - Levantar el Proyecto GestiondePedidosSB
# Detalle de cada Proyecto:
# - ApiUsuarios: con la interfaz para login y registro de usuarios. Al levantar se dejo el swagger en caso de tener que verificar la interfaz
# - ApiGestionPedidos: con la interfaz para Crear Orden/Pedido, productos, materiales. Al levantar se dejo el swagger en caso de tener que verificar la interfaz
# - UtllidadesGenerales: utilidades genericas usadas en la solucion.
# - GestiondePedidosSB: front-end de la aplicacion, inicia con ventana de login/Registro
# Puntos logrados:
# - Aplicación de MVC en netcore6, con WebAPI
# - El frontend cob Razor Pages
# - Creacion del proyecto de Test Unitiarios.
# - Creacion de 6 entidades,algunas tablas Generales se sustituyeron por Enumerados (TipoRol,Estados,Provincias,TipoMoneda)ya que no  eran requeridos en el alcance sin embargo eran datos necesarios
# - Capa de datos en entityframework (code first) InMemoryDatabase
# - Registro de usuario (Solo de usuario con Rol Usuario)
# - Registro de orden/pedido de aquellos productos que tengan todos los materiales necesarios para su elaboracion
# - Ingreso/login por los diferentes usuarios a la aplicacion y visualizacion de items segun su rol
# Puntos Faltantes:
# - Los puntos opcionales no fueron logrados
# - Facturar las ordenes finalizadas.
# - Ajustar el stok de materiales.
# - Generar comanda.
# - Diferentes impuestos de cada una de nuestras provincias
# - No se logro terminar de configurar casos para el test.
# - No hay Validaciones de campos de entrada
# - Mensajes/alertas durante el proceso de registro de la orden/pedido
 
#  Indicaciones para ver aplicacion
#  - Se puede registrar un nuevo usuario que por default es de Rol Usuario
#  - Los Roles Administrador,Supervisor y Empleado se puede ingresar con los siguientes datos
#    Rol Administrador
#    Usuario: Administrador, Clave: colocar cualquiera
#    Rol Empleado
#    Usuario: Empleado, Clave: colocar cualquiera
#    Rol Supervisor
#    Usuario: Supervisor, Clave: colocar cualquiera
#   - Al momento de levantar/ingresar a la aplicacion, se crean unos datos por defecto para la entidad Producto,Material, ProductoMaterial


