
namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public class LinearPath : IPath
    {
        private readonly Vector2 start;
        private readonly Vector2 end;

        private readonly float length;

        public LinearPath(Vector3 start, Vector3 end)
        {
            this.start = new Vector2(start.x, start.z);
            this.end = new Vector2(start.x, start.z);
            this.length = Vector2.Distance(this.start, this.end);
        }

        public float Length => this.length;

        public Vector3 GetPointOnPath(float transition, float height)
        {
            var vector = Vector2.MoveTowards(this.start, this.end, Mathf.Clamp(transition, 0, this.length));
            return new Vector3(vector.x, height, vector.y);
        }
    }
}
