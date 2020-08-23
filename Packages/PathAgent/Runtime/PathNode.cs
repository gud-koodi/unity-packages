namespace GudKoodi.PathAgent.Runtime
{
    using UnityEngine;

    public class PathNode : MonoBehaviour
    {
        private const float MAX_GIZMO_KNOT_DISTANCE = 2f;

        [SerializeField]
        private PathNode left;

        [SerializeField]
        private PathNode right;

        private IPath pathToLeftNode;
        private IPath pathToRightNode;

        public PathNode Left => this.left;

        public PathNode Right => this.right;

        public float DistanceToLeftNode => this.pathToLeftNode.Length;

        public float DistanceToRightNode => this.pathToRightNode.Length;

        public Vector3 MoveTowardsLeftNode(float distance)
        {
            return this.pathToLeftNode.GetPointOnPath(distance);
        }

        public Vector3 MoveTowardsRightNode(float distance)
        {
            return this.pathToRightNode.GetPointOnPath(distance);
        }

        void Start()
        {
            if (this.left == null)
            {
                this.left = this;
            }

            if (this.right == null)
            {
                this.right = this;
            }

            this.pathToLeftNode = new LinearPath(this.transform.position, this.left.transform.position);
            this.pathToRightNode = new LinearPath(this.transform.position, this.right.transform.position);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            if (this.left)
            {
                var path = new LinearPath(this.transform.position, this.left.transform.position);
                if (path.Length > 0)
                {
                    var interval = path.Length / Mathf.Ceil(path.Length / MAX_GIZMO_KNOT_DISTANCE);
                    for (float i = 0; i < path.Length; i += interval)
                    {
                        Gizmos.DrawSphere(path.GetPointOnPath(i), 0.1f);
                    }
                }
            }


            if (this.right)
            {
                var path = new LinearPath(this.transform.position, this.right.transform.position);
                if (path.Length > 0)
                {
                    var interval = path.Length / Mathf.Ceil(path.Length / MAX_GIZMO_KNOT_DISTANCE);
                    for (float i = 0; i < path.Length; i += interval)
                    {
                        Gizmos.DrawSphere(path.GetPointOnPath(i), 0.1f);
                    }
                }
            }
        }
    }
}
