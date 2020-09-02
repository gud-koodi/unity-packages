namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public class PathAgent : MonoBehaviour
    {
        public float Speed;

        public PathNode StartNode;

        private int pathMask;

        private Rigidbody rb;

        private PathPosition pathPosition;
        private PathPosition previousPosition;

        public PathPosition PathPosition => this.pathPosition;

        void Awake()
        {
            this.pathMask = LayerMask.GetMask("Path");
            this.rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            this.pathPosition = new PathPosition(this.StartNode);
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
    }
}
