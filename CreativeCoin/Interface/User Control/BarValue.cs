using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public class BarValue : IComparable
    {
        public BarValue(string label, string date, int coinValue)
        {
            Label = label;
            Date = date;
            CoinValue = coinValue;
        }
        public string Label { set; get; }
        public string Date { set; get; }
        public int CoinValue { set; get; }

        public int CompareTo(object obj)
        {

            return 0;
        }
    }
}
