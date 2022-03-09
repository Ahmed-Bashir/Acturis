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
    public partial class LocatePartActivity
    {

        private LocatePartActivityMessage messageField;

        /// <remarks/>
        public LocatePartActivityMessage Message
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityMessage
    {

        private LocatePartActivityMessageContactRef contactRefField;

        private LocatePartActivityMessagePartRef partRefField;

        private LocatePartActivityMessageActivityDateFrom activityDateFromField;

        private LocatePartActivityMessageActivityDateTo activityDateToField;

        private LocatePartActivityMessageShowVersionsIndicator showVersionsIndicatorField;

        private LocatePartActivityMessageShowHiddenEntriesIndicator showHiddenEntriesIndicatorField;

        private LocatePartActivityMessageShowCommentaryEntriesOnlyIndicator showCommentaryEntriesOnlyIndicatorField;

        private LocatePartActivityMessageActivityType activityTypeField;

        private LocatePartActivityMessageFileType fileTypeField;

        private LocatePartActivityMessageActivityDirection activityDirectionField;

        private LocatePartActivityMessageIncludeActivitiesOnThisPartOnlyIndicator includeActivitiesOnThisPartOnlyIndicatorField;

        private LocatePartActivityMessageIncludeViewOnWebOnlyIndicator includeViewOnWebOnlyIndicatorField;

        private LocatePartActivityMessageDetailSearchOnType detailSearchOnTypeField;

        private LocatePartActivityMessageDetailSearchOnValue detailSearchOnValueField;

        private LocatePartActivityMessageActivitiesWithAttachmentsOnlyIndicator activitiesWithAttachmentsOnlyIndicatorField;

        private LocatePartActivityMessageActivityRef activityRefField;

        private LocatePartActivityMessageFirstResult firstResultField;

        private LocatePartActivityMessageLastResult lastResultField;

        /// <remarks/>
        public LocatePartActivityMessageContactRef ContactRef
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
        public LocatePartActivityMessagePartRef PartRef
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
        public LocatePartActivityMessageActivityDateFrom ActivityDateFrom
        {
            get
            {
                return this.activityDateFromField;
            }
            set
            {
                this.activityDateFromField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageActivityDateTo ActivityDateTo
        {
            get
            {
                return this.activityDateToField;
            }
            set
            {
                this.activityDateToField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageShowVersionsIndicator ShowVersionsIndicator
        {
            get
            {
                return this.showVersionsIndicatorField;
            }
            set
            {
                this.showVersionsIndicatorField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageShowHiddenEntriesIndicator ShowHiddenEntriesIndicator
        {
            get
            {
                return this.showHiddenEntriesIndicatorField;
            }
            set
            {
                this.showHiddenEntriesIndicatorField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageShowCommentaryEntriesOnlyIndicator ShowCommentaryEntriesOnlyIndicator
        {
            get
            {
                return this.showCommentaryEntriesOnlyIndicatorField;
            }
            set
            {
                this.showCommentaryEntriesOnlyIndicatorField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageActivityType ActivityType
        {
            get
            {
                return this.activityTypeField;
            }
            set
            {
                this.activityTypeField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageFileType FileType
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
        public LocatePartActivityMessageActivityDirection ActivityDirection
        {
            get
            {
                return this.activityDirectionField;
            }
            set
            {
                this.activityDirectionField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageIncludeActivitiesOnThisPartOnlyIndicator IncludeActivitiesOnThisPartOnlyIndicator
        {
            get
            {
                return this.includeActivitiesOnThisPartOnlyIndicatorField;
            }
            set
            {
                this.includeActivitiesOnThisPartOnlyIndicatorField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageIncludeViewOnWebOnlyIndicator IncludeViewOnWebOnlyIndicator
        {
            get
            {
                return this.includeViewOnWebOnlyIndicatorField;
            }
            set
            {
                this.includeViewOnWebOnlyIndicatorField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageDetailSearchOnType DetailSearchOnType
        {
            get
            {
                return this.detailSearchOnTypeField;
            }
            set
            {
                this.detailSearchOnTypeField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageDetailSearchOnValue DetailSearchOnValue
        {
            get
            {
                return this.detailSearchOnValueField;
            }
            set
            {
                this.detailSearchOnValueField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageActivitiesWithAttachmentsOnlyIndicator ActivitiesWithAttachmentsOnlyIndicator
        {
            get
            {
                return this.activitiesWithAttachmentsOnlyIndicatorField;
            }
            set
            {
                this.activitiesWithAttachmentsOnlyIndicatorField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageActivityRef ActivityRef
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
        public LocatePartActivityMessageFirstResult FirstResult
        {
            get
            {
                return this.firstResultField;
            }
            set
            {
                this.firstResultField = value;
            }
        }

        /// <remarks/>
        public LocatePartActivityMessageLastResult LastResult
        {
            get
            {
                return this.lastResultField;
            }
            set
            {
                this.lastResultField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class LocatePartActivityMessageContactRef
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
    public partial class LocatePartActivityMessagePartRef
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
    public partial class LocatePartActivityMessageActivityDateFrom
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
    public partial class LocatePartActivityMessageActivityDateTo
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
    public partial class LocatePartActivityMessageShowVersionsIndicator
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
    public partial class LocatePartActivityMessageShowHiddenEntriesIndicator
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
    public partial class LocatePartActivityMessageShowCommentaryEntriesOnlyIndicator
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
    public partial class LocatePartActivityMessageActivityType
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
    public partial class LocatePartActivityMessageFileType
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
    public partial class LocatePartActivityMessageActivityDirection
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
    public partial class LocatePartActivityMessageIncludeActivitiesOnThisPartOnlyIndicator
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
    public partial class LocatePartActivityMessageIncludeViewOnWebOnlyIndicator
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
    public partial class LocatePartActivityMessageDetailSearchOnType
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
    public partial class LocatePartActivityMessageDetailSearchOnValue
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
    public partial class LocatePartActivityMessageActivitiesWithAttachmentsOnlyIndicator
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
    public partial class LocatePartActivityMessageActivityRef
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
    public partial class LocatePartActivityMessageFirstResult
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
    public partial class LocatePartActivityMessageLastResult
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


}
