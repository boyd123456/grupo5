namespace PlataExpress.Models
{
    public class anotacionBaseDeDatos
    {

        public static List<Remesa> Remesas = new List<Remesa>()
        {
            new Remesa
            {
                IdRemesa = 1,
                TipoOperacion = "Envío",
                Nombre = "Juan Perez",
                Agencia = "Lima Centro",
                Monto = 200,
                Comision = 10,
                FechaEnvio = DateTime.Now,
                Estado = "En proceso"
            },

            new Remesa
            {
                IdRemesa = 2,
                TipoOperacion = "Envío",
                Nombre = "Maria Lopez",
                Agencia = "San Isidro",
                Monto = 350,
                Comision = 15,
                FechaEnvio = DateTime.Now,
                Estado = "Completado"
            }
        };

        /*CREATE TABLE [dbo].[Usuarios]
(
	[IdUsuario] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nombres] VARCHAR(100) NOT NULL, 
    [Apellidos] VARCHAR(100) NOT NULL,
    [Correo] VARCHAR(100) NOT NULL, 
    [Usuario] VARCHAR(100) NOT NULL, 
    [Clave] VARCHAR(100) NOT NULL, 
    [Telefono] VARCHAR(20) NOT NULL, 
    [Dni] VARCHAR(8) NOT NULL, 
    [Rol] VARCHAR(50) NOT NULL, 
    [FechaRegistro] DATETIME NOT NULL DEFAULT GETDATE()




        ------------------------------------

        CONEXION DE LA BASE DE DATOS


        "ConnectionStrings": {
    "DineroConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BDplataExpress;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  
},
)
*/

    }
}
