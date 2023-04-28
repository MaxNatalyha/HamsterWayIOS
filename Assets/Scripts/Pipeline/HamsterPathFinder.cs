using System.Collections.Generic;
using System.Linq;

namespace Pipeline
{
    public class HamsterPathFinder
    {
        public HamsterPath FindHamsterPath(Connector[] houseConnectors)
        {
            var paths = CalculatePossiblePaths(houseConnectors);
            var path = ChooseHamsterPath(paths);

            return path;
        }
    
        private HamsterPath ChooseHamsterPath(List<HamsterPath> allPaths)
        {
            var victoriousPath = allPaths.Find(p => p.isVictoriousPath);

            if (victoriousPath != null) return victoriousPath;

            var path = allPaths.OrderByDescending(p => p.wayPoints.Count).First();
        
            return path;
        }

        private List<HamsterPath> CalculatePossiblePaths(Connector[] houseConnectors)
        {
            var paths = new List<HamsterPath>();

            for (int i = 0; i < houseConnectors.Length; i++)
            {
                if(!houseConnectors[i].IsConnected) continue;
                paths.Add(CalculatePath(houseConnectors[i]));
            }

            return paths;
        }

        private HamsterPath CalculatePath(Connector startConnector)
        {
            HamsterPath path = new HamsterPath();
        
            path.wayPoints.Add(new WayPoint(startConnector.Rect.position));

            var nextConnector = startConnector.NextObjectConnector.Output;

            while (true)
            {
                if(nextConnector.MidPathPoint != null) path.wayPoints.Add(new WayPoint(nextConnector.MidPathPoint.position));
                
                path.wayPoints.Add(new WayPoint(nextConnector.Rect.position));

                if (nextConnector.IsConnected && nextConnector.NextObjectConnector.Output != null)
                    nextConnector = nextConnector.NextObjectConnector.Output;
                else
                {
                    path.isVictoriousPath = nextConnector.ParentConnectableObject is Bowl;
                    break;
                }
            }
        
            return path;
        }
    }
}
