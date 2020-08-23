namespace GudKoodi.PathAgent.Runtime
{
    using UnityEngine;

    public class PathNode : MonoBehaviour
    {
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
            return this.pathToLeftNode.GetPointOnCurve(distance);
        }

        public Vector3 MoveTowardsRightNode(float distance)
        {
            return this.pathToRightNode.GetPointOnCurve(distance);
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
    }
}
