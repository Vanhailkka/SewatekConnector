using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures;
using Tekla.Common.Geometry;

namespace SewatekConnector
{
   class Connector
   {
      private Model _model = new Model();

      private Dictionary<string, string> _sewatekComponents = new Dictionary<string, string>();

      internal Connector(Button button)
      {
         button.Click += ButtonOnClick;
         SetUpSewatekComponents();
      }

      private void SetUpSewatekComponents()
      {
         _sewatekComponents.Add("K160_2", "EB_SEINALAPIVIENTI_LV_K160_2");
         _sewatekComponents.Add("K160_3", "EB_SEINALAPIVIENTI_LV_K160_3");
         _sewatekComponents.Add("K70", "EB_SEINALAPIVIENTI_MOD_K70");
         _sewatekComponents.Add("K100", "EB_SEINALAPIVIENTI_MOD_K100");
         _sewatekComponents.Add("K120", "EB_SEINALAPIVIENTI_MOD_K120");
         _sewatekComponents.Add("K160", "EB_SEINALAPIVIENTI_MOD_K160");
         _sewatekComponents.Add("2R", "EB_SEINALAPIVIENTI_SAH_2R");
         _sewatekComponents.Add("3R", "EB_SEINALAPIVIENTI_SAH_3R");
      }

      private void ButtonOnClick(object sender, EventArgs eventArgs)
      {
         var selector = new Tekla.Structures.Model.UI.ModelObjectSelector();
         var selected = selector.GetSelectedObjects();

         var workPlaneHandler = _model.GetWorkPlaneHandler();
         var current = workPlaneHandler.GetCurrentTransformationPlane();


         while (selected.MoveNext())
         {
            var beam = selected.Current as Beam;
            if (beam == null)
            {
               continue;
            }

            var direction = new Vector(beam.EndPoint - beam.StartPoint);
            var side = Vector.Cross(direction, new Vector(0, 0, 200));
            var workPlane = new TransformationPlane(beam.StartPoint, direction,
               side);
            workPlaneHandler.SetCurrentTransformationPlane(workPlane);
            _model.CommitChanges();
            var solid = beam.GetSolid();
            var getSurround = _model.GetModelObjectSelector().GetObjectsByBoundingBox(solid.MinimumPoint - new Point(20, 20, 20),
               solid.MaximumPoint + new Point(20, 20, 20));
            var sewatekList = new List<InsertData>();

            while (getSurround.MoveNext())
            {
               var sewatek = getSurround.Current as Beam;
               if (sewatek != null && sewatek.Name == "VOID")
               {
                  var property = "";
                  sewatek.GetUserProperty("NOTE", ref property);

                  if (property.IndexOf("Sewatek") != -1 || property.IndexOf("SW") != -1)
                  {
                     var sewatekDir = new Vector(sewatek.EndPoint - sewatek.StartPoint);
                     sewatekDir.Normalize();
                     var line = new LineSegment(sewatek.StartPoint - sewatekDir*100, sewatek.EndPoint + sewatekDir *100);
                     var beamSolid = beam.GetSolid();
                     var intersects = beamSolid.Intersect(line);
                     if (intersects.Count >= 2)
                     {
                        sewatekList.Add(AddInsertData((Point)intersects[0], (Point)intersects[1],property));
                     }
                  }
               }
            }

            var dir = FindDirection(sewatekList);

            foreach (var insertData in sewatekList)
            {
               if (insertData.Amount > 0)
               {
                  InsertSewatek(beam, insertData.StartPoint, insertData.Description, insertData,dir,insertData.Amount);
               }
            }

            workPlaneHandler.SetCurrentTransformationPlane(current);
            _model.CommitChanges();
         }

      }

      private bool FindDirection(List<InsertData> data )
      {
         var currentId = -1;

         for (var i=0; i < data.Count;i++)
         {

            if (currentId == -1 || data[i].StartPoint.X < data[currentId].StartPoint.X)
            {
               currentId = i;
            }
         }

         if (data[currentId].Amount >= 1)
         {
            return true;
         }
         
         return false;

      } 

      private InsertData AddInsertData(Point startPoint, Point endPoint, string noteField)
      {
         var upVector = new Vector(0, 0, 100);
         var dirVector = new Vector(endPoint - startPoint);
         dirVector.Normalize();
         var sideVector = Vector.Cross(dirVector, upVector);
         sideVector.Normalize();

         foreach (var sewatekComponent in _sewatekComponents)
         {
            if (Regex.IsMatch(noteField.ToUpper(), sewatekComponent.Key))
            {
               var data = new InsertData(startPoint, endPoint,noteField, sewatekComponent.Key, true) {ComponentName = sewatekComponent.Value};
               return data;
            }
         }

         return new InsertData(startPoint, endPoint, noteField, "", false);
      }

      private void InsertSewatek(Beam mainPart, Point startPoint, string noteField, InsertData insertData, bool directionIsPositiveX, int amount)
      {
         var solid = mainPart.GetSolid();
         var componentInput = new ComponentInput();
         if (directionIsPositiveX)
         {
            var point = startPoint;
            point.Y = solid.MinimumPoint.Y;
            componentInput.AddTwoInputPositions(startPoint, startPoint + new Point(100,0,0));
         }
         else
         {
            var point = startPoint;
            point.Y = solid.MaximumPoint.Y;
            componentInput.AddTwoInputPositions(point, point + new Point(-100, 0, 0));
         }

         var component = new Component(componentInput) { Name = insertData.ComponentName, Number = -100000 };
               
         component.Insert();
         
         component.SetUserProperty(UdafieldNames.NumberOfHorizontal, amount.ToString());
         component.SetUserProperty(UdafieldNames.NumberOfVertical, "1");
         component.SetUserProperty(UdafieldNames.Name, "Sewatek");
         component.SetUserProperty(UdafieldNames.Description, noteField);
         component.SetUserProperty(UdafieldNames.WidthOfPanel, insertData.PanelWidth);
         
         Phase phase;
         mainPart.GetPhase(out phase);
         component.SetPhase(phase);
         component.Modify();

         var assembly = mainPart.GetAssembly();
         assembly.Add(component);
         assembly.Modify();

      }
   }
}
