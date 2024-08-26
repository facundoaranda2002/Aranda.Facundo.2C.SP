using Entidades.Exceptions;
using Entidades.Interfaces;
using Entidades.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entidades.Files
{
    public static class FileManager
    {
        private static string path;

        static FileManager()
        {
            FileManager.path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\20230622_Alumn\\";
            FileManager.ValidaExistenciaDeDirectorio();
        }

        private static void ValidaExistenciaDeDirectorio()
        {
            if (!Directory.Exists(FileManager.path))
            {
                try
                {
                    Directory.CreateDirectory(FileManager.path);
                }
                catch (Exception ex)
                {
                    FileManager.Guardar("Error al crear el directorio", "logs.txt", true);
                    throw new FileManagerException("Error al crear el directorio", ex);
                }
            }
        }

        public static bool Serializar<T>(T elemento, string nombreArchivo) where T : class//indico que T(lo que recibe) tiene que ser siempre por referencia
        {
            if (Path.GetExtension(nombreArchivo) == ".json")//devuelve punto y nombre de la extension
            {
                using (StreamWriter sw = new StreamWriter(FileManager.path + nombreArchivo))
                {
                    JsonSerializerOptions opciones = new JsonSerializerOptions();
                    opciones.WriteIndented = true;
                    sw.WriteLine(JsonSerializer.Serialize<T>(elemento, opciones));
                    return true;
                }
            }
            else
            {
                FileManager.Guardar("Error al serializar", "logs.txt", true);
                throw new FileManagerException("Error al serializar");
            }

            
        }

        public static void Guardar(string data, string nombreArchivo, bool append)
        {
            if (Path.GetExtension(nombreArchivo) == ".txt")
            {
                using (StreamWriter sw = new StreamWriter(FileManager.path + nombreArchivo, append))
                {
                    //se almacena en texto plano
                    sw.WriteLine(data);
                }
            }
            else
            {
                FileManager.Guardar("Error al guardar", "logs.txt", true);
                throw new FileManagerException("Error al guardar");
            }
        }
    }
}
