using System;
using System.Drawing;

namespace ShapeConsoleApp.Models
{
    /// <summary>
    /// IShape
    /// </summary>
    interface IShape
    {
        /// <summary>
        /// Return Area value from the object
        /// </summary>
        /// <returns></returns>
        float GetArea();

        /// <summary>
        /// Draw the object
        /// </summary>
        /// <param name="gr"></param>
        void Draw(Graphics gr);

        /// <summary>
        /// Get Html
        /// </summary>
        /// <returns></returns>
        string GetHtml();

        /// <summary>
        /// Create File w image for HTML render
        /// </summary>
        void CreateFile();
    }
    /// <summary>
    /// Shape object
    /// </summary>
    abstract class Shape
    {

        public int X { get; set; }
        public int Y { get; set; }

        private readonly int _id = new Random(DateTime.Now.Ticks.GetHashCode()).Next(1000, int.MaxValue);
        public int Id
        {
            get { return _id; }
        }

        public string Name { get; set; }

        private readonly SolidBrush _color = NewRandColor();
        public SolidBrush SolidBrushColor
        {
            get { return _color; }
        }

        protected string OutputPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\html_output\\";

        /// <summary>
        /// Generates a New Random Color
        /// </summary>
        /// <returns></returns>
        private static SolidBrush NewRandColor()
        {
            var r = new Random(DateTime.Now.Ticks.GetHashCode());
            var red = r.Next(0, byte.MaxValue + 1);
            var green = r.Next(0, byte.MaxValue + 1);
            var blue = r.Next(0, byte.MaxValue + 1);
            return new SolidBrush(Color.FromArgb(red, green, blue));
        }


    }

    /// <summary>
    /// Circle
    /// </summary>
    class Circle : Shape, IShape
    {
        public int Radius { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        public Circle(int x, int y, int radius)
        {
            Name = "Circle";
            X = x;
            Y = y;
            Radius = radius;
            CreateFile();
        }

        /// <summary>
        /// Return Area value from the object
        /// </summary>
        /// <returns></returns>
        public float GetArea()
        {
            return (float)(Math.PI * Radius * Radius);
        }

        /// <summary>
        /// Draw the object
        /// </summary>
        /// <param name="gr"></param>load
        public void Draw(Graphics gr)
        {
            gr.FillEllipse(SolidBrushColor, X + 512 - (Radius / 2), (Y * -1) + 384 - (Radius / 2), Radius, Radius);
        }


        /// <summary>
        /// Get HTML for render
        /// </summary>
        /// <returns></returns>
        public string GetHtml()
        {
            return @"<div style='float: left; width: 126px; border: solid 1px orange'>
                        <table style='width:100%'>
                            <tr>
                                <td colspan='2' align='center'><img src='" + Id + ".png' /></td>" +
                            @"</tr>
                            <tr>
                                <td>Id:</td>
                                <td>" + Id + "</td>" +
                            @"</tr>
                            <tr>
                                <td>Area:</td>
                                <td>" + GetArea() + "</td>" +
                            @"</tr>
                        </table>
                    </div>";
        }

        /// <summary>
        /// Create File w image for HTML render
        /// </summary>
        public void CreateFile()
        {
            using (var bmp = new Bitmap(60, 60))
            {
                using (var gr = Graphics.FromImage(bmp))
                {
                    gr.FillRectangle(Brushes.Transparent, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));

                    gr.FillEllipse(SolidBrushColor, 10, 10, 40, 40);

                    var path = System.IO.Path.Combine(OutputPath, Id + ".png");
                    bmp.Save(path);
                }
            }
        }
    }

    class Square : Shape, IShape
    {
        public int Side { get; set; }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="side"></param>
        public Square(int x, int y, int side)
        {
            Name = "Square";
            X = x;
            Y = y;
            Side = side;
            CreateFile();
        }

        /// <summary>
        /// Return Area value from the object
        /// </summary>
        /// <returns></returns>
        public float GetArea()
        {
            return Side * Side;
        }

