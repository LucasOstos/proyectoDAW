using DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SERVICIOS
{
    public class GestorIntegridad
    {
        private readonly IntegridadDAL dal = new IntegridadDAL();

        public string CalcularDigitoVerificador(string[] datos)
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


        public void GuardarIntegridadTabla(TablasBD tabla)
        {
            List<string> DVHs = dal.ObtenerDVHs(tabla);
            int cantidadRegistros = DVHs.Count;
            string DigitoVerificadorVertical = CalcularDigitoVerificador(DVHs.ToArray());
            dal.GuardarRegistroIntegridad(tabla, DigitoVerificadorVertical, cantidadRegistros);
        }

        public void GuardarIntegridadTodasLasTablas()
        {
            var tablasString = dal.ObtenerTablasAVerificar();
            foreach (var tablaStr in tablasString)
            {
                if (Enum.TryParse<TablasBD>(tablaStr, out var tablaEnum))
                {
                    GuardarIntegridadTabla(tablaEnum);
                }
            }
        }

        public void RecalcularTodasLasTablas()
        {
            var tablasString = dal.ObtenerTablasAVerificar();

            foreach (var tablaStr in tablasString)
            {
                if (Enum.TryParse<TablasBD>(tablaStr, out var tablaEnum))
                {
                    var datosTabla = dal.ObtenerDatosTabla(tablaEnum);

                    if (datosTabla != null && datosTabla.Count > 0)
                    {
                        foreach (var datos in datosTabla)
                        {
                            string dvhCalculado = CalcularDigitoVerificador(datos.datos);
                            dal.GuardarNuevoDVH(tablaEnum, datos.datos[0], dvhCalculado); 
                        }

                        GuardarIntegridadTabla(tablaEnum);
                    }
                }
            }
        }


        public string VerificarIntegridadTodasLasTablas()
        {
            var tablasString = dal.ObtenerTablasAVerificar();
            var mensajesDeError = new StringBuilder();

            foreach (var tablaStr in tablasString)
            {
                if (Enum.TryParse<TablasBD>(tablaStr, out var tablaEnum))
                {
                    string mensaje = VerificarIntegridadTabla(tablaEnum);
                    if (!string.IsNullOrEmpty(mensaje))
                    {
                        mensajesDeError.AppendLine(mensaje);
                    }
                }
            }

            return mensajesDeError.ToString();
        }

        public string VerificarIntegridadTabla(TablasBD tabla)
        {
            var datosTabla = dal.ObtenerDatosTabla(tabla);
            var datosLeidos = dal.LeerRegistroIntegridad(tabla);
            var mensajesDeError = new StringBuilder();
            var DVHs = new List<string>();
            string mensajeDVV = null;

            if (datosLeidos == null)
            {
                mensajesDeError.AppendLine($"Error al leer la tabla {tabla.ToString()}");
                return mensajesDeError.ToString();
            }

            if (datosTabla.Count > datosLeidos.Value.CR)
                mensajesDeError.AppendLine($"Se han agregado registros de manera externa en la tabla {tabla.ToString()}.");

            if (datosTabla.Count < datosLeidos.Value.CR)
                mensajesDeError.AppendLine($"Se han eliminado registros de manera externa en la tabla {tabla.ToString()}.");

            foreach (var datos in datosTabla)
            {
                string dvhCalculado = CalcularDigitoVerificador(datos.datos);
                DVHs.Add(dvhCalculado);

                if (dvhCalculado != datos.dvh)
                {
                    mensajesDeError.AppendLine($"Error en registro con clave \"{datos.datos[0]}\" en la tabla {tabla.ToString()}.");
                }
            }

            string DVVCalculado = CalcularDigitoVerificador(DVHs.ToArray());

            if (DVVCalculado != datosLeidos.Value.DVV)
                mensajeDVV = $" La tabla \"{tabla}\" posee datos corruptos.";

            if (mensajeDVV != null)
                mensajesDeError.Insert(0, mensajeDVV + Environment.NewLine);

            return mensajesDeError.ToString();
        }








        //public void GuardarIntegridad(TablasBD tabla)
        //{
        //    var (filas, columnas) = dal.ObtenerDatosTabla(tabla);
        //    List<(int, string)> HVD = CalcularDV(filas);
        //    List<(int, string)> VVD = CalcularDV(columnas);
        //    dal.GuardarRegistroIntegridad(tabla, HVD, VVD);
        //}

        //public void GuardarIntegridadTodasLasTablas()
        //{
        //    var tablasString = dal.ObtenerTablasAVerificar();

        //    foreach (var tablaStr in tablasString)
        //    {
        //        if (Enum.TryParse<TablasBD>(tablaStr, out var tablaEnum))
        //        {
        //            GuardarIntegridad(tablaEnum);
        //        }
        //        else
        //        {
        //            // Manejar tablas no mapeadas si se desea
        //        }
        //    }
        //}

        //public Dictionary<TablasBD, bool> VerificarIntegridadTodasLasTablas()
        //{
        //    var resultados = new Dictionary<TablasBD, bool>();
        //    var tablasString = dal.ObtenerTablasAVerificar();
        //    foreach (var tablaStr in tablasString)
        //    {
        //        if (Enum.TryParse<TablasBD>(tablaStr, out var tablaEnum))
        //        {
        //            bool resultado = VerificarIntegridad(tablaEnum);
        //            resultados[tablaEnum] = resultado;
        //        }
        //        else
        //        {
        //            resultados[default] = false;
        //        }
        //    }
        //    return resultados;
        //}

        //public bool VerificarIntegridad(TablasBD tabla)
        //{
        //    var (filas, columnas) = dal.ObtenerDatosTabla(tabla);
        //    var HVD = CalcularDV(filas);
        //    var VVD = CalcularDV(columnas);

        //    var resultado = dal.LeerRegistroIntegridad(tabla);
        //    if (resultado == null) return false;

        //    return true;
        //    //return HVD == resultado.Value.HVD && VVD == resultado.Value.VVD;
        //}

        //private List<(int, string)> CalcularDV(List<string[]> datos)
        //{
        //    var hasheados = new List<(int, string)>();
        //    foreach (var fila in datos)
        //        hasheados.Add((int.Parse(datos[0].ToString()), CalcularDVFila(fila)));
        //    return hasheados;
        //}

        //private string CalcularDVFila(string[] datos)
        //{
        //    var encriptador = new Encriptador();
        //    string acumulado = "";
        //    foreach (var item in datos)
        //    {
        //        acumulado += item;
        //        acumulado = encriptador.EncriptarIrreversible(acumulado);
        //    }
        //    return acumulado;
        //}
    }
}
