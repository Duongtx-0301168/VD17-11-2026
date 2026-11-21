using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Text;
using System.Windows.Forms;

namespace VD17_11_2026
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Class2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);

            IList<Element> wallList = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();

            StringBuilder sb = new StringBuilder();
            foreach (Element wall in wallList)
            {
                sb.AppendLine(wall.Name + "::" + wall.Id);
                foreach (Parameter para in wall.Parameters)
                {
                    sb.Append(getParameterInfomation(para, doc, ":"));
                    sb.AppendLine();
                }
                sb.AppendLine();
            }
            MessageBox.Show(sb.ToString());
            return Result.Succeeded;
        }

        private string getParameterInfomation(Parameter para, Document doc, string seperator)
        {
            string defName = para.Definition.Name;
            switch (para.StorageType)
            {
                case StorageType.Double:
                    return defName + seperator + para.AsValueString();
                case StorageType.ElementId:
                    ElementId id = para.AsElementId();
                    if (id.Value >= 0)
                        return defName + seperator + doc.GetElement(id)?.Name;
                    else
                        return defName + seperator + id.Value.ToString();
                case StorageType.Integer:
                    return defName + seperator + para.AsValueString();
                case StorageType.String:
                    return defName + seperator + para.AsString();
                default:
                    return "unknown parameter";
            }
        }
    }
}
