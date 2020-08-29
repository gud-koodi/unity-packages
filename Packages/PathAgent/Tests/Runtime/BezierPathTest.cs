namespace GudKoodi.PathAgent.Tests
{
    using NUnit.Framework;
    using UnityEngine;

    public class BezierPathTest
    {
        private BezierPath GetLinearPath(Vector3 a, Vector3 d)
        {
            var length = Vector3.Distance(a, d);
            var b = Vector3.MoveTowards(a, d, length / 3);
            var c = Vector3.MoveTowards(a, d, 2 * length / 3);
            return new BezierPath(a, b, c, d);
        }

        [Test]
        public void BezierPathFromSinglePointHasLengthOfZero()
        {
            var path = new BezierPath(Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero);
            Assert.Zero(path.Length);
        }

        [Test]
        public void BezierPathFromLineOfVerticalPointsHasLengthOfZero()
        {
            var path = this.GetLinearPath(Vector3.zero, 3 * Vector3.up);
            Assert.Zero(path.Length);
        }

        [Test]
        public void BezierPathFromLineOfHorizontalPointsHasLengthOfSegment()
        {
            var path = this.GetLinearPath(Vector3.zero, 3 * Vector3.right);
            Assert.AreEqual(3, path.Length);
        }

        [Test]
        public void PointOnLinearBezierPathIsCorrect()
        {
            var path = this.GetLinearPath(Vector3.zero, 10 * Vector3.right);
            foreach (var i in new int[] { 0, 1, 3, 6, 10 })
            {
                Assert.AreEqual(i * Vector3.right, path.GetPointOnPath(i, 0));
            }
        }

        [Test]
        public void GetPointOnPathIsClampedToLength()
        {
            var path = this.GetLinearPath(Vector3.zero, Vector3.right);
            Assert.AreEqual(Vector3.zero, path.GetPointOnPath(-1, 0));
            Assert.AreEqual(Vector3.right, path.GetPointOnPath(2, 0));
        }
    }
}
