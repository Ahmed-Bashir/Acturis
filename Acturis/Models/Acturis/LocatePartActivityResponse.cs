using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot(ElementName = "LocatePartActivityResponse")]
public class LocatePartActivityResponse
{

	[XmlElement(ElementName = "Message")]
	public List<Message> Message { get; set; }

}





[XmlRoot(ElementName = "DocumentRef")]
public class DocumentRef
{
	[XmlAttribute(AttributeName = "Detail")]
	public string Detail { get; set; }

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }

	
}

