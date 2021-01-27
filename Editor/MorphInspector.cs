using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MorphEditor
{
    public class MorphInspector : EditorWindow
    {
        public static SkinnedMeshRenderer smr;
        private PointFilter pointFilter = new PointFilter();
        public static bool editMode;

        [MenuItem("Window/Morph")]
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
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Edit") && !editMode)
                {
                    editMode = true;
                    pointFilter.Enable();

                }
                if (GUILayout.Button("Cancel") && editMode)
                {
                    editMode = false;
                    pointFilter.Disable();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
