using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertExcel
{
    internal class Product: IDisposable
    {
        private uint Identity = 0;
        public uint Id { get; private set; }
        public string Name { get; set; }
        public string Institute { get; set; }
        public string Activity { get; set; }
        public string DateOfActivity { get; set; }
        public Product(string Name,string Institute, string Activity,string DateOfActivity)
        {
            Id = Identity++;
            this.Name = Name;
            this.Institute = Institute;
            this.Activity = Activity;
            this.DateOfActivity = DateOfActivity;
        }
        public Product()
        {
            Name = "NULL";
            Activity = "NULL";

        }

        public void Dispose()
        {
            Console.WriteLine("Ya disposible");
        }
    }
}
