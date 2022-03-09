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
    public partial class GetDocumentFromActivityResponse
    {

        private GetDocumentFromActivityResponseMessage messageField;

        private GetDocumentFromActivityResponseResults resultsField;

        /// <remarks/>
        public GetDocumentFromActivityResponseMessage Message
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
        public GetDocumentFromActivityResponseResults Results
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
    public partial class GetDocumentFromActivityResponseMessage
    {

        private GetDocumentFromActivityResponseMessageContactRef contactRefField;

        private GetDocumentFromActivityResponseMessageActivityRef activityRefField;

        private GetDocumentFromActivityResponseMessageDocumentRef documentRefField;

        private GetDocumentFromActivityResponseMessageDocumentData documentDataField;

        /// <remarks/>
        public GetDocumentFromActivityResponseMessageContactRef ContactRef
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
        public GetDocumentFromActivityResponseMessageActivityRef ActivityRef
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
        public GetDocumentFromActivityResponseMessageDocumentRef DocumentRef
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

        /// <remarks/>
        public GetDocumentFromActivityResponseMessageDocumentData DocumentData
        {
            get
            {
                return this.documentDataField;
            }
            set
            {
                this.documentDataField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GetDocumentFromActivityResponseMessageContactRef
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
    public partial class GetDocumentFromActivityResponseMessageActivityRef
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
    public partial class GetDocumentFromActivityResponseMessageDocumentRef
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
    public partial class GetDocumentFromActivityResponseMessageDocumentData
    {

        private string valueField;

        private string detailField;

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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class GetDocumentFromActivityResponseResults
    {

        private GetDocumentFromActivityResponseResultsResult resultField;

        /// <remarks/>
        public GetDocumentFromActivityResponseResultsResult Result
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
    public partial class GetDocumentFromActivityResponseResultsResult
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
