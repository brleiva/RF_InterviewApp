using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace ShapeConsoleApp.Models
{
    interface IShape
    {
        float GetArea();

        void Draw(Graphics gr);
    }
    abstract class Shape
    {

        public int X { get; set; }
        public int Y { get; set; }

        private readonly int _id = new Random().Next(1000, int.MaxValue);
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

        private static SolidBrush NewRandColor()
        {
            var r = new Random();
            var red = r.Next(0, byte.MaxValue + 1);
            var green = r.Next(0, byte.MaxValue + 1);
            var blue = r.Next(0, byte.MaxValue + 1);
            return new SolidBrush(Color.FromArgb(red, green, blue));
        }



    }

    class Circle : Shape, IShape
    {
        public int Radius { get; set; }

        public float GetArea()
        {
            return (float)(Math.PI * Radius * Radius);
        }


        public void Draw(Graphics gr)
        {
            gr.FillEllipse(SolidBrushColor, X + 512 - (Radius / 2), Y + 384 - (Radius / 2), Radius, Radius);
        }
    }

    class Square : Shape, IShape
    {
        public int Side { get; set; }

        public float GetArea()
        {
            return Side * Side;
        }

        public void Draw(Graphics gr)
        {
            throw new NotImplementedException();
        }
    }

    class Rectangle : Shape, IShape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public float GetArea()
        {
            return Width * Height;
        }

        public void Draw(Graphics gr)
        {
            throw new NotImplementedException();
        }
    }

    class Triangle : Shape, IShape
    {
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public int X3 { get; set; }
        public int Y3 { get; set; }

        public float GetArea()
        {
            return Math.Abs((X * (Y2 - Y3) + X2 * (Y3 - Y) + X3 * (Y - Y2)) / 2);
        }

        public void Draw(Graphics gr)
        {
            throw new NotImplementedException();
        }
    }

    class Donut : Shape, IShape
    {
        public int Radius1 { get; set; }

        public int Radius2 { get; set; }


        public float GetArea()
        {
            return (float)((Math.PI * Radius2 * Radius2) - (Math.PI * Radius1 * Radius1));
        }

        public void Draw(Graphics gr)
        {
            throw new NotImplementedException();
        }
    }

}
