using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace calculadoraServidor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var server = new NamedPipeServerStream("Pipe1");
                server.WaitForConnection();
                Console.WriteLine("Servidor esperando datos");
                StreamReader reader = new StreamReader(server);
                StreamWriter writer = new StreamWriter(server);
                int resultado = 0;
                while (true)
                {
                    var entrada = reader.ReadLine();
                    Console.WriteLine(String.Join("Se ha elegido la opcion: ", entrada));
                    int opcion = int.Parse(entrada);
                    Console.WriteLine("Ha elegido opción 1");
                    var operando1 = reader.ReadLine();
                    Console.WriteLine("Operando 1: " + operando1);
                    var operando2 = reader.ReadLine();
                    Console.WriteLine("Operando 2: " + operando2);
                    int op1 = int.Parse(operando1);
                    int op2 = int.Parse(operando2);
                    switch (opcion)
                    {
                        case 1:
                            resultado = op1 + op2;
                            writer.WriteLine(resultado);
                            writer.Flush();
                            break;
                        case 2:
                            resultado = op1 - op2;
                            writer.WriteLine(resultado);
                            writer.Flush();
                            break;
                        case 3:
                            resultado = op1 * op2;
                            writer.WriteLine(resultado);
                            writer.Flush();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error de conexión. Apagando servidor.");
            }


        }
    }
}