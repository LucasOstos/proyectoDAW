using DAL;
using System;
using System.Collections.Generic;

namespace SERVICIOS
{
    public class GestorIntegridad
    {
        private readonly IntegridadDAL dal = new IntegridadDAL();

        public void GuardarIntegridad(TablasBD tabla)
        {
            var (filas, columnas) = dal.ObtenerDatosTabla(tabla);
            var HVD = CalcularDV(filas);
            var VVD = CalcularDV(columnas);
            dal.GuardarRegistroIntegridad(tabla, HVD, VVD);
        }

        public void GuardarIntegridadTodasLasTablas()
        {
            var tablasString = dal.ObtenerTablasAVerificar();

            foreach (var tablaStr in tablasString)
            {
                if (Enum.TryParse<TablasBD>(tablaStr, out var tablaEnum))
                {
                    GuardarIntegridad(tablaEnum);
                }
                else
                {
                    // Manejar tablas no mapeadas si se desea
                }
            }
        }

        public Dictionary<TablasBD, bool> VerificarIntegridadTodasLasTablas()
        {
            var resultados = new Dictionary<TablasBD, bool>();

            var tablasString = dal.ObtenerTablasAVerificar();

            foreach (var tablaStr in tablasString)
            {
                if (Enum.TryParse<TablasBD>(tablaStr, out var tablaEnum))
                {
                    bool resultado = VerificarIntegridad(tablaEnum);
                    resultados[tablaEnum] = resultado;
                }
                else
                {
                    resultados[default] = false;
                }
            }

            return resultados;
        }

        public bool VerificarIntegridad(TablasBD tabla)
        {
            var (filas, columnas) = dal.ObtenerDatosTabla(tabla);
            var HVD = CalcularDV(filas);
            var VVD = CalcularDV(columnas);

            var resultado = dal.LeerRegistroIntegridad(tabla);
            if (resultado == null) return false;

            return HVD == resultado.Value.HVD && VVD == resultado.Value.VVD;
        }

        private string CalcularDV(List<string[]> datos)
        {
            var hasheados = new List<string>();
            foreach (var fila in datos)
                hasheados.Add(CalcularDVFila(fila));
            return CalcularDVFila(hasheados.ToArray());
        }

        private string CalcularDVFila(string[] datos)
        {
            var encriptador = new Encriptador();
            string acumulado = "";
            foreach (var item in datos)
            {
                acumulado += item;
                acumulado = encriptador.EncriptarIrreversible(acumulado);
            }
            return acumulado;
        }
    }
}
