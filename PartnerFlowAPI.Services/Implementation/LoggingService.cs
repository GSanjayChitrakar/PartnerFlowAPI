using FGLI_SharedLibrary.Abstractions;

namespace PartnerFlowAPI.Services.Implementation
{
    public class LoggingService : ILoggerService
    {
        public void LogDebug(string message, params object[] propertyValues)
        {
            // Implement logging logic here
        }

        public void LogInformation(string message, params object[] propertyValues)
        {
            // Implement logging logic here
        }

        public void LogWarning(string message, params object[] propertyValues)
        {
            // Implement logging logic here
        }

        public void LogError(Exception exception, string message, params object[] propertyValues)
        {
            // Implement logging logic here
        }

        public void LogCritical(Exception exception, string message, params object[] propertyValues)
        {
            // Implement logging logic here
        }
    }
}