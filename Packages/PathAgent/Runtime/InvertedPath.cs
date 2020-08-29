namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public class InvertedPath : IPath
    {
        private IPath path;

        public InvertedPath(IPath path)
        {
            this.path = path;
        }

        public float Length => this.path.Length;

        public Vector3 GetPointOnPath(float transition, float height)
        {
            return this.path.GetPointOnPath(this.path.Length - transition, height);
        }
    }
}
