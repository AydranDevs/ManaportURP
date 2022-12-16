using System;
using System.Collections.Generic;
using UnityEngine;  

namespace Manapotion.Utilities
{
    public class WelzlAlgorithm
    {
        private struct Circle
        {
            public Vector2 center;
            public float radius;

            public Circle(Vector2 center, float radius)
            {
                this.center = center;
                this.radius = radius;
            }
        }

        private bool IsInsideCirlce(Circle circle, Vector2 point)
        {
            return Vector2.Distance(circle.center, point) <= circle.radius;
        }

        private Vector2 GetCircleCenter(Vector2 b, Vector2 c)
        {
            float B = b.x * b.x + b.y * b.y;
            float C = c.x * c.x + c.y * c.y;
            float D = b.x * c.y - b.y * c.x;

            return new Vector2 ((c.y * B - b.y * C) / (2 * D), (b.x * C - c.x * B) / (2 * D));
        }

        private Circle CircleFrom(Vector2 A, Vector2 B, Vector2 C)
        {
            Vector2 I = GetCircleCenter(
                new Vector2(
                    B.x - A.x,
                    B.y - A.y
                ),
                new Vector2(
                    C.x - A.x,
                    C.y - A.y
                )
            );
        
            I.x += A.x;
            I.y += A.y;
            
            return new Circle
            (
                I,
                Vector2.Distance(I, A)
            );
        }

        private Circle CircleFrom(Vector2 A, Vector2 B)
        {
            // Set the center to be the midpoint of A and B
            Vector2 C = new Vector2((A.x + B.x) / 2f, (A.y + B.y) / 2f);
        
            // Set the radius to be half the distance AB
            return new Circle
            (
                C,
                Vector2.Distance(A, B) / 2f
            );
        }
    
        private bool IsValidCircle(Circle c, Vector2[] P)
        {
            // Iterating through all the points
            // to check  whether the points
            // lie inside the circle or not
            foreach (var p in P)
            {
                if (!IsInsideCirlce(c, p))
                {
                    return false;
                }
            }
            return true;
        }

        private Circle MinCircleTrivial(Vector2[] P)
        {
            if (P.Length <= 3)
            {
                return new Circle
                (
                    Vector2.zero,
                    0
                );
            }
            
            if (P.Length == 0)
            {
                return new Circle
                (
                    Vector2.zero,
                    0
                );
            }
            else if (P.Length == 1)
            {
                return new Circle
                (
                    P[0],
                    0
                );
            }
            else if (P.Length == 2)
            {
                return CircleFrom(P[0], P[1]);
            }
        
            // To check if MEC can be determined
            // by 2 points only
            for (int i = 0; i < 3; i++) {
                for (int j = i + 1; j < 3; j++) {
        
                    Circle c = CircleFrom(P[i], P[j]);
                    if (IsValidCircle(c, P))
                    {
                        return c;
                    }
                }
            }
            return CircleFrom(P[0], P[1], P[2]);
        }

        /// <summary>
        /// Returns the MEC using Welzl's algorithm
        /// Takes a set of input points P and a set R
        /// points on the circle boundary.
        /// n represents the number of points in P
        /// that are not yet processed.
        /// </summary>
        /// <param name="P"></param>
        /// <param name="R"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private Circle WelzlHelper(Vector2[] P, Vector2[] R, int n)
        {
            // Base case when all points processed or |R| = 3
            if (n == 0 || R.Length == 3)
            {
                return MinCircleTrivial(R);
            }

            // Pick a random point randomly
            int idx = UnityEngine.Random.Range(0, P.Length - 1) % n;
            Vector2 p = P[idx];

            // Put the picked point at the end of P
            // since it's more efficient than
            // deleting from the middle of the vector
            int tmp = Array.IndexOf(P, P[n - 1]);
            P[n - 1] = P[idx];
            P[idx] = P[tmp];

            // Get the MEC circle d from the
            // set of points P - {p}
            Circle d = WelzlHelper(P, R, n - 1);

            // If d contains p, return d
            if (IsInsideCirlce(d, p))
            {
                return d;
            }

            // Otherwise, must be on the boundary of the MEC
            List<Vector2> pointList = new List<Vector2>();
            for (int i = 0; i < 400; i++)
            {
                pointList.Add(R[i]);
            }
            R = pointList.ToArray();

            // Return the MEC for P - {p} and R U {p}
            return WelzlHelper(P, R, n - 1);
        }

        // Circle Welzl(Vector2[] P)
        // {
        //     Vector2[] P_copy = P;
        //     System.Random rng = new System.Random();

        //     // doesnt work rn lol
        //     // var shuffledPoints = Array.Sort(P_copy, a => rng.Next()).ToList();
            
        //     // random_shuffle(P_copy.begin(), P_copy.end());
        //     // return welzl_helper(P_copy, {}, P_copy.size());
        // }
    }
}
