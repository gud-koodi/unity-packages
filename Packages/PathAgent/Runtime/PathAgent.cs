namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public class PathAgent : MonoBehaviour
    {
        public float Speed;

        private int pathMask;

        private Rigidbody rb;

        private PathPosition pathPosition;

        void Awake()
        {
            this.pathMask = LayerMask.GetMask("Path");
            this.rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            var node = this.FindClosestWaypoint();
            if (!node)
            {
                Debug.LogError("Failed to find a waypoint.");
            }
            this.pathPosition = new PathPosition(node);
            this.rb.position = this.pathPosition.WorldPosition;
        }

        void FixedUpdate()
        {
            this.pathPosition = this.pathPosition + this.Speed * Time.fixedDeltaTime;
            var newPosition = this.pathPosition.WorldPosition;
            newPosition.y = rb.position.y;
            this.rb.MovePosition(newPosition);
        }

        private PathNode FindClosestWaypoint()
        {
            var waypoints = Physics.OverlapSphere(rb.position, 5.0f, pathMask);
            if (waypoints.Length == 0)
            {
                Debug.LogWarning("No path nodes found.");
                return null;
            }

            // "Closest"
            var waypoint = waypoints[0].GetComponent<PathNode>();
            if (waypoint == null)
            {
                Debug.LogWarning("No path node component found.");
            }

            return waypoint;
        }
    }
}
