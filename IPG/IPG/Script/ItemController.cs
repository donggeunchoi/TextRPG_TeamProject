using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPG
{
    internal class ItemController
    {
        public int Remaining;
        public string Name;
        public int Effect;
        public string Desc;
        public int Price;
        public string ItemType;

        public bool IsSold = false;
        public bool isUse = false;
        public bool IsBuy = false;
    }
}
