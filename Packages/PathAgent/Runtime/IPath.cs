namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public interface IPath
    {
        float Length { get; }

        Quaternion GetCameraTangentRotationOnPath(float transition);

        Vector3 GetPointOnPath(float transition, float height);
    }
}
