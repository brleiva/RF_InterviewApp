using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShapeConsoleApp.Models;
using Rectangle = ShapeConsoleApp.Models.Rectangle;

namespace ShapeConsoleApp.Business
{
    class ShapeProgram
    {
        public IList<IShape> ShapeList = new List<IShape>();
        public string OutputPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\html_output\\";

        /// <summary>
        /// Start a Program Method
        /// </summary>
        public void StartProgram()
        {
            CleanOutput();
            InitOutput();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            do
            {
                Console.WriteLine("\nType a new command....\n\n");
                Console.Write(">");
            } while (ReadFromConsole());

            CleanOutput();

        }

        /// <summary>
        /// Read From Console
        /// </summary>
        /// <returns></returns>
        public bool ReadFromConsole(string args = null)
        {
            string[] line = (args == null? Console.ReadLine() : args).Split(new char[] { ' ' });

            switch (line[0].ToUpper())
            {
                case "DRAW":
                    switch (line[1].ToUpper())
                    {
                        case "CIRCLE":
                            AddCircle(line);
                            break;
                        case "SQUARE":
                            AddSquare(line);
                            break;
                        case "RECTANGLE":
                            AddRectangle(line);
                            break;
                        case "TRIANGLE":
                            AddTriangle(line);
                            break;
                        case "DONUT":
                            AddDonut(line);
                            break;
                    }
                    break;
                case "SHOW":
                    OpenHTML();
                    break;
                case "LOAD":
                    LoadShapes();
                    break;
                case "EXIT":
                    return false;
                default:
                    Console.WriteLine("Error: '" + line[0] + "' is not recognized as an internal command!");
                    break;
            }

            return true;
        }

        
        /// <summary>
        /// Add a Circle
        /// </summary>
        /// <param name="args"></param>
        private void AddCircle(string[] args)
        {
            var values = GetDrawValues(args, 5);
            if (values != null)
            {
                var circle = new Circle(values[0], values[1], values[2]);
                ShapeList.Add(circle);
                Console.WriteLine("\n=> " + circle.Name + " Id: " + circle.Id + " with centre at (x,y): (" + circle.X + "," + circle.Y + ") and radius: " + circle.Radius);
                Console.WriteLine("\nAREA: " + circle.GetArea());

            }
        }

        /// <summary>
        /// Add Square
        /// </summary>
        /// <param name="line"></param>
        private void AddSquare(string[] args)
        {
            var values = GetDrawValues(args, 5);
            if (values != null)
            {
                var square = new Square(values[0], values[1], values[2]);
                ShapeList.Add(square);
                Console.WriteLine("\n=> " + square.Name + " Id: " + square.Id + " at (x,y): (" + square.X + "," + square.Y + ") and side: " + square.Side);
                Console.WriteLine("\nAREA: " + square.GetArea());

            }
        }

        /// <summary>
        /// Add Rectangle
        /// </summary>
        /// <param name="line"></param>
        private void AddRectangle(string[] args)
        {
            var values = GetDrawValues(args, 6);
            if (values != null)
            {
                var rectangle = new Rectangle(values[0], values[1], values[2], values[3]);
                ShapeList.Add(rectangle);
                Console.WriteLine("\n=> " + rectangle.Name + " Id: " + rectangle.Id + " at (x,y): (" + rectangle.X + "," + rectangle.Y + "), width: " + rectangle.Width + " and height:" + rectangle.Height);
                Console.WriteLine("\nAREA: " + rectangle.GetArea());

            }
        }

        /// <summary>
        /// Add Triangle
        /// </summary>
        /// <param name="args"></param>
        private void AddTriangle(string[] args)
        {
            var values = GetDrawValues(args, 8);
            if (values != null)
            {
                var triangle = new Triangle(values[0], values[1], values[2], values[3], values[4], values[5]);
                ShapeList.Add(triangle);
                Console.WriteLine("\n=> " + triangle.Name + " Id: " + triangle.Id + " at (x,y) points: (" + triangle.X + "," + triangle.Y + "), (" + triangle.X2 + ", " + triangle.Y2 + "), (" + triangle.X3 + ", " + triangle.Y3 + ")");
                Console.WriteLine("\nAREA: " + triangle.GetArea());
            }
        }

