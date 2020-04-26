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
    public class SwitchEntity
    {
        UInt64 id;
        string name;
        string status;
        double x;
        double y;

        public SwitchEntity() { }

        public SwitchEntity(UInt64 id, string name, string status, double x, double y)
        {
            this.id = id;
            this.name = name;
            this.status = status;
            this.x = x;
            this.y = y;
        }

        public UInt64 Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public string Status { get => status; set => status = value; }
    }
}
