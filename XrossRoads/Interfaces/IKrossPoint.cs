using System;

using XrossRoads.Enums;

namespace XrossRoads.Interfaces
{
    public interface IKrossPoint
    {
        Guid Id { get; set; }
        bool IsEndPoint { get; }
        bool HasBall { get; set; }

        IKrossPoint LeftPath { get; set; }
        IKrossPoint RightPath { get; set; }

        Direction OpenPath { get; set; }

        void InjectBall();
    }
}
