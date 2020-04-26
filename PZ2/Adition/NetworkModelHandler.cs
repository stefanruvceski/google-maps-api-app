using PZ2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ2.Adition
{
    public static class NetworkModelHandler
    {
        public static NetworkModel DoParse(string path)
        {
            NetworkModel networkModel = XMLHandler.Load<NetworkModel>(path);
            return PositionHandler.DoParse(networkModel);
        }
    }
}
