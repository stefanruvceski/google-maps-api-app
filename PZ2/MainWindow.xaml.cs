using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using PZ2.Adition;
using PZ2.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PZ2
{
    public partial class MainWindow : Window
    {
        #region Fields
        NetworkModel networkModel = new NetworkModel();
        List<Task> tasks = new List<Task>();
        #endregion

        #region Contructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Load Map
        private void gMap_Load(object sender, EventArgs e)
        {
            gMap = MapHandler.DoLoad(gMap);
            networkModel = NetworkModelHandler.DoParse(@"..\..\DB\Geographic.xml");
        }
        #endregion

        #region Button Methods
        private void buttonClearMap_Click(object sender, RoutedEventArgs e)
        {
            gMap = MapHandler.DoCler(gMap);
        }

        private void buttonLoadAll_Click(object sender, RoutedEventArgs e)
        {
            gMap = MapHandler.DoCler(gMap);
            GMapOverlay substationEntities = new GMapOverlay("substationEntities");
            GMapOverlay nodeEntities = new GMapOverlay("nodeEntities");
            GMapOverlay switchEntities = new GMapOverlay("switchEntities");
            GMapOverlay lineEntities = new GMapOverlay("lineEntities");

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < networkModel.substationEntities.Count; i++)
                {
                    try
                    {
                        GMap.NET.WindowsForms.Markers.GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(networkModel.substationEntities[i].X, networkModel.substationEntities[i].Y), GMarkerGoogleType.green_small);
                        marker.ToolTipText = "Substation\n" + networkModel.substationEntities[i].Id + "\n" + networkModel.substationEntities[i].Name;
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        substationEntities.Markers.Add(marker);

                    }
                    catch { }
                }
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                try
                {
                    for (int i = 0; i < networkModel.nodeEntities.Count; i++)
                    {
                        GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(networkModel.nodeEntities[i].X, networkModel.nodeEntities[i].Y), GMarkerGoogleType.blue_small);
                        marker.ToolTipText = "Node\n" + networkModel.nodeEntities[i].Id + "\n" + networkModel.nodeEntities[i].Name;
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        nodeEntities.Markers.Add(marker);
                    }
                }
                catch { }
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < networkModel.switchEntities.Count; i++)
                {
                    try
                    {
                        GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(networkModel.switchEntities[i].X, networkModel.switchEntities[i].Y), GMarkerGoogleType.red_small);
                        marker.ToolTipText = "Switch\n" + networkModel.switchEntities[i].Id + "\n" + networkModel.switchEntities[i].Name;
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        switchEntities.Markers.Add(marker);

                    }
                    catch { }
                }
            }));

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < networkModel.vertices.Count; i++)
                {
                    try
                    {
                        List<PointLatLng> points = new List<PointLatLng>();

                        for (int j = 0; j < networkModel.vertices[i].Vertices.Count; j++)
                        {
                            points.Add(new PointLatLng(networkModel.vertices[i].Vertices[j].X, networkModel.vertices[i].Vertices[j].Y));
                        }
                        GMapRoute polygon = new GMapRoute(points, networkModel.vertices[i].Name);
                        //polygon.Fill = new SolidBrush(System.Drawing.Color.FromArgb(50, System.Drawing.Color.Red));
                        polygon.Stroke = new System.Drawing.Pen(System.Drawing.Color.Black, 1);
                        polygon.IsHitTestVisible = true;
                        lineEntities.Routes.Add(polygon);

                    }
                    catch { }
                }
            }));

            Task.WaitAll(tasks.ToArray());

            gMap.Overlays.Add(lineEntities);
            gMap.Overlays.Add(switchEntities);
            gMap.Overlays.Add(nodeEntities);
            gMap.Overlays.Add(substationEntities);
            gMap.Zoom = gMap.Zoom + 1;
            tasks.Clear();
        }

        private void buttonLoadSubstations_Click(object sender, RoutedEventArgs e)
        {
            gMap = MapHandler.DoCler(gMap);
            GMapOverlay substationEntities = new GMapOverlay("substationEntities");

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < networkModel.substationEntities.Count; i++)
                {
                    try
                    {
                        GMap.NET.WindowsForms.Markers.GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(networkModel.substationEntities[i].X, networkModel.substationEntities[i].Y), GMarkerGoogleType.green_small);
                        marker.ToolTipText = "Substation\n" + networkModel.substationEntities[i].Id + "\n" + networkModel.substationEntities[i].Name;
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        substationEntities.Markers.Add(marker);

                    }
                    catch { }
                }
            }));

            gMap.Overlays.Add(substationEntities);
            tasks.Clear();
        }

        private void buttonLoadNodes_Click(object sender, RoutedEventArgs e)
        {
            gMap = MapHandler.DoCler(gMap);
            GMapOverlay nodeEntities = new GMapOverlay("nodeEntities");

            tasks.Add(Task.Factory.StartNew(() =>
            {
                try
                {
                    for (int i = 0; i < networkModel.nodeEntities.Count; i++)
                    {
                        GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(networkModel.nodeEntities[i].X, networkModel.nodeEntities[i].Y), GMarkerGoogleType.blue_small);
                        marker.ToolTipText = "Node\n" + networkModel.nodeEntities[i].Id + "\n" + networkModel.nodeEntities[i].Name;
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        nodeEntities.Markers.Add(marker);
                    }
                }
                catch { }
            }));

            gMap.Overlays.Add(nodeEntities);
            tasks.Clear();
        }

        private void buttonLoadSwitches_Click(object sender, RoutedEventArgs e)
        {
            gMap = MapHandler.DoCler(gMap);
            GMapOverlay switchEntities = new GMapOverlay("switchEntities");

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < networkModel.switchEntities.Count; i++)
                {
                    try
                    {
                        GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(networkModel.switchEntities[i].X, networkModel.switchEntities[i].Y), GMarkerGoogleType.red_small);
                        marker.ToolTipText = "Switch\n" + networkModel.switchEntities[i].Id + "\n" + networkModel.switchEntities[i].Name;
                        marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        switchEntities.Markers.Add(marker);

                    }
                    catch { }
                }
            }));

            gMap.Overlays.Add(switchEntities);
            tasks.Clear();
        }

        private void buttonLoadLines_Click(object sender, RoutedEventArgs e)
        {
            gMap = MapHandler.DoCler(gMap);
            GMapOverlay lineEntities = new GMapOverlay("lineEntities");

            tasks.Add(Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < networkModel.vertices.Count; i++)
                {
                    try
                    {
                        List<PointLatLng> points = new List<PointLatLng>();

                        for (int j = 0; j < networkModel.vertices[i].Vertices.Count; j++)
                        {
                            points.Add(new PointLatLng(networkModel.vertices[i].Vertices[j].X, networkModel.vertices[i].Vertices[j].Y));
                        }
                        GMapRoute polygon = new GMapRoute(points, networkModel.vertices[i].Name);
                        //polygon.Fill = new SolidBrush(System.Drawing.Color.FromArgb(50, System.Drawing.Color.Red));
                        polygon.Stroke = new System.Drawing.Pen(System.Drawing.Color.Black, 1);
                        polygon.IsHitTestVisible = true;
                        
                        lineEntities.Routes.Add(polygon);

                    }
                    catch { }
                }
            }));
            gMap.Overlays.Add(lineEntities);
            tasks.Clear();
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        #endregion

        private void gMap_OnRouteClick(GMapRoute item, System.Windows.Forms.MouseEventArgs e)
        {
            GMapOverlay switchEntities = new GMapOverlay("connect");
            foreach (NodeEntity node in networkModel.nodeEntities)
            {
                foreach (PointLatLng latlng in item.Points)
                {
                    if ((Math.Round(node.X, 6) == Math.Round(latlng.Lat, 6)) && ((Math.Round(node.Y, 6) == Math.Round(latlng.Lng, 6))))
                    {
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(latlng.Lat, latlng.Lng), GMarkerGoogleType.pink_dot);
                                //marker.ToolTipText = "Switch\n" + networkModel.switchEntities[i].Id + "\n" + networkModel.switchEntities[i].Name;
                                //marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                                switchEntities.Markers.Add(marker);

                            }
                            catch { }
                        }));
                    }
                }
            }

            foreach (SwitchEntity node in networkModel.switchEntities)
            {
                foreach (PointLatLng latlng in item.Points)
                {
                    if ((Math.Round(node.X, 6) == Math.Round(latlng.Lat, 6)) && ((Math.Round(node.Y, 6) == Math.Round(latlng.Lng, 6))))
                    {
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(latlng.Lat, latlng.Lng), GMarkerGoogleType.pink_dot);
                                //marker.ToolTipText = "Switch\n" + networkModel.switchEntities[i].Id + "\n" + networkModel.switchEntities[i].Name;
                                //marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                                switchEntities.Markers.Add(marker);

                            }
                            catch { }
                        }));
                    }
                }
            }

            foreach (SubstationEntity node in networkModel.substationEntities)
            {
                foreach (PointLatLng latlng in item.Points)
                {
                    if ((Math.Round(node.X, 6) == Math.Round(latlng.Lat, 6)) && ((Math.Round(node.Y, 6) == Math.Round(latlng.Lng, 6))))
                    {
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(latlng.Lat, latlng.Lng), GMarkerGoogleType.pink_dot);
                                //marker.ToolTipText = "Switch\n" + networkModel.switchEntities[i].Id + "\n" + networkModel.switchEntities[i].Name;
                                //marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                                switchEntities.Markers.Add(marker);

                            }
                            catch { }
                        }));
                    }
                }
            }

            gMap.Overlays.Add(switchEntities);
            tasks.Clear();
        }
    }
}
