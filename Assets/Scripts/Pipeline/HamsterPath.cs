using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pipeline
{
    [Serializable]
    public class HamsterPath
    {
        public List<WayPoint> wayPoints = new List<WayPoint>();
        public bool isVictoriousPath;
    }

    [Serializable]
    public class WayPoint
    {
        public Vector3 position;

        public WayPoint(Vector3 pos)
        {
            position = pos;
        }
    }
}