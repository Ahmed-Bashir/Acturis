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
            string dateFrom = DateTime.Now.ToString("yyyy-MM-dd");
            string dateTo = DateTime.Now.ToString("yyyy-MM-dd");
            //string dateFrom = "2022-03-31";
            //string dateTo = "2022-03-31";

            var client = _httpClientFactory.CreateClient("BluelightApi");
            var response = await client.GetAsync(client.BaseAddress + $"/api/v1/Acturis/Memberships/NameChange?dateFrom={dateFrom}&dateTo={dateTo}");

            if (!response.IsSuccessStatusCode) return new List<Bluelight>();


            return await response.Content.ReadFromJsonAsync<List<Bluelight>>();

        }

        public async Task<List<Bluelight>> GetCancellationsAsync()
        {
            string dateFrom = DateTime.Now.ToString("yyyy-MM-dd");
            string dateTo = DateTime.Now.ToString("yyyy-MM-dd");
            //string dateFrom = "2022-03-19";
            //string dateTo = "2022-03-19";

            var client = _httpClientFactory.CreateClient("BluelightApi");
            var response = await client.GetAsync(client.BaseAddress + $"api/v1/Acturis/Memberships/Cancellations?dateFrom={dateFrom}&dateTo={dateTo}");

            if (!response.IsSuccessStatusCode) return new List<Bluelight>();


            return await response.Content.ReadFromJsonAsync<List<Bluelight>>();

        }

        public async Task<List<Bluelight>> GetMembersAsync()
        {
            string dateFrom = DateTime.Now.ToString("yyyy-MM-dd");
            string dateTo = DateTime.Now.ToString("yyyy-MM-dd");
            //string dateFrom = "2022-04-01";
            //string dateTo = "2022-04-01";

            var client = _httpClientFactory.CreateClient("BluelightApi");
            var response = await client.GetAsync(client.BaseAddress + $"api/v1/Acturis/Memberships?dateFrom={dateFrom}&dateTo={dateTo}");


            if (!response.IsSuccessStatusCode) return new List<Bluelight>();


            return await response.Content.ReadFromJsonAsync<List<Bluelight>>();

        }




        public async Task PostMembership(ActurisMembership acturisMembership)
        {
            var client = _httpClientFactory.CreateClient("BluelightApi");
            dynamic blResponse = null;


            try
            {


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
                    _logger.LogError($"Unable to post {acturisMembership.ContactNumber} to /api/v1/Acturis/Memberships - StatusCode: {response.ReasonPhrase}");

                    await _emailService.Report($"Unable to post {acturisMembership.ContactNumber} to Bluelight - StatusCode: {response.ReasonPhrase}");
                }

                if (blResponse.Success == true)
                {
                    _logger.LogInformation($"{acturisMembership.ContactNumber} posted to /api/v1/Acturis/Memberships. Success: {blResponse.Success}");
                }
                else
                {
                    _logger.LogError($"Unable to post {acturisMembership.ContactNumber} to /api/v1/Acturis/Memberships. ErrorMessage: {blResponse.ErrorMessage}");

                    await _emailService.Report($"Unable to post {acturisMembership.ContactNumber} to /api/v1/Acturis/Memberships. ErrorMessage: {blResponse.ErrorMessage}");

                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await _emailService.Report(ex.Message);
            }



        }


        public async Task PostMembershipCertificates(ActurisMembership acturisMembership)
        {
            var client = _httpClientFactory.CreateClient("BluelightApi");
            dynamic blResponse = null;

            try
            {
                if (acturisMembership.Certificates.Any())

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
                            _logger.LogError($"Unable to post {acturisCertificate.Certificate_FileName} to /api/v1/Acturis/MembershipCertificate - StatusCode: {response.ReasonPhrase}");

                            await _emailService.Report($"Unable to post {acturisCertificate.Certificate_FileName} to /api/v1/Acturis/MembershipCertificate - StatusCode: {response.ReasonPhrase}");
                        }

                        if (blResponse.Success == true)
                        {
                            _logger.LogInformation($"{acturisCertificate.Certificate_FileName} posted to /api/v1/Acturis/MembershipCertificate. Success: {blResponse.Success}");
                        }
                        else
                        {
                            _logger.LogError($"Unable to post {acturisCertificate.Certificate_FileName} to /api/v1/Acturis/MembershipCertificate. ErrorMessage: {blResponse.ErrorMessage}");

                            await _emailService.Report($"Unable to post {acturisCertificate.Certificate_FileName} to /api/v1/Acturis/MembershipCertificate. ErrorMessage: {blResponse.ErrorMessage}");
                        }


                    }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                await _emailService.Report(ex.Message);
            }


        }

        public async Task ClearNameChange(ActurisMembership acturisMembership)
        {
            var client = _httpClientFactory.CreateClient("BluelightApi");
            dynamic blResponse = null;

            try
            {


               // var content = new StringContent(string., Encoding.UTF8, "application/json");
                var response = await client.PostAsync(client.BaseAddress + $"/api/v1/Acturis/ClearNameChange?membershipId={ acturisMembership.Id }", null);
                blResponse = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Unable to post {acturisMembership.ContactNumber} to /api/v1/Acturis/ClearNameChange - StatusCode: {response.ReasonPhrase}");

                    await _emailService.Report($"Unable to post {acturisMembership.ContactNumber} to /api/v1/Acturis/ClearNameChange - StatusCode: {response.ReasonPhrase}");
                }

                if (blResponse.Success == true)
                {
                    _logger.LogInformation($"{acturisMembership.ContactNumber} posted to /api/v1/Acturis/ClearNameChange. Success: {blResponse.Success}");
                }
                else
                {
                    _logger.LogError($"Unable to post {acturisMembership.ContactNumber} to /api/v1/Acturis/ClearNameChange. ErrorMessage: {blResponse.ErrorMessage}");

                    await _emailService.Report($"Unable to post {acturisMembership.ContactNumber} to /api/v1/Acturis/ClearNameChange. ErrorMessage: {blResponse.ErrorMessage}");

                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await _emailService.Report(ex.Message);
            }



        }

    }
}
