using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Serilog;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Acturis.Interface;
using Acturis.Service;
using System.Threading.Tasks;
using Acturis.Interfaces;
using Acturis.Services;
using System.Text.RegularExpressions;
using Topshelf;

namespace Acturis
{
    public class Program
    {
        private readonly IActurisApiService _ActurisApiService;

        public Program(IActurisApiService acturisApiService)
        {
            _ActurisApiService = acturisApiService;
        }

        private static void Main()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            var config = LoadConfiguration();
            var path = AppDomain.CurrentDomain.BaseDirectory + "ActurisLog.txt";

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            Log.Information("Application starting");

            var topshelfExitCode = HostFactory.Run(configure =>
            {
                configure.Service<Application>(x =>
                {
                    x.ConstructUsing(name => serviceProvider.GetService<Application>());

                    x.WhenStarted(application => application.Start());
                    x.WhenStopped(application => application.Stop());
                });

                configure.RunAsLocalSystem();
                configure.SetDisplayName("Acturis");
                configure.SetServiceName("Actuirs");
                configure.SetDescription("Provides Pacey members with Insurance");
            });

            var exitCode = (int)Convert.ChangeType(topshelfExitCode, topshelfExitCode.GetTypeCode());
            Environment.ExitCode = exitCode;


        }


        //static async Task Main(string[] args)
        //{
        //    var services = ConfigureServices();
        //    var serviceProvider = services.BuildServiceProvider();
        //    await serviceProvider.GetService<Program>().Run();
        //    var config = LoadConfiguration();

        //    var path = AppDomain.CurrentDomain.BaseDirectory + "ActurisLog.txt";

        //    Log.Logger = new LoggerConfiguration()
        //        .ReadFrom.Configuration(config)
        //        .Enrich.FromLogContext()
        //        .WriteTo.Console()
        //        .WriteTo.File(path)
        //        .CreateLogger();

        //    Log.Information("Application starting");
        //}

        //public async Task Run()
        //{
          

        //    await _ActurisApiService.SendApprovedMembersAsync();

        //    //var Ids = new List<string>()
        //    //{
        //    //    "4a58afe3-307c-4e8e-9239-64559e691009",
        //    //    "03c98b24-b549-4ddc-b531-c16a06507e9d",
        //    //    "f3ea5458-b3b6-4e3e-a66c-9c35ebbff4f6"

        //    //};





        //    //var jobIds = _ActurisApiService.TestJobsIDAsync(Ids).GetAwaiter().GetResult();

        //    //var locatePolicies = _ActurisApiService.LocatePolicyUploadAsync(jobIds).GetAwaiter().GetResult();

        //    //var locatePolicyResponses = _ActurisApiService.locatePolicyAsync(locatePolicies).GetAwaiter().GetResult();

        //    //var locateActivityResponses =  await _ActurisApiService.LocatePartActivityAsync(locatePolicyResponses);

        //    //await _ActurisApiService.GetDocumentFromActivityAsync(locateActivityResponses);
        //}

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);
            services.AddLogging(configure => configure.AddSerilog());

            services.AddTransient<IActurisApiService, ActurisApiService>();
            services.AddTransient<Program>();
            services.AddTransient<IBluelightApiService, BluelightApiService>();
            services.AddTransient<IActurisApiService, ActurisApiService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddOptions();
            services.AddTransient<Application>();
            services.Configure<SmtpSetting>(config.GetSection(
                                   "smtpsettings"));

            var appsettings = config.GetRequiredSection("Environment").Value.Equals("live") ? "appsettingsLive" : "appsettingsDev";

            services.AddHttpClient("ActurisGWS", client => 
            {
                client.BaseAddress = new Uri(config.GetValue<string>($"{appsettings}:ActurisGwsUrl"));
               
                
                var authToken = Encoding.ASCII.GetBytes($"{config.GetValue<string>($"{appsettings}:ActurisUserName")}:{config.GetValue<string>($"{appsettings}:ActurisPassword")}");
               
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
               

                var formData = new List<KeyValuePair<string, string>>();
                formData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                formData.Add(new KeyValuePair<string, string>("scopes", "quotes policy_upload gws"));
                var content = new FormUrlEncodedContent(formData);

                var response = client.PostAsync(config.GetValue<string>($"{appsettings}:ActurisTokenUrl"), content).Result;
                var token = response.Content.ReadAsStringAsync().Result;

                dynamic acturisResponse = JsonConvert.DeserializeObject(token);
                token = acturisResponse.access_token;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            });


            services.AddHttpClient("ActurisPolicyUpload", client =>
            {
                client.BaseAddress = new Uri(config.GetValue<string>($"{appsettings}:ActurisPolicyUploadUrl"));


                var authToken = Encoding.ASCII.GetBytes($"{config.GetValue<string>($"{appsettings}:ActurisUserName")}:{config.GetValue<string>($"{appsettings}:ActurisPassword")}");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));


                var formData = new List<KeyValuePair<string, string>>();
                formData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                formData.Add(new KeyValuePair<string, string>("scopes", "quotes policy_upload gws"));
                var content = new FormUrlEncodedContent(formData);

                var response = client.PostAsync(config.GetValue<string>($"{appsettings}:ActurisTokenUrl"), content).Result;
                var token = response.Content.ReadAsStringAsync().Result;

                dynamic acturisResponse = JsonConvert.DeserializeObject(token);
                token = acturisResponse.access_token;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            });

            services.AddHttpClient("BluelightApi", client =>
            {
                client.BaseAddress = new Uri(config.GetValue<string>($"{appsettings}:BluelightApiUrl"));

                var authToken = Encoding.ASCII.GetBytes($"{config.GetValue<string>($"{appsettings}:BluelightUserName")}:{config.GetValue<string>($"{appsettings}:BluelightPassword")}");


                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
                var token = client.GetStringAsync(config.GetValue<string>($"{appsettings}:BluelightTokenUrl")).Result;



                token = token.Substring(1, token.Length - 2);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            });




            return services;
        }

            public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                



            return builder.Build();
        }
    }
}
