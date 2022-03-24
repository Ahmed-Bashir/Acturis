using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Acturis.Models
{
	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(PolicyUploadRequest));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (PolicyUploadRequest)serializer.Deserialize(reader);
	// }

	[XmlRoot(ElementName = "OldContactNo")]
	public class OldContactNo
	{

		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "ClientTitle")]
	public class ClientTitle
	{

		[XmlAttribute(AttributeName = "Value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "FullName")]
	public class FullName
	{

		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "ClientFirstName")]
	public class ClientFirstName
	{

		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "ClientSurname")]
	public class ClientSurname
	{

		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }

		[XmlElement]
        public object MyProperty { get; set; }
    }

	[XmlRoot(ElementName = "DateOfBirth")]
	public class DateOfBirth
	{

		[XmlAttribute(AttributeName = "Value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "EmailAddress")]
	public class EmailAddress
	{

		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "WorkPhone")]
	public class WorkPhone
	{

		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "MobilePhone")]
	public class MobilePhone
	{

		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "HomePhone")]
	public class HomePhone
	{

		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "AddOrUpdateContact")]
	public class AddOrUpdateContact
	{

		[XmlElement(ElementName = "OldContactNo")]
		public OldContactNo OldContactNo { get; set; }

		[XmlElement(ElementName = "ClientTitle")]
		public ClientTitle ClientTitle { get; set; }

		[XmlElement(ElementName = "FullName")]
		public FullName FullName { get; set; }

		[XmlElement(ElementName = "ClientFirstName")]
		public ClientFirstName ClientFirstName { get; set; }

		[XmlElement(ElementName = "ClientSurname")]
		public ClientSurname ClientSurname { get; set; }

		[XmlElement(ElementName = "DateOfBirth")]
		public DateOfBirth DateOfBirth { get; set; }

		[XmlElement(ElementName = "EmailAddress")]
		public EmailAddress EmailAddress { get; set; }

		[XmlElement(ElementName = "WorkPhone")]
		public WorkPhone WorkPhone { get; set; }

		[XmlElement(ElementName = "MobilePhone")]
		public MobilePhone MobilePhone { get; set; }

		[XmlElement(ElementName = "HomePhone")]
		public HomePhone HomePhone { get; set; }
	}

	[XmlRoot(ElementName = "EffectiveDate")]
	public class EffectiveDate
	{

		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "TermEndDate")]
	public class TermEndDate
	{

		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "PolicyDetails")]
	public class PolicyDetails
	{

		[XmlElement(ElementName = "EffectiveDate")]
		public EffectiveDate EffectiveDate { get; set; }

		[XmlElement(ElementName = "TermEndDate")]
		public TermEndDate TermEndDate { get; set; }
	}
	[XmlRoot(ElementName = "BusinessEvent")]
	public class BusinessEvent
	{

		[XmlAttribute(AttributeName = "Value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "ExistingPolicy")]
	public class ExistingPolicy
	{

		[XmlElement(ElementName = "BusinessEvent")]
		public BusinessEvent BusinessEvent { get; set; }

		[XmlElement(ElementName = "InsurerPolicyNo")]
		public InsurerPolicyNo InsurerPolicyNo { get; set; }

		
	}

	[XmlRoot(ElementName = "InsurerPolicyNo")]
	public class InsurerPolicyNo
	{

		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "ChildminderNanny")]
	public class ChildminderNanny
	{

		[XmlAttribute(AttributeName = "Value")]
		public int Value { get; set; }
	}

	[XmlRoot(ElementName = "ERNNo")]
	public class ERNNo
	{

		[XmlAttribute(AttributeName = "Value")]
		public string Value { get; set; }
	}

	[XmlRoot(ElementName = "AdditionalDetails")]

	public class AdditionalDetails
	{

		[XmlElement(ElementName = "InsurerPolicyNo")]
		public InsurerPolicyNo InsurerPolicyNo { get; set; }

		[XmlElement(ElementName = "ChildminderNanny")]
		public ChildminderNanny ChildminderNanny { get; set; }

		[XmlElement(ElementName = "ERNNo")]
		public ERNNo ERNNo { get; set; }
	}


	[XmlRoot(ElementName = "ExistingPolicyDetails")]
	public class ExistingPolicyDetails
	{

		[XmlElement(ElementName = "EffectiveDate")]
		public EffectiveDate EffectiveDate { get; set; }

		[XmlElement(ElementName = "TermEndDate")]
		public TermEndDate TermEndDate { get; set; }

		[XmlElement(ElementName = "ChildminderNanny")]
		public ChildminderNanny ChildminderNanny { get; set; }

		[XmlElement(ElementName = "ERNNo")]
		public ERNNo ERNNo { get; set; }
	}

	[XmlRoot(ElementName = "PolicyUploadRequest")]
	public class PolicyUploadRequest
	{

		[XmlElement(ElementName = "MappingID")]
		public string MappingID { get; set; }

		[XmlElement(ElementName = "AddOrUpdateContact")]
		public AddOrUpdateContact AddOrUpdateContact { get; set; }

		[XmlElement(ElementName = "PolicyDetails")]
		public PolicyDetails PolicyDetails { get; set; }

		[XmlElement(ElementName = "AdditionalDetails")]
		public AdditionalDetails AdditionalDetails { get; set; }

		[XmlElement(ElementName = "ExistingPolicy")]
		public ExistingPolicy ExistingPolicy { get; set; }

		[XmlElement(ElementName = "ExistingPolicyDetails")]
		public ExistingPolicyDetails ExistingPolicyDetails { get; set; }

		[XmlElement(ElementName = "xmlns")]

		public object Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}


}
