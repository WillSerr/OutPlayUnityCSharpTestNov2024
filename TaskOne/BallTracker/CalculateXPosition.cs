using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace BallTracker
{
    internal class CalculateXPosition
    {
        bool TryCalculateXPositionAtHeight(
            float h,
            Vector2 p,
            Vector2 v,
            float G,
            float w,
            ref float xPosition)
        {
            float deltaH = h - p.Y;

            //Vsqr = Usqr + 2as -> 0 - 2as = Usqr -> s = -(usqr/2a)
            float heightGainedInFlight = -1 * ((v.Y * v.Y) / (2 * G));
            
            //Needs a readover but should work as far as I can think.
            if(heightGainedInFlight < deltaH)
            {
                return false;
            }


            //Calclaute time of flight
            //Using solved quadratic of s = ut + (1/2)a(tsqr)
            float timeOfFlight;
            //timeOfFlight = -1/a * (U - sqrt((Usqr) + 2as)
            timeOfFlight = (-1/G) * (v.Y - (float)Math.Sqrt(((v.Y * v.Y) + (2 * G * deltaH))));


            //Determine latteral travel as a point on a triangle wave

            return true;
        }
    }
}
