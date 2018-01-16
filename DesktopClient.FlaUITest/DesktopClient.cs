using System;
using System.IO;
using FlaUI.Core;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.Conditions;

namespace DesktopClient.Test
{
    [TestClass]
    public class DesktopClient
    {
        static Application application;

        [ClassInitialize]
        public static void Initilize(TestContext context)
        {
            var appDirectory = @"C:\tfs\jvsoutalm01\JeevesERP\JeevesPlatform\Kernel\R5\5.1SP_List\Source\app\Jeeves.WPF.Client\bin\Release\";
            application = Application.Launch(Path.Combine(appDirectory, "Jeeves.WPF.Client.exe"));
        }


        [TestMethod]
        public void Login()
        {
            using (var automation = new UIA3Automation())
            {
                var mainWindow = application.GetMainWindow(automation);
                Assert.IsNotNull(mainWindow);

                var loginWindow = mainWindow
                    .FindFirstDescendant(cf =>
                    cf.ByAutomationId("JeevesManageLoginWindow"));
                Assert.IsNotNull(loginWindow);

                loginWindow
                    .FindFirstChild(cf => cf.ByAutomationId("UserTextBox"))
                    .AsTextBox().Text = "sa";

                loginWindow
                    .FindFirstChild(cf => cf.ByAutomationId("PasswordTextBox"))
                    .AsTextBox().Text = "Jeeves@123";

                loginWindow
                    .FindFirstChild(cf => cf.ByAutomationId("SignatureTextBox"))
                    .AsTextBox().Text = "jsa";
                loginWindow
                    .FindFirstChild(cf => cf.ByAutomationId("OKButton"))
                    .AsButton().Click();
            }
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            application?.Close();
        }
    }
}
