namespace GudKoodi.PathAgent.Tests
{
    using NUnit.Framework;
    using UnityEngine;

    public class InvertedPathTest
    {
        [Test]
        public void BezierPathInvertionWorks()
        {
            var a = Vector3.zero;
            var b = new Vector3(30, 0, 50);
            var c = new Vector3(50, 0, 70);
            var d = new Vector3(100, 0, 100);
            var bezierPath = new BezierPath(a, b, c, d);
            var invertedPath = new InvertedPath(bezierPath);

            var length = bezierPath.Length;
            Assert.AreEqual(length, invertedPath.Length);

            foreach (var i in new float[] { 0f, 0.1f, 0.3f, 0.6f, 1f })
            {
                var t = i * length;
                Assert.True(bezierPath.GetPointOnPath(t, 0) == invertedPath.GetPointOnPath(length - t, 0));
            }
        }
    }
}
