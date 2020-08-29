namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public class PathAgent : MonoBehaviour
    {
        public float Speed;

        private int pathMask;

        private Rigidbody rb;

        private PathPosition pathPosition;
        private PathPosition previousPosition;

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
            this.previousPosition = this.pathPosition;
            this.rb.position = this.pathPosition.getWorldPosition(this.rb.position.y);
        }

        void FixedUpdate()
        {
            var rbPosition = this.rb.position;
            this.previousPosition = this.closer(this.rb.position, this.pathPosition, this.previousPosition);
            this.pathPosition = previousPosition + this.Speed * Time.fixedDeltaTime;
            var vectorPosition = this.pathPosition.getWorldPosition(this.rb.position.y);
            vectorPosition.y = rb.position.y;
            this.rb.MovePosition(vectorPosition);
        }

        private PathPosition closer(Vector3 from, PathPosition a, PathPosition b)
        {
            var distanceA = (from - a.getWorldPosition(this.rb.position.y)).sqrMagnitude;
            var distanceB = (from - b.getWorldPosition(this.rb.position.y)).sqrMagnitude;
            return (distanceA <= distanceB) ? a : b;
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
