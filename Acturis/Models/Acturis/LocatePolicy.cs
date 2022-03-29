// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(LocatePolicy));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (LocatePolicy)serializer.Deserialize(reader);
// }

using System.Xml.Serialization;

[XmlRoot(ElementName = "LocatePolicySearchOn")]
public class LocatePolicySearchOn
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "LocatePolicySearchOnValue")]
public class LocatePolicySearchOnValue
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "ProductTarget")]
public class ProductTarget
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "PolicyStatusGroup")]
public class PolicyStatusGroup
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "PolicyStatus")]
public class PolicyStatus
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "PolicyBandStatus")]
public class PolicyBandStatus
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "EffectiveDateFrom")]
public class EffectiveDateFrom
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "EffectiveDateTo")]
public class EffectiveDateTo
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "OpenItemsOnlyIndicator")]
public class OpenItemsOnlyIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "IncludeRemarketablePoliciesOnlyIndicator")]
public class IncludeRemarketablePoliciesOnlyIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "IncludePoliciesToBeRenewedOnlyIndicator")]
public class IncludePoliciesToBeRenewedOnlyIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "IncludeClaimLinkablePoliciesOnlyIndicator")]
public class IncludeClaimLinkablePoliciesOnlyIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "IncludePolicyYearVersionsOnlyIndicator")]
public class IncludePolicyYearVersionsOnlyIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "IncludeAddOnPoliciesIndicator")]
public class IncludeAddOnPoliciesIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "IncludeNonSystemPoliciesIndicator")]
public class IncludeNonSystemPoliciesIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "FirstResult")]
public class FirstResult
{

	[XmlAttribute(AttributeName = "Value")]
	public int Value { get; set; }
}

[XmlRoot(ElementName = "LastResult")]
public class LastResult
{

	[XmlAttribute(AttributeName = "Value")]
	public int Value { get; set; }
}

[XmlRoot(ElementName = "LocatePolicy")]
public class LocatePolicy
{

	[XmlElement(ElementName = "Message")]
	public Message Message { get; set; }
}

