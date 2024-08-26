using System.Data.SqlClient;
using System.Numerics;
using Entidades.Excepciones;
using Entidades.Exceptions;
using Entidades.Files;
using Entidades.Interfaces;
using Entidades.Modelos;

namespace Entidades.DataBase
{
    public static class DataBaseManager
    {
        private static string stringConnection;
        private static SqlConnection connection;
        static DataBaseManager()
        {
            DataBaseManager.stringConnection = "Server=.;Database=20230622SP;Trusted_Connection=True;";
        }

        public static string GetImagenComida(string tipo)
        {
            try
            {
                using (DataBaseManager.connection = new SqlConnection(DataBaseManager.stringConnection))
                {
                    string query = "SELECT * FROM comidas WHERE tipo_comida =@tipo";
                    SqlCommand cmd = new SqlCommand(query, DataBaseManager.connection);
                    cmd.Parameters.AddWithValue("tipo_comida", tipo);
                    DataBaseManager.connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        //le paso el numero de columna
                        return reader.GetString(2);
                    }
                    FileManager.Guardar("El tipo de comida es inexistente", "logs.txt", true);
                    throw new ComidaInvalidaExeption("El tipo de comida es inexistente");
                }
            }
            catch (Exception ex)
            {
                FileManager.Guardar("Error al intentar leer de la tabla comidas", "logs.txt", true);
                throw new DataBaseManagerException($"Error al intentar leer de la tabla comidas", ex);
            }
        }

        public static bool GuardarTicket<T>(string nombreEmpleado, T comida) where T : IComestible, new()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(stringConnection))//como usamos el using no hace falta usar el connection.close
                {
                    string sentencia = "INSERT INTO tickets (empleado, ticket) VALUES (@nombre, @ticket)";
                    SqlCommand cmd = new SqlCommand(sentencia, connection);
                    cmd.Parameters.AddWithValue("nombre", nombreEmpleado);
                    cmd.Parameters.AddWithValue("ticket", comida.Ticket);
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                FileManager.Guardar("Error al intentar escribir en la tabla tickets", "logs.txt", true);
                throw new DataBaseManagerException($"Error al intentar escribir en la tabla tickets", ex);
            }
        }
    }
}
