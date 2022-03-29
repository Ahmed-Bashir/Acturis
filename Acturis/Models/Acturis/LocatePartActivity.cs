// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(LocatePartActivity));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (LocatePartActivity)serializer.Deserialize(reader);
// }

using System.Xml.Serialization;


[XmlRoot(ElementName = "ActivityDateFrom")]
public class ActivityDateFrom
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "ActivityDateTo")]
public class ActivityDateTo
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "ShowVersionsIndicator")]
public class ShowVersionsIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public int Value { get; set; }
}

[XmlRoot(ElementName = "ShowHiddenEntriesIndicator")]
public class ShowHiddenEntriesIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public int Value { get; set; }
}

[XmlRoot(ElementName = "ShowCommentaryEntriesOnlyIndicator")]
public class ShowCommentaryEntriesOnlyIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public int Value { get; set; }
}

[XmlRoot(ElementName = "ActivityType")]
public class ActivityType
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "FileType")]
public class FileType
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "ActivityDirection")]
public class ActivityDirection
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "IncludeActivitiesOnThisPartOnlyIndicator")]
public class IncludeActivitiesOnThisPartOnlyIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public int Value { get; set; }
}

[XmlRoot(ElementName = "IncludeViewOnWebOnlyIndicator")]
public class IncludeViewOnWebOnlyIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public int Value { get; set; }
}

[XmlRoot(ElementName = "DetailSearchOnType")]
public class DetailSearchOnType
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "DetailSearchOnValue")]
public class DetailSearchOnValue
{

	[XmlAttribute(AttributeName = "Value")]
	public string Value { get; set; }
}

[XmlRoot(ElementName = "ActivitiesWithAttachmentsOnlyIndicator")]
public class ActivitiesWithAttachmentsOnlyIndicator
{

	[XmlAttribute(AttributeName = "Value")]
	public int Value { get; set; }
}





[XmlRoot(ElementName = "LocatePartActivity")]
public class LocatePartActivity
{

	[XmlElement(ElementName = "Message")]
	public Message Message { get; set; }
}

