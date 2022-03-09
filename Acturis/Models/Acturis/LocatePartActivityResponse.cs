using System;
using System.Collections.Generic;
using System.Text;

namespace Acturis.Models
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class LocatePartActivityResponse
    {

        private LocatePartActivityResponseMessage[] messageField;

        private LocatePartActivityResponseResults resultsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Message")]
        public LocatePartActivityResponseMessage[] Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityResponseResults Results
        {
            get
            {
                return this.resultsField;
            }
            set
            {
                this.resultsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityResponseMessage
    {

        private LocatePartActivityResponseMessageContactRef contactRefField;

        private LocatePartActivityResponseMessagePartRef partRefField;

        private LocatePartActivityResponseMessageContactName contactNameField;

        private LocatePartActivityResponseMessageActivityRef activityRefField;

        private LocatePartActivityResponseMessageActivityDescription activityDescriptionField;

        private LocatePartActivityResponseMessageLinkedTasksIndicator linkedTasksIndicatorField;

        private LocatePartActivityResponseMessageAttachedFiles attachedFilesField;

        private LocatePartActivityResponseMessageDocumentRef documentRefField;

        /// <remarks/>
        public LocatePartActivityResponseMessageContactRef ContactRef
        {
            get
            {
                return this.contactRefField;
            }
            set
            {
                this.contactRefField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityResponseMessagePartRef PartRef
        {
            get
            {
                return this.partRefField;
            }
            set
            {
                this.partRefField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityResponseMessageContactName ContactName
        {
            get
            {
                return this.contactNameField;
            }
            set
            {
                this.contactNameField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityResponseMessageActivityRef ActivityRef
        {
            get
            {
                return this.activityRefField;
            }
            set
            {
                this.activityRefField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityResponseMessageActivityDescription ActivityDescription
        {
            get
            {
                return this.activityDescriptionField;
            }
            set
            {
                this.activityDescriptionField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityResponseMessageLinkedTasksIndicator LinkedTasksIndicator
        {
            get
            {
                return this.linkedTasksIndicatorField;
            }
            set
            {
                this.linkedTasksIndicatorField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityResponseMessageAttachedFiles AttachedFiles
        {
            get
            {
                return this.attachedFilesField;
            }
            set
            {
                this.attachedFilesField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityResponseMessageDocumentRef DocumentRef
        {
            get
            {
                return this.documentRefField;
            }
            set
            {
                this.documentRefField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityResponseMessageContactRef
    {

        private uint valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityResponseMessagePartRef
    {

        private ulong valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityResponseMessageContactName
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityResponseMessageActivityRef
    {

        private uint valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityResponseMessageActivityDescription
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityResponseMessageLinkedTasksIndicator
    {

        private byte valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityResponseMessageAttachedFiles
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityResponseMessageDocumentRef
    {

        private uint valueField;

        private string detailField;

        private byte fileStatusField;

        private uint addedByField;

        private uint addedDateField;

        private uint fileSizeField;

        private byte fileTypeField;

        private string origFileNameField;

        private byte printQueueIndicatorField;

        private byte emailQueueIndicatorField;

        private byte viewOnWebQueueIndicatorField;

        private byte parentDocumentIndicatorField;

        private byte previewIndicatorField;

        private byte fileExtensionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Detail
        {
            get
            {
                return this.detailField;
            }
            set
            {
                this.detailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte FileStatus
        {
            get
            {
                return this.fileStatusField;
            }
            set
            {
                this.fileStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint AddedBy
        {
            get
            {
                return this.addedByField;
            }
            set
            {
                this.addedByField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint AddedDate
        {
            get
            {
                return this.addedDateField;
            }
            set
            {
                this.addedDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint FileSize
        {
            get
            {
                return this.fileSizeField;
            }
            set
            {
                this.fileSizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte FileType
        {
            get
            {
                return this.fileTypeField;
            }
            set
            {
                this.fileTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string OrigFileName
        {
            get
            {
                return this.origFileNameField;
            }
            set
            {
                this.origFileNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte PrintQueueIndicator
        {
            get
            {
                return this.printQueueIndicatorField;
            }
            set
            {
                this.printQueueIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte EmailQueueIndicator
        {
            get
            {
                return this.emailQueueIndicatorField;
            }
            set
            {
                this.emailQueueIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ViewOnWebQueueIndicator
        {
            get
            {
                return this.viewOnWebQueueIndicatorField;
            }
            set
            {
                this.viewOnWebQueueIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ParentDocumentIndicator
        {
            get
            {
                return this.parentDocumentIndicatorField;
            }
            set
            {
                this.parentDocumentIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte PreviewIndicator
        {
            get
            {
                return this.previewIndicatorField;
            }
            set
            {
                this.previewIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte FileExtension
        {
            get
            {
                return this.fileExtensionField;
            }
            set
            {
                this.fileExtensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityResponseResults
    {

        private LocatePartActivityResponseResultsResult resultField;

        /// <remarks/>
        public LocatePartActivityResponseResultsResult Result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityResponseResultsResult
    {

        private string valueField;

        private string descField;

        private uint codeField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Desc
        {
            get
            {
                return this.descField;
            }
            set
            {
                this.descField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }


}
