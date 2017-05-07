using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASTBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new TCCLParser();

            var name = "good1p.txt";
            Console.WriteLine("Parsing file " + name);
            parser.Parse(name);
            Console.WriteLine("Parsing complete");

            bool fileIsValid = false;
            while (!fileIsValid)
            {
                Console.Write("Please enter the file name to parse: ");
                var fileName = Console.ReadLine();
                if (!string.IsNullOrEmpty(fileName))
                {
                    fileName = fileName.Trim();
                    Console.WriteLine(fileName.Substring(fileName.Length - 4));
                    if (!fileName.Substring(fileName.Length - 4).Equals(".txt"))
                    {
                        fileName += ".txt";
                    }
                    fileIsValid = true;
                }
                else
                {
                    Console.WriteLine("Unable to process given file name " +
                                      "\"{0}\", please try again.", fileName);
                }
                // TODO: check if file exists? etc. depending on what .Parse() handles
                
            }
           

        }
    }
}
