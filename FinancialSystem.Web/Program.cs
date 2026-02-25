using System;


public class BancoSys
{
    public static void Main(String[] args)
    {
        Console.WriteLine("BIENVENIDO AL PROGRAMA BANCARIO\n");

        // PRIMERO SE INSTANCIA EL OBJETO
        Cliente cliente = new Cliente();

        // LUEGO SE INICIALIZAN LOS VALORES DEL OBJETO (ESTADO)
        // Se invocan los métodos NumeroDocumento y CuentaDeAhorros del objeto 'cliente',
        // pasando como argumentos los valores 1234567890 y 500000000 respectivamente.
        // Esto establece el número de documento y el saldo inicial de la cuenta de ahorros en las propiedades correspondientes del cliente.

        cliente.NumeroDocumento(1234567890);// PASO 3 - Invocación de método sobre objeto: asigna el documento
        cliente.CuentaDeAhorros(500000000);
        cliente.LaCuentaCorrinete(70000000);

        // FINALMENTE SE UTILIZAN EN EL MENÚ
        Menu menu = new Menu();
        menu.MostrarMenu(cliente);

    }


    // Clase Cliente
    public class Cliente
    {
        //Atributos 
        public string Nombre { get; private set; }
        public int Documento { get; private set; } // PASO 1 Propiedad Documento: Propiedad autoimplementada: lectura pública, escritura restringida a la clase
        public int Edad { get; private set; }

        public int CuentaAhorros { get; private set; }
        public int CuentaCorriente { get; private set; }
        public int RetiroDeAhorros { get; private set; }

        //Constructor
        public void datosCliente(string nombre, int edad)
        {
            Nombre = nombre;
            Edad = edad;
        }

        // PASO 2 - Método NumeroDocumento:
        // Asigna el número de documento al cliente y lo retorna (return).
        // Uso: permite establecer el valor de la propiedad Documento y obtenerlo inmediatamente.
        public int NumeroDocumento(int documento)
        {
            Documento = documento;
            return Documento;
        }

        // Método con Retorno de Cuenta de Ahorros del Cliente
        public int CuentaDeAhorros(int cuentaAhorros)
        {
            CuentaAhorros = cuentaAhorros;
            return CuentaAhorros;
        }
        public int LaCuentaCorrinete(int cuentaCorriente)
        {
            CuentaCorriente = cuentaCorriente;
            return CuentaCorriente;
        }

        //Es un Método void porque su responsabilidad NO es devolver un valor, sino modificar el estado del objeto.
        public void DepositarAhorros(int monto)
        {
            if (monto <= 0)
            {
                Console.WriteLine("El monto a depositar debe ser mayor a cero.");
                return;
            }
            // Se actualiza el valor de Cuenta de Ahorros (Es el calculo)
            CuentaAhorros += monto;
        }

        public void DepositarCorriente(int monto)
        {
            if (monto <= 0)
            {
                Console.WriteLine("El monto a depositar debe ser mayor a cero");
                return; // En metodos void (Return) termina con la ejecución del método.
            }
            // Se actualiza el valor de Cuenta de Ahorros (Es el calculo)
            CuentaCorriente += monto;
        }

        public void retirarAhorros(int retiroDineroAhorro)
        {
            if (retiroDineroAhorro <= 0)
            {
                Console.WriteLine("El monto a retirar debe ser mayor a cero.");
                return;
            } 
            if (retiroDineroAhorro > CuentaAhorros)
            {
                Console.WriteLine("Fondos Insuficientes en la cuenta de ahorros");
                return;
            }
            CuentaAhorros -= retiroDineroAhorro;
        }

        public void retirarCuentaC(int retiroDineroCorriente)
        {
            if (retiroDineroCorriente <= 0)
            {
                Console.WriteLine("El monto a retirar debe ser mator a cero.");
                return;
            }
            if (retiroDineroCorriente > CuentaCorriente)
            {
                Console.WriteLine("Fondos insuficientes en la cuenta corriente");
                return;
            }
            CuentaCorriente -= retiroDineroCorriente;
        }

        public void MostrarEstadoCuenta()
     {
         Console.WriteLine("=== ESTADO DE CUENTA ===");
         Console.WriteLine("Cliente: " + Nombre);
         Console.WriteLine("Documento: " + Documento);
         Console.WriteLine("Edad: " + Edad);
         Console.WriteLine("Saldo Cuenta de Ahorros: " + CuentaAhorros + " Pesos");
         Console.WriteLine("Saldo Cuenta Corriente: " + CuentaCorriente + " Pesos");
         Console.WriteLine("=========================");
}

    }


    // Creo el Metodo Menú
    class Menu
    {
        //Debo crear dentro de la Clase Menú el Objeto Cliente
        //Cliente cliente = new Cliente();

