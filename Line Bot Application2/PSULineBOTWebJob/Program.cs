using System;
using Microsoft.Azure.WebJobs;

namespace PSULineBOTWebJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        //MulticastMessage mesall = new MulticastMessage();
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {


            JobHostConfiguration config = new JobHostConfiguration();
           // config.UseTimers();
            Console.WriteLine("Hi There EiEi!!");
            Console.WriteLine("Hi There EiEi!!");
           
            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }
            var host = new JobHost(config);
            host.CallAsync(typeof(Functions).GetMethod("ProcessMethod"));
           
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
            
        }
        
    
    }
}
