﻿using Selenium.Framework.Development.Kit.Helper.Attributes;
using System.Runtime.Serialization;


namespace Selenium.Framework.Development.Kit.Configuration
{
    [DataContract(Namespace = "")]
    public class AppConfigMember
    {
        #region Enum Objects
        public enum LocalizationType
        {
            [StringValue("en")]
            en,
            [StringValue("de")]
            de,
            [StringValue("es")]
            es,
            [StringValue("fr")]
            fr,
            [StringValue("it")]
            it
        }

        public enum BrowserLocalization
        {
            [StringValue("en")]
            en,
            [StringValue("de")]
            de,
            [StringValue("es")]
            es,
            [StringValue("fr")]
            fr,
            [StringValue("it")]
            it
        }

        public enum BrowserType
        {
            [StringValue("Unknown")]
            Unknown = 0,

            [StringValue("Chrome")]
            Chrome = 1,

            [StringValue("InternetExplorer")]
            IE = 2,

            [StringValue("Firefox")]
            Firefox = 3,

            [StringValue("Safari")]
            Safari = 4,

            [StringValue("Edge")]
            Edge = 5
        }

        public enum WebDriverExecutionType
        {
            [StringValue("Local")]
            Local = 1,

            [StringValue("Grid")]
            Grid = 2
        }
        #endregion 

        #region Wait Settings In Second
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public LocalizationType Localization { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 2)]
        public int ObjectTimeout { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 3)]
        public int PollingInterval { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 4)]
        public int PageTimeout { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 5)]
        public int CommandTimeout { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 6)]
        public int WaitForFreeSlotOnHubTimeout { get; private set; }
        #endregion

        #region Browser Environment Setup (Chrome, IE and Firefox)(Grid or Local)
        [DataMember(EmitDefaultValue = false, Order = 7)]
        public string Url { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 8)]
        public string UserName { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 9)]
        public string Password { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 10)]
        public string Environment { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 11)]
        public bool NoCache { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 12)]
        public string ProfileName { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 13)]
        public BrowserType Browser { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 14)]
        public WebDriverExecutionType ExecutionType { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 15)]
        public string GridHost { get; private set; }

        public string GridUrl => GridHost + "wd/hub";
        #endregion

        #region Download Or Upload Location
        [DataMember(EmitDefaultValue = false, Order = 16)]
        public string RootDownloadLocation { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 17)]
        public string RootUploadLocation { get; private set; }
        #endregion

        #region Automation Report Or Log Location
        [DataMember(EmitDefaultValue = false, Order = 18)]
        public string AutomationReportPath { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 19)]
        public string AutomationLogPath { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 20)]
        public string AutomationReportFolderName { get; private set; }

        [DataMember(EmitDefaultValue = false, Order = 21)]
        public string AutomationLogFolderName { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 22)]
        public string IELogFileLocation { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 23)]
        public string FirefoxLogFileLocation { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 24)]
        public string ChromeLogFileLocation { get; private set; }
        [DataMember(EmitDefaultValue = false, Order = 25)]
        public string PageLoadStrategy { get; private set; }
        #endregion

    }
}
