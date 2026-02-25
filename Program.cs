using System;


public class FinancieroSys
{
    //El método Main debe estar separado de las Clases
    public static void Main(String[] args)
    {
        Console.WriteLine("Bienvenido al Sistema Financiero");
        Console.WriteLine("\n Menú Sistema Financiero: ");

        //Invocar Menú
        Menu menu = new Menu();
        menu.MostrarMenu();
    }

    public class Cliente
    {
        //Datos del Cliente
        public string Nombre { get; set; }
        public int Edad { get; set; }

        //Datos Financieros 
        // Propiedad con acceso público de lectura y escritura restringida a la propia clase.
        // Desde fuera solo se puede consultar su valor, pero únicamente métodos internos de 'Cliente' pueden modificarlo.
        public int Documento { get; private set; }
        public HistorialCrediticio Historial { get; private set; }

        //Aprobaciones de créditos
        public bool CreditoPersonal { get; private set; }
        public bool CreditoHipotecario { get; private set; }
        public bool CreditoVehicular { get; private set; }

        //Resultados financieros
        public decimal CuotaMensual { get; private set; }
        public decimal Intereses { get; private set; }
        public string Observaciones { get; private set; }

        //Métodos de asignación (forma corta)
        public void AsignarDocumento(int documento) => Documento = documento;
        public void AsignarHistorial(HistorialCrediticio historial) => Historial = historial;
        public void AsignarCreditoPersonal(bool aprobado) => CreditoPersonal = aprobado;
        public void AsignarCreditoHipotecario(bool aprobado) => CreditoHipotecario = aprobado;
        public void AsignarCreditoVehicular(bool aprobado) => CreditoVehicular = aprobado;

        public void datosCliente(string nombre, int edad)
        {
            Nombre = nombre;
            Edad = edad;
        }

        public void Presentarse()
        {
            Console.WriteLine("Mi nombre es: " + Nombre);
            Console.WriteLine("Mi edad es: " + Edad);
        }

        // Método para calcular resultados
        public void CalcularResultados(decimal monto, int plazoMeses, decimal tasaInteres)
        {
            decimal tasaMensual = tasaInteres / 12;
            CuotaMensual = monto * tasaMensual / (1 - (decimal)Math.Pow(1 + (double)tasaMensual, -plazoMeses));

            Intereses = (CuotaMensual * plazoMeses) - monto;

            if (Historial == HistorialCrediticio.Bueno)
                Observaciones = "Cliente con buen historial, bajo riesgo.";
            else if (Historial == HistorialCrediticio.Regular)
                Observaciones = "Cliente con historial regular, riesgo moderado.";
            else
                Observaciones = "Cliente con historial malo, alto riesgo.";
        }
    }

    public enum HistorialCrediticio
    {
        Bueno = 1,
        Regular = 2,
        Malo = 3
    }


    class Menu
    {
        // Variables globales para reutilizar en cálculos
        int monto;
        int meses;
        int ingresosMensuales;
        int gastosFijos;
        int deudasActuales;

        // Variables para el Hipotecario
        double montoHipotecarioGuardado;
        int plazoMesesHipotecarioGuardado;
        decimal tasaHipotecariaGuardada;


        Cliente cliente = new Cliente();

