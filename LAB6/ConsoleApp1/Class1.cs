using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class1
    {
        int var1;
        int var2;
    
        // атрибут
        [NewAttribute("Описание для var1")]
        public int VAR1
        {
            get{ return this.var1; }

            set{ this.var1 = value; }
        }

        public int VAR2
        {
            get{ return this.var2; }

            set{ this.var2 = value; }
        }

        public Class1 ()
        {
            var1 = 0;
            var2 = 0;
        }
       
        public Class1(int a, int b)
        {
            var1 = a;
            var2 = b;
        }

        static public int Plus(int a, int b){ return a + b; }

        static public int Minus(int a, int b){ return a - b; }

        public void Print(){ Console.WriteLine(this.var1.ToString() + " и " + this.var2.ToString()); }

        
    }
}
