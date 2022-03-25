using Acturis.Interface;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace Acturis
{
    public class Application
    {

        private readonly Timer _timer;
        private readonly IActurisApiService _acturisApiService;
        private bool _isRunInProgress;

        public Application(IActurisApiService acturisApiService)
        {
            _acturisApiService = acturisApiService;
            _isRunInProgress = false;
            _timer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            _timer.Elapsed += OnTick;
        }

        public void Start() => _timer.Start();

        public void Stop() => _timer.Stop();

        private async void OnTick(object sender, EventArgs args)
        { 
         
            if (_isRunInProgress)
            {
              
                return;
            }

            _isRunInProgress = true;

            await _acturisApiService.PolicyUploadRequestAsync();

            await _acturisApiService.RenewalPolicyUploadRequestAsync();
         
            await _acturisApiService.MTAPolicyUploadRequestAsync();
         
            await _acturisApiService.CancellationPolicyUploadRequestAsync();

            _isRunInProgress = false;

           
        }
    }
}
