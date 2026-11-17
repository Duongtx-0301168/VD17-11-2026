using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Windows.Forms;
using TaskDialog = Autodesk.Revit.UI.TaskDialog;

namespace VD17_11_2026
{
    
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document document = uidoc.Document;
            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();
            TaskDialog.Show("Revit", "Số lượng đối tượng được chọn: " + selectedIds.Count.ToString());

            ICollection<ElementId> selectedWallIds = new List<ElementId>();
            ICollection<ElementId> SelectedFloor = new List<ElementId>();
            ICollection<ElementId> SelectedColumn = new List<ElementId>();

            ICollection<Element> elementsColumns = new List<Element>();
            if (selectedIds.Count > 0)
            {
                FilteredElementCollector collector = new FilteredElementCollector(document, selectedIds);
                elementsColumns = collector.OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_StructuralColumns).ToElements();
            }

            foreach (ElementId id in selectedIds)
            {
                Element elements1 = document.GetElement(id);
                if (elements1 is Wall)
                {
                    selectedWallIds.Add(id);
                }
                if (elements1 is Floor)
                {
                    SelectedFloor.Add(id);
                }
            }

            foreach (Element colElement in elementsColumns)
            {
                SelectedColumn.Add(colElement.Id);
            }

            // Set the created element set as current select element set.
            uidoc.Selection.SetElementIds(selectedWallIds);

            if (0 != selectedWallIds.Count)
            {
                TaskDialog.Show("Revit", selectedWallIds.Count.ToString() + " Đối tượng Tường đã được chọn!");
            }
            else
            {
                TaskDialog.Show("Revit", "Không có đối tượng tường nào được chọn!");
            }

            uidoc.Selection.SetElementIds(SelectedColumn);
            if (0 != elementsColumns.Count)
            {
                TaskDialog.Show("Revit", elementsColumns.Count.ToString() + " Đối tượng Cột đã được chọn!");
            }
            else
            {
                TaskDialog.Show("Revit", "Không có đối tượng Cột nào được chọn!");
            }
            return Result.Succeeded;
        }
    }
}