        /// <summary>
        /// Draw the object
        /// </summary>
        /// <param name="gr"></param>
        public void Draw(Graphics gr)
        {
            gr.FillRectangle(SolidBrushColor, new System.Drawing.Rectangle(X + 512, (Y * -1) + 384, Side, Side));
        }

        /// <summary>
        /// Get HTML for render
        /// </summary>
        /// <returns></returns>
        public string GetHtml()
        {
            return @"<div style='float: left; width: 126px; border: solid 1px orange'>
                        <table style='width:100%'>
                            <tr>
                                <td colspan='2' align='center'><img src='" + Id + ".png' /></td>" +
                            @"</tr>
                            <tr>
                                <td>Id:</td>
                                <td>" + Id + "</td>" +
                            @"</tr>
                            <tr>
                                <td>Area:</td>
                                <td>" + GetArea() + "</td>" +
                            @"</tr>
                        </table>
                    </div>";
        }

        /// <summary>
        /// Create File w image for HTML render
        /// </summary>
        public void CreateFile()
        {
            using (var bmp = new Bitmap(60, 60))
            {
                using (var gr = Graphics.FromImage(bmp))
                {
                    gr.FillRectangle(Brushes.Transparent, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));

                    gr.FillRectangle(SolidBrushColor, new System.Drawing.Rectangle(10, 10, 40, 40));

                    var path = System.IO.Path.Combine(OutputPath, Id + ".png");
                    bmp.Save(path);
                }
            }
        }
    }

    class Rectangle : Shape, IShape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rectangle(int x, int y, int width, int height)
        {
            Name = "Rectangle";
            X = x;
            Y = y;
            Width = width;
            Height = height;
            CreateFile();

        }


        /// <summary>
        /// Return Area value from the object
        /// </summary>
        /// <returns></returns>
        public float GetArea()
        {
            return Width * Height;
        }

        /// <summary>
        /// Draw the object
        /// </summary>
        /// <param name="gr"></param>
        public void Draw(Graphics gr)
        {
            gr.FillRectangle(SolidBrushColor, new System.Drawing.Rectangle(X + 512, (Y * -1) + 384, Width, Height));
        }

        /// <summary>
        /// Get HTML for render
        /// </summary>
        /// <returns></returns>
        public string GetHtml()
        {
            return @"<div style='float: left; width: 126px; border: solid 1px orange'>
                        <table style='width:100%'>
                            <tr>
                                <td colspan='2' align='center'><img src='" + Id + ".png' /></td>" +
                            @"</tr>
                            <tr>
                                <td>Id:</td>
                                <td>" + Id + "</td>" +
                            @"</tr>
                            <tr>
                                <td>Area:</td>
                                <td>" + GetArea() + "</td>" +
                            @"</tr>
                        </table>
                    </div>";
        }

        /// <summary>
        /// Create File w image for HTML render
        /// </summary>
        public void CreateFile()
        {
            using (var bmp = new Bitmap(60, 60))
            {
                using (var gr = Graphics.FromImage(bmp))
                {
                    gr.FillRectangle(Brushes.Transparent, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));

                    gr.FillRectangle(SolidBrushColor, new System.Drawing.Rectangle(10, 20, 40, 20));

                    var path = System.IO.Path.Combine(OutputPath, Id + ".png");
                    bmp.Save(path);
                }
            }
        }
    }

    class Triangle : Shape, IShape
    {
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public int X3 { get; set; }
        public int Y3 { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        public Triangle(int x, int y, int x2, int y2, int x3, int y3)
        {
            Name = "Triangle";
            X = x;
            Y = y;

            X2 = x2;
            Y2 = y2;

            X3 = x3;
            Y3 = y3;
            CreateFile();

        }

        /// <summary>
        /// Return Area value from the object
        /// </summary>
        /// <returns></returns>
        public float GetArea()
        {
            return Math.Abs((X * (Y2 - Y3) + X2 * (Y3 - Y) + X3 * (Y - Y2)) / 2);
        }

        /// <summary>
        /// Draw the object
        /// </summary>
        /// <param name="gr"></param>
        public void Draw(Graphics gr)
        {
            gr.FillPolygon(SolidBrushColor, new Point[] { new Point(X + 512, (Y * -1) + 384), new Point(X2 + 512, (Y2 * -1) + 384), new Point(X3 + 512, (Y3 * -1) + 384) }   );
        }

        /// <summary>
        /// Get HTML for render
        /// </summary>
        /// <returns></returns>
        public string GetHtml()
        {
            return @"<div style='float: left; width: 126px; border: solid 1px orange'>
                        <table style='width:100%'>
                            <tr>
                                <td colspan='2' align='center'><img src='" + Id + ".png' /></td>" +
                            @"</tr>
                            <tr>
                                <td>Id:</td>
                                <td>" + Id + "</td>" +
                            @"</tr>
                            <tr>
                                <td>Area:</td>
                                <td>" + GetArea() + "</td>" +
                            @"</tr>
                        </table>
                    </div>";
        }

        /// <summary>
        /// Create File w image for HTML render
        /// </summary>
        public void CreateFile()
        {
            using (var bmp = new Bitmap(60, 60))
            {
                using (var gr = Graphics.FromImage(bmp))
                {
                    gr.FillRectangle(Brushes.Transparent, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));

                    gr.FillPolygon(SolidBrushColor, new Point[] { new Point(10, 50), new Point(25,10), new Point(50,50) });

                    var path = System.IO.Path.Combine(OutputPath, Id + ".png");
                    bmp.Save(path);
                }
            }
        }
    }

    class Donut : Shape, IShape
    {
        public int Radius1 { get; set; }

        public int Radius2 { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius1"></param>
        /// <param name="radius2"></param>
        public Donut(int x, int y, int radius1, int radius2)
        {
            Name = "Donut";
            X = x;
            Y = y;
            Radius1 = radius1;
            Radius2 = radius2;
            CreateFile();
        }

        /// <summary>
        /// Return Area value from the object
        /// </summary>
        /// <returns></returns>
        public float GetArea()
        {
            return (float)((Math.PI * Radius2 * Radius2) - (Math.PI * Radius1 * Radius1));
        }

        /// <summary>
        /// Draw the object
        /// </summary>
        /// <param name="gr"></param>load
        public void Draw(Graphics gr)
        {
            gr.FillEllipse(SolidBrushColor, X + 512 - (Radius2 / 2), (Y * -1) + 384 - (Radius2 / 2), Radius2, Radius2);
            gr.FillEllipse(Brushes.Orange, X + 512 - (Radius1 / 2), (Y * -1) + 384 - (Radius1 / 2), Radius1, Radius1);
        }


        /// <summary>
        /// Get HTML for render
        /// </summary>
        /// <returns></returns>
        public string GetHtml()
        {
            return @"<div style='float: left; width: 126px; border: solid 1px orange'>
                        <table style='width:100%'>
                            <tr>
                                <td colspan='2' align='center'><img src='" + Id + ".png' /></td>" +
                            @"</tr>
                            <tr>
                                <td>Id:</td>
                                <td>" + Id + "</td>" +
                            @"</tr>
                            <tr>
                                <td>Area:</td>
                                <td>" + GetArea() + "</td>" +
                            @"</tr>
                        </table>
                    </div>";
        }

        /// <summary>
        /// Create File w image for HTML render
        /// </summary>
        public void CreateFile()
        {
            using (var bmp = new Bitmap(60, 60))
            {
                using (var gr = Graphics.FromImage(bmp))
                {
                    gr.FillRectangle(Brushes.Transparent, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));

                    gr.FillEllipse(SolidBrushColor, 10, 10, 40, 40);
                    gr.FillEllipse(Brushes.White, 20, 20, 20, 20);

                    var path = System.IO.Path.Combine(OutputPath, Id + ".png");
                    bmp.Save(path);
                }
            }
        }
    }

}
