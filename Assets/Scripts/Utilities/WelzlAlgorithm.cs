using System;
using System.Collections.Generic;
using UnityEngine;  

namespace Manapotion.Utilities
{
    public static class WelzlAlgorithm
    {
        private static bool IsInsideCircle(Circle circle, Vector2 point)
        {
            return Vector2.Distance(circle.center, point) <= circle.radius;
        }

        private static Vector2 GetCircleCenter(Vector2 b, Vector2 c)
        {
            float B = b.x * b.x + b.y * b.y;
            float C = c.x * c.x + c.y * c.y;
            float D = b.x * c.y - b.y * c.x;

            return new Vector2 ((c.y * B - b.y * C) / (2 * D), (b.x * C - c.x * B) / (2 * D));
        }

        private static Circle CircleFrom(Vector2 A, Vector2 B, Vector2 C)
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

        private static Circle CircleFrom(Vector2 A, Vector2 B)
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
    
        private static bool IsValidCircle(Circle circle, List<Vector2> pointsList)
        {
            // Iterating through all the points
            // to check  whether the points
            // lie inside the circle or not
            foreach (var point in pointsList)
            {
                if (!IsInsideCircle(circle, point))
                {
                    return false;
                }
            }
            return true;
        }

        private static Circle MinCircleTrivial(List<Vector2> pointsList)
        {   
            // if no points in pointsList, return a "null circle"
            if (pointsList.Count == 0)
            {
                return new Circle
                (
                    Vector2.zero,
                    0
                );
            }
            // if only one point in pointsList, return a circle with a radius of 0 and a center position equal to the point
            else if (pointsList.Count == 1)
            {
                return new Circle
                (
                    pointsList[0],
                    0
                );
            }
            // if there are two points in pointsList, return a circle created from those two points
            else if (pointsList.Count == 2)
            {
                return CircleFrom(pointsList[0], pointsList[1]);
            }
        
            // To check if MEC can be determined
            // by 2 points only
            for (int i = 0; i < 3; i++) {
                for (int j = i + 1; j < 3; j++) {
        
                    Circle c = CircleFrom(pointsList[i], pointsList[j]);
                    if (IsValidCircle(c, pointsList))
                    {
                        return c;
                    }
                }
            }
            return CircleFrom(pointsList[0], pointsList[1], pointsList[2]);
        }

        private static Circle WelzlHelper(List<Vector2> P, List<Vector2> R, int n)
        {
            // if all points have been processed or R.Count == 3, return a circle from those three points.
            if (n == 0 || R.Count == 3)
            {
                return MinCircleTrivial(R);
            }

            // Pick a random point
            int idx = UnityEngine.Random.Range(0, P.Count - 1) % n;
            Vector2 p = P[idx];

            // Put the picked point at the end of P
            // since it's more efficient than
            // deleting from the middle of the List
            P.Remove(P[idx]);
            P.Add(p);

            // Get the Minimum Enclosing Circle d from the
            // set of points P - {p}
            var d = WelzlHelper(P, R, n - 1);

            // If d contains p, return d
            if (IsInsideCircle(d, p)) {
                return d;
            }

            // Otherwise, must be on the boundary of the Minimum Enclosing Circle
            R.Add(p);

            // Return the Minimum Enclosing Circle for P - {p} and R U {p}
            return WelzlHelper(P, R, n - 1);
        }

        public static Circle Welzl(List<Vector2> P)
        {
            var P_copy = P;
            System.Random rng = new System.Random();
            
            Shuffle(rng, P_copy);
            return WelzlHelper(P_copy, new List<Vector2>(), P_copy.Count);
        }

        private static void Shuffle<T>(System.Random rng, List<T> list)
        {
            int n = list.Count;
            while (n > 1) 
            {
                int k = rng.Next(n--);
                T temp = list[n];
                list[n] = list[k];
                list[k] = temp;
            }
        }
    }
}
