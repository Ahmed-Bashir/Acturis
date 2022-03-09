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



        public async Task<List<Bluelight>> GetMembersAsync()
        {
            //string dateFrom = DateTime.Today.AddDays(-14).ToString("yyyy-MM-dd");
            //string dateTo = DateTime.Now.ToString("yyyy-MM-dd");
            string dateFrom = "2022-03-07";
            string dateTo = "2022-03-07";

            var client = _httpClientFactory.CreateClient("BluelightApi");
            var response = await client.GetAsync(client.BaseAddress + $"/api/v1/Acturis/Memberships?dateFrom={dateFrom}&dateTo={dateTo}");

            if (!response.IsSuccessStatusCode) return new List<Bluelight>();
          

            return await response.Content.ReadFromJsonAsync<List<Bluelight>>();

        }


        public async Task<Bluelight> GetMembershipIdAsync(string conNumber)
        {


            var client = _httpClientFactory.CreateClient("BluelightApi");

            var response = await client.GetAsync(client.BaseAddress + $"/api/v1/Contacts/SearchByMembershipNumber?membershipNumber={conNumber}");
            if (!response.IsSuccessStatusCode) return new Bluelight();
            
            return await response.Content.ReadFromJsonAsync<Bluelight>();

        }


        public async Task PostMembership(List<ActurisMembership> acturisMemberships)
        {
            var client = _httpClientFactory.CreateClient("BluelightApi");
            dynamic blResponse = null;

            try
            {
                foreach (var acturisMembership in acturisMemberships)
                {
                    var id = GetMembershipIdAsync(acturisMembership.Id).GetAwaiter().GetResult().CurrentMembership.Id;

                    PostMembershipCertificates(acturisMembership.Certificates, id).GetAwaiter().GetResult();

                    var data = new
                    {
                        Id = id,

                        ActurisIntegrationDate = acturisMembership.ActurisIntegrationDate
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(client.BaseAddress + "/api/v1/Acturis/Memberships", content);
                    blResponse = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                    if (!response.IsSuccessStatusCode)

                    _logger.LogError($"Unable to post member detials with Id:{id} - StatusCode: {response.ReasonPhrase}");

                    await _emailService.ReportError($"Unable to post member detials with Id:{id} - StatusCode: {response.ReasonPhrase}");


                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await _emailService.ReportError(ex.Message);
            }

            if (blResponse.Success)
            {
                _logger.LogInformation($"Member details posted to BlueLight. Success: {blResponse.Success} Id: {blResponse.Id}");
            }
            else
            {
                _logger.LogError($"Unable to post member detais to BlueLight. ErrorMessage: {blResponse.ErrorMessage}");

                await _emailService.ReportError($"Unable to post member detais to BlueLight. ErrorMessage: {blResponse.ErrorMessage}");

            }

        }


        public async Task PostMembershipCertificates(List<ActurisCertificate> acturisCertificates, string id)
        {
            var client = _httpClientFactory.CreateClient("BluelightApi");
            dynamic blResponse = null;

            try
            {
                foreach (var acturisCertificate in acturisCertificates)
                {

                    var data = new
                    {
                        Id = id,
                        Certificate_FileName = acturisCertificate.Certificate_FileName,
                        Certificate_FileContents = acturisCertificate.Certificate_FileContents
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(client.BaseAddress + "/api/v1/Acturis/MembershipCertificate", content);
                    blResponse = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                    if (!response.IsSuccessStatusCode)

                        _logger.LogError($"Unable to post certificate pertaining to Id:{id} - StatusCode: {response.ReasonPhrase}");

                    await _emailService.ReportError($"Unable to post certificate pertaining to Id:{id} - StatusCode: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            if (blResponse.Success)
            {
                _logger.LogInformation($"Certificates posted to BlueLight. Success: {blResponse.Success} Id: {blResponse.Id}");
            }
            else
            {
                _logger.LogError($"Unable to post certificates to BlueLight. ErrorMessage: {blResponse.ErrorMessage}");

                await _emailService.ReportError($"Unable to post certificates to BlueLight. ErrorMessage: {blResponse.ErrorMessage}");
            }


        }

    }
}
