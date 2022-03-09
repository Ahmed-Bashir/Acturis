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
            public partial class LocatePolicy
        {

            private LocatePolicyMessage messageField;

            /// <remarks/>
            public LocatePolicyMessage Message
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
        public partial class LocatePolicyMessage
        {

            private LocatePolicyMessageLocatePolicySearchOn locatePolicySearchOnField;

            private LocatePolicyMessageLocatePolicySearchOnValue locatePolicySearchOnValueField;

            private LocatePolicyMessageProductTarget productTargetField;

            private LocatePolicyMessagePolicyStatusGroup policyStatusGroupField;

            private LocatePolicyMessagePolicyStatus policyStatusField;

            private LocatePolicyMessagePolicyBandStatus policyBandStatusField;

            private LocatePolicyMessageEffectiveDateFrom effectiveDateFromField;

            private LocatePolicyMessageEffectiveDateTo effectiveDateToField;

            private LocatePolicyMessageOpenItemsOnlyIndicator openItemsOnlyIndicatorField;

            private LocatePolicyMessageIncludeRemarketablePoliciesOnlyIndicator includeRemarketablePoliciesOnlyIndicatorField;

            private LocatePolicyMessageIncludePoliciesToBeRenewedOnlyIndicator includePoliciesToBeRenewedOnlyIndicatorField;

            private LocatePolicyMessageIncludeClaimLinkablePoliciesOnlyIndicator includeClaimLinkablePoliciesOnlyIndicatorField;

            private LocatePolicyMessageIncludePolicyYearVersionsOnlyIndicator includePolicyYearVersionsOnlyIndicatorField;

            private LocatePolicyMessageIncludeAddOnPoliciesIndicator includeAddOnPoliciesIndicatorField;

            private LocatePolicyMessageIncludeNonSystemPoliciesIndicator includeNonSystemPoliciesIndicatorField;

            private LocatePolicyMessageFirstResult firstResultField;

            private LocatePolicyMessageLastResult lastResultField;

            /// <remarks/>
            public LocatePolicyMessageLocatePolicySearchOn LocatePolicySearchOn
            {
                get
                {
                    return this.locatePolicySearchOnField;
                }
                set
                {
                    this.locatePolicySearchOnField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessageLocatePolicySearchOnValue LocatePolicySearchOnValue
            {
                get
                {
                    return this.locatePolicySearchOnValueField;
                }
                set
                {
                    this.locatePolicySearchOnValueField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessageProductTarget ProductTarget
            {
                get
                {
                    return this.productTargetField;
                }
                set
                {
                    this.productTargetField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessagePolicyStatusGroup PolicyStatusGroup
            {
                get
                {
                    return this.policyStatusGroupField;
                }
                set
                {
                    this.policyStatusGroupField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessagePolicyStatus PolicyStatus
            {
                get
                {
                    return this.policyStatusField;
                }
                set
                {
                    this.policyStatusField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessagePolicyBandStatus PolicyBandStatus
            {
                get
                {
                    return this.policyBandStatusField;
                }
                set
                {
                    this.policyBandStatusField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessageEffectiveDateFrom EffectiveDateFrom
            {
                get
                {
                    return this.effectiveDateFromField;
                }
                set
                {
                    this.effectiveDateFromField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessageEffectiveDateTo EffectiveDateTo
            {
                get
                {
                    return this.effectiveDateToField;
                }
                set
                {
                    this.effectiveDateToField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessageOpenItemsOnlyIndicator OpenItemsOnlyIndicator
            {
                get
                {
                    return this.openItemsOnlyIndicatorField;
                }
                set
                {
                    this.openItemsOnlyIndicatorField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessageIncludeRemarketablePoliciesOnlyIndicator IncludeRemarketablePoliciesOnlyIndicator
            {
                get
                {
                    return this.includeRemarketablePoliciesOnlyIndicatorField;
                }
                set
                {
                    this.includeRemarketablePoliciesOnlyIndicatorField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessageIncludePoliciesToBeRenewedOnlyIndicator IncludePoliciesToBeRenewedOnlyIndicator
            {
                get
                {
                    return this.includePoliciesToBeRenewedOnlyIndicatorField;
                }
                set
                {
                    this.includePoliciesToBeRenewedOnlyIndicatorField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessageIncludeClaimLinkablePoliciesOnlyIndicator IncludeClaimLinkablePoliciesOnlyIndicator
            {
                get
                {
                    return this.includeClaimLinkablePoliciesOnlyIndicatorField;
                }
                set
                {
                    this.includeClaimLinkablePoliciesOnlyIndicatorField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessageIncludePolicyYearVersionsOnlyIndicator IncludePolicyYearVersionsOnlyIndicator
            {
                get
                {
                    return this.includePolicyYearVersionsOnlyIndicatorField;
                }
                set
                {
                    this.includePolicyYearVersionsOnlyIndicatorField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessageIncludeAddOnPoliciesIndicator IncludeAddOnPoliciesIndicator
            {
                get
                {
                    return this.includeAddOnPoliciesIndicatorField;
                }
                set
                {
                    this.includeAddOnPoliciesIndicatorField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessageIncludeNonSystemPoliciesIndicator IncludeNonSystemPoliciesIndicator
            {
                get
                {
                    return this.includeNonSystemPoliciesIndicatorField;
                }
                set
                {
                    this.includeNonSystemPoliciesIndicatorField = value;
                }
            }

            /// <remarks/>
            public LocatePolicyMessageFirstResult FirstResult
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
            public LocatePolicyMessageLastResult LastResult
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
        public partial class LocatePolicyMessageLocatePolicySearchOn
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
        public partial class LocatePolicyMessageLocatePolicySearchOnValue
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
        public partial class LocatePolicyMessageProductTarget
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
        public partial class LocatePolicyMessagePolicyStatusGroup
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
        public partial class LocatePolicyMessagePolicyStatus
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
        public partial class LocatePolicyMessagePolicyBandStatus
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
        public partial class LocatePolicyMessageEffectiveDateFrom
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
        public partial class LocatePolicyMessageEffectiveDateTo
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
        public partial class LocatePolicyMessageOpenItemsOnlyIndicator
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
        public partial class LocatePolicyMessageIncludeRemarketablePoliciesOnlyIndicator
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
        public partial class LocatePolicyMessageIncludePoliciesToBeRenewedOnlyIndicator
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
        public partial class LocatePolicyMessageIncludeClaimLinkablePoliciesOnlyIndicator
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
        public partial class LocatePolicyMessageIncludePolicyYearVersionsOnlyIndicator
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
        public partial class LocatePolicyMessageIncludeAddOnPoliciesIndicator
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
        public partial class LocatePolicyMessageIncludeNonSystemPoliciesIndicator
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
        public partial class LocatePolicyMessageFirstResult
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
        public partial class LocatePolicyMessageLastResult
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

