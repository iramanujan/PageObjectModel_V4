using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Selenium.Framework.Development.Kit.Helper.Utils;
using Selenium.Framework.Development.Kit.Model.Extensions;
using TestHarness.Steps.Context;
using TestHarness.Steps.Login;
using TestHarness.Steps.Qualifications;

namespace Test.Qualifications
{
    [TestFixture(Category = "Orange Hrm Live")]
    public class Education : BaseTest
    {
        private EducationStep educationStep = null;
        private LoginStep loginStep = null;

        [SetUp]
        public void SetUp()
        {
            ObjReport.CreateTest(TestContext.CurrentContext.Test.Name);
            myContext = TestHarnessContextHelper.CreateDefault();
            webDriver.InitializeApplication();
            this.loginStep = new LoginStep(webDriver);
            educationStep = new EducationStep(webDriver);
            loginStep.LoginOrangeHRM(appConfigMember.UserName, appConfigMember.Password);
            
        }

        [TestCase(TestName = "Validate Add New Education", Author = "Anuj Jain")]
        public void ValidateAddEducation()
        {
            educationStep.NavigateToEducation();
            for(int i=0; i<=2;i++)
                educationStep.AddEducation(RandomUtils.RandomizeAlphabeticalString(10));
        }


        [TearDown]
        public void OrangeHrmBaseTestOneTimeTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                ObjReport.Error(TestContext.CurrentContext.Test.MethodName);
            }
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Warning)
            {
                ObjReport.Warning(TestContext.CurrentContext.Test.MethodName);
            }
            webDriver.QuitWebDriver();
        }
    }
}
