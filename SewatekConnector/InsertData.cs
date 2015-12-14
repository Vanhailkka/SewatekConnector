using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;

namespace SewatekConnector
{
   class InsertData
   {
      internal Point StartPoint;
      internal Point EndPoint;
      internal string Note;
      internal string Description;
      internal bool IsFirst;
      internal bool Used;
      internal string ComponentName;
      internal string ModuleName;
      internal int Amount;
      internal double PanelWidth;

      internal InsertData(Point startPoint, Point endPoint, string note, string moduleName, bool isFirst)
      {
         StartPoint = startPoint;
         EndPoint = endPoint;
         Note = note;
         IsFirst = isFirst;
         Used = false;
         ComponentName = "";
         ModuleName = moduleName;
         Amount = CountAmount();
      }

      private int CountAmount()
      {
         var info = Note.Replace("Sewatek", "").Replace("SW", "");
         if (info == "")
         {
            return 0;
         }
         info = info.Substring(0, info.IndexOf(","));
         PanelWidth = (int) Math.Round(Distance.PointToPoint(StartPoint, EndPoint));
         Description = info + ", " + ModuleName + ", S" + PanelWidth;

         var data = info.Split('/');

         return data.Length;
      }
   }
}
