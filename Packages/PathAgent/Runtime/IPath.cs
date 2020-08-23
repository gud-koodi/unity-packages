namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public interface IPath
    {
        float Length { get; }

        Vector3 GetPointOnPath(float transition);
    }
}
