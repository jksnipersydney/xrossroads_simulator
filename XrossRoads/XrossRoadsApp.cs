using System;

using XrossRoads.Enums;
using XrossRoads.Interfaces;
using XrossRoads.Models;

namespace XrossRoads
{
    public class XrossRoadsApp
    {
        public void Simulate(int levels)
        {
            Console.WriteLine("********** SIM Started *************");
            Console.WriteLine("Generating pathways");

            var map = GenerateRoadmap(levels);

            Console.WriteLine("Pathways Generated");

            var expectedEmptyPoint = FindUnlikeliestPath(map);
            var totalBalls = TotalNumberOfBalls(levels);

            Console.WriteLine($"Prerequisite => Expected: {expectedEmptyPoint.ToString()} | Total Balls: {totalBalls.ToString()}");

            for (int i = 0; i < totalBalls; i++)
            {
                Console.WriteLine($"Injecting ball # {i+1}");
                map.InjectBall();
            }

            var emptyEndpointId = FindEmptyEndpoint(map);

            Console.WriteLine($"Result => EmptyEndpointId: {emptyEndpointId.ToString()}");

            Console.WriteLine($"Matches Expectations => {expectedEmptyPoint == emptyEndpointId}");

            Console.WriteLine("********** SIM Finished *************");
        }

        private IKrossPoint GenerateRoadmap(int levels)
        {
            return new KrossPoint(0, levels);
        }

        private Guid FindUnlikeliestPath(IKrossPoint point)
        {
            if (point.IsEndPoint)
                return point.Id;

            return FindUnlikeliestPath(point.OpenPath == Direction.Left ? point.RightPath : point.LeftPath);
        }

        private int TotalNumberOfBalls(int levels)
        {
            return (int)Math.Pow(2, levels) - 1;
        }

        private Guid FindEmptyEndpoint(IKrossPoint point)
        {
            if (point.IsEndPoint)
                return point.HasBall ? Guid.Empty : point.Id;

            var leftPathId = FindEmptyEndpoint(point.LeftPath);
            var rightPathId = FindEmptyEndpoint(point.RightPath);

            if (leftPathId != Guid.Empty) return leftPathId;
            if (rightPathId != Guid.Empty) return rightPathId;

            return Guid.Empty;
        }
    }
}
