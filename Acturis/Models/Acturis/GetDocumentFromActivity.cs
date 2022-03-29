// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(GetDocumentFromActivity));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (GetDocumentFromActivity)serializer.Deserialize(reader);
// }

using System.Xml.Serialization;


[XmlRoot(ElementName = "GetDocumentFromActivity")]
public class GetDocumentFromActivity
{

	[XmlElement(ElementName = "Message")]
	public Message Message { get; set; }
}

