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
    public class NodeEntity
    {
        UInt64 id;
        string name;
        double x;
        double y;

        public NodeEntity() { }
        public NodeEntity(UInt64 id, string name, double x, double y)
        {
            this.id = id;
            this.name = name;
            this.x = x;
            this.y = y;
        }

        public UInt64 Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }

    }
}
