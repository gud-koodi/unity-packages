namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public class PathNode : MonoBehaviour
    {
        private const float MAX_GIZMO_KNOT_DISTANCE = 2f;

        // allow these fields to be null
#pragma warning disable CS0649
        [SerializeField]
        private PathNode left;

        [SerializeField]
        private PathNode right;
#pragma warning restore CS0649

        public Vector3 CameraGuide;

        private IPath pathToLeftNode;
        private IPath pathToRightNode;

        public PathNode Left => this.left;

        public PathNode Right => this.right;

        public Quaternion CameraTangentRotation
        {
            get
            {
                var v = this.transform.position - this.CameraGuide;
                v.y = 0;
                return Quaternion.LookRotation(v);
            }
        }

        public float DistanceToLeftNode => this.pathToLeftNode.Length;

        public float DistanceToRightNode => this.pathToRightNode.Length;

        private Vector3 LeftEdge => this.transform.position - 0.5f * this.transform.localScale.x * this.transform.right;

        private Vector3 RightEdge => this.transform.position + 0.5f * this.transform.localScale.x * this.transform.right;

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
            this.CreatePaths();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.CameraGuide, new Vector3(this.transform.position.x, this.CameraGuide.y, this.transform.position.z));

            Gizmos.color = Color.green;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }

        void OnDrawGizmosSelected()
        {
            this.CreatePaths();
            var upperHeight = this.transform.position.y + 0.5f * this.transform.localScale.y;
            var lowerHeight = this.transform.position.y - 0.5f * this.transform.localScale.y;

            Gizmos.color = Color.yellow;
            foreach (var vector in new Vector3[] {
                (this.left) ? this.left.transform.position : this.LeftEdge,
                this.transform.position,
                (this.right) ? this.right.transform.position : this.RightEdge,
            })
            {
                Gizmos.DrawSphere(vector, 0.2f);
            }

            Gizmos.color = Color.red;
            foreach (var path in new IPath[] { this.pathToLeftNode, this.pathToRightNode })
            {
                if (path.Length > 0)
                {
                    var interval = path.Length / Mathf.Ceil(path.Length / MAX_GIZMO_KNOT_DISTANCE);
                    for (float i = 0; i < path.Length; i += interval)
                    {
                        Gizmos.DrawLine(path.GetPointOnPath(i, upperHeight), path.GetPointOnPath(i, lowerHeight));
                    }
                }
            }
        }

        private void CreatePaths()
        {
            this.pathToLeftNode = (this.left)
                ? this.CreatePathToLeftNode()
                : (IPath)new LinearPath(this.transform.position, Quaternion.identity, this.LeftEdge, Quaternion.identity);
            this.pathToRightNode = (this.right)
                ? this.CreatePathToRightNode()
                : (IPath)new LinearPath(this.transform.position, Quaternion.identity, this.RightEdge, Quaternion.identity);
        }

        private IPath CreatePathToLeftNode()
        {
            return new InvertedPath(new BezierPath(
                this.left.transform.position,
                this.left.CameraTangentRotation,
                this.left.RightEdge,
                this.LeftEdge,
                this.transform.position,
                this.CameraTangentRotation
            ));
        }

        private IPath CreatePathToRightNode()
        {
            return new BezierPath(
                this.transform.position,
                this.CameraTangentRotation,
                this.RightEdge,
                this.right.LeftEdge,
                this.right.transform.position,
                this.right.CameraTangentRotation
            );
        }
    }
}
