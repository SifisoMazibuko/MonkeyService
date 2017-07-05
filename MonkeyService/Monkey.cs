using Newtonsoft.Json;
using System;
namespace MonkeyService
{
    public class Monkey
    {
        public string Name
        {
            get;
            set;
        }
        public string Location
        {
            get;
            set;
        }
        public string Details
        {
            get;
            set;
        }
        public object Image
        {
            get;
            set;
        }
        public int Population
        {
            get;
            set;
        }
        public Monkey()
        {   
        }
    }
}
