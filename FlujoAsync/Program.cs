namespace FlujoAsync
{
    using System.Diagnostics;
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Iniciamos un contador de tiempo - SINCRONA
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("\n ******************************************************");
            Console.WriteLine("\n Bienvenido a la calculadora de hipotecas Sincrona");
            Console.WriteLine("\n ******************************************************");

            var aniosVidaLaboral = CalculadorHipotecaSync.ObtenerAnosVidaLaboral();
            Console.WriteLine($"\n Años de Vida Laboral Obtenidos: {aniosVidaLaboral}");

            var esTipoContratoIndefinido = CalculadorHipotecaSync.EsTipoContratoIndefinido();
            Console.WriteLine($"\n Tipo de contrato indefinido: {esTipoContratoIndefinido}");

            var sueldoNeto = CalculadorHipotecaSync.ObtenerSueldoNeto();
            Console.WriteLine($"\n SueldoNeto obtenido: {sueldoNeto}€");

            var gastosMensuales = CalculadorHipotecaSync.ObtenerGastosMensuales();
            Console.WriteLine($"\n Gastos Mensuales obtenidos: {gastosMensuales}€");

            var hipotecaConcedida = CalculadorHipotecaSync.AnalizarInformacionParaConcederHipoteca(
                aniosVidaLaboral,
                esTipoContratoIndefinido,
                sueldoNeto,
                gastosMensuales,
                cantidadSolicitada: 5000,
                anosPagar: 30
                );

            var resultado = hipotecaConcedida ? "APROBADA" : "DENEGADA";
            Console.WriteLine($"\n Analisis Finalizado. Su solicitud de hipoteca ha sido: {resultado}");

            stopwatch.Stop();

            Console.WriteLine($"\n La operacion sincrona ha durado: {stopwatch.Elapsed}");

            // Iniciamos un contador de tiempo - ASINCRONA
            stopwatch.Restart();
            Console.WriteLine("\n ******************************************************");
            Console.WriteLine("\n Bienvenido a la calculadora de hipotecas As incrona");
            Console.WriteLine("\n ******************************************************");

            Task<int> aniosVidaLaboralAsync = CalcularHipotecaAsync.ObtenerAnosVidaLaboral();
            Task<bool> esTipoContratoIndefinidoAsync = CalcularHipotecaAsync.EsTipoContratoIndefinido();
            Task<int> sueldoNetoAsync = CalcularHipotecaAsync.ObtenerSueldoNeto();
            Task<int> gastosMensualesAsync = CalcularHipotecaAsync.ObtenerGastosMensuales();

            var analisisHipotecaAsync = new List<Task>
            {
                aniosVidaLaboralAsync,
                esTipoContratoIndefinidoAsync,
                sueldoNetoAsync,
                gastosMensualesAsync
            };
            while (analisisHipotecaAsync.Any())
            {
                Task tareaFinalizada = await Task.WhenAny(analisisHipotecaAsync);
                if (tareaFinalizada == aniosVidaLaboralAsync)
                {
                    Console.WriteLine($"\n Años de Vida Laboral Obtenidos: {aniosVidaLaboralAsync.Result}");
                }
                else if (tareaFinalizada == esTipoContratoIndefinidoAsync)
                {
                    Console.WriteLine($"\n Tipo de contrato indefinido: {esTipoContratoIndefinidoAsync.Result}");
                }
                else if (tareaFinalizada == sueldoNetoAsync)
                {
                    Console.WriteLine($"\n SueldoNeto obtenido: {sueldoNetoAsync.Result}€");
                }
                else if (tareaFinalizada == gastosMensualesAsync)
                {
                    Console.WriteLine($"\n Gastos Mensuales obtenidos: {gastosMensualesAsync.Result}€");
                }
                analisisHipotecaAsync.Remove(tareaFinalizada); // eliminamos de la lista de tareas para ir vaciando y salir del while
            }
            var hipotecaConcedidaAsync = CalculadorHipotecaSync.AnalizarInformacionParaConcederHipoteca(
                aniosVidaLaboralAsync.Result,
                esTipoContratoIndefinidoAsync.Result,
                sueldoNetoAsync.Result,
                gastosMensualesAsync.Result,
                cantidadSolicitada: 5000,
                anosPagar: 30
                );

            var resultadoAsync = hipotecaConcedidaAsync ? "APROBADA" : "DENEGADA";
            Console.WriteLine($"\n Analisis Finalizado. Su solicitud de hipoteca ha sido: {resultadoAsync}");
            stopwatch.Stop();
            Console.WriteLine($"\n La operacion asincrona ha durado: {stopwatch.Elapsed}");
            Console.Read();
        }
    }
}