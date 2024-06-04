using Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract event EventHandler Update;
        public abstract int Width { get; }
        public abstract int Height { get; }
        internal abstract List<IBall> balls { get; }
        public abstract void CreateBallsList(int count);
        public abstract Task UpdateBallsAsync();
        public abstract void Start();
        public abstract void Stop();
        public abstract void SetInterval(int ms);
        public abstract double GetX(int i);
        public abstract double GetY(int i);
        public abstract int GetSize(int i);
        public abstract int GetCount { get; }

        public static LogicAbstractApi CreateApi(int width, int height, TimerApi timer = null)
        {
            return new LogicApi(width, height, timer ?? TimerApi.CreateBallTimer());
        }
    }
}
