using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;


namespace calculadoraProceso
{
    class Program
    {
        private Process p1 = null;
        static void Main(string[] args)
        {
            Process p;
            ejecutaServidor(out p);
            Task.Delay(1000).Wait();

            var cliente = new NamedPipeClientStream("Pipe1");
            cliente.Connect();
            StreamReader reader = new StreamReader(cliente);
            StreamWriter writer = new StreamWriter(cliente);

            String entrada;
            int opcion = 0;
            do
            {
                Menu();
                entrada = Console.ReadLine();
                opcion = Int32.Parse(entrada);
                if(opcion!=-1) 
                { 
                    writer.WriteLine(opcion);
                    writer.Flush();
                    Console.WriteLine("Introduzca el primer operando: ");
                    var operando1 = Console.ReadLine();
                    writer.WriteLine(operando1);
                    writer.Flush();
                    Console.WriteLine("Introduzca el segundo operando: ");
                    var operando2 = Console.ReadLine();
                    writer.WriteLine(operando2);
                    writer.Flush();
                    Console.WriteLine("Resultado: {0}", reader.ReadLine());
                    writer.Flush();
                }
            } while (opcion != -1);


        }

        static Process ejecutaServidor(out Process p1)
        {
            // iniciar un proceso con el servidor y devolver
            ProcessStartInfo info = new ProcessStartInfo(@"C:\Users\iuriz\source\repos\Prog-Servicios\calculadoraProceso\calculadoraServer\bin\Release\netcoreapp3.0\publish\calculadoraServidor.exe");
            // su valor por defecto el false, si se establece a true no se "crea" ventana
            info.CreateNoWindow = false;
            info.WindowStyle = ProcessWindowStyle.Normal;
            // indica si se utiliza el cmd para lanzar el proceso
            info.UseShellExecute = true;
            p1 = Process.Start(info);
            return p1;
        }

        static void Menu()
        {
            Console.WriteLine("Operación a realizar:");
            Console.WriteLine("1 Suma");
            Console.WriteLine("2 Resta");
            Console.WriteLine("3 Multiplicación");
            Console.WriteLine("(-1) salir");
        }
    }
}
    