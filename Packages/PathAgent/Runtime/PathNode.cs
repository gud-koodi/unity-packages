namespace GudKoodi.PathAgent
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

        public Vector3 MoveTowardsLeftNode(float distance, float height)
        {
            return this.pathToLeftNode.GetPointOnPath(distance, height);
        }

        public Vector3 MoveTowardsRightNode(float distance, float height)
        {
            return this.pathToRightNode.GetPointOnPath(distance, height);
        }

        void Start()
        {
            if (this.left == null)
            {
                this.left = this;
                this.pathToLeftNode = new LinearPath(this.transform.position, this.transform.position);
            }
            else
            {
                this.pathToLeftNode = new InvertedPath(this.left.CreatePathToRightNode());
            }

            if (this.right == null)
            {
                this.right = this;
                this.pathToRightNode = new LinearPath(this.transform.position, this.transform.position);
            }
            else
            {
                this.pathToRightNode = this.CreatePathToRightNode();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(this.transform.position, this.transform.lossyScale);
        }

        void OnDrawGizmosSelected()
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
                        Gizmos.DrawSphere(path.GetPointOnPath(i, this.transform.position.y), 0.1f);
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
                        Gizmos.DrawSphere(path.GetPointOnPath(i, this.transform.position.y), 0.1f);
                    }
                }
            }
        }

        private BezierPath CreatePathToRightNode()
        {
            return new BezierPath(
                this.transform.position,
                this.transform.position + 5 * this.transform.forward,
                this.right.transform.position - 5 * this.right.transform.forward,
                this.right.transform.position
            );
        }
    }
}
