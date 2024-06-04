using Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic
{
    internal class LogicApi : LogicAbstractApi
    {
        private readonly TimerApi timer;
        private readonly DataAbstractApi dataLayer;
        private readonly object collisionLock = new object();

        public override int Width { get; }
        public override int Height { get; }
        internal override List<IBall> balls { get; }

        public LogicApi(int width, int height, TimerApi timer)
        {
            dataLayer = DataAbstractApi.CreateApi(width, height);
            Width = width;
            Height = height;
            this.timer = timer;
            balls = new List<IBall>();
            SetInterval(30);
            this.timer.Tick += async (sender, args) => await UpdateBallsAsync();
        }

        public override void CreateBallsList(int count)
        {
            if (count > 0)
            {
                for (uint i = 0; i < count; i++)
                {
                    IBall ball = dataLayer.CreateBall();
                    balls.Add(ball);
                }
            }
            else if (count < 0)
            {
                for (int i = count; i < 0; i++)
                {
                    if (balls.Count > 0)
                    {
                        balls.RemoveAt(balls.Count - 1);
                    }
                }
            }
        }

        public override event EventHandler Update { add => timer.Tick += value; remove => timer.Tick -= value; }

        public override double GetX(int i) => balls[i].X;
        public override int GetCount => balls.Count;
        public override double GetY(int i) => balls[i].Y;
        public override int GetSize(int i) => balls[i].Size;

        public override async Task UpdateBallsAsync()
        {
            var tasks = new List<Task>();

            foreach (var ball in balls)
            {
                tasks.Add(Task.Run(() => ball.UpdatePosition(Width, Height)));
            }

            await Task.WhenAll(tasks);

            DetectAndResolveCollisions();
        }

        private void DetectAndResolveCollisions()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    lock (collisionLock)
                    {
                        balls[i].ResolveCollision(balls[j]);
                    }
                }
            }
        }

        public override void Start() => timer.Start();
        public override void Stop() => timer.Stop();
        public override void SetInterval(int ms) => timer.Interval = TimeSpan.FromMilliseconds(ms);
    }
}
