using System.Xml.Serialization;

[XmlRoot(ElementName = "Message")]
public class Message
{
	


	/// <summary>
	/// Order of the below properties matter
	/// </summary>


	[XmlElement(ElementName = "ContactRef")]
	public ContactRef ContactRef { get; set; }

	[XmlElement(ElementName = "PartRef")]
	public PartRef PartRef { get; set; }



	/// <summary>
	/// Locate policy 
	/// </summary>

	[XmlElement(ElementName = "LocatePolicySearchOn")]
	public LocatePolicySearchOn LocatePolicySearchOn { get; set; }

	[XmlElement(ElementName = "LocatePolicySearchOnValue")]
	public LocatePolicySearchOnValue LocatePolicySearchOnValue { get; set; }

	[XmlElement(ElementName = "ProductTarget")]
	public ProductTarget ProductTarget { get; set; }

	[XmlElement(ElementName = "PolicyStatusGroup")]
	public PolicyStatusGroup PolicyStatusGroup { get; set; }

	[XmlElement(ElementName = "PolicyStatus")]
	public PolicyStatus PolicyStatus { get; set; }

	[XmlElement(ElementName = "PolicyBandStatus")]
	public PolicyBandStatus PolicyBandStatus { get; set; }

	[XmlElement(ElementName = "EffectiveDateFrom")]
	public EffectiveDateFrom EffectiveDateFrom { get; set; }

	[XmlElement(ElementName = "EffectiveDateTo")]
	public EffectiveDateTo EffectiveDateTo { get; set; }

	[XmlElement(ElementName = "OpenItemsOnlyIndicator")]
	public OpenItemsOnlyIndicator OpenItemsOnlyIndicator { get; set; }

	[XmlElement(ElementName = "IncludeRemarketablePoliciesOnlyIndicator")]
	public IncludeRemarketablePoliciesOnlyIndicator IncludeRemarketablePoliciesOnlyIndicator { get; set; }

	[XmlElement(ElementName = "IncludePoliciesToBeRenewedOnlyIndicator")]
	public IncludePoliciesToBeRenewedOnlyIndicator IncludePoliciesToBeRenewedOnlyIndicator { get; set; }

	[XmlElement(ElementName = "IncludeClaimLinkablePoliciesOnlyIndicator")]
	public IncludeClaimLinkablePoliciesOnlyIndicator IncludeClaimLinkablePoliciesOnlyIndicator { get; set; }

	[XmlElement(ElementName = "IncludePolicyYearVersionsOnlyIndicator")]
	public IncludePolicyYearVersionsOnlyIndicator IncludePolicyYearVersionsOnlyIndicator { get; set; }

	[XmlElement(ElementName = "IncludeAddOnPoliciesIndicator")]
	public IncludeAddOnPoliciesIndicator IncludeAddOnPoliciesIndicator { get; set; }

	[XmlElement(ElementName = "IncludeNonSystemPoliciesIndicator")]
	public IncludeNonSystemPoliciesIndicator IncludeNonSystemPoliciesIndicator { get; set; }





	/// <summary>
	/// LocatePartActivity
	/// </summary>

	[XmlElement(ElementName = "ActivityDateFrom")]
	public ActivityDateFrom ActivityDateFrom { get; set; }

	[XmlElement(ElementName = "ActivityDateTo")]
	public ActivityDateTo ActivityDateTo { get; set; }

	[XmlElement(ElementName = "ShowVersionsIndicator")]
	public ShowVersionsIndicator ShowVersionsIndicator { get; set; }

	[XmlElement(ElementName = "ShowHiddenEntriesIndicator")]
	public ShowHiddenEntriesIndicator ShowHiddenEntriesIndicator { get; set; }

	[XmlElement(ElementName = "ShowCommentaryEntriesOnlyIndicator")]
	public ShowCommentaryEntriesOnlyIndicator ShowCommentaryEntriesOnlyIndicator { get; set; }

	[XmlElement(ElementName = "ActivityType")]
	public ActivityType ActivityType { get; set; }

	[XmlElement(ElementName = "FileType")]
	public FileType FileType { get; set; }

	[XmlElement(ElementName = "ActivityDirection")]
	public ActivityDirection ActivityDirection { get; set; }

	[XmlElement(ElementName = "IncludeActivitiesOnThisPartOnlyIndicator")]
	public IncludeActivitiesOnThisPartOnlyIndicator IncludeActivitiesOnThisPartOnlyIndicator { get; set; }

	[XmlElement(ElementName = "IncludeViewOnWebOnlyIndicator")]
	public IncludeViewOnWebOnlyIndicator IncludeViewOnWebOnlyIndicator { get; set; }

	[XmlElement(ElementName = "DetailSearchOnType")]
	public DetailSearchOnType DetailSearchOnType { get; set; }

	[XmlElement(ElementName = "DetailSearchOnValue")]
	public DetailSearchOnValue DetailSearchOnValue { get; set; }

	[XmlElement(ElementName = "ActivitiesWithAttachmentsOnlyIndicator")]
	public ActivitiesWithAttachmentsOnlyIndicator ActivitiesWithAttachmentsOnlyIndicator { get; set; }

	[XmlElement(ElementName = "ActivityRef")]
	public ActivityRef ActivityRef { get; set; }

	[XmlElement(ElementName = "FirstResult")]
	public FirstResult FirstResult { get; set; }

	[XmlElement(ElementName = "LastResult")]
	public LastResult LastResult { get; set; }


	/// <summary>
	/// GetDocumentFromActivity 
	/// </summary>

	[XmlElement(ElementName = "DocumentData")]
	public DocumentData DocumentData { get; set; }

	[XmlElement(ElementName = "DocumentRef")]
	public DocumentRef DocumentRef { get; set; }
}
