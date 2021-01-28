using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MorphEditor
{
    public class GizmoUtilities
    {
        public static bool enable;
        public static MorphEditor editor;

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected | GizmoType.Active)]
        static void DrawGizmo(MorphSet ms, GizmoType type)
        {
            enable = MorphInspector.editMode && editor.ms && ms.GetHashCode() == editor.ms.GetHashCode();
            if (!enable)
                return;
            var mesh = ms.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            Gizmos.color = Color.green;
            foreach (int i in editor.mark)
            {
                Gizmos.DrawSphere(ms.transform.TransformPoint(mesh.vertices[i]), 0.01f);
            }
        }
    }
}
