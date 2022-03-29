using System.Xml.Serialization;

[XmlRoot(ElementName = "ContactRef")]
public class ContactRef
{
	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "PartRef")]
public class PartRef
{
	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "ActivityRef")]
public class ActivityRef
{


	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "LocatePolicyResponse")]
public class LocatePolicyResponse
{

	[XmlElement(ElementName = "Message")]
	public Message Message { get; set; }

}
