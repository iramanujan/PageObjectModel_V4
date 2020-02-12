﻿using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using Selenium.Framework.Development.Kit.Configuration;
using Selenium.Framework.Development.Kit.Helper.Log;
using Selenium.Framework.Development.Kit.Helper.Utils;
using Selenium.Framework.Development.Kit.Model.WebDriver.Interface;
using Selenium.Framework.Development.Kit.Model.WebDriver.InternetExplorer.IEConfiguration;
using System;
using System.IO;

namespace Selenium.Framework.Development.Kit.Model.WebDriver.InternetExplorer.Local
{
    public class LocalInternetExplorerDriver : IWebDriverFactory
    {
        private static readonly AppConfigMember appConfigMember = AppConfigReader.GetToolConfig();
        private InternetExplorerDriverService internetExplorerDriverService = null;
        private InternetExplorerOptions internetExplorerOptions = null;
        private IWebDriver webDriver = null;

        public void BeforeWebDriverSetupSetps()
        {
            this.internetExplorerDriverService = InternetExplorerConfiguration.GetInternetExplorerDriverService();
            this.internetExplorerOptions = InternetExplorerConfiguration.GetInternetExplorerOptions();
        }

        public IWebDriver InitializeWebDriver()
        {
            try
            {
                BeforeWebDriverSetupSetps();
                this.webDriver = new InternetExplorerDriver(this.internetExplorerDriverService, this.internetExplorerOptions,TimeSpan.FromSeconds(appConfigMember.CommandTimeout));
                AfterWebDriverSetupSetps();
            }
            catch (Exception ObjException)
            {
                Logger.Error("An Exception Occurred While Creating ChromeDriver Object." + ObjException.Message);
            }
            return this.webDriver;
        }

        public void AfterWebDriverSetupSetps()
        {
            if (Directory.Exists(appConfigMember.RootDownloadLocation))
            {
                StepsExecutor.ExecuteSafely(() => Directory.Delete(appConfigMember.RootDownloadLocation, true));
                StepsExecutor.ExecuteSafely(() => Directory.CreateDirectory(appConfigMember.RootDownloadLocation));
            }
            if (Directory.Exists(appConfigMember.RootUploadLocation))
            {
                StepsExecutor.ExecuteSafely(() => Directory.Delete(appConfigMember.RootUploadLocation, true));
                StepsExecutor.ExecuteSafely(() => Directory.CreateDirectory(appConfigMember.RootUploadLocation));
            }
        }
    }
}
