using System.Xml.Serialization;



[XmlRoot(ElementName = "DocumentData")]
public class DocumentData
{


	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }


}

[XmlRoot(ElementName = "GetDocumentFromActivityResponse")]
public class GetDocumentFromActivityResponse
{

	[XmlElement(ElementName = "Message")]
	public Message Message { get; set; }

}

