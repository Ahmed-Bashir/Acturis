using Acturis.Interface;
using Acturis.Interfaces;
using Acturis.Models;
using Acturis.Models.Acturis;
using Acturis.Models.BlueLight;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Acturis.Service
{
    public class ActurisApiService : IActurisApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBluelightApiService _bluelightApiService;
        private readonly ILogger<ActurisApiService> _logger;
        private readonly IEmailService _emailService;


        public ActurisApiService(IHttpClientFactory httpClientFactory, IBluelightApiService bluelightApiService, ILogger<ActurisApiService> logger, IEmailService emailService)
        {
            _httpClientFactory = httpClientFactory;
            _bluelightApiService = bluelightApiService;
            _logger = logger;
            _emailService = emailService;

        }

        //mapping objects 


        public async Task PolicyUploadRequestAsync()
        {

            var client = _httpClientFactory.CreateClient("ActurisPolicyUpload");


            var members = await _bluelightApiService.GetMembersAsync();

          

            //var filteredMembers = members.Where(x => x.Contact.ContactNumber.Equals("CON-11432"));

            if (members.Any())


                // var filteredMembers = members.Where(x => x.Contact.ContactNumber.Equals("CON-11383") || x.Contact.ContactNumber.Equals("CON-11383"));

                foreach (var member in members)
                {
                    try
                    {

                        _logger.LogInformation($"Creating Policy for {member.Contact.ContactNumber}...");

                        var policyUploadRequest = AddOrUpdateContact(member);

                        var policyDetails = new PolicyDetails();
                        policyDetails.EffectiveDate = new EffectiveDate() { Value = member.ValidFrom.ToString("yyyyMMdd") };

                        policyUploadRequest.PolicyDetails = policyDetails;

                        var additionalDetails = new AdditionalDetails();
                        additionalDetails.InsurerPolicyNo = new InsurerPolicyNo() { Value = member.Contact.ContactNumber };


                        additionalDetails.ChildminderNanny = new ChildminderNanny() { Value = member.Grade.Name.GetValueFromDisplayName<Models.Acturis.Grade>() };

                        additionalDetails.ERNNo = new ERNNo() { Value = member.Contact.EmployeeReferenceNumber ?? "-" };

                        policyUploadRequest.AdditionalDetails = additionalDetails;


                        var xml = Serialize(policyUploadRequest, typeof(PolicyUploadRequest));

                        var response = await PostToActuris(xml, client);


                        _logger.LogInformation($"Policy uploaded.");


                        var policyUploadResponse = Deserialize(response, typeof(Response), "Response");


                        //var policyUploadResponse = new Response()
                        //{
                        //    PolicyUploadJobID = "43e7ba5f-6110-4a83-884c-7cec7863f66a",
                        //    EffectiveDate = "20220314",
                        //    ConNumber = "CON-11332",

                        //};

                        var locatePolicy = await LocatePolicyUploadAsync(policyUploadResponse);

                        var locatePolicyResponse = await locatePolicyAsync(locatePolicy);

                        var locatePartActivityResponse = await LocatePartActivityAsync(locatePolicyResponse);

                        var acturisMember = await GetDocumentFromActivityAsync(locatePartActivityResponse);

                        acturisMember.Id = member.Id;
                        acturisMember.ContactNumber = member.Contact.ContactNumber;

                        await _bluelightApiService.PostMembership(acturisMember);


                        _logger.LogInformation($"Policy upload for {member.Contact.ContactNumber} complete.\n");

                        await _emailService.Report($"Policy upload for {member.Contact.ContactNumber} complete");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{ex.Message} {member.Contact.ContactNumber} policy upload failed.");


                        await _emailService.ReportUnsuccessfulUpload(member, ex.Message);

                        await _bluelightApiService.PostMembership(new ActurisMembership() { Id = member.Id, ContactNumber = member.Contact.ContactNumber, Certificates = new List<ActurisCertificate>() });

                    }
                    finally
                    {
                        Log.CloseAndFlush();
                    }


                }


        }

       

        public async Task MTAPolicyUploadRequestAsync()
        {

            var client = _httpClientFactory.CreateClient("ActurisPolicyUpload");


            var members = await _bluelightApiService.GetNameChangeAsync();

            //var filteredMembers = members.Where(x => x.Contact.ContactNumber.Equals("CON-11404"));
            //|| x.Contact.ContactNumber.Equals("CON-11372"));

            

            if (members.Any())

                foreach (var member in members)
                {
                    try
                    {

                        _logger.LogInformation($"Creating MTA policy for {member.Contact.ContactNumber}...");

                        var policyUploadRequest = AddOrUpdateContact(member);

                        /* MTA part */

                        var existingPolicy = new ExistingPolicy();
                        existingPolicy.BusinessEvent = new BusinessEvent() { Value = 2 };
                        existingPolicy.InsurerPolicyNo = new InsurerPolicyNo() { Value = member.Contact.ContactNumber };


                        policyUploadRequest.ExistingPolicy = existingPolicy;

                        var existingPolicyDetails = new ExistingPolicyDetails();
                        existingPolicyDetails.EffectiveDate = new EffectiveDate() { Value = member.ValidFrom.ToString("yyyyMMdd") };
                        existingPolicyDetails.ChildminderNanny = new ChildminderNanny() { Value = member.Grade.Name.GetValueFromDisplayName<Models.Acturis.Grade>() };
                        existingPolicyDetails.ERNNo = new ERNNo() { Value = member.Contact.EmployeeReferenceNumber ?? "-" };

                        policyUploadRequest.ExistingPolicyDetails = existingPolicyDetails;


                        var xml = Serialize(policyUploadRequest, typeof(PolicyUploadRequest));

                        var response = await PostToActuris(xml, client);

                        _logger.LogInformation($"Policy uploaded.");



                        var policyUploadResponse = Deserialize(response, typeof(Response), "Response");


                        //var policyUploadResponse = new Response()
                        //{
                        //    PolicyUploadJobID = "a0cbb7f3-ada4-4db0-a6ee-3992c3373b5d",
                        //    EffectiveDate = "20220314",
                        //    ConNumber = "CON-11333",

                        //};




                        var locatePolicy = await LocatePolicyUploadAsync(policyUploadResponse);

                        var locatePolicyResponse = await locatePolicyAsync(locatePolicy);

                        var locatePartActivityResponse = await LocatePartActivityAsync(locatePolicyResponse);

                        var acturisMember = await GetDocumentFromActivityAsync(locatePartActivityResponse);

                        acturisMember.Id = member.Id;
                        acturisMember.ContactNumber = member.Contact.ContactNumber;

                        await _bluelightApiService.PostMembership(acturisMember);

                        _logger.LogInformation($"MTA policy upload for {member.Contact.ContactNumber} complete.\n");

                        await _emailService.Report($"MTA policy upload for {member.Contact.ContactNumber} complete.");

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{ex.Message} {member.Contact.ContactNumber} policy upload failed.");


                        await _emailService.ReportUnsuccessfulUpload(member, ex.Message);

                        await _bluelightApiService.PostMembership(new ActurisMembership() { Id = member.Id, ContactNumber = member.Contact.ContactNumber, Certificates = new List<ActurisCertificate>() });
                    }
                    finally
                    {
                        Log.CloseAndFlush();
                    }


                }





        }

        public async Task RenewalPolicyUploadRequestAsync()
        {

            var client = _httpClientFactory.CreateClient("ActurisPolicyUpload");


            var members = await _bluelightApiService.GetMembersAsync();

           

            var filteredMembers = members.Where(x => x.SubType.Label.Equals("Renewal"));

            if (filteredMembers.Any())

                
                foreach (var member in filteredMembers)
                {
                    try
                    {
                        _logger.LogInformation($"Creating Renewal policy for {member.Contact.ContactNumber}...");


                        var policyUploadRequest = AddOrUpdateContact(member);

                        /* Renewal part */
                        var existingPolicy = new ExistingPolicy();
                        existingPolicy.BusinessEvent = new BusinessEvent() { Value = 3 };
                        existingPolicy.InsurerPolicyNo = new InsurerPolicyNo() { Value = member.Contact.ContactNumber };
                        policyUploadRequest.ExistingPolicy = existingPolicy;

                        var existingPolicyDetails = new ExistingPolicyDetails();
                        existingPolicyDetails.EffectiveDate = new EffectiveDate() { Value = DateTime.Now.ToString("yyyyMMdd") };
                        existingPolicyDetails.ChildminderNanny = new ChildminderNanny() { Value = member.Grade.Name.GetValueFromDisplayName<Models.Acturis.Grade>() };
                        existingPolicyDetails.ERNNo = new ERNNo() { Value = member.Contact.EmployeeReferenceNumber ?? "-" };

                        policyUploadRequest.ExistingPolicyDetails = existingPolicyDetails;


                        var xml = Serialize(policyUploadRequest, typeof(PolicyUploadRequest));

                        var response = await PostToActuris(xml, client);

                        _logger.LogInformation($"Policy for {member.Contact.ContactNumber} uploaded");

                        var policyUploadResponse = Deserialize(response, typeof(Response), "Response");


                        var locatePolicy = await LocatePolicyUploadAsync(policyUploadResponse);
                        var locatePolicyResponse = await locatePolicyAsync(locatePolicy);
                        var locatePartActivityResponse = await LocatePartActivityAsync(locatePolicyResponse);
                        var acturisMember = await GetDocumentFromActivityAsync(locatePartActivityResponse);

                        acturisMember.Id = member.Id;
                        acturisMember.ContactNumber = member.Contact.ContactNumber;

                        await _bluelightApiService.PostMembership(acturisMember);

                        _logger.LogInformation($"Renewal upload for {member.Contact.ContactNumber} complete.\n");

                        await _emailService.Report($"Renewal upload for {member.Contact.ContactNumber} complete.");

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{ex.Message} {member.Contact.ContactNumber} policy upload failed.");


                        await _emailService.ReportUnsuccessfulUpload(member, ex.Message);

                        await _bluelightApiService.PostMembership(new ActurisMembership() { Id = member.Id, ContactNumber = member.Contact.ContactNumber, Certificates = new List<ActurisCertificate>() });


                    }
                    finally
                    {
                        Log.CloseAndFlush();
                    }

                }


        }

        public async Task CancellationPolicyUploadRequestAsync()
        {

            var client = _httpClientFactory.CreateClient("ActurisPolicyUpload");


            var members = await _bluelightApiService.GetCancellationsAsync();

            //var filteredMembers = members.Where(x => x.Contact.ContactNumber.Equals("CON-11351"));

            if (members.Any())


                foreach (var member in members)
                {
                    try
                    {
                        _logger.LogInformation($"Creating policy for {member.Contact.ContactNumber}...");


                        var policyUploadRequest = AddOrUpdateContact(member);

                        /* Cancellation part */
                        var existingPolicy = new ExistingPolicy();
                        existingPolicy.BusinessEvent = new BusinessEvent() { Value = 4 };
                        existingPolicy.InsurerPolicyNo = new InsurerPolicyNo() { Value = member.Contact.ContactNumber };

                        policyUploadRequest.ExistingPolicy = existingPolicy;
                        var existingPolicyDetails = new ExistingPolicyDetails();
                        existingPolicyDetails.EffectiveDate = new EffectiveDate() { Value = DateTime.Now.ToString("yyyyMMdd") };
                        existingPolicyDetails.ChildminderNanny = new ChildminderNanny() { Value = EnumHelper.GetValueFromDisplayName<Models.Acturis.Grade>(member.Grade.Name) };

                        policyUploadRequest.ExistingPolicyDetails = existingPolicyDetails;


                        var xml = Serialize(policyUploadRequest, typeof(PolicyUploadRequest));

                        var response = await PostToActuris(xml, client);

                        _logger.LogInformation($"Policy uploaded.");



                        var policyUploadResponse = Deserialize(response, typeof(Response), "Response");




                        //var policyUploadResponse = new Response()
                        //{
                        //    PolicyUploadJobID = "0c5dc717-0fb0-4114-a34f-2cb4212c7ff1",
                        //    EffectiveDate = 20220303
                        //};

                        var locatePolicy = await LocatePolicyUploadAsync(policyUploadResponse);
                        var locatePolicyResponse = await locatePolicyAsync(locatePolicy);

                        await _bluelightApiService.PostMembership(new ActurisMembership() { Id = member.Id, ContactNumber = member.Contact.ContactNumber, Certificates = new List<ActurisCertificate>() });

                        _logger.LogInformation($"Cancellation upload for {member.Contact.ContactNumber} complete.\n.");

                        await _emailService.Report($"Cancellation upload for {member.Contact.ContactNumber} complete.");

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{ex.Message} {member.Contact.ContactNumber} policy upload failed.");


                        await _emailService.ReportUnsuccessfulUpload(member, ex.Message);

                        await _bluelightApiService.PostMembership(new ActurisMembership() { Id = member.Id, ContactNumber = member.Contact.ContactNumber, Certificates = new List<ActurisCertificate>() });
                    }
                    finally
                    {
                        Log.CloseAndFlush();
                    }


                }


        }

        public PolicyUploadRequest AddOrUpdateContact(Bluelight member)
        {
           
            var policyUploadRequest = new PolicyUploadRequest();

            var title = member.Contact.Title.Label.Equals("") ? "NotApplicable" : member.Contact.Title.Label;
            //Console.WriteLine(Convert.ToInt32(Enum.Parse(typeof(Models.Acturis.Title), member.Contact.Title.Label) ?? 80));

            policyUploadRequest.MappingID = "PibEmployersLiabilityPACEY";

            var addOrUpdateContact = new AddOrUpdateContact();

            addOrUpdateContact.OldContactNo = new OldContactNo()
            {
                Value = member.Contact.ContactNumber
            };
            addOrUpdateContact.ClientTitle = new ClientTitle()
            {
                Value = Convert.ToInt32(Enum.Parse(typeof(Models.Acturis.Title), title)) 

            };
            addOrUpdateContact.FullName = new FullName
            {
                Value = member.Contact.FullName
            };
            addOrUpdateContact.ClientFirstName = new ClientFirstName()
            {
                Value = member.Contact.FirstName
            };
            addOrUpdateContact.ClientSurname = new ClientSurname()
            {
                Value = member.Contact.LastName
            };
            addOrUpdateContact.DateOfBirth = new DateOfBirth()
            {
                Value = Convert.ToInt32(member.Contact.DateOfBirth.ToString("yyyyMMdd"))
            };
            addOrUpdateContact.EmailAddress = new EmailAddress()
            {
                Value = member.Contact.EmailAddress1
            };
            addOrUpdateContact.WorkPhone = new WorkPhone() { Value = member.Contact.WorkPhone ?? "-" };
            addOrUpdateContact.MobilePhone = new MobilePhone() { Value = member.Contact.HomeMobile ?? "-" };
            addOrUpdateContact.HomePhone = new HomePhone() { Value = member.Contact.HomePhone ?? "-" };

            policyUploadRequest.AddOrUpdateContact = addOrUpdateContact;


            return policyUploadRequest;
        }

        private async Task<string> PostToActuris(string xml, HttpClient client)
        {
            var content = new StringContent(xml, Encoding.UTF8, "application/xml");
            var response = await client.PostAsync(client.BaseAddress, content);
            var result = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
            {

                _logger.LogInformation($"Possible syntax error.");

                throw new Exception(response.ReasonPhrase + ". " + result);

            }

            return result;
        }

        public async Task<LocatePolicy> LocatePolicyUploadAsync(Response jobId)
        {

            var client = _httpClientFactory.CreateClient("ActurisPolicyUpload");

            Response response = null;

            _logger.LogInformation($"Policy upload Job Id: {jobId.PolicyUploadJobID}");


            do
            {


                var result = await client.GetAsync(client.BaseAddress + $"/{jobId.PolicyUploadJobID}");

               

                if (result.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    continue;
                }

                response = Deserialize(result.Content.ReadAsStringAsync().Result, typeof(Response), "Response");

               


                //Response status code does not indicate success: 503 (Server Busy)
                //You have exceed your Quota



                if (response.Status == "Error")
                {


                    throw new Exception(response.ErrorMessage);


                }

                _logger.LogInformation($"Policy status: {response.Status}.");

                await Task.Delay(10000);
                //await Task.Delay((int)TimeSpan.FromSeconds(10).TotalSeconds);

                if (response.Status == null)
                {
                    continue;
                }


            }
            while (response.Status != "Complete");
            



            _logger.LogInformation($"Policy Ref: {response.VersionRef}.");


            return new LocatePolicy()
            {
                Message = new LocatePolicyMessage()
                {
                    LocatePolicySearchOn = new LocatePolicyMessageLocatePolicySearchOn()
                    {
                        Value = 1
                    },
                    LocatePolicySearchOnValue = new LocatePolicyMessageLocatePolicySearchOnValue()
                    {
                        Value = Convert.ToUInt32(response.VersionRef)
                    },
                    ProductTarget = new LocatePolicyMessageProductTarget()
                    {
                        Value = ""
                    },
                    PolicyStatusGroup = new LocatePolicyMessagePolicyStatusGroup()
                    {
                        Value = ""
                    },
                    PolicyStatus = new LocatePolicyMessagePolicyStatus()
                    {
                        Value = ""
                    },
                    PolicyBandStatus = new LocatePolicyMessagePolicyBandStatus()
                    {
                        Value = ""
                    },
                    EffectiveDateFrom = new LocatePolicyMessageEffectiveDateFrom()
                    {
                        Value = ""
                    },
                    EffectiveDateTo = new LocatePolicyMessageEffectiveDateTo()
                    {
                        Value = ""
                    },
                    OpenItemsOnlyIndicator = new LocatePolicyMessageOpenItemsOnlyIndicator()
                    {
                        Value = 5
                    },
                    IncludeRemarketablePoliciesOnlyIndicator = new LocatePolicyMessageIncludeRemarketablePoliciesOnlyIndicator()
                    {
                        Value = 5
                    },
                    IncludePoliciesToBeRenewedOnlyIndicator = new LocatePolicyMessageIncludePoliciesToBeRenewedOnlyIndicator()
                    {
                        Value = 5
                    },
                    IncludeClaimLinkablePoliciesOnlyIndicator = new LocatePolicyMessageIncludeClaimLinkablePoliciesOnlyIndicator()
                    {
                        Value = 5
                    },
                    IncludePolicyYearVersionsOnlyIndicator = new LocatePolicyMessageIncludePolicyYearVersionsOnlyIndicator()
                    {
                        Value = 5
                    },
                    IncludeAddOnPoliciesIndicator = new LocatePolicyMessageIncludeAddOnPoliciesIndicator()
                    {
                        Value = 6
                    },
                    IncludeNonSystemPoliciesIndicator = new LocatePolicyMessageIncludeNonSystemPoliciesIndicator()
                    {
                        Value = 5
                    },
                    FirstResult = new LocatePolicyMessageFirstResult()
                    {
                        Value = 1
                    },
                    LastResult = new LocatePolicyMessageLastResult()
                    {
                        Value = 50
                    }
                }
            };


        }

        public async Task<LocatePolicyResponse> locatePolicyAsync(LocatePolicy locatePolicy)
        {

            var client = _httpClientFactory.CreateClient("ActurisGWS");



            var xml = Serialize(locatePolicy, typeof(LocatePolicy));

            var content = new StringContent(xml, Encoding.UTF8, "application/xml");
            var response = await client.PostAsync(client.BaseAddress, content);
            var result = response.Content.ReadAsStringAsync().Result;

            if (!result.Contains("ContactRef"))

                throw new Exception(result);



            return Deserialize(result, typeof(LocatePolicyResponse), "LocatePolicyResponse");

        }

        public async Task<LocatePartActivityResponse> LocatePartActivityAsync(LocatePolicyResponse locatePolicyResponse)
        {
            var client = _httpClientFactory.CreateClient("ActurisGWS");

            _logger.LogInformation($"Contact ref: {locatePolicyResponse.Message.ContactRef.Value} and Part ref:{locatePolicyResponse.Message.PartRef.Value} for locating Part Activity.");

            var locatePartActivity = new LocatePartActivity()
            {
                Message = new LocatePartActivityMessage()
                {
                    ContactRef = new LocatePartActivityMessageContactRef()
                    {
                        Value = locatePolicyResponse.Message.ContactRef.Value
                    },

                    PartRef = new LocatePartActivityMessagePartRef()
                    {
                        Value = locatePolicyResponse.Message.PartRef.Value

                    },
                    ActivityDateFrom = new LocatePartActivityMessageActivityDateFrom()
                    {
                        Value = Convert.ToUInt32(DateTime.Now.ToString("yyyyMMdd"))
                    },
                    ActivityDateTo = new LocatePartActivityMessageActivityDateTo()
                    {
                        Value = ""
                    },
                    ShowVersionsIndicator = new LocatePartActivityMessageShowVersionsIndicator()
                    {
                        Value = 5
                    },

                    ShowHiddenEntriesIndicator = new LocatePartActivityMessageShowHiddenEntriesIndicator()
                    {
                        Value = 5
                    },
                    ShowCommentaryEntriesOnlyIndicator = new LocatePartActivityMessageShowCommentaryEntriesOnlyIndicator()
                    {
                        Value = 5
                    },
                    ActivityType = new LocatePartActivityMessageActivityType()
                    {
                        Value = ""
                    },

                    FileType = new LocatePartActivityMessageFileType()
                    {
                        Value = ""
                    },
                    ActivityDirection = new LocatePartActivityMessageActivityDirection()
                    {
                        Value = ""
                    },
                    IncludeActivitiesOnThisPartOnlyIndicator = new LocatePartActivityMessageIncludeActivitiesOnThisPartOnlyIndicator()
                    {
                        Value = 6
                    },
                    IncludeViewOnWebOnlyIndicator = new LocatePartActivityMessageIncludeViewOnWebOnlyIndicator()
                    {
                        Value = 5
                    },
                    DetailSearchOnType = new LocatePartActivityMessageDetailSearchOnType()
                    {
                        Value = ""
                    },
                    DetailSearchOnValue = new LocatePartActivityMessageDetailSearchOnValue()
                    {
                        Value = ""
                    },
                    ActivitiesWithAttachmentsOnlyIndicator = new LocatePartActivityMessageActivitiesWithAttachmentsOnlyIndicator()
                    {
                        Value = 6
                    },
                    ActivityRef = new LocatePartActivityMessageActivityRef()
                    {
                        Value = ""
                    },
                    FirstResult = new LocatePartActivityMessageFirstResult()
                    {
                        Value = 1
                    },
                    LastResult = new LocatePartActivityMessageLastResult()
                    {
                        Value = 50
                    }


                }
            };



            var xml = Serialize(locatePartActivity, typeof(LocatePartActivity));

            var content = new StringContent(xml, Encoding.UTF8, "application/xml");
            var response = await client.PostAsync(client.BaseAddress, content);
            var result = response.Content.ReadAsStringAsync().Result;


            if (!result.Contains("ContactRef"))

                throw new Exception(result);



            return Deserialize(result, typeof(LocatePartActivityResponse), "LocatePartActivityResponse");


        }

        public async Task<ActurisMembership> GetDocumentFromActivityAsync(LocatePartActivityResponse locatePartActivityResponse)
        {

            var client = _httpClientFactory.CreateClient("ActurisGWS");

            var certificates = new List<ActurisCertificate>();

            dynamic member = null;
            dynamic documentFromActivityResponse = null;

            _logger.LogInformation($"Getting documents...");

            foreach (var item in locatePartActivityResponse.Message)
            {

                var document = new GetDocumentFromActivity()
                {


                    Message = new GetDocumentFromActivityMessage()
                    {
                        ActivityRef = new GetDocumentFromActivityMessageActivityRef()
                        {
                            Value = item.ActivityRef.Value
                        },
                        DocumentRef = new GetDocumentFromActivityMessageDocumentRef()
                        {
                            Value = item.DocumentRef.Value
                        }
                    }
                };


                var xml = Serialize(document, typeof(GetDocumentFromActivity));

                var content = new StringContent(xml, Encoding.UTF8, "application/xml");
                var response = await client.PostAsync(client.BaseAddress, content);
                var result = response.Content.ReadAsStringAsync().Result;

                if (!result.Contains("ContactRef"))

                    throw new Exception(result);

                documentFromActivityResponse = Deserialize(result, typeof(GetDocumentFromActivityResponse), "GetDocumentFromActivityResponse");


                string fileName = documentFromActivityResponse.Message.DocumentRef.Detail;


                if (fileName.Contains("Public"))
                {
                    fileName = "PLICertificate.pdf";
                }
                else if (fileName.Contains("Employers"))
                {
                    fileName = "ELICertificate.pdf";
                }
                else
                {
                    fileName = "Schedule.pdf";
                }

                _logger.LogInformation($"Contact ref:{item.ActivityRef.Value} and Part ref:{item.DocumentRef.Value} for {fileName}");

                var certificate = new ActurisCertificate()
                {

                    Certificate_FileName = fileName,
                    Certificate_FileContents = documentFromActivityResponse.Message.DocumentData.Value
                };


                certificates.Add(certificate);


            }





            member = new ActurisMembership()
            {

                Certificates = certificates,


            };

            return member;


        }







        public string Serialize(object obj, Type dataType)
        {


            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var settings = new XmlWriterSettings()
            {
                Indent = true,
                OmitXmlDeclaration = true,
            };

            var xml = string.Empty;

            var serializer = new XmlSerializer(dataType);

            using (StringWriter textWriter = new StringWriter())

            using (var writer = XmlWriter.Create(textWriter, settings))
            {


                serializer.Serialize(writer, obj, emptyNamespaces);
                xml = textWriter.ToString();

            }

            return xml;
        }


        public dynamic Deserialize(string xml, dynamic dataType, string XmlRootAttribute)
        {


            var serializer = new XmlSerializer(dataType, new XmlRootAttribute(XmlRootAttribute)) ?? new XmlSerializer(dataType);



            using (TextReader reader = new StringReader(xml))
            {



                return dataType = Convert.ChangeType(serializer.Deserialize(reader), dataType);

            }


        }



    }
}
