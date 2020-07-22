using UnityEngine;

public class HyperVertice : MonoBehaviour
{
    public float StartW;
    private Vector4 hyperPosition;

    void Start()
    {
        Vector3 startPosition = this.transform.localPosition;
        this.hyperPosition = new Vector4(startPosition.x, startPosition.y, startPosition.z, StartW);
        this.UpdatePosition();
    }

    public void Rotate(Matrix4x4 rotator)
    {
        this.hyperPosition = rotator * this.hyperPosition;
        this.UpdatePosition();
    }

    private void UpdatePosition()
    {
        this.transform.localPosition = this.hyperPosition;
    }
}
