using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Text;
using System.Windows.Forms;

namespace VD17_11_2026
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Class3 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);

            IList<Element> columnList = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            StringBuilder output = new StringBuilder("Tất cả các Walls trong BIM model :" + columnList.Count() + "\r\n");

            foreach (Element e1 in columnList)
            {
                string elemName = e1.Id.ToString() + "\n";
                ElementType? type = doc.GetElement(e1.GetTypeId()) as ElementType;
                Parameter? h1 = type?.LookupParameter("Width");
                if (h1 == null)
                {
                    continue;
                }
                double hh1 = ChangeUnitFeetToMillimeter(h1.AsDouble());
                elemName = elemName + "Width = " + hh1.ToString() + "\n";
                output.Append(elemName);
                output.AppendLine();
            }
            MessageBox.Show(output.ToString());
            return Result.Succeeded;
        }

        static double ChangeUnitFeetToMillimeter(double Value)
        {
            return Math.Round(UnitUtils.ConvertFromInternalUnits(Value, UnitTypeId.Millimeters), 2);
        }
    }
}
