namespace GudKoodi.PathAgent.Runtime
{
    using UnityEngine;

    public interface IPath
    {
        float Length { get; }

        Vector3 GetPointOnCurve(float transition);
    }
}
