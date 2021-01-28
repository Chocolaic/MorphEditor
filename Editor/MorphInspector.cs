using UnityEditor;
using UnityEngine;

namespace MorphEditor
{
    public class MorphInspector : EditorWindow
    {
        public static bool editMode;

        private SkinnedMeshRenderer smr;
        private MorphEditor morphEditor = new MorphEditor();
        private Vector2 shapeScroll;
        private int selectShape;

        [UnityEditor.MenuItem("Window/Morph")]
        static void EnableWindow()
        {
            EditorWindow window = GetWindow(typeof(MorphInspector), false, "Morph");
            window.Show();
        }
        private void OnGUI()
        {
            smr = (SkinnedMeshRenderer)EditorGUILayout.ObjectField("", smr, typeof(SkinnedMeshRenderer), true);
            if (smr)
            {
                GUILayout.Label("BlendShapes:");
                using (var scope = new EditorGUILayout.ScrollViewScope(shapeScroll, GUILayout.Height(100)))
                {
                    shapeScroll = scope.scrollPosition;
                    for (int i = 0; i < smr.sharedMesh.blendShapeCount; i++)
                    {
                        using (var vscope = new EditorGUILayout.VerticalScope())
                        {
                            GUI.Box(vscope.rect, new GUIContent());
                            if (EditorGUILayout.Toggle(smr.sharedMesh.GetBlendShapeName(i), i == selectShape))
                            {
                                selectShape = i;
                            }
                        }

                    }
                }
                if (!editMode && GUILayout.Button("Edit"))
                {
                    morphEditor.Init(smr, selectShape);
                    editMode = true;
                    morphEditor.Enable();
                }
                if (editMode && GUILayout.Button("Save"))
                {
                    editMode = false;
                    morphEditor.Save(selectShape);
                }
                if (editMode && GUILayout.Button("Cancel"))
                {
                    editMode = false;
                    morphEditor.Disable();
                }
                if (editMode)
                {
                    GUILayout.Label("Select Area:");
                    morphEditor.pointFilter.radius = EditorGUILayout.Slider(morphEditor.pointFilter.radius, 0.0005f, 0.02f);
                }
            }
        }
    }
}
