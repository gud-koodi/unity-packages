namespace GudKoodi.PathAgent
{
    using UnityEngine;

    public readonly struct PathPosition
    {
        public static PathPosition operator +(PathPosition p, float f) =>
            (f < 0)
                ? p.NegativeTransformation(f)
                : p.GetNormalizedPositiveTransition(f);

        public PathPosition(PathNode pathNode) : this(pathNode, 0)
        {
        }

        private PathPosition(PathNode pathNode, float transition)
        {
            this.PathNode = pathNode;
            this.Transition = transition;
        }

        public PathNode PathNode { get; }

        public float Transition { get; }

        public Vector3 WorldPosition => this.PathNode.MoveTowardsRightNode(this.Transition);

        private PathPosition NegativeTransformation(float negativeTransition)
        {
            var node = this.PathNode;
            var relativePosition = this.Transition + negativeTransition;

            while (relativePosition < 0)
            {
                if (node.DistanceToLeftNode == 0)
                {
                    return new PathPosition(node);
                }
                relativePosition += node.DistanceToLeftNode;
                node = node.Left;
            }

            return new PathPosition(node, relativePosition);
        }

        private PathPosition GetNormalizedPositiveTransition(float amount)
        {
            var node = this.PathNode;
            var relativePosition = this.Transition + amount;

            while (relativePosition >= node.DistanceToRightNode)
            {
                if (node.DistanceToRightNode == 0)
                {
                    return new PathPosition(node);
                }
                relativePosition -= node.DistanceToRightNode;
                node = node.Right;
            }

            return new PathPosition(node, relativePosition);
        }
    }
}
