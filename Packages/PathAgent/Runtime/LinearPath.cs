
namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public class LinearPath : IPath
    {
        private readonly Vector2 Start;
        private readonly Quaternion StartCameraRotation;
        private readonly Vector2 End;
        private readonly Quaternion EndCameraRotation;

        public LinearPath(Vector3 start, Quaternion startCameraRotation, Vector3 end, Quaternion endCameraRotation)
        {
            this.Start = new Vector2(start.x, start.z);
            this.StartCameraRotation = startCameraRotation;
            this.End = new Vector2(start.x, start.z);
            this.EndCameraRotation = endCameraRotation;
            this.Length = Vector2.Distance(this.Start, this.End);
        }

        public float Length { get; }

        public Quaternion GetCameraTangentRotationOnPath(float transition)
        {
            return Quaternion.Slerp(StartCameraRotation, EndCameraRotation, transition / this.Length);
        }

        public Vector3 GetPointOnPath(float transition, float height)
        {
            var vector = Vector2.MoveTowards(this.Start, this.End, Mathf.Clamp(transition, 0, this.Length));
            return new Vector3(vector.x, height, vector.y);
        }
    }
}
