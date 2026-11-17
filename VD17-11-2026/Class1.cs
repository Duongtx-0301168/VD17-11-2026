
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using System.Windows.Forms;
namespace VD17_11_2026
{
    
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Autodesk.Revit.UI.TaskDialog.Show("Revit Add-in", "Hello World");
            return Result.Succeeded;
        }
    }
}