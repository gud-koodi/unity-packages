
namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public class BezierPath : IPath
    {
        private const int SEGMENTS = 5;

        private readonly Quaternion StartCameraRotation;
        private readonly Quaternion EndCameraRotation;
        private readonly Vector2[] Points;
        private readonly float[] Sublengths;

        public BezierPath(Vector3 a, Vector3 b, Vector3 c, Vector3 d) : this(a, Quaternion.identity, b, c, d, Quaternion.identity)
        {
        }

        public BezierPath(Vector3 a, Quaternion startCameraRotation, Vector3 b, Vector3 c, Vector3 d, Quaternion endCameraRotation)
        {
            this.StartCameraRotation = startCameraRotation;
            this.EndCameraRotation = endCameraRotation;
            this.Points = new Vector2[SEGMENTS + 1];
            for (int i = 0; i <= SEGMENTS; i++)
            {
                var t = (float)i / SEGMENTS;
                var _t = 1 - t;
                var point = Mathf.Pow(_t, 3) * a + 3 * Mathf.Pow(_t, 2) * t * b + 3 * _t * Mathf.Pow(t, 2) * c + Mathf.Pow(t, 3) * d;
                this.Points[i] = new Vector2(point.x, point.z);
            }

            var length = 0f;
            this.Sublengths = new float[SEGMENTS];
            for (int i = 0; i < SEGMENTS; i++)
            {
                this.Sublengths[i] = Vector2.Distance(this.Points[i], this.Points[i + 1]);
                length += this.Sublengths[i];
            }

            this.Length = length;
        }

        public float Length { get; }

        public Quaternion GetCameraTangentRotationOnPath(float transition)
        {
            return Quaternion.Slerp(StartCameraRotation, EndCameraRotation, transition / this.Length);
        }

        public Vector3 GetPointOnPath(float transition, float height)
        {
            transition = Mathf.Clamp(transition, 0, this.Length);
            if (transition >= this.Length)
            {
                var v = this.Points[SEGMENTS];
                v.y = height;
                return v;
            }

            var i = 0;
            while (transition > Sublengths[i])
            {
                transition -= Sublengths[i];
                i++;
            }

            var vector = Vector2.MoveTowards(this.Points[i], this.Points[i + 1], transition);
            return new Vector3(vector.x, height, vector.y);
        }
    }
}
