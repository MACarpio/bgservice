
namespace bgservice.Services;


public class BgService : BackgroundService // Insertamos referencias de BackgroundService
{
    protected IServiceProvider _serviceProvider; // Insertamos referencias de IServiceProvider
    private Timer? _timer; // Insertamos referencias de Timer para ejecutar el servicio en un intervalo de tiempo

    public BgService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider; // Inyectamos el servicio de IServiceProvider
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) // Sobreescribimos el método ExecuteAsync
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5)); // Inicializamos el timer para ejecutar el servicio cada 5 segundos
        return Task.CompletedTask;
    }


    private void DoWork(object? state) //Método que se ejecutara cada intervalo de tiempo
    {
        using (var scope = _serviceProvider.CreateScope()) // Creamos un scope para poder inyectar los servicios 
        {
            // Ejemplo de uso con DbContext
            // var services = scope.ServiceProvider;
            // var context = services.GetRequiredService<AppDbContext>();
            // SendCorreoSolicitud sendCorreoSolicitud = new SendCorreoSolicitud(context);
            // sendCorreoSolicitud.EnviarCorreosFaltantes();
            // sendCorreoSolicitud.CorreoVerificacion("mpacarpio@gmail.com");
            Console.WriteLine("Servicio de Sincronizacion." + DateTime.Now);
        }
    }

    public Task StopAsync(CancellationToken stoppingToken) // Método que se ejecutara cuando se detenga el servicio
    {

        _timer?.Change(Timeout.Infinite, 0); // Detenemos el timer
        return Task.CompletedTask; // Retornamos la tarea completada
    }

    public void Dispose() // Método que se ejecutara cuando se detenga el servicio
    {
        _timer?.Dispose(); // Detenemos el timer
    }
}
