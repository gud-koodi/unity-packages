
namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public class BezierPath : IPath
    {
        private const int SEGMENTS = 5;

        private readonly Vector3[] points;
        private readonly float[] sublengths;

        public BezierPath(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            float height = Mathf.Min(a.y, d.y);
            this.points = new Vector3[SEGMENTS+1];
            for (int i = 0; i <= SEGMENTS; i++)
            {
                var t = (float) i / SEGMENTS;
                var _t = 1 - t;
                this.points[i] = Mathf.Pow(_t, 3) * a + 3 * Mathf.Pow(_t, 2) * t * b + 3 * _t * Mathf.Pow(t, 2) * c + Mathf.Pow(t, 3) * d;
                this.points[i].y = height;
            }

            var length = 0f;
            this.sublengths = new float[SEGMENTS];
            for (int i = 0; i < SEGMENTS; i++)
            {
                this.sublengths[i] = Vector3.Distance(this.points[i], this.points[i+1]);
                length += this.sublengths[i];
            }

            this.Length = length;
        }

        public float Length { get; }

        public Vector3 GetPointOnPath(float transition, float height)
        {
            transition = Mathf.Clamp(transition, 0, this.Length);
            if (transition >= this.Length)
            {
                var v = this.points[SEGMENTS];
                v.y = height;
                return v;
            }

            var i = 0;
            while (transition > sublengths[i])
            {
                transition -= sublengths[i];
                i++;
            }

            var vector = Vector3.MoveTowards(this.points[i], this.points[i+1], transition);
            vector.y = height;
            return vector;
        }
    }
}