        /// <summary>
        /// Add Donut
        /// </summary>
        /// <param name="args"></param>
        private void AddDonut(string[] args)
        {
            var values = GetDrawValues(args, 6);
            if (values != null)
            {
                var donut = new Donut(values[0], values[1], values[2], values[3]);
                ShapeList.Add(donut);
                Console.WriteLine("\n=> " + donut.Name + " Id: " + donut.Id + " with centre at (x,y): (" + donut.X + "," + donut.Y + "), radius1 : " + donut.Radius1 + " & radius2 : " + donut.Radius2);
                Console.WriteLine("\nAREA: " + donut.GetArea());

            }
        }

        /// <summary>
        /// Open HTML page
        /// </summary>
        private void OpenHTML()
        {
            CreateFiles();
            try
            {
                Console.Write("Opening HTML browser...");
                System.Diagnostics.Process.Start(System.IO.Path.Combine(OutputPath, "index.html"));
                Console.Write("OK\n");
            }
            catch (Exception)
            {
                Console.Write("FAIL\n");
            }
            
        }

        /// <summary>
        /// Open HTML page
        /// </summary>
        private void LoadShapes()
        {
            string[] shapes = ConfigurationManager.AppSettings["SHAPES_LIST"].Split(new char[] { ',' });
            foreach (var shape in shapes)
            {
                ReadFromConsole(shape);
                Thread.Sleep(100);
            }

            Console.WriteLine("\n\nLoaded: "+shapes.Length+" Shapes!");
        }


        /// <summary>
        /// Get Draw Values
        /// </summary>
        /// <param name="args"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private int[] GetDrawValues(string[] args, int length)
        {
            if (args.Length != length)
            {
                Console.WriteLine("Argument does not match with the expected values");
                return null;
            }
            var result = new int[args.Length - 2];
            var errors = 0;

            for (var i = 0; i < result.Length; ++i)
            {
                if (!int.TryParse(args[i + 2], out result[i]))
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


        /// <summary>
        /// Create Files for HTML page
        /// </summary>
        private void CreateFiles()
        {
            CreateShapeFile();
            CreateHtmlFile();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateHtmlFile()
        {
            var shapes = new StringBuilder();
            double area = 0;
            foreach (var shape in ShapeList)
            {
                shapes.Append(shape.GetHtml());
                area += shape.GetArea();
            }

            string html = @"<!DOCTYPE html>
                            <html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
                            <head>
                                <meta charset='utf-8' />
                                <title>Rule financial - Interview Output</title>
                            </head>"+
                            "<body style='font-family:\"Segoe UI\", sans-serif, Arial; font-size: 10pt;'>"+
                                "<img src='"+System.IO.Path.Combine(OutputPath, "shapes.png")+"' style='display: block; margin-left: auto; margin-right: auto' />"+
                                "<div style='display: block; margin-left: auto; margin-right: auto; width:1024px; color: Red; font-size: 14pt;'> Total surface area of shapes: " + area.ToString("0.00") + "</div>" +
                                "<div style='display: block; margin-left: auto; margin-right: auto; width:1024px;'>" + shapes.ToString() + "</div>" +
                            @"</body>
                            </html>";

            System.IO.File.WriteAllText(System.IO.Path.Combine(OutputPath, "index.html"), html);

        }

        /// <summary>
        /// Create Shape File
        /// </summary>
        private void CreateShapeFile()
        {

            using (var bmp = new Bitmap(1024, 768))
            {
                using (var gr = Graphics.FromImage(bmp))
                {
                    gr.FillRectangle(Brushes.Orange, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));


                    foreach (var shape in ShapeList)
                    {
                        shape.Draw(gr);
                    }

                    var path = System.IO.Path.Combine(OutputPath, "shapes.png");
                    bmp.Save(path);
                }
            }
        }


        /// <summary>
        /// Clean Output Directory
        /// </summary>
        private void CleanOutput()
        {
            if (Directory.Exists(OutputPath))
            {
                Directory.Delete(OutputPath, true);
            }
        }

        /// <summary>
        /// Init Output Directory
        /// </summary>
        private void InitOutput()
        {
            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }
        }


    }
}
