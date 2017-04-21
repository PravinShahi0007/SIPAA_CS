using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIPAA_CS.Accesos;
using SIPAA_CS;
using SIPAA_CS.RecursosHumanos;
using SIPAA_CS.RecursosHumanos.Asignaciones;
using SIPAA_CS.RecursosHumanos.Catalogos;
using SIPAA_CS.RecursosHumanos.Reportes;
using SIPAA_CS.RecursosHumanos.Procesos;
using SIPAA_CS.Accesos.Reportes;

namespace SIPAA_CS
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FiltroProcesosUsuarios());
        }
    }
}
