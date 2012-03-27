// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.38967
//    <NameSpace>RemoteDemoControl</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>False</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net40</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>False</GenerateXMLAttributes><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>False</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace RemoteDemoControl
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;


    public partial class InterPlayDemo
    {

        private string titleField;

        private System.DateTime dateCreatedField;

        private bool dateCreatedFieldSpecified;

        private string descriptionField;

        private string thumbnailFileField;

        private string thumbnailUrlField;

        private string launchScriptFileField;

        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        public System.DateTime DateCreated
        {
            get
            {
                return this.dateCreatedField;
            }
            set
            {
                this.dateCreatedField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DateCreatedSpecified
        {
            get
            {
                return this.dateCreatedFieldSpecified;
            }
            set
            {
                this.dateCreatedFieldSpecified = value;
            }
        }

        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        public string ThumbnailFile
        {
            get
            {
                return this.thumbnailFileField;
            }
            set
            {
                this.thumbnailFileField = value;
            }
        }

        public string ThumbnailUrl
        {
            get
            {
                return this.thumbnailUrlField;
            }
            set
            {
                this.thumbnailUrlField = value;
            }
        }

        public string LaunchScriptFile
        {
            get
            {
                return this.launchScriptFileField;
            }
            set
            {
                this.launchScriptFileField = value;
            }
        }
    }
}
