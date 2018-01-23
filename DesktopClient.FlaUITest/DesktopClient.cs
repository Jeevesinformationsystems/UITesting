using System;
using System.IO;
using FlaUI.Core;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FlaUI.Core.Conditions;
using FlaUI.Core.Tools;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;
using FlaUI.Core.Input;
using System.Diagnostics;

namespace DesktopClient.Test
{
    [TestClass]
    public class DesktopClient
    {
        static Application application;
        private static AutomationElement mainWindow;

        [ClassInitialize]
        public static void Initilize(TestContext context)
        {
            var appDirectory = @"C:\tfs\JeevesTFS\Jeeves\Kernel\Feature\5.1SP_List\Source\app\Jeeves.WPF.Client\bin\Release\";
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(appDirectory, "Jeeves.WPF.Client.exe");
            startInfo.Arguments = @"/Usa /pJeeves@123  /sjsa";
            application = Application.Launch(startInfo);
        }


        [TestMethod]
        public void FlaUILogin()
        {
            using (var automation = new UIA3Automation())
            {
                mainWindow = GetMainWindow(automation);
                Login();
                OpenProgram("order1");
            }
        }

        private void OpenProgram(string programName)
        {
            var navigationBarProgramsButton = mainWindow.FindFirstDescendant
                (cf => cf.ByAutomationId("NavigationBarPrograms"));
            Assert.IsNotNull(navigationBarProgramsButton);
            navigationBarProgramsButton.Click();

            var openProgramTextBox = mainWindow.FindFirstDescendant
                (cf => cf.ByAutomationId("OpenProgramTextBox"))
                .AsTextBox();
            Assert.IsNotNull(openProgramTextBox);
            openProgramTextBox.Text = programName;
            Keyboard.TypeVirtualKeyCode((ushort)VirtualKeyShort.ENTER);
        }

        private static void Login()
        {
            var loginWindow = GetLoginWindow(out bool loggedIn);
            if (loggedIn)
                return;
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

            Retry.While(LoggedIn, TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(10));
        }

        private static AutomationElement GetLoginWindow(out bool loggedIn)
        {
            loggedIn = false;
            AutomationElement loginWindow = null;

            Retry.While(() =>
            {
                loginWindow = mainWindow
                   .FindFirstDescendant(cf =>
                   cf.ByAutomationId("JeevesManageLoginWindow"));
                Trace.WriteLine($"LoginWindow   {loginWindow == null}");

                return loginWindow != null && !LoggedIn();

            }, TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(10));

            loggedIn = LoggedIn();
            return loginWindow;
        }

        private static bool LoggedIn()
        {
            return mainWindow
                .FindFirstDescendant(cf => cf.ByAutomationId("ConnectBtn"))
                .AsButton()
                .HelpText == "Disconnect";
        }

        private static AutomationElement GetMainWindow(UIA3Automation automation)
        {
            mainWindow = application.GetMainWindow(automation);
            Assert.IsNotNull(mainWindow);
            return mainWindow;
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            application?.Close();
        }
    }
}
