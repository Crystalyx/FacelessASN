using System;
using FacelessASN.Bytes;

namespace ASNInteractive
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new ByteArray("as\nn");
            Console.WriteLine(arr.AsString());
        }
    }
}