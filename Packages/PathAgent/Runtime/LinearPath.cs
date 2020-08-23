
namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public class LinearPath : IPath
    {
        private readonly Vector3 start;
        private readonly Vector3 end;

        private readonly float length;

        public LinearPath(Vector3 start, Vector3 end)
        {
            float height = Mathf.Min(start.y, end.y);
            this.start = new Vector3(start.x, height, start.z);
            this.end = new Vector3(end.x, height, end.z);
            this.length = Vector3.Distance(this.start, this.end);
        }

        public float Length => this.length;

        public Vector3 GetPointOnPath(float transition)
        {
            return Vector3.MoveTowards(this.start, this.end, Mathf.Clamp(transition, 0, this.length));
        }
    }
}
