using System;
using System.IO;
using System.Linq;
using System.Reflection;
using HtmlConvertor.Common.Convertors;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using var htmlRenderer = new HtmlRenderer("h1>pejman</h1>", path);
            var images = htmlRenderer.ConvertElementsToImages("//*[contains(@class,'must-be-image1')]")
                .ToList();

            Console.ReadLine();
        }
    }
}
