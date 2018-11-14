using System;
using System.Text;

namespace CFunctionalProg
{
    public delegate void ActionRef<T>(ref T item);

    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendWithLine(
            this StringBuilder @this,
            string value)
                => @this.Append(value).AppendLine();

        public static StringBuilder WithLine(
            this StringBuilder @this,
            Func<StringBuilder, StringBuilder> func) 
                => func(@this).AppendLine();

        public static T Tee<T> (
            this T @this,
            Action<T> action)
        {
            action(@this);
            return @this;
        }

        public static object[] ObjectForEach(
                this object[] @this,
                ActionRef<object> func
            )
        {
            for (int i = 0; i < @this.Length; i++)
            {
                func(ref @this[i]);
            }

            return @this;
        }

        public static StringBuilder AddArray(
            this StringBuilder @this,
            object[] list
            )
        {

            list.ObjectForEach((ref object el) => {
                Console.WriteLine(el.ToString());
            });

            return @this;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            new StringBuilder()
                .WithLine((el) => el.Append("Test"))
                .WithLine((el) => el.AppendWithLine("Test2"))
                .WithLine((el) => el.AppendFormat("Imie {0} Nazwisko {1}", "Damian", "Stepniak"))
                .AddArray(new object[] { 1, 2, 3 })
                .Tee((el) => Console.WriteLine(el.ToString()));

            var array = new object[] { 1, 2, 3 };
            array.ObjectForEach((ref object el) => {
                var intV = int.Parse(el.ToString());
                intV += 1;
                el = intV;
            });

            array.ObjectForEach((ref object el) =>
            {
                Console.WriteLine(el.ToString());
            })
            .Tee((el) => Console.ReadLine());
        }
    }
}
