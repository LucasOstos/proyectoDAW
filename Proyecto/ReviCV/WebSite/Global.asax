<%@ Application Language="C#" %>


<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        var timer = new System.Timers.Timer(1000);
        timer.Elapsed += (s, ev) =>
        {
            var gestor = new SERVICIOS.GestorIntegridad();
            string errores = gestor.VerificarIntegridadTodasLasTablas();
            SERVICIOS.SingletonIntegridad.Instancia.ActualizarEstado(
                string.IsNullOrEmpty(errores),
                errores
            );
        };
        timer.Start();

        Application["IntegridadTimer"] = timer;

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Código que se ejecuta al cerrarse la aplicación

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando se produce un error no controlado

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta al iniciarse una nueva sesión

    }

    void Session_End(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando finaliza una sesión. 
        // Nota: el evento Session_End se produce solamente con el modo sessionstate
        // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer
        // o SQLServer, el evento no se produce.

    }

</script>
