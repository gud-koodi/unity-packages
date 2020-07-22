using UnityEngine;

public class HyperEdge : MonoBehaviour
{
    public HyperVertice vertice1;
    public HyperVertice vertice2;

    private Vector3 startScale;

    void Start()
    {
        this.startScale = this.transform.localScale;
    }

    public void UpdateTransform()
    {
        var direction = vertice2.transform.localPosition - vertice1.transform.localPosition;
        this.transform.localPosition = (vertice1.transform.localPosition + vertice2.transform.localPosition) / 2;
        this.transform.localScale = new Vector3(this.startScale.x, direction.magnitude / 2, this.startScale.z);
        this.transform.localRotation = Quaternion.FromToRotation(Vector3.up, direction);
    }
}
