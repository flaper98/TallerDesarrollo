using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace MyFirstSolution
{
    internal class PrimaPracticaFlavioPerez
    {
        private static ILogger logger;
        static void Main(string[] args)
        {
            ConfigureLogger();
            try
            {
                Persona datos = ObtenerDatos();
                string mensaje = GenerarMensaje(datos);
                Console.WriteLine(mensaje);

                Log.Information("Datos de persona procesados correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Log.Error(ex, "Se produjo un error durante la ejecución del programa.");
            }
            Console.ReadKey();
        }
        private static void ConfigureLogger()
        {
            string logFilePath = $"Logs/Log_{DateTime.Now:yyMMdd}.txt";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Logger configurado correctamente.");
        }

        private static Persona ObtenerDatos()
        {
            Console.Write("Ingrese su nombre (máximo 10 caracteres): ");
            string nombre = Console.ReadLine();
            if (nombre.Length > 10)
            {
                throw new Exception("El nombre ingresado supera los 10 caracteres.");
            }

            Console.Write("Ingrese su edad (0-99): ");
            int edad;
            if (!int.TryParse(Console.ReadLine(), out edad) || edad < 0 || edad > 99)
            {
                throw new Exception("La edad ingresada no es válida.");
            }

            Console.Write("Ingrese su género: ");
            bool genero;
            if (!bool.TryParse(Console.ReadLine(), out genero))
            {
                throw new Exception("El género ingresado no es válido.");
            }

            return new Persona(nombre, edad, genero);
        }

        private static string GenerarMensaje(Persona datos)
        {

            string generoStr = datos.Genero ? "Masculino" : "Femenino";
            return $"Hola {datos.Nombre}, {datos.Edad} años, género {generoStr}";
        }
    }

    public class Persona
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public bool Genero { get; set; }

        public Persona(string nombre, int edad, bool genero)
        {
            Nombre = nombre;
            Edad = edad;
            Genero = genero;
        }
    }
}
