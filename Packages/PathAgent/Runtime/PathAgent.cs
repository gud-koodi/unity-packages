namespace GudKoodi.LineWalker.Runtime
{
    using UnityEngine;

    public class PathAgent : MonoBehaviour
    {
        public float Speed;

        private int pathMask;
        
        [SerializeField]
        private PathNode pathNode;

        private Rigidbody rb;

        [SerializeField]
        private float transition;

        void Awake()
        {
            this.pathMask = LayerMask.GetMask("Path");
            this.rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            this.pathNode = this.FindClosestWaypoint();
            this.rb.position = this.pathNode.transform.position;
        }

        void FixedUpdate()
        {
            this.transition += this.Speed * Time.fixedDeltaTime;
            while (this.transition < 0)
            {
                this.transition += this.pathNode.DistanceToLeftNode;
                this.pathNode = this.pathNode.Left;
                if (this.pathNode.DistanceToLeftNode == 0)
                {
                    this.transition = 0;
                    return;
                }
            }
            while (this.transition >= this.pathNode.DistanceToRightNode)
            {
                this.transition -= this.pathNode.DistanceToRightNode;
                this.pathNode = this.pathNode.Right;
                if (this.pathNode.DistanceToRightNode == 0)
                {
                    this.transition = 0;
                    return;
                }
            }

            var newPosition = this.pathNode.MoveTowardsRightNode(transition);
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
