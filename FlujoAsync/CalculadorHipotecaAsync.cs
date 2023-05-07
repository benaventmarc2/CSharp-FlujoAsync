using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlujoAsync
{
    public static class CalcularHipotecaAsync
    {
        public static async Task<int> ObtenerAnosVidaLaboral()
        {
            Console.WriteLine("\n Obteniendo años de vida laboral...");
            await Task.Delay(5000); // Esperamos 5 segundos
            return new Random().Next(1, 35); // Devolvemos un valor aleatorio entre 1 y 35
        }
        public static async Task<bool> EsTipoContratoIndefinido()
        {
            Console.WriteLine("\nVerificando si el tipo de contrato es indefinido...");
            await Task.Delay(5000);
            return (new Random().Next(1, 10)) % 2 == 0; // Devolvemos true o false si el valor aleatorio es par / impar
        }
        public static async Task<int> ObtenerSueldoNeto()
        {
            Console.WriteLine("\n Obteniendo sueldo neto...");
            await Task.Delay(5000); // Esperamos 5 segundos
            return new Random().Next(800, 6000); // Devolvemos un valor aleatorio entre 800 y 6000
        }

        public static async Task<int> ObtenerGastosMensuales()
        {
            Console.WriteLine("\n Obteniendo gastos mensuales del usuario...");
            await Task.Delay(5000); // Esperamos 5 segundos
            return new Random().Next(200, 1000);
        }
        public static bool AnalizarInformacionParaConcederHipoteca(int anosVidaLaboral, bool tipoContratoEsIndefinido, int sueldoNeto, int gastosMensuales, int cantidadSolicitada, int anosPagar)
        {
            Console.WriteLine("\nAnalizando información para conceder hipoteca...");
            if (anosVidaLaboral < 2) return false;

            // Obtener la cuota
            var cuota = (cantidadSolicitada / anosPagar) / 12;
            if (cuota >= sueldoNeto || cuota > (sueldoNeto / 2)) return false;
            var porcentajeGastosSobreSueldo = ((gastosMensuales * 100) / sueldoNeto);
            if (porcentajeGastosSobreSueldo > 30) return false;
            if ((cuota + gastosMensuales) >= sueldoNeto) return false;
            if (!tipoContratoEsIndefinido)
            {
                if ((cuota + gastosMensuales) > (sueldoNeto / 3)) return false;
                else return true;
            }
            return true;
        }
    }
}
