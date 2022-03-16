using Acturis.Interfaces;
using Acturis.Models.BlueLight;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acturis.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.Json;
using System.Net.Http.Json;
using System.Text;
using System.Linq;

namespace Acturis.Services
{

    /// 
    /// <summary>
    /// 
    /// </summary>
    public class BluelightApiService : IBluelightApiService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IEmailService _emailService;
        private ILogger<IBluelightApiService> _logger;




        public BluelightApiService(ILogger<IBluelightApiService> logger, IHttpClientFactory httpClientFactory, IEmailService emailService)
        {
            _logger = logger;


            _httpClientFactory = httpClientFactory;
            _emailService = emailService;
        }

        public async Task<List<Bluelight>> GetNameChangeAsync()
        {
            //string dateFrom = DateTime.Now.ToString("yyyy-MM-dd");
            //string dateTo = DateTime.Now.ToString("yyyy-MM-dd");
            string dateFrom = "2022-03-20";
            string dateTo = "2022-03-20";

            var client = _httpClientFactory.CreateClient("BluelightApi");
            var response = await client.GetAsync(client.BaseAddress + $"/api/v1/Acturis/Memberships/NameChange?dateFrom={dateFrom}&dateTo={dateTo}");

            if (!response.IsSuccessStatusCode) return new List<Bluelight>();


            return await response.Content.ReadFromJsonAsync<List<Bluelight>>();

        }

        public async Task<List<Bluelight>> GetCancellationsAsync()
        {
            //string dateFrom = DateTime.Now.ToString("yyyy-MM-dd");
            //string dateTo = DateTime.Now.ToString("yyyy-MM-dd");
            string dateFrom = "2022-03-15";
            string dateTo = "2022-03-15";

            var client = _httpClientFactory.CreateClient("BluelightApi");
            var response = await client.GetAsync(client.BaseAddress + $"/api/v1/Acturis/Memberships/Cancellations?dateFrom={dateFrom}&dateTo={dateTo}");

            if (!response.IsSuccessStatusCode) return new List<Bluelight>();


            return await response.Content.ReadFromJsonAsync<List<Bluelight>>();

        }

        public async Task<List<Bluelight>> GetMembersAsync()
        {
            //string dateFrom = DateTime.Now.ToString("yyyy-MM-dd");
            //string dateTo = DateTime.Now.ToString("yyyy-MM-dd");
            string dateFrom = "2022-03-20";
            string dateTo = "2022-03-20";

            var client = _httpClientFactory.CreateClient("BluelightApi");
            var response = await client.GetAsync(client.BaseAddress + $"/api/v1/Acturis/Memberships?dateFrom={dateFrom}&dateTo={dateTo}");

            
            if (!response.IsSuccessStatusCode) return new List<Bluelight>();
          

            return await response.Content.ReadFromJsonAsync<List<Bluelight>>();

        }


        //public async Task<Bluelight> GetMembershipIdAsync(string conNumber)
        //{


        //    var client = _httpClientFactory.CreateClient("BluelightApi");

        //    var response = await client.GetAsync(client.BaseAddress + $"/api/v1/Contacts/SearchByMembershipNumber?membershipNumber={conNumber}");
        //    if (!response.IsSuccessStatusCode) return new Bluelight();
            
        //    return await response.Content.ReadFromJsonAsync<Bluelight>();

        //}


        public async Task PostMembership(ActurisMembership acturisMembership)
        {
            var client = _httpClientFactory.CreateClient("BluelightApi");
            dynamic blResponse = null;


            try
            {
                  //var id = GetMembershipIdAsync(acturisMembership.Id).GetAwaiter().GetResult().CurrentMembership.Id;

                   await PostMembershipCertificates(acturisMembership);

                    var data = new
                    {
                        Id = acturisMembership.Id,

                        ActurisIntegrationDate = acturisMembership.ActurisIntegrationDate
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(client.BaseAddress + "/api/v1/Acturis/Memberships", content);
                    blResponse = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError($"Unable to post {acturisMembership.ContactNumber} to Bluelight - StatusCode: {response.ReasonPhrase}");

                        await _emailService.ReportError($"Unable to post {acturisMembership.ContactNumber} to Bluelight - StatusCode: {response.ReasonPhrase}");
                    }

                if (blResponse.Success == true)
                {
                    _logger.LogInformation($"Member with {acturisMembership.ContactNumber} posted to BlueLight. Success: {blResponse.Success}");
                }
                else
                {
                    _logger.LogError($"Unable to post member with {acturisMembership.ContactNumber} to BlueLight. ErrorMessage: {blResponse.ErrorMessage}");

                    await _emailService.ReportError($"Unable to post member with {acturisMembership.ContactNumber} to BlueLight. ErrorMessage: {blResponse.ErrorMessage}");

                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await _emailService.ReportError(ex.Message);
            }

            

        }


        public async Task PostMembershipCertificates(ActurisMembership acturisMembership)
        {
            var client = _httpClientFactory.CreateClient("BluelightApi");
            dynamic blResponse = null;

            try
            {
                foreach (var acturisCertificate in acturisMembership.Certificates)
                {

                    var data = new
                    {
                        Id = acturisMembership.Id,
                        Certificate_FileName = acturisCertificate.Certificate_FileName,
                        Certificate_FileContents = acturisCertificate.Certificate_FileContents
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(client.BaseAddress + "/api/v1/Acturis/MembershipCertificate", content);
                    blResponse = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError($"Unable to post {acturisCertificate.Certificate_FileName} pertaining to {acturisMembership.ContactNumber} - StatusCode: {response.ReasonPhrase}");

                        await _emailService.ReportError($"Unable to post {acturisCertificate.Certificate_FileName} pertaining to {acturisMembership.ContactNumber} - StatusCode: {response.ReasonPhrase}");
                    }

                    if (blResponse.Success == true)
                    {
                        _logger.LogInformation($"{acturisMembership.ContactNumber}: {acturisCertificate.Certificate_FileName} posted to BlueLight. Success: {blResponse.Success}");
                    }
                    else
                    {
                        _logger.LogError($"Unable to post {acturisMembership.ContactNumber}: {acturisCertificate.Certificate_FileName} to BlueLight. ErrorMessage: {blResponse.ErrorMessage}");

                        await _emailService.ReportError($"Unable to post {acturisMembership.ContactNumber}: {acturisCertificate.Certificate_FileName} to BlueLight. ErrorMessage: {blResponse.ErrorMessage}");
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                await _emailService.ReportError(ex.Message);
            }


        }

    }
}
