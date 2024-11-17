using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace BallTracker
{
    internal class Source
    {
        static void Main(string[] args)
        {
            CalculateXPosition calc = new CalculateXPosition();

            float h = 7;
            Vector2 p = new Vector2(5.0f,5.0f);
            Vector2 v = new Vector2(5.0f, 5.0f); ;
            float G = -9.8f;
            float w = 10;
            float xPosition = 0;

            var rand = new Random();


            for (int i = 0; i < 1000; ++i)
            {
                v.X = ((float)rand.NextDouble() - 0.5f) * 15.0f; // random between -7.5 and 7.5
                v.Y = ((float)rand.NextDouble() - 0.5f) * 15.0f; // random between -7.5 and 7.5
                if(calc.TryCalculateXPositionAtHeight(h, p, v, G, w, ref xPosition))
                {
                    Console.WriteLine(xPosition.ToString());
                }
            }


            return;
        }
    }
}
