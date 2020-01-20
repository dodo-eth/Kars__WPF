using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kars
{
    class Employee
    {
        public string Name { get; private set; }
        public string X { get; private set; }   
        public string Y { get; private set; }
        public string X_DZ { get; private set; }
        public string Select { get; private set; }

        public Employee(string name, string x, string y, string x_dz,string select)
        {
            Name = name;
            X = x;
            Y = y;
            X_DZ = x_dz;
            Select = select;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
