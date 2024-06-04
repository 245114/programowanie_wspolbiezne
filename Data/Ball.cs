using System;
using System.ComponentModel;

namespace Data
{
    public interface IBall
    {
        double X { get; }
        double Y { get; }
        int Size { get; }
        double Mass { get; }
        double velocityX { get; set; }
        double velocityY { get; set; }

        void UpdatePosition(int gridWidth, int gridHeight);
        void ResolveCollision(IBall other);
       
    }
    internal class Ball : IBall
    {
        private readonly int size;
        private readonly double mass;
        private double x;
        private double y;
        private double velocityX;
        private double velocityY;
        private int radius;
        private double newX;
        private double newY;

        public Ball(int size, double x, double y, double velocityX, double velocityY, double mass)
        {
            this.size = size;
            this.x = x;
            this.y = y;
            this.velocityX = velocityX;
            this.velocityX = velocityX;  
            this.velocityY = velocityY;
            this.mass = mass;
        }

        public Ball(int radius, double x, double y, double newX, double newY)
        {
            this.radius = radius;
            this.x = x;
            this.y = y;
            this.newX = newX;
            this.newY = newY;
        }

        public int Size { get => size; }
        public double X { get => x; }
        public double Y { get => y; }
        public double Mass { get => mass; }

        

        double IBall.velocityX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        double IBall.X => throw new NotImplementedException();

        double IBall.Y => throw new NotImplementedException();

        int IBall.Size => throw new NotImplementedException();

        double IBall.Mass => throw new NotImplementedException();

        double IBall.velocityY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void UpdatePosition(int gridWidth, int gridHeight)
        {
            x += velocityX;
            y += velocityY;
            // Check for wall collisions
            if (x <= 0 || x >= gridWidth - size)
            {
                velocityX = -velocityX;
                x = x <= 0 ? 0 : gridWidth - size;
            }

            if (y <= 0 || y >= gridHeight - size)
            {
                velocityY = -velocityY;
                y = y <= 0 ? 0 : gridHeight - size;
            }
        }

        public void ResolveCollision(IBall other)
        {
            double dx = other.X - this.X;
            double dy = other.Y - this.Y;
            double distance = Math.Sqrt(dx * dy + dy * dy);

            if (distance < (this.Size + other.Size) / 2)

            {
                double nx = dx / distance;
                double ny = dy / distance;

                double tx = -ny;
                double ty = nx;

                double dpTan1 = this.velocityX * tx + this.velocityY * ty;
                double dpTan2 = other.velocityX * tx + other.velocityY * ty;

                double dpNorm1 = this.velocityX * nx + this.velocityY * ny;
                double dpNorm2 = other.velocityX * nx + other.velocityY * ny;

                double m1 = (dpNorm1 * (this.Mass - other.Mass) + 2.0 * other.Mass * dpNorm2) / (this.Mass + other.Mass);
                double m2 = (dpNorm2 * (other.Mass - this.Mass) + 2.0 * this.Mass * dpNorm1) / (this.Mass + other.Mass);

                this.velocityX = tx * dpTan1 + nx * m1;
                this.velocityY = ty * dpTan1 + ny * m1;
                other.velocityX = tx * dpTan2 + nx * m2;
                other.velocityY = ty * dpTan2 + ny * m2;
            }
        }

        void IBall.UpdatePosition(int gridWidth, int gridHeight)
        {
            throw new NotImplementedException();
        }

        void IBall.ResolveCollision(IBall other)
        {
            throw new NotImplementedException();
        }
    }

}
