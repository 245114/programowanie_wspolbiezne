using System;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract IBall CreateBall();
        public static DataAbstractApi CreateApi(int width, int height)
        {
            return new DataApi(width, height);
        }
    }

    internal class DataApi : DataAbstractApi
    {
        private readonly Random random = new Random();
        private int Width;
        private int Height;
        private readonly double speedFactor = 4; // Stała prędkość dla wszystkich kul

        public DataApi(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override IBall CreateBall()
        {
            int radius = 30;
            double x = random.Next(radius + 20, Width - radius - 20);
            double y = random.Next(radius + 20, Height - radius - 20);
            double velocityX = 0;
            double velocityY = 0;
            double mass = random.Next(1, 5); // Przypisanie losowej masy kuli
            // Losowanie prędkości początkowej
            while (velocityX == 0)
            {
               velocityX = random.Next(-5, 5) + random.NextDouble();
            }
            while (velocityY == 0)
            {
                velocityY = random.Next(-5, 5) + random.NextDouble();
            }

            // Używanie stałej prędkości
            velocityX *= speedFactor;
            velocityY *= speedFactor;

            return new Ball(radius, x, y, velocityX, velocityY);
        }
    }
}
