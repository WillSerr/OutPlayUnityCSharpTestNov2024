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
        public bool TryCalculateXPositionAtHeight(
            float h,
            Vector2 p,
            Vector2 v,
            float G,
            float w,
            ref float xPosition)
        {
            float deltaH = h - p.Y;
            float heightGainedInFlight = 0;

            if (v.Y > 0) //If traveeling upwards
            {
                //Vsqr = Usqr + 2as -> 0 - 2as = Usqr -> s = -(usqr/2a)
                heightGainedInFlight = -1 * ((v.Y * v.Y) / (2 * G));
            }
            else //Ball Dropping
            {
                heightGainedInFlight = 0;
            }
            
            //If the ball doesnt gain enough height to reach h, return false
            if(heightGainedInFlight < deltaH)
            {
                return false;
            }


            //Calclaute time of flight
            //Using solved quadratic of s = ut + (1/2)a(tsqr)
            float timeOfFlight;
            //timeOfFlight = -1/a * (U +/- sqrt((Usqr) + 2as)
            timeOfFlight = (-1/G) * (v.Y + (float)Math.Sqrt(((v.Y * v.Y) + (2 * G * deltaH))));
            if (timeOfFlight < 0) //Can't have a negative time of flight
            {
                timeOfFlight = (-1 / G) * (v.Y - (float)Math.Sqrt(((v.Y * v.Y) + (2 * G * deltaH))));
            }


            //Determine latteral travel as a point on a triangle wave

            float latteralDistance = Math.Abs((v.X * timeOfFlight) + p.X);  //Use absolute so % operator works properly

            float latteralPosition;
            //Function of a triangle wave = Abs((x % p) - p/2) where amplitude = 1/2 p
            latteralPosition = Math.Abs((latteralDistance % w) - (0.5f * w)) * 2.0f;
            latteralPosition = w - latteralPosition; //Shift phase 180 deg

            //Set output
            xPosition = latteralPosition;

            return true;
        }
    }
}
