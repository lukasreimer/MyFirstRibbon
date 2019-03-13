using System;
using System.IO;
using System.Reflection;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;

namespace MyFirstRibbon
{
    /// <remarks>
    /// This application's main class. The class must be public.
    /// </remarks>
    public class CsAddPanel : IExternalApplication
    {
        // Both OnStartup and OnShutdown must be implemented as public method
        public Result OnStartup(UIControlledApplication application)
        {
            // Add a new ribbon panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("NewRibbonPanel");

            // Create a push button to trigger a command add it to the ribbon panel.
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData("cmdHelloWorld", "Hello World", thisAssemblyPath, "MyFirstRibbon.HelloWorld");

            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;

            // Optionally, other properties may be assigned to the button
            // a) tool-tip
            pushButton.ToolTip = "Say hello to the entire world.";

            // b) large bitmap
            pushButton.LargeImage = GetEmbeddedImage("MyFirstRibbon.Resources.Globe-32x32.png");

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            // nothing to clean up in this simple case
            return Result.Succeeded;
        }

        private BitmapFrame GetEmbeddedImage(string path)
        {
            // Hint from: https://thebuildingcoder.typepad.com/blog/2009/11/ribbon-embed-image.html
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream(path);
                BitmapFrame frame = BitmapFrame.Create(stream);
                return frame;
            }
            catch
            {
                return null;
            }
        }
    }

    /// <remarks>
    /// The "HelloWorld" external command. The class must be public.
    /// </remarks>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class HelloWorld : IExternalCommand
    {
        // The main Execute method (inherited from IExternalCommand) must be public
        public Result Execute(ExternalCommandData revit,
            ref string message, ElementSet elements)
        {
            TaskDialog.Show("Revit", "Hello World");
            return Result.Succeeded;
        }
    }
}
