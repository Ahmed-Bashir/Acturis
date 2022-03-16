using Acturis.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Acturis
{
    public class Application
    {
 
        private readonly Timer _timer;
        private readonly IActurisApiService _acturisApiService;

        public Application(IActurisApiService acturisApiService)
        {
            _acturisApiService = acturisApiService;
            OnTick(null, EventArgs.Empty);
            _timer = new Timer(TimeSpan.FromSeconds(60).TotalMilliseconds);
            _timer.Elapsed += OnTick;
            
        }

        public void Start() => _timer.Start();

        public void Stop() => _timer.Stop();

        private void OnTick(object sender, EventArgs args)
        {
            //_acturisApiService.UploadPoliciesAsync().GetAwaiter().GetResult();
            _acturisApiService.UploadMtaPolicies().GetAwaiter().GetResult();
            //_acturisApiService.CancelPolicyAsync().GetAwaiter().GetResult();

        }
    }
}
