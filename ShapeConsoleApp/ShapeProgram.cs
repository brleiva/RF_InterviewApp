using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
                Console.WriteLine("\nType a new command...\n\n");
                Console.Write("root@rulefinancial # ");
            } while (ReadFromConsole());

            CleanOutput();

        }

        /// <summary>
        /// Read From Console
        /// </summary>
        /// <returns></returns>
        public bool ReadFromConsole(string args = null)
        {
            string[] line = (args == null ? Console.ReadLine() : args).Split(new char[] { ' ' });

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
                case "HELP":
                    PrintHelp();
                    break;
                case "LOAD":
                    LoadShapes();
                    break;
                case "LIST":
                    ListShapes();
                    break;
                case "CLEAR":
                    ClearShapes();
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
        /// Clear
        /// </summary>
        private void ClearShapes()
        {
            ShapeList.Clear();
        }

        /// <summary>
        /// List all shapes
        /// </summary>
        private void ListShapes()
        {
            double area = 0;
            foreach (var shape in ShapeList)
            {
                shape.PrintInfo();
                area += shape.GetArea();
            }

            Console.WriteLine("\nTotal surface area of shapes: " + area + "\n");
        }

        /// <summary>
        /// Print help
        /// </summary>
        private void PrintHelp()
        {
            //Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            Console.WriteLine("============================ H E L P Information ===============================");
            Console.WriteLine("USAGE:\n\tdraw circle x y radius\n\tdraw square x y side\n\tdraw triangle x1 y1 x2 y2 x3 y3\n\tdraw donut x y radius1 radius2\n\tdraw rectangle x y width height");
            Console.WriteLine("\n\tONLY Integer values are expected\n\t******\n");

            Console.WriteLine("\tload -> This command load by default shapes from App.config file");

            Console.WriteLine("\thelp -> Prints this information");

            Console.WriteLine("\tshow -> Shows a HTML file with the shapes");

            Console.WriteLine("\tlist -> Prints the information of all shapes and total area");

            Console.WriteLine("\texit -> Closes the program");

            Console.WriteLine("\n============================ H E L P Information ===============================");

            Console.ForegroundColor = ConsoleColor.Green;
            
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
                circle.PrintInfo();
                PrintInsideShapes();
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
                square.PrintInfo();
                PrintInsideShapes();
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
                rectangle.PrintInfo();
                PrintInsideShapes();
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
                triangle.PrintInfo();
                PrintInsideShapes();
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
                donut.PrintInfo();
                PrintInsideShapes();
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
            }

            Console.WriteLine("\n\nLoaded: " + shapes.Length + " Shapes!");
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
                            </head>" +
                            "<body style='font-family:\"Segoe UI\", sans-serif, Arial; font-size: 10pt;'>" +
                                "<img src='" + System.IO.Path.Combine(OutputPath, "shapes.png") + "' style='display: block; margin-left: auto; margin-right: auto' />" +
                                "<div style='display: block; margin-left: auto; margin-right: auto; width:1024px; color: Red; font-size: 14pt;'>" +
                                    "<table><tr><td nowrap>Total surface area of shapes:  " + area.ToString("0.00") + "</td><td width='100%' >&nbsp;</td><td nowrap style='border-left: groove 5px red; color: gray; font-size: 10pt;'>Shapes inside, click the shape!</td></tr></table>"+
                                "</div>" +
                                "<div style='display: block; margin-left: auto; margin-right: auto; width:1024px;'>" + shapes.ToString() + "</div>" +
                            @"</body>
                            <script>
                                function showInsideShapes(id, name, obj) {
                                    var str = '';
                                    for (var i = 0; i < obj.length; i++) {" +
                                        "str += '<img src=\"' + obj[i] + '.png\" />Id:' + obj[i];" +
                                   @"}
                                    var w = 400;
                                    var h = 300;
                                    var left = (screen.width / 2) - (w / 2);
                                    var top = (screen.height / 2) - (h / 2);
                                    var OpenWindow = window.open('', 'Details', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);" +
                                    "OpenWindow.document.write(\"<table style='width: 100%; font-family:Arial; font-size:8pt'><tr><td align='center'><table style='font-size:12pt; font-weight:bold'><tr><td><img src='\" + id + \".png' /></td><td>\" + name + \"&nbsp;&nbsp;Id:&nbsp;\" + id + \"</td></tr></table></td></tr><tr><td align='center' style='font-size: 12pt; font-weight: bold'><hr/>Shapes Inside</td></tr><tr><td style='float: left; width: 390px;'>\" + str + \"</td></tr></table>\");" +
                                @"}
                            </script>
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

        /// <summary>
        /// Print Inside Shapes
        /// </summary>
        private void PrintInsideShapes()
        {
            CheckInsideShapes();
            var id = ShapeList.Last().Id;
            var list = (from shape in ShapeList where shape.InsideShapes.Contains(id) select "Shape -> " + shape.Name + " (Id:" + shape.Id + ")").ToList();
            if (list.Count <= 0) return;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("NOTE: This shape is inside of:");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.ForegroundColor = ConsoleColor.Green;
        }

        /// <summary>
        /// Check Inside Shapes
        /// </summary>
        private void CheckInsideShapes()
        {
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["CHECK_INSIDE_SHAPES"]))
                return;

            var aux = ShapeList.Last();
            for (int i = 0; i < ShapeList.ToArray().Length - 1; i++)
            {
                using (var bmp = new Bitmap(1024, 768))
                {
                    using (var gr = Graphics.FromImage(bmp))
                    {
                        gr.FillRectangle(Brushes.Orange, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));
                        aux.Draw(gr);
                        ShapeList[i].Draw(gr);

                        if (!ShapeList[i].Name.Equals("Donut") && (CountColors(bmp) == 2)) //That means shape[i] cointains aux shape
                        {
                            ShapeList[i].InsideShapes.Add(aux.Id);
                        }
                        else if (ShapeList[i].Name.Equals("Donut") && (CountColors(bmp) == 2)) //That means shape[i] cointains aux shape
                        {
                            gr.FillRectangle(Brushes.Orange, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));
                            aux.Draw(gr);
                            var auxColors = CountByColor(aux.SolidBrushColor.Color, bmp);
                            ((Donut)ShapeList[i]).Draw2(gr);
                            if (CountColors(bmp) > 2)
                            {
                                if (auxColors == CountByColor(aux.SolidBrushColor.Color, bmp))
                                    ShapeList[i].InsideShapes.Add(aux.Id);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Count By Color
        /// </summary>
        /// <param name="c"></param>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public int CountByColor(Color c, Bitmap bitmap)
        {
            int r = 0;
            for (var y = 0; y < bitmap.Size.Height; ++y)
                for (var x = 0; x < bitmap.Size.Width; ++x)
                    if (c == bitmap.GetPixel(x, y))
                        r++;
            return r;
        }

        /// <summary>
        /// Count Colors
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private int CountColors(Bitmap bitmap)
        {
            var colors = new HashSet<Color>();

            for (var y = 0; y < bitmap.Size.Height; ++y)
            {
                for (var x = 0; x < bitmap.Size.Width; ++x)
                {
                    colors.Add(bitmap.GetPixel(x, y));
                }
            }
            return colors.Count;
        }


    }
}
