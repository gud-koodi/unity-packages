namespace GudKoodi.PathAgent.Example
{
    using UnityEngine;

    public class CameraScript : MonoBehaviour
    {
        public float Distance = 5f;
        public PathAgent Player;

        void Start()
        {
            this.Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PathAgent>();
        }

        void LateUpdate()
        {
            var p = this.Player.transform.position;
            this.transform.position = p + this.Player.PathPosition.CameraTangentRotation * Vector3.back * this.Distance;
            this.transform.LookAt(this.Player.transform.position);
        }
    }
}
