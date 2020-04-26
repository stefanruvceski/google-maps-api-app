using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ2.Adition
{
    public static class MapHandler
    {
        public static GMap.NET.WindowsForms.GMapControl DoCler(GMapControl gMapControl)
        {
            for (int i = 0; i < gMapControl.Overlays.Count; i++)
            {
                gMapControl.Overlays[i].Clear();
            }
            gMapControl.Overlays.Clear();

            return gMapControl;
        }

        public static GMapControl DoLoad(GMapControl gMapControl)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            gMapControl.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;

            gMapControl.MinZoom = 2;
            gMapControl.MaxZoom = 20;
            gMapControl.Zoom = 12;
            gMapControl.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;

            double blX = 45.2325;
            double blY = 19.793909;
            double trX = 45.277031;
            double trY = 19.894459;
            gMapControl.Position = new PointLatLng((blX + trX) / 2, (blY + trY) / 2);

            gMapControl.ShowCenter = false;

            gMapControl.CanDragMap = true;
            gMapControl.DragButton = System.Windows.Forms.MouseButtons.Left;

            return gMapControl;
        }

    }
}
