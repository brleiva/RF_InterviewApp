using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeConsoleApp.Models;

namespace ShapeConsoleApp.Business
{
    class ShapeProgram
    {
        public IList<IShape> ShapeList = new List<IShape>();

        public void StartProgram()
        {
            
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Clear();

            do
            {   
                Console.WriteLine("\nType a new command....\n\n");
                Console.Write(">");
            } while (ReadFromConsole());

            CreateFile();
        }

        public bool ReadFromConsole()
        {
            string[] line = Console.ReadLine().Split(new char[] { ' ' });

            switch (line[0].ToUpper())
            {
                case "DRAW":
                    switch (line[1].ToUpper())
                    {
                        case "CIRCLE":
                            var values = GetDrawValues(line, 5);
                            if (values != null)
                            {
                                var circle = new Circle { Name = "Circle", X = values[0], Y = values[1], Radius = values[2] };
                                ShapeList.Add(circle);
                                Console.WriteLine("\n=> " + circle.Name + " Id: " + circle.Id + " with centre at (x,y): (" + circle.X + "," + circle.Y + ") and radius: "+circle.Radius);
                                Console.WriteLine("\nAREA: " + circle.GetArea());

                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case "EXIT":
                    return false;
                default:
                    break;
            }

            return true;
        }

        public int[] GetDrawValues(string[] args, int length)
        {
            if (args.Length != length)
            {
                Console.WriteLine("Argument does not match with the expected values");
                return null;
            }
            var result = new int[args.Length-2];
            var errors = 0;

            for (var i = 0; i < result.Length; ++i)
            {
                if (!int.TryParse(args[i+2], out result[i]))
                    errors++;
            }

            if (errors > 0)
            {
                Console.WriteLine("Argument does not match with integer values");
                return null;
            }
            return result;
        }


        private static Char GetKeyPress(String msg, Char[] validChars)
        {

            /**
             line = 

                if (Char.ToUpper(GetKeyPress(" (Y/N): ", new Char[] { 'Y', 'N' })) == 'N')
                    continueFlag = false;
             */
            ConsoleKeyInfo keyPressed;
            bool valid = false;

            Console.WriteLine();
            do
            {
                Console.Write(msg);
                keyPressed = Console.ReadKey();
                Console.WriteLine();
                if (Array.Exists(validChars, ch => ch.Equals(Char.ToUpper(keyPressed.KeyChar))))
                    valid = true;

            } while (!valid);
            return keyPressed.KeyChar;
        }

        private void CreateFile()
        {

            using (var bmp = new Bitmap(1024, 768))
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.FillRectangle(Brushes.Orange, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));


                foreach (var shape in ShapeList)
                {
                    shape.Draw(gr);
                }

                var path = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "Example.png");
                bmp.Save(path);
            }
        }

        
    }
}
