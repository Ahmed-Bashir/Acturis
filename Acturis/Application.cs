using Acturis.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace Acturis
{
    public class Application
    {

        private readonly Timer _timer;
        private readonly IActurisApiService _acturisApiService;
        private readonly ILogger<Application> _logger;
        private bool _isRunInProgress;

        public Application(IActurisApiService acturisApiService, ILogger<Application> logger)
        {
            _acturisApiService = acturisApiService;
            _logger = logger;
            _isRunInProgress = false;

            _timer = new Timer(10000) { AutoReset = true};
            _timer.Elapsed += OnTick;
        }

        public void Start() => _timer.Start();

        public void Stop() => _timer.Stop();

        private async void OnTick(object sender, EventArgs args)
        { 
         
            if (_isRunInProgress)
            {
               // _logger.LogInformation("Run in progress");
                return;
            }

            _isRunInProgress = true;

          //  _logger.LogInformation("I'm Alive!");

           //await _acturisApiService.PolicyUploadRequestAsync();

            //await _acturisApiService.RenewalPolicyUploadRequestAsync();
          
           await _acturisApiService.MTAPolicyUploadRequestAsync();
           
            //await _acturisApiService.CancellationPolicyUploadRequestAsync();

            _isRunInProgress = false;

           
        }
    }
}
