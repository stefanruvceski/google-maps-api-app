using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PZ2.Model
{
    [Serializable]
    [XmlRoot("NetworkModel")]
    public class LineEntity
    {
        private UInt64 id;
        private string name;
        private bool isUnderground;
        private double r;
        private string conductorMaterial;
        private string lineType;
        // vidi za ova tri da li int ili double
        private int thermalConstantHeat;
        private UInt64 firstEnd;
        private UInt64 secondEnd;
        private List<Point> vertices { get; set; }

        public UInt64 Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public bool IsUnderground { get => isUnderground; set => isUnderground = value; }
        public double R { get => r; set => r = value; }
        public string ConductorMaterial { get => conductorMaterial; set => conductorMaterial = value; }
        public string LineType { get => lineType; set => lineType = value; }
        public int ThermalConstantHeat { get => thermalConstantHeat; set => thermalConstantHeat = value; }
        public UInt64 FirstEnd { get => firstEnd; set => firstEnd = value; }
        public UInt64 SecondEnd { get => secondEnd; set => secondEnd = value; }

        [XmlArray("Vertices")]
        [XmlArrayItem("Point", typeof(Point))]
        public List<Point> Vertices { get => vertices; set => vertices = value; }

        public LineEntity()
        {
           
        }

        public LineEntity(ulong id, string name, bool isUnderground, double r, string conductorMaterial, string lineType, int thermalConstantHeat, UInt64 firstEnd, UInt64 secondEnd, List<Point> vertices)
        {
            this.id = id;
            this.name = name;
            this.isUnderground = isUnderground;
            this.r = r;
            this.conductorMaterial = conductorMaterial;
            this.lineType = lineType;
            this.thermalConstantHeat = thermalConstantHeat;
            this.firstEnd = firstEnd;
            this.secondEnd = secondEnd;
            this.vertices = vertices;
        }
    }
}
