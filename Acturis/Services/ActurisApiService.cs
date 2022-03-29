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




        public async Task PolicyUploadRequestAsync()
        {

            var client = _httpClientFactory.CreateClient("ActurisPolicyUpload");


            var members = await _bluelightApiService.GetMembersAsync();



            var filteredMembers = members.Where(x => x.Contact.ContactNumber.Equals("CON-10004880") || x.Contact.ContactNumber.Equals("CON-00075694") || x.Contact.ContactNumber.Equals("CON-00005271") || x.Contact.ContactNumber.Equals("CON-00003747")
            || x.Contact.ContactNumber.Equals("CON-00070243") || x.Contact.ContactNumber.Equals("CON-00189276") || x.Contact.ContactNumber.Equals("CON-00076718"));

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


                        //    PolicyUploadJobID = "ea74f8c7-9a02-4522-9b62-ba6432b46174"


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
                        _logger.LogError($"{ex.Message} {member.Contact.ContactNumber} policy upload failed.\n");


                        await _emailService.ReportUnsuccessfulUpload(member, ex.Message);

                        await _bluelightApiService.PostMembership(new ActurisMembership() { Id = member.Id, ContactNumber = member.Contact.ContactNumber, Certificates = new List<ActurisCertificate>() });

                    }



                }


        }



        public async Task MTAPolicyUploadRequestAsync()
        {

            var client = _httpClientFactory.CreateClient("ActurisPolicyUpload");


            var members = await _bluelightApiService.GetNameChangeAsync();

            var filteredMembers = members.Where(x => x.Contact.ContactNumber.Equals("CON-11404"));
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

                        await _bluelightApiService.ClearNameChange(acturisMember);



                        _logger.LogInformation($"MTA policy upload for {member.Contact.ContactNumber} complete.\n");

                        await _emailService.Report($"MTA policy upload for {member.Contact.ContactNumber} complete.");

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"{ex.Message} {member.Contact.ContactNumber} policy upload failed.\n");


                        await _emailService.ReportUnsuccessfulUpload(member, ex.Message);

                        await _bluelightApiService.PostMembership(new ActurisMembership() { Id = member.Id, ContactNumber = member.Contact.ContactNumber, Certificates = new List<ActurisCertificate>() });
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
                        _logger.LogError($"{ex.Message} {member.Contact.ContactNumber} policy upload failed.\n");


                        await _emailService.ReportUnsuccessfulUpload(member, ex.Message);

                        await _bluelightApiService.PostMembership(new ActurisMembership() { Id = member.Id, ContactNumber = member.Contact.ContactNumber, Certificates = new List<ActurisCertificate>() });


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
                        _logger.LogError($"{ex.Message} {member.Contact.ContactNumber} policy upload failed.\n");


                        await _emailService.ReportUnsuccessfulUpload(member, ex.Message);

                        await _bluelightApiService.PostMembership(new ActurisMembership() { Id = member.Id, ContactNumber = member.Contact.ContactNumber, Certificates = new List<ActurisCertificate>() });
                    }



                }


        }

        public PolicyUploadRequest AddOrUpdateContact(Bluelight member)
        {

            var policyUploadRequest = new PolicyUploadRequest();

            policyUploadRequest.MappingID = "PibEmployersLiabilityPACEY";

            var addOrUpdateContact = new AddOrUpdateContact();

            addOrUpdateContact.OldContactNo = new OldContactNo()
            {
                Value = member.Contact.ContactNumber
            };
            var title = member.Contact.Title.Label.Equals("") ? "NotApplicable" : member.Contact.Title.Label;
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

                throw new Exception($"{response.ReasonPhrase} { result}.\n");

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


                if (response.Status == null)
                {
                    continue;
                }

                //Response status code does not indicate success: 503 (Server Busy)
                //You have exceed your Quota



                if (response.Status == "Error")
                {


                    throw new Exception(response.ErrorMessage);


                }

                _logger.LogInformation($"Policy status: {response.Status}.");

                await Task.Delay(10000);
                //await Task.Delay((int)TimeSpan.FromSeconds(10).TotalSeconds);




            }
            while (response.Status != "Complete");




            _logger.LogInformation($"Policy Ref: {response.VersionRef}.");


            return new LocatePolicy()
            {
                Message = new Message()
                {
                    LocatePolicySearchOn = new LocatePolicySearchOn()
                    {
                        Value = "1"
                    },
                    LocatePolicySearchOnValue = new LocatePolicySearchOnValue()
                    {
                        Value = response.VersionRef
                    },
                    ProductTarget = new ProductTarget()
                    {
                        Value = ""
                    },
                    PolicyStatusGroup = new PolicyStatusGroup()
                    {
                        Value = ""
                    },
                    PolicyStatus = new PolicyStatus()
                    {
                        Value = ""
                    },
                    PolicyBandStatus = new PolicyBandStatus()
                    {
                        Value = ""
                    },
                    EffectiveDateFrom = new EffectiveDateFrom()
                    {
                        Value = ""
                    },
                    EffectiveDateTo = new EffectiveDateTo()
                    {
                        Value = ""
                    },
                    OpenItemsOnlyIndicator = new OpenItemsOnlyIndicator()
                    {
                        Value = "5"
                    },
                    IncludeRemarketablePoliciesOnlyIndicator = new IncludeRemarketablePoliciesOnlyIndicator()
                    {
                        Value = "5"
                    },
                    IncludePoliciesToBeRenewedOnlyIndicator = new IncludePoliciesToBeRenewedOnlyIndicator()
                    {
                        Value = "5"
                    },
                    IncludeClaimLinkablePoliciesOnlyIndicator = new IncludeClaimLinkablePoliciesOnlyIndicator()
                    {
                        Value = "5"
                    },
                    IncludePolicyYearVersionsOnlyIndicator = new IncludePolicyYearVersionsOnlyIndicator()
                    {
                        Value = "5"
                    },
                    IncludeAddOnPoliciesIndicator = new IncludeAddOnPoliciesIndicator()
                    {
                        Value = "6"
                    },
                    IncludeNonSystemPoliciesIndicator = new IncludeNonSystemPoliciesIndicator()
                    {
                        Value = "5"
                    },
                    FirstResult = new FirstResult()
                    {
                        Value = 1
                    },
                    LastResult = new LastResult()
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

            var response = await PostToActuris(xml, client);



            return Deserialize(response, typeof(LocatePolicyResponse), "LocatePolicyResponse");

        }

        public async Task<LocatePartActivityResponse> LocatePartActivityAsync(LocatePolicyResponse locatePolicyResponse)
        {
            var client = _httpClientFactory.CreateClient("ActurisGWS");

            _logger.LogInformation($"Contact ref: {locatePolicyResponse.Message.ContactRef.Value} and Part ref:{locatePolicyResponse.Message.PartRef.Value} for locating Part Activity.");

            var locatePartActivity = new LocatePartActivity()
            {
                Message = new Message()
                {
                    ContactRef = new ContactRef()
                    {
                        Value = locatePolicyResponse.Message.ContactRef.Value
                    },

                    PartRef = new PartRef()
                    {
                        Value = locatePolicyResponse.Message.PartRef.Value

                    },
                    ActivityDateFrom = new ActivityDateFrom()
                    {
                        Value = DateTime.Now.ToString("yyyyMMdd").ToString()
                    },
                    ActivityDateTo = new ActivityDateTo()
                    {
                        Value = ""
                    },
                    ShowVersionsIndicator = new ShowVersionsIndicator()
                    {
                        Value = 5
                    },

                    ShowHiddenEntriesIndicator = new ShowHiddenEntriesIndicator()
                    {
                        Value = 5
                    },
                    ShowCommentaryEntriesOnlyIndicator = new ShowCommentaryEntriesOnlyIndicator()
                    {
                        Value = 5
                    },
                    ActivityType = new ActivityType()
                    {
                        Value = ""
                    },

                    FileType = new FileType()
                    {
                        Value = ""
                    },
                    ActivityDirection = new ActivityDirection()
                    {
                        Value = ""
                    },
                    IncludeActivitiesOnThisPartOnlyIndicator = new IncludeActivitiesOnThisPartOnlyIndicator()
                    {
                        Value = 6
                    },
                    IncludeViewOnWebOnlyIndicator = new IncludeViewOnWebOnlyIndicator()
                    {
                        Value = 5
                    },
                    DetailSearchOnType = new DetailSearchOnType()
                    {
                        Value = ""
                    },
                    DetailSearchOnValue = new DetailSearchOnValue()
                    {
                        Value = ""
                    },
                    ActivitiesWithAttachmentsOnlyIndicator = new ActivitiesWithAttachmentsOnlyIndicator()
                    {
                        Value = 6
                    },
                    ActivityRef = new ActivityRef()
                    {
                        Value = ""
                    },
                    FirstResult = new FirstResult()
                    {
                        Value = 1
                    },
                    LastResult = new LastResult()
                    {
                        Value = 50
                    }


                }
            };



            var xml = Serialize(locatePartActivity, typeof(LocatePartActivity));

            var response = await PostToActuris(xml, client);



            return Deserialize(response, typeof(LocatePartActivityResponse), "LocatePartActivityResponse");


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


                    Message = new Message()
                    {
                        ActivityRef = new ActivityRef()
                        {
                            Value = item.ActivityRef.Value
                        },
                        DocumentRef = new DocumentRef()
                        {
                            Value = item.DocumentRef.Value
                        }
                    }
                };


                var xml = Serialize(document, typeof(GetDocumentFromActivity));

                var response = await PostToActuris(xml, client);

                documentFromActivityResponse = Deserialize(response, typeof(GetDocumentFromActivityResponse), "GetDocumentFromActivityResponse");


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
