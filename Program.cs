using System;
using proyecto_final_selenium.lib;

namespace proyecto_final_selenium
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parametros
            var blazeTest = new BlazeTest();
            string url = "https://www.demoblaze.com";

            // Inicio del flujo
            blazeTest.StartBrowser()
                     .StartTest(url)
                     .Wait(5000);
            // Fin del flujo
            blazeTest.CloseBrowser();
        }
    }
}
