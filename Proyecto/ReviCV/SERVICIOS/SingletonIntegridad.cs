using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICIOS
{
    public class SingletonIntegridad
    {
        private static readonly Lazy<SingletonIntegridad> _instancia =
            new Lazy<SingletonIntegridad>(() => new SingletonIntegridad());

        public static SingletonIntegridad Instancia => _instancia.Value;

        private SingletonIntegridad() { }

        public bool BaseIntegra { get; private set; }

        public string Detalles { get; private set; }

        public void ActualizarEstado(bool integra, string detalles)
        {
            BaseIntegra = integra;
            Detalles = detalles;
        }
    }
}
