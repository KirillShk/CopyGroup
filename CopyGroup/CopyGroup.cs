using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyGroup
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CopyGroup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            Reference reference = uidoc.Selection.PickObject(ObjectType.Element, "Выберите группу");
            Element element = doc.GetElement(reference);
            Group group = element as Group;

            XYZ point = uidoc.Selection.PickPoint("Выберите точку");
            Transaction ts = new Transaction(doc);
            ts.Start("Копирование группы");
            doc.Create.PlaceGroup(point, group.GroupType);
            ts.Commit();

            return Result.Succeeded;
        }
    }
}
