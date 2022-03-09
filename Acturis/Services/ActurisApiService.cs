using Acturis.Interface;
using Acturis.Interfaces;
using Acturis.Models;
using Acturis.Models.Acturis;
using Acturis.Models.BlueLight;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
    public class ActurisApiService: IActurisApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBluelightApiService _bluelightApiService;
        private readonly ILogger<ActurisApiService> _logger;
        private readonly IEmailService _emailService;
        

        public ActurisApiService(IHttpClientFactory httpClientFactory, IBluelightApiService  bluelightApiService,  ILogger<ActurisApiService> logger, IEmailService emailService )
        {
            _httpClientFactory = httpClientFactory;
            _bluelightApiService = bluelightApiService;
            _logger = logger;
            _emailService = emailService;
            
        }

       


        public async Task<List<ActurisMembership>> GetCertificatesAsync()
        {

            var client = _httpClientFactory.CreateClient("ActurisPolicyUpload");


            var members = await _bluelightApiService.GetMembersAsync();

            var acturisMembers = new List<ActurisMembership>();

            foreach (var member in members)
            {
                try
                {

                    var policyUploadRequest = new PolicyUploadRequest();

                    policyUploadRequest.MappingID = "PibEmployersLiabilityPACEY";

                    var addOrUpdateContact = new AddOrUpdateContact();
                    addOrUpdateContact.OldContactNo = new OldContactNo()
                    {
                        Value = member.Contact.ContactNumber
                    };
                    addOrUpdateContact.ClientTitle = new ClientTitle()
                    {
                        Value = Convert.ToInt32(Enum.Parse(typeof(Models.Acturis.Title), member.Contact.Title.Label))
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

                    var policyDetails = new PolicyDetails();
                    policyDetails.EffectiveDate = new EffectiveDate() { Value = member.ValidFrom.ToString("yyyyMMdd") };

                    policyUploadRequest.PolicyDetails = policyDetails;

                    var additionalDetails = new AdditionalDetails();
                    additionalDetails.InsurerPolicyNo = new InsurerPolicyNo() { Value = member.Contact.ContactNumber };


                    additionalDetails.ChildminderNanny = new ChildminderNanny() { Value = member.Grade.Name.GetValueFromDisplayName<Models.Acturis.Grade>() };

                    additionalDetails.ERNNo = new ERNNo() { Value = member.Contact.EmployeeReferenceNumber ?? "-" };

                    policyUploadRequest.AdditionalDetails = additionalDetails;


                    var xml = Serialize(policyUploadRequest, typeof(PolicyUploadRequest));

                    var content = new StringContent(xml, Encoding.UTF8, "application/xml");
                    var response = await client.PostAsync(client.BaseAddress, content);
                    var result = response.Content.ReadAsStringAsync().Result;

                    var policyUploadResponse = Deserialize(result, typeof(Response), "Response");


                    policyUploadResponse.EffectiveDate = policyUploadRequest.PolicyDetails.EffectiveDate.Value;

            //        var policyUploadResponse = new Response()
            //{
            //    PolicyUploadJobID = "0c5dc717-0fb0-4114-a34f-2cb4212c7ff1",
            //    EffectiveDate = 20220303
            //};


            var locatePolicy = LocatePolicyUploadAsync(policyUploadResponse).GetAwaiter().GetResult();

            var locatePolicyResponse = locatePolicyAsync(locatePolicy).GetAwaiter().GetResult();

            var locatePartActivityResponse = LocatePartActivityAsync(locatePolicyResponse).GetAwaiter().GetResult();

            var acturisMember = GetDocumentFromActivityAsync(locatePartActivityResponse).GetAwaiter().GetResult();


            acturisMembers.Add(acturisMember);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message + "\n" + member.Contact.ContactNumber + " policy was not uploaded");

                    // await _emailService.SendUnapprovedMembers(member, ex.Message);
                }

            }



            return acturisMembers;

        }

        public async Task<List<ActurisMembership>> RenewPolicyAsync()
        {

            var client = _httpClientFactory.CreateClient("ActurisPolicyUpload");


            var members = await _bluelightApiService.GetMembersAsync();

            var acturisMembers = new List<ActurisMembership>();

            foreach (var member in members)
            {
                try
                {

                    var policyUploadRequest = new PolicyUploadRequest();

                    policyUploadRequest.MappingID = "PibEmployersLiabilityPACEY";

                    var addOrUpdateContact = new AddOrUpdateContact();
                    addOrUpdateContact.OldContactNo = new OldContactNo()
                    {
                        Value = member.Contact.ContactNumber
                    };
                    addOrUpdateContact.ClientTitle = new ClientTitle()
                    {
                        Value = Convert.ToInt32(Enum.Parse(typeof(Models.Acturis.Title), member.Contact.Title.Label))
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

                    /* Renewal part */
                    var existingPolicy = new ExistingPolicy();
                    existingPolicy.BusinessEvent = new BusinessEvent() { Value = 3 };

                    policyUploadRequest.ExistingPolicy = existingPolicy;

                    var existingPolicyDetails = new ExistingPolicyDetails();
                    existingPolicyDetails.EffectiveDate = new EffectiveDate() { Value = member.ValidFrom.ToString("yyyyMMdd") };
                   // existingPolicyDetails.ChildminderNanny = new ChildminderNanny() { Value = Models.Acturis.Grade.GetGrade()[member.Grade.Name] };
                    existingPolicyDetails.TermEndDate = new TermEndDate() { Value = member.ValidTo.ToString("yyyyMMdd") };

                    policyUploadRequest.ExistingPolicyDetails = existingPolicyDetails;


                    var xml = Serialize(policyUploadRequest, typeof(PolicyUploadRequest));

                    var content = new StringContent(xml, Encoding.UTF8, "application/xml");
                    var response = await client.PostAsync(client.BaseAddress, content);
                    var result = response.Content.ReadAsStringAsync().Result;

                    var policyUploadResponse = Deserialize(result, typeof(Response), "Response");


                    policyUploadResponse.EffectiveDate = policyUploadRequest.PolicyDetails.EffectiveDate.Value;

                    //var policyUploadResponse = new Response()
                    //{
                    //    PolicyUploadJobID = "0c5dc717-0fb0-4114-a34f-2cb4212c7ff1",
                    //    EffectiveDate = 20220303
                    //};


                    var locatePolicy = LocatePolicyUploadAsync(policyUploadResponse).GetAwaiter().GetResult();

                    var locatePolicyResponse = locatePolicyAsync(locatePolicy).GetAwaiter().GetResult();

                    var locatePartActivityResponse = LocatePartActivityAsync(locatePolicyResponse).GetAwaiter().GetResult();

                    var acturisMember = GetDocumentFromActivityAsync(locatePartActivityResponse).GetAwaiter().GetResult();


                    acturisMembers.Add(acturisMember);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message + "\n" + member.Contact.ContactNumber + " policy was not uploaded");

                    // await _emailService.SendUnapprovedMembers(member, ex.Message);
                }

            }



            return acturisMembers;

        }

        public async Task<List<string>> CancelPolicyAsync()
        {

            var client = _httpClientFactory.CreateClient("ActurisPolicyUpload");


            var members = await _bluelightApiService.GetMembersAsync();

            var acturisMembers = new List<string>();

            foreach (var member in members)
            {
                try
                {

                    var policyUploadRequest = new PolicyUploadRequest();

                    policyUploadRequest.MappingID = "PibEmployersLiabilityPACEY";

                    var addOrUpdateContact = new AddOrUpdateContact();
                    addOrUpdateContact.OldContactNo = new OldContactNo()
                    {
                        Value = member.Contact.ContactNumber
                    };
                    addOrUpdateContact.ClientTitle = new ClientTitle()
                    {
                        Value = Convert.ToInt32(Enum.Parse(typeof(Models.Acturis.Title), member.Contact.Title.Label))
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

                    /* Renewal part */
                    var existingPolicy = new ExistingPolicy();
                    existingPolicy.BusinessEvent = new BusinessEvent() { Value = 3 };

                    policyUploadRequest.ExistingPolicy = existingPolicy;
                    var existingPolicyDetails = new ExistingPolicyDetails();
                    existingPolicyDetails.EffectiveDate = new EffectiveDate() { Value = member.ValidFrom.ToString("yyyyMMdd") };
                    existingPolicyDetails.ChildminderNanny = new ChildminderNanny() { Value = EnumHelper.GetValueFromDisplayName<Models.Acturis.Grade>(member.Grade.Name) };
                    existingPolicyDetails.TermEndDate = new TermEndDate() { Value = member.ValidTo.ToString("yyyyMMdd") };

                    policyUploadRequest.ExistingPolicyDetails = existingPolicyDetails;


                    var xml = Serialize(policyUploadRequest, typeof(PolicyUploadRequest));

                    var content = new StringContent(xml, Encoding.UTF8, "application/xml");
                    var response = await client.PostAsync(client.BaseAddress, content);
                    var result = response.Content.ReadAsStringAsync().Result;

                    var policyUploadResponse = Deserialize(result, typeof(Response), "Response");


                    policyUploadResponse.EffectiveDate = policyUploadRequest.PolicyDetails.EffectiveDate.Value;

                    //var policyUploadResponse = new Response()
                    //{
                    //    PolicyUploadJobID = "0c5dc717-0fb0-4114-a34f-2cb4212c7ff1",
                    //    EffectiveDate = 20220303
                    //};


                    var locatePolicy = LocatePolicyUploadAsync(policyUploadResponse).GetAwaiter().GetResult();

                    var locatePolicyResponse = locatePolicyAsync(locatePolicy).GetAwaiter().GetResult();

                   // return something 

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message + "\n" + member.Contact.ContactNumber + " policy was not uploaded");

                    // await _emailService.SendUnapprovedMembers(member, ex.Message);
                }

            }



            return acturisMembers;

        }

        public async Task<LocatePolicy> LocatePolicyUploadAsync(Response jobId)
        {

            var client = _httpClientFactory.CreateClient("ActurisPolicyUpload");

            dynamic response = null;

            do
            {

                response = Deserialize(await client.GetStringAsync(client.BaseAddress + $"/{jobId.PolicyUploadJobID}"), typeof(Response), "Response");

                if (response.Status == "Error")
                {
                    Console.WriteLine(response.ErrorMessage);

                    throw new Exception(response.ErrorMessage);



                }

                _logger.LogInformation($"Policy upload status: {response.Status}...");

                //await Task.Delay((int)TimeSpan.FromSeconds(10).TotalSeconds);
            }
            while (response.Status != "Complete");

            _logger.LogInformation($"Policy upload status: {response.Status}. Policy Ref: {response.PolicyRef}");
            

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
                        Value = Convert.ToUInt32(response.PolicyRef)
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
                        Value = response.EffectiveDate.ToString()
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

        public async Task<LocatePolicyResponse>  locatePolicyAsync(LocatePolicy locatePolicy)
        {

            var client = _httpClientFactory.CreateClient("ActurisGWS");


            _logger.LogInformation($"Locating policy... ");

            var xml = Serialize(locatePolicy, typeof(LocatePolicy));

            var content = new StringContent(xml, Encoding.UTF8, "application/xml");
            var response = await client.PostAsync(client.BaseAddress, content);
            var result = response.Content.ReadAsStringAsync().Result;

            if (!result.Contains("ContactRef"))

                throw new Exception(result);

            _logger.LogInformation($"Policy found. ");

            return Deserialize(result, typeof(LocatePolicyResponse), "LocatePolicyResponse");

        }

        public async Task<LocatePartActivityResponse> LocatePartActivityAsync(LocatePolicyResponse locatePolicyResponse)
        {
            var client = _httpClientFactory.CreateClient("ActurisGWS");

            _logger.LogInformation($"Using - Contact ref: {locatePolicyResponse.Message.ContactRef.Value} and Part ref:{locatePolicyResponse.Message.PartRef.Value} to find Part Activity...");

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
                            Value = locatePolicyResponse.Message.EffectiveDate.Value
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

            _logger.LogInformation($"Loactating part activity...");

            var xml = Serialize(locatePartActivity, typeof(LocatePartActivity));

            var content = new StringContent(xml, Encoding.UTF8, "application/xml");
            var response = await client.PostAsync(client.BaseAddress, content);
            var result = response.Content.ReadAsStringAsync().Result;


            if (!result.Contains("ContactRef"))

                throw new Exception(result);

            _logger.LogInformation($"Part activity found.");

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

                

                var fileName = documentFromActivityResponse.Message.DocumentRef.Detail.Contains("Certificate") ? "Certificate" : "Schedule";

                _logger.LogInformation($" Found: {documentFromActivityResponse.Message.DocumentRef.Detail}. Identifiers used - Activity Ref: {item.ActivityRef.Value}, Document Ref:{item.ActivityRef.Value}");

                var certificate = new ActurisCertificate()
                    {
                       
                        Certificate_FileName = fileName,
                        Certificate_FileContents = documentFromActivityResponse.Message.DocumentData.Value
                    };


                    certificates.Add(certificate);


                }


                string certificateName = documentFromActivityResponse.Message.DocumentRef.Detail;

             

                var words = certificateName.Split(' ');
                var conNumber = string.Empty;

                conNumber = words[words.Length - 2];
             
           

                member = new ActurisMembership()
                {
                    Id = conNumber,
                    Certificates = certificates,


                };

                return member;



   

        }

        public async Task SendApprovedMembersAsync()
        {
            var members = await GetCertificatesAsync();

            if (members.Any())
            {
                await _bluelightApiService.PostMembership(members);

                
            }


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


        public dynamic Deserialize(string xml, dynamic dataType,string XmlRootAttribute)
        {

           
            var serializer = new XmlSerializer(dataType, new XmlRootAttribute(XmlRootAttribute)) ?? new XmlSerializer(dataType);



                using (TextReader reader = new StringReader(xml))
                {



                return dataType = Convert.ChangeType(serializer.Deserialize(reader), dataType); 
                
                }
    
            
        }

       

    }
}
