using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Acturis.Models
{
	[XmlRoot(ElementName = "Response")]
	public class Response
	{
		[XmlElement(ElementName = "ErrorCode")]
		public int ErrorCode { get; set; }

		[XmlElement(ElementName = "ErrorID")]
		public int ErrorID { get; set; }

		[XmlElement(ElementName = "ErrorMessage")]
		public string ErrorMessage { get; set; }


		[XmlElement(ElementName = "PolicyUploadJobID")]
		public string PolicyUploadJobID { get; set; }

		[XmlElement(ElementName = "Status")]
		public string Status { get; set; }

		[XmlElement(ElementName = "ContactRef")]
		public int ContactRef { get; set; }

		[XmlElement(ElementName = "VersionRef")]
		public int VersionRef { get; set; }

		[XmlElement(ElementName = "PolicyRef")]
		public int PolicyRef { get; set; }

		[XmlElement(ElementName = "RfqRef")]
		public int RfqRef { get; set; }

		[XmlElement(ElementName = "DocumentRef")]
		public List<int> DocumentRef { get; set; }

		
	}

    

}
