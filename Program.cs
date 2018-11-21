using System;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace notify_deployment_appinsights
{
    public class Program
    {
        static void Main(string[] args)
        {
            TelemetryConfiguration.Active.InstrumentationKey = Arguments.InstrumentationKey();
            var telemetryClient = new TelemetryClient();
            
            Console.WriteLine("Writing to: " + Arguments.InstrumentationKey());
            Console.WriteLine("Service deployed: " + Arguments.ServiceName());

            var traceTelemetry = new TraceTelemetry("New Deployment of " + Arguments.ServiceName());
            AddCustomPropertiesIfAny(traceTelemetry);
            telemetryClient.TrackTrace(traceTelemetry);

            telemetryClient.Flush();

            Console.WriteLine("All done!");
        }

        private static void AddCustomPropertiesIfAny(TraceTelemetry traceTelemetry)
        {
            if(Arguments.CustomProperties().Any())
            {
                foreach(var customProperty in Arguments.CustomProperties())
                {
                    traceTelemetry.Properties.Add(customProperty.Key, customProperty.Value);
                }
            }
        }

    }
}
