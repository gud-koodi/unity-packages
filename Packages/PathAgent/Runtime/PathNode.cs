namespace GudKoodi.PathAgent.Runtime
{
    using UnityEngine;

    public class PathNode : MonoBehaviour
    {
        public PathNode Left
        {
            get => this.left;
        }

        public PathNode Right
        {
            get => this.right;
        }

        public float DistanceToLeftNode
        {
            get => this.distanceToLeftNode;
        }

        public float DistanceToRightNode
        {
            get => this.distanceToRightNode;
        }

        [SerializeField]
        private PathNode left;

        [SerializeField]
        private PathNode right;

        private float distanceToLeftNode;
        private float distanceToRightNode;

        public Vector3 MoveTowardsLeftNode(float distance)
        {
            return this.moveTowardsNode(this.Left, distance);
        }

        public Vector3 MoveTowardsRightNode(float distance)
        {
            return this.moveTowardsNode(this.Right, distance);
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

            this.distanceToLeftNode = Vector3.Distance(this.transform.position, this.left.transform.position);
            this.distanceToRightNode = Vector3.Distance(this.transform.position, this.right.transform.position);
        }

        private Vector3 moveTowardsNode(PathNode target, float distance)
        {
            var result = Vector3.MoveTowards(this.transform.position, target.transform.position, distance);
            result.y = 0;
            return result;
        }
    }
}
