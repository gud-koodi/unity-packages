using UnityEngine;

public class HyperController : MonoBehaviour
{
    public float speed;

    private HyperEdge[] edges;
    private HyperVertice[] vertices;

    void Start()
    {
        this.vertices = this.GetComponentsInChildren<HyperVertice>();
        this.edges = this.GetComponentsInChildren<HyperEdge>();
    }

    void Update()
    {
        var radius = (Input.GetKey(KeyCode.LeftShift) ? -1 : 1) * speed * Time.deltaTime;
        var cos = Mathf.Cos(radius);
        var sin = Mathf.Sin(radius);
        var rotator = Matrix4x4.identity;
        if (Input.GetKey(KeyCode.Alpha1)) { rotator *= new Matrix4x4(new Vector4(cos, -sin, 0, 0), new Vector4(sin, cos, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1)); }
        if (Input.GetKey(KeyCode.Alpha2)) { rotator *= new Matrix4x4(new Vector4(cos, 0, -sin, 0), new Vector4(0, 1, 0, 0), new Vector4(sin, 0, cos, 0), new Vector4(0, 0, 0, 1)); }
        if (Input.GetKey(KeyCode.Alpha3)) { rotator *= new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, cos, -sin, 0), new Vector4(0, sin, cos, 0), new Vector4(0, 0, 0, 1)); }
        if (Input.GetKey(KeyCode.Alpha4)) { rotator *= new Matrix4x4(new Vector4(cos, 0, 0, -sin), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(sin, 0, 0, cos)); }
        if (Input.GetKey(KeyCode.Alpha5)) { rotator *= new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, cos, 0, -sin), new Vector4(0, 0, 1, 0), new Vector4(0, sin, 0, cos)); }
        if (Input.GetKey(KeyCode.Alpha6)) { rotator *= new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, cos, -sin), new Vector4(0, 0, sin, cos)); }
        foreach (var vertice in this.vertices)
        {
            vertice.Rotate(rotator);
        }
        foreach (var edge in edges)
        {
            edge.UpdateTransform();
        }
    }
}
