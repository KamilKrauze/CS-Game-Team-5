using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_GridGame_Team5
{
    // Used to store all the planes that are going be used in the damage step
    class PlaneList
    {
        // Lists to be used in parallel that store the location of the plane that will be shooting and its target.
        public List<int> sourceX;
        public List<int> sourceY;
        public List<int> targetX;
        public List<int> targetY;

        public PlaneList()
        {
            this.sourceX.Clear();
            this.sourceY.Clear();
            this.targetX.Clear();
            this.targetY.Clear();
        }

        public void resetList()
        {
            this.sourceX.Clear();
            this.sourceY.Clear();
            this.targetX.Clear();
            this.targetY.Clear();
        }

        public void addPlanes(int srcX, int srcY, int targX, int targY)
        {
            this.sourceX.Add(srcX);
            this.sourceY.Add(srcY);
            this.targetX.Add(targX);
            this.targetY.Add(targY);
        }
    }
}
