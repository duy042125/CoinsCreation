using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Database_Component
{
    public class Behavior
    {
        public Behavior(string theName, string theBehavior1, string theBehavior2, string theBehavior3, string theBehavior4, 
            string theStar5_reward1, string theStar5_reward2, string theStar5_reward3, string theStar10_reward1, string theStar10_reward2, string theStar10_reward3, string theStar15_reward1, string theStar15_reward2, string theStar20_reward)
        {
            name = theName;
            behavior1 = theBehavior1;
            behavior2 = theBehavior2;
            behavior3 = theBehavior3;
            behavior4 = theBehavior4;
            star5_reward1 = theStar5_reward1;
            star5_reward2 = theStar5_reward2;
            star5_reward3 = theStar5_reward3;
            star10_reward1 = theStar10_reward1;
            star10_reward2 = theStar10_reward2;
            star10_reward3 = theStar10_reward3;
            star15_reward1 = theStar15_reward1;
            star15_reward2 = theStar15_reward2;
            star20_reward = theStar20_reward;
        }

        public Behavior()
        {
            name = null;
            behavior1 = null;
            behavior2 = null;
            behavior3 = null;
            behavior4 = null;
            star5_reward1 = null;
            star5_reward2 = null;
            star5_reward3 = null;
            star10_reward1 = null;
            star10_reward2 = null;
            star10_reward3 = null;
            star15_reward1 = null;
            star15_reward2 = null;
            star20_reward = null;
        }

        public string name { get; set; }
        public string behavior1 { get; set; }
        public string behavior2 { get; set; }
        public string behavior3 { get; set; }
        public string behavior4 { get; set; }
        public string star5_reward1 { get; set; }
        public string star5_reward2 { get; set; }
        public string star5_reward3 { get; set; }
        public string star10_reward1 { get; set; }
        public string star10_reward2 { get; set; }
        public string star10_reward3 { get; set; }
        public string star15_reward1 { get; set; }
        public string star15_reward2 { get; set; }
        public string star20_reward { get; set; }
    }
}