        public void MostrarMenu()
        {
            while (true)
            {
                Console.WriteLine("1.Registrar Cliente");
                Console.WriteLine("2.Solicitar Crédito");
                Console.WriteLine("3.Evaluar Crédito");
                Console.WriteLine("4.Ver Resultados");
                Console.WriteLine("5.Salir");

                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Ingrese su Nombre Completo: ");
                        cliente.Nombre = Console.ReadLine();

                        Console.WriteLine("\n Ingrese su Edad: ");
                        cliente.Edad = int.Parse(Console.ReadLine());

                        if (cliente.Edad < 18)
                        {
                            Console.WriteLine("Usted es menor de edad: Lo sentimos, no puede continuar");
                            break;
                        }

                        Console.WriteLine("\n Ingrese su Número de Identificación: ");
                        int documento = int.Parse(Console.ReadLine());
                        // El número de documento no puede ser cero, negativo ni repetirse con un documento existente
                        if (documento <= 0 || documento == cliente.Documento)
                        {
                            Console.WriteLine("Ingreso inválido");
                            break;
                        }
                        //Invocando documeto
                        cliente.AsignarDocumento(documento);

                        Console.WriteLine("\n ¿Cuales son sus Ingresos Mensuales?: ");
                        ingresosMensuales = int.Parse(Console.ReadLine());

                        Console.WriteLine("\nIngrese el monto mensual destinado a gastos fijos: ");
                        gastosFijos = int.Parse(Console.ReadLine());

                        double porcentajeGastosFijos = (double)gastosFijos / ingresosMensuales * 100;
                        Console.WriteLine($"Porcentaje de gastos fijos: {porcentajeGastosFijos:F2}%");

                        Console.WriteLine("\nIngrese el monto total de sus deudas actuales: ");
                        deudasActuales = int.Parse(Console.ReadLine());

                        Console.WriteLine("\n === SELECCIONE HISTORIAL CREDITICIO: ===");
                        Console.WriteLine("1. Bueno");
                        Console.WriteLine("2. Regular");
                        Console.WriteLine("3. Malo");
                        int opcionHistorial = int.Parse(Console.ReadLine());

                        // Convierte la opción ingresada a un valor del enum HistorialCrediticio y la asigna al cliente
                        cliente.AsignarHistorial((HistorialCrediticio)opcionHistorial);

                        Console.WriteLine("\n¡Cliente registrado exitosamente!");
                        break;

                    case 2:
                        Console.WriteLine("\n=== PROCESO PARA SOLICITAR UN CRÉDITO ===");
                        Console.WriteLine("Ingrese el monto solicitado: ");
                        monto = int.Parse(Console.ReadLine());

                        Console.WriteLine("\nIngrese el plazo del crédito en meses: ");
                        meses = int.Parse(Console.ReadLine());
                        break;

                    case 3:
                        Console.WriteLine("\n=== EVALUACIÓN AUTOMÁTICA ===");
                        Console.WriteLine("1. Personal");
                        Console.WriteLine("2. Hipotecario");
                        Console.WriteLine("3. Vehicular");
                        int tipoCredito = int.Parse(Console.ReadLine());

                        switch (tipoCredito)
                        {
                            case 1: // PERSONAL
                                Console.WriteLine("\nEvaluación Crédito Personal");
                                Console.WriteLine("Ingrese la tasa de interés anual (%): ");
                                double tasaAnual = double.Parse(Console.ReadLine()) / 100;
                                double tasaMensual = tasaAnual / 12;

                                double cuotaPersonal = (monto * tasaMensual) / (1 - Math.Pow(1 + tasaMensual, -meses));
                                double costoTotalPersonal = cuotaPersonal * meses;
                                double interesesPersonal = costoTotalPersonal - monto;
                                double capacidadPagoPersonal = (cuotaPersonal / ingresosMensuales) * 100;

                                Console.WriteLine($"Cuota mensual: {cuotaPersonal:F2}");
                                Console.WriteLine($"Costo total: {costoTotalPersonal:F2}, Intereses: {interesesPersonal:F2}");
                                Console.WriteLine($"La cuota representa el {capacidadPagoPersonal:F2}% de sus ingresos.");
                                if (capacidadPagoPersonal <= 40 && deudasActuales == 0 && cliente.Historial == HistorialCrediticio.Bueno)
                                {
                                    cliente.AsignarCreditoPersonal(true);   
                                    Console.WriteLine("Crédito Personal APROBADO");
                                }
                                else
                                {
                                    cliente.AsignarCreditoPersonal(false);  
                                    Console.WriteLine("Crédito Personal RECHAZADO");
                                }
                                break;

                            case 2: // HIPOTECARIO
                                Console.WriteLine("\nEvaluación Crédito Hipotecario");
                                Console.WriteLine("Ingrese el monto solicitado: ");
                                double montoHipotecario = double.Parse(Console.ReadLine());

                                Console.WriteLine("Ingrese el plazo en años: ");
                                int plazoAnios = int.Parse(Console.ReadLine());
                                int plazoMeses = plazoAnios * 12;

                                Console.WriteLine("Ingrese la tasa de interés anual (%): ");
                                double tasaAnualHipotecaria = double.Parse(Console.ReadLine()) / 100;

                                // GUARDAR datos del crédito hipotecario
                                montoHipotecarioGuardado = montoHipotecario;
                                plazoMesesHipotecarioGuardado = plazoMeses;
                                tasaHipotecariaGuardada = (decimal)tasaAnualHipotecaria;

                                double tasaMensualHipotecaria = tasaAnualHipotecaria / 12;

                                double cuotaHipotecaria = (montoHipotecario * tasaMensualHipotecaria) /
                                                          (1 - Math.Pow(1 + tasaMensualHipotecaria, -plazoMeses));
                                double costoTotalHipotecario = cuotaHipotecaria * plazoMeses;
                                double interesesHipotecario = costoTotalHipotecario - montoHipotecario;
                                double capacidadPagoHipotecario = (cuotaHipotecaria / ingresosMensuales) * 100;

                                Console.WriteLine($"Cuota mensual: {cuotaHipotecaria:F2}");
                                Console.WriteLine($"Costo total: {costoTotalHipotecario:F2}, Intereses: {interesesHipotecario:F2}");
                                Console.WriteLine($"La cuota representa el {capacidadPagoHipotecario:F2}% de sus ingresos.");

                                if (capacidadPagoHipotecario <= 30 && deudasActuales == 0 && cliente.Historial != HistorialCrediticio.Malo)
                                {
                                    cliente.AsignarCreditoHipotecario(true);   
                                    Console.WriteLine("Crédito Hipotecario APROBADO");
                                }
                                else
                                {
                                    cliente.AsignarCreditoHipotecario(false);  
                                    Console.WriteLine("Crédito Hipotecario RECHAZADO");
                                }

                                break;

                            case 3: // VEHICULAR
                                Console.WriteLine("\nEvaluación Crédito Vehicular");
                                Console.WriteLine("Ingrese el monto solicitado: ");
                                double montoVehicular = double.Parse(Console.ReadLine());

                                Console.WriteLine("Ingrese el plazo en años (1 a 7): ");
                                int plazoAniosVehicular = int.Parse(Console.ReadLine());
                                int plazoMesesVehicular = plazoAniosVehicular * 12;

                                Console.WriteLine("Ingrese la tasa de interés anual (%): ");
                                double tasaAnualVehicular = double.Parse(Console.ReadLine()) / 100;
                                double tasaMensualVehicular = tasaAnualVehicular / 12;

                                double cuotaVehicular = (montoVehicular * tasaMensualVehicular) /
                                                        (1 - Math.Pow(1 + tasaMensualVehicular, -plazoMesesVehicular));
                                double costoTotalVehicular = cuotaVehicular * plazoMesesVehicular;
                                double interesesVehicular = costoTotalVehicular - montoVehicular;
                                double capacidadPagoVehicular = (cuotaVehicular / ingresosMensuales) * 100;

                                Console.WriteLine($"Cuota mensual: {cuotaVehicular:F2}");
                                Console.WriteLine($"Costo total: {costoTotalVehicular:F2}, Intereses: {interesesVehicular:F2}");
                                Console.WriteLine($"La cuota representa el {capacidadPagoVehicular:F2}% de sus ingresos.");

                                if (capacidadPagoVehicular <= 35 && deudasActuales == 0 && cliente.Historial == HistorialCrediticio.Bueno)
                                {
                                    cliente.AsignarCreditoVehicular(true);  
                                    Console.WriteLine("Crédito Vehicular APROBADO");
                                }
                                else
                                {
                                    cliente.AsignarCreditoVehicular(false);  
                                    Console.WriteLine("Crédito Vehicular RECHAZADO");
                                }

                                break;

                            default:
                                Console.WriteLine("Opción inválida.");
                                break;
                        }
                        break;
                    case 4:
                        Console.WriteLine("=== RESUMEN DEL CRÉDITO SOLICITADO ===");
                        Console.WriteLine("El crédito personal fue: " + (cliente.CreditoPersonal ? "APROBADO" : "RECHAZADO"));
                        Console.WriteLine("El crédito hipotecario fue: " + (cliente.CreditoHipotecario ? "APROBADO" : "RECHAZADO"));
                        Console.WriteLine("El crédito vehicular fue: " + (cliente.CreditoVehicular ? "APROBADO" : "RECHAZADO"));
                        Console.WriteLine("El riesgo financiero es: " + cliente.Historial);

                        if (cliente.CreditoHipotecario)
                        {
                            cliente.CalcularResultados(
                                (decimal)montoHipotecarioGuardado,
                                plazoMesesHipotecarioGuardado,
                                tasaHipotecariaGuardada
                            );
                        }


                        Console.WriteLine("La cuota mensual es de: $" + cliente.CuotaMensual.ToString("N2"));
                        Console.WriteLine("Los intereses son: $" + cliente.Intereses.ToString("N2"));
                        Console.WriteLine("Observaciones: " + cliente.Observaciones);
                        break;


                    default:
                        Console.WriteLine("Algo salió mal");
                        break;
                }
            }
        }
    }
}
