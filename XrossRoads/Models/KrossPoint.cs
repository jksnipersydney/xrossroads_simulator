using System;

using XrossRoads.Enums;
using XrossRoads.Interfaces;

namespace XrossRoads.Models
{
    public class KrossPoint : IKrossPoint
    {
        public Guid Id { get; set; }

        public int CurrentLevel { get; set; }
        public int TotalLevels { get; set; }

        public IKrossPoint LeftPath { get; set; }
        public IKrossPoint RightPath { get; set; }
        public Direction OpenPath { get; set; }

        public bool IsEndPoint { get { return CurrentLevel == TotalLevels; } }
        public bool HasBall { get; set; }

        public KrossPoint(int currentLevel, int totalLevels)
        {
            this.Id = Guid.NewGuid();
            this.CurrentLevel = currentLevel;
            this.TotalLevels = totalLevels;

            GeneratePaths();
        }

        public void InjectBall()
        {
            if (IsEndPoint)
            {
                HasBall = true;
                return;
            }

            if (OpenPath == Direction.Left)
            {
                OpenPath = Direction.Right;
                LeftPath.InjectBall();
            }
            else
            {
                OpenPath = Direction.Left;
                RightPath.InjectBall();
            }
        }

        private void GeneratePaths()
        {
            if (TotalLevels <= CurrentLevel) return;

            OpenPath = GetRandomDirection();

            LeftPath = new KrossPoint(CurrentLevel + 1, TotalLevels);
            RightPath = new KrossPoint(CurrentLevel + 1, TotalLevels);
        }

        private Direction GetRandomDirection()
        {
            return (Direction)new Random().Next(0, 1);
        }
    }
}
