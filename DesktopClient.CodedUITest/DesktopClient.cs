using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace DesktopClient.CodedUITest
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class DesktopClient
    {
        static ApplicationUnderTest application;

        [ClassInitialize]
        public static void Initilize(TestContext context)
        {
            //Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;

            //Playback.PlaybackSettings.SmartMatchOptions = SmartMatchOptions.None;
            //Playback.Initialize();
            //var appDirectory = @"C:\tfs\JeevesTFS\Jeeves\Kernel\Feature\5.1SP_List\Source\app\Jeeves.WPF.Client\bin\Release\";
            //application = ApplicationUnderTest.Launch(Path.Combine(appDirectory, "Jeeves.WPF.Client.exe"), "");
        }

        [TestMethod]
        public void CodedUITestByCoding()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.

            //var mainWindow = new WpfWindow();
            //mainWindow.SearchProperties[UITestControl.PropertyNames.Name] = "Jeeves ERP";
            //mainWindow.WindowTitles.Add("Jeeves ERP");
            //var col = mainWindow.FindMatchingControls();
            //mainWindow.Find();

            //var loginWindow = new WpfWindow(mainWindow);
            //loginWindow.SearchProperties[WpfControl.PropertyNames.AutomationId] = "JeevesManageLoginWindow";
            //var col2 = loginWindow.FindMatchingControls();
            //loginWindow.Find()
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.Disabled;

            Playback.PlaybackSettings.SmartMatchOptions = SmartMatchOptions.None;
            //Playback.Initialize();
            var appDirectory = @"C:\tfs\JeevesTFS\Jeeves\Kernel\Feature\5.1SP_List\Source\app\Jeeves.WPF.Client\bin\Release\";
            var application = ApplicationUnderTest.Launch(Path.Combine(appDirectory, "Jeeves.WPF.Client.exe"), "");


            var UIUserTextBoxEdit = new WpfEdit(application);
            var UIPasswordTextBoxEdit = new WpfEdit(application);
            var UISignatureTextBoxEdit = new WpfEdit(application);
            var UIOKButton = new WpfButton(application);
            var programbutton = new WpfText(application);
            var openprogram = new WpfEdit(application);


            UIUserTextBoxEdit.SearchProperties[WpfControl.PropertyNames.AutomationId] = "UserTextBox";
            UIUserTextBoxEdit.Text = "sa";

            UIPasswordTextBoxEdit.SearchProperties[WpfControl.PropertyNames.AutomationId] = "PasswordTextBox";
            UIPasswordTextBoxEdit.Text = "Jeeves@123";

            UISignatureTextBoxEdit.SearchProperties[WpfControl.PropertyNames.AutomationId] = "SignatureTextBox";
            UISignatureTextBoxEdit.Text = "jsa";

            UIOKButton.SearchProperties[WpfControl.PropertyNames.AutomationId] = "OKButton";
            Mouse.Click(UIOKButton);

            programbutton.SearchProperties[WpfControl.PropertyNames.AutomationId] = "NavigationBarPrograms";
            Mouse.Click(programbutton);

            openprogram.SearchProperties[WpfControl.PropertyNames.AutomationId] = "OpenProgramTextBox";
            Keyboard.SendKeys(openprogram, "order1");
            Keyboard.SendKeys(openprogram, "{Enter}");
        }
        
        [ClassCleanup]
        public static void CleanUp()
        {
            //application?.Close();
            //Playback.Cleanup();
        }
    }
}
