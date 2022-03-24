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
            _timer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds);
            _timer.Elapsed += OnTick;
        }

        public void Start() => _timer.Start();

        public void Stop() => _timer.Stop();

        private async void OnTick(object sender, EventArgs args)
        {

          

            if (_isRunInProgress)
            {
                //Console.WriteLine("Waiting for current job to finish before starting new.");
                return;
            }

            _isRunInProgress = true;

            //Console.WriteLine("Starting new run.");

            //Console.WriteLine("--1--");
            await _acturisApiService.PolicyUploadRequestAsync();
            //await Task.Delay(10000);

            ////Console.WriteLine("--2--");
            //await _acturisApiService.RenewalPolicyUploadRequestAsync();
            ////await Task.Delay(10000);

            ////Console.WriteLine("--3--");
            //await _acturisApiService.MTAPolicyUploadRequestAsync();
            ////await Task.Delay(10000);

            ////Console.WriteLine("--4--");
            //await _acturisApiService.CancellationPolicyUploadRequestAsync();

            _isRunInProgress = false;

            // new Task(async () => await _acturisApiService.PolicyUploadRequestAsync().ContinueWith(async x => await _acturisApiService.RenewalPolicyUploadRequestAsync())).Start();
        }
    }
}
