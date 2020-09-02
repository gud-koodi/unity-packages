namespace GudKoodi.PathAgent.Editor
{
    using GudKoodi.PathAgent;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(PathNode))]
    public class PathNodeInspector : Editor
    {
        private PathNode node;

        private SerializedProperty left;
        private SerializedProperty right;

        void OnEnable()
        {
            this.node = (PathNode)target;
            this.left = serializedObject.FindProperty("left");
            this.right = serializedObject.FindProperty("right");
        }

        void OnSceneGUI()
        {
            this.node.CameraGuide = Handles.PositionHandle(this.node.CameraGuide, this.node.transform.rotation);
        }

        public override void OnInspectorGUI()
        {
            EditorGUIUtility.labelWidth = 50;
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Neighbors");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(this.left, new GUIContent("Left"));
            EditorGUILayout.PropertyField(this.right, new GUIContent("Right"));
            EditorGUILayout.EndHorizontal();

            this.node.CameraGuide = EditorGUILayout.Vector3Field("Camera", this.node.CameraGuide);

            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