        //Toda Instruccion de una Clase debe estar contenida en un Metodo Ej: (MostrarMenú)


        public void MostrarMenu(Cliente cliente)
        {


            while (true)
            {
                Console.WriteLine("=== ESTE ES EL MENÚ BANCARIO ===");
                Console.WriteLine("1. Registro de Cliente");
                Console.WriteLine("2. Saldo Cuenta de Ahorros");
                Console.WriteLine("3. Saldo Cuenta Corriente");
                Console.WriteLine("4. Depósitos");
                Console.WriteLine("5. Retiros");
                Console.WriteLine("6. Estado de Cuenta");

                Console.WriteLine("Ingrese el número del proceso que desea iniciar:");
                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {

                    case 1:
                        Console.WriteLine("Ingresar su Nombre y Apellidos completos");
                        string nombre = Console.ReadLine();

                        Console.WriteLine("Que edad tiene usted\n");
                        int edad = int.Parse(Console.ReadLine());
                        if (edad < 18)
                        {
                            Console.WriteLine("Lo sentimos: Usted es menor de edad");
                            break;
                        }

                        Console.WriteLine("Digite su número de Documento\n");
                        int suDocumento = int.Parse(Console.ReadLine());

                        // Validar que el documento no sea negativo y que coincida con el del cliente
                        if (suDocumento <= 0 || suDocumento != cliente.Documento)
                        {
                            Console.WriteLine("El número de documento ingresado no es válido.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Documento válido");
                        }
                        break;
                    case 2:
                        Console.WriteLine("=== INFORMACIÓN DE LA CUENTA ===");
                        Console.WriteLine("1. Consultar Saldo Cuenta de Ahorros");

                        int consultaAhorro = int.Parse(Console.ReadLine());

                        if (consultaAhorro == 1)
                        {
                            Console.WriteLine("Su Saldo Actual es: " + (cliente.CuentaAhorros) + " Pesos\n");
                        }
                        break;

                    case 3:
                        Console.WriteLine("=== INFORMACIÓN DE LA CUENTA ===");
                        Console.WriteLine("1. Consultar Saldo Cuenta Corriente");

                        int consultaCorriente = int.Parse(Console.ReadLine());

                        if (consultaCorriente == 1)
                        {
                            Console.WriteLine("Su Saldo Actual es: " + (cliente.CuentaCorriente) + " Pesos\n");
                        }
                        break;
                    case 4:
                        Console.WriteLine("=== TRANSACCIÓN: DEPÓSITO DE FONDOS ===");
                        Console.WriteLine("=== SELECCIÓN DE CUENTA ===");
                        Console.WriteLine("1. Cuenta de Ahorros");
                        Console.WriteLine("2. Cuenta Corriente");

                        int fondos = int.Parse(Console.ReadLine());

                        if (fondos == 1)
                        {
                            Console.WriteLine("Ingrese el monto a depositar: ");
                            int depositarMontoAhorros = int.Parse(Console.ReadLine());

                            cliente.DepositarAhorros(depositarMontoAhorros);
                            Console.WriteLine("Depósito realizado con éxito. Su nuevo saldo es: " + cliente.CuentaAhorros);

                        }
                        else if (fondos == 2)
                        {
                            Console.WriteLine("Ingrese el monto a depositar: ");
                            int depositarMontoCorriente = int.Parse(Console.ReadLine());

                            cliente.DepositarCorriente(depositarMontoCorriente);
                            Console.WriteLine("Depósito realizado con éxito. Su nuevo saldo es: " + cliente.CuentaCorriente);
                        }
                        break;
                    case 5:
                        Console.WriteLine("=== RETIRO DE DINERO ===");
                        Console.WriteLine("=== SELECCIÓN DE CUENTA ===");
                        Console.WriteLine("1. Cuenta de Ahorros");
                        Console.WriteLine("2. Cuenta Corriente");

                        int seleccion = int.Parse(Console.ReadLine());

                        if (seleccion == 1)
                        {
                            Console.WriteLine("Ingrese el monto a retirar: ");
                            int retiroAh = int.Parse(Console.ReadLine());

                            cliente.retirarAhorros(retiroAh);
                            Console.WriteLine("Retiro realizado con éxito: su nuevo saldo es: " + cliente.CuentaAhorros);

                        } if (seleccion == 2)
                        {
                            Console.WriteLine("Ingrese el monto a retirar: ");
                            int retiroCc = int.Parse(Console.ReadLine());

                            cliente.retirarCuentaC(retiroCc); Console.WriteLine("Retiro realizado con éxito: su nuevo saldo es: " + cliente.CuentaCorriente);
                        }
                        break;

                        case 6:
                        cliente.MostrarEstadoCuenta();
                        break;

                    default:
                    Console.WriteLine("Algo salió mal: intentalo de nuevo");
                    break;
                }
            }
        }
    }
}
