using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {

        delegate int PlusOrMinus(int a, int b);

        // Методы, реализующие делегат (методы "типа" делегата)       
        static int Plus(int a, int b) { return a + b; }
        static int Minus(int a, int b) { return a - b; }

        // Использование обощенного делегата Func<>      
        static void PlusOrMinusMethodFunc(string str, int i1, int i2, Func<int, int, int> PlusOrMinusParam)
        {
            int Result = PlusOrMinusParam(i1, i2);
            Console.WriteLine(str + Result.ToString());
        }

        // Использование делегата
        static void PlusOrMinusMethod(string str, int i1, int i2, PlusOrMinus PlusOrMinusParam)
            {
                int Result = PlusOrMinusParam(i1, i2);
                Console.WriteLine(str + Result.ToString());
            }

        // Проверка, что у свойства есть атрибут заданного типа   
        public static bool GetPropertyAttribute(PropertyInfo checkType, Type attributeType, out object attribute)
        {
            bool Result = false;
            attribute = null;

            //Поиск атрибутов с заданным типом         
            var isAttribute = checkType.GetCustomAttributes(attributeType, false);
            if (isAttribute.Length > 0) 
            {
                Result = true;
                attribute = isAttribute[0];
            }
            return Result;
        }



        static void Main(string[] args)
        {
            // ДЕЛЕГАТЫ
            int i1 = 1;
            int i2 = 2;

            PlusOrMinusMethod("Плюс: ", i1, i2, Plus);
            PlusOrMinusMethod("Минус: ", i1, i2, Minus);

            //Создание экземпляра делегата на основе метода           
            PlusOrMinus pm1 = new PlusOrMinus(Plus);
            PlusOrMinusMethod("Создание экземпляра делегата на основе метода: ", i1, i2, pm1);
            
            //Создание анонимного метода         
            PlusOrMinus pm2 = delegate(int param1, int param2){ return param1 + param2; };

            PlusOrMinusMethod("Создание экземпляра делегата на основе анонимного метода: ", i1, i2, pm2);  
            PlusOrMinusMethod("Создание экземпляра делегата на основе лямбда-выражения: ", i1, i2, (x, y) => x - y);

            //Func<>
            Console.WriteLine("\nИспользование обощенного делегата Func<>");  
            PlusOrMinusMethodFunc("Создание экземпляра делегата на основе метода: ", i1, i2, Plus);
            PlusOrMinusMethodFunc("Создание экземпляра делегата на основе лямбдавыражения 3: ", i1, i2, (x, y) => x + y);


            // РЕФЛЕКСИЯ

            Type t = typeof(Class1);

            Console.WriteLine("\nКонструкторы:");
            foreach (var x in t.GetConstructors()) { Console.WriteLine(x); }
            Console.WriteLine("\nМетоды:");
            foreach (var x in t.GetMethods()) { Console.WriteLine(x); }
            Console.WriteLine("\nСвойства:");
            foreach (var x in t.GetProperties()) { Console.WriteLine(x); }
            Console.WriteLine("\nСвойства, помеченные атрибутом:");
            foreach (var x in t.GetProperties())
            { object attrObj;
                if (GetPropertyAttribute(x, typeof(NewAttribute), out attrObj))
                { NewAttribute attr = attrObj as NewAttribute;
                    Console.WriteLine(x.Name + " - " + attr.Description);
                }
            }

            //Создание объекта через рефлексию
            Console.WriteLine("\nСвойства, помеченные атрибутом:");
            Class1 fi = (Class1)t.InvokeMember(null, BindingFlags.CreateInstance, null, null, new object[] { });
            //Параметры вызова метода       
            object[] parameters = new object[] { 1, 2 };
            //Вызов метода        
            object Result = t.InvokeMember("Plus", BindingFlags.InvokeMethod, null, fi, parameters);
            Console.WriteLine("Plus(1,2)={0}", Result); 

            Console.ReadLine();
        }
    }
}
