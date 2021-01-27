using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MorphEditor
{
    public class GizmoUtilities
    {
        public static bool enable;

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected | GizmoType.Active)]
        static void DrawGizmo(SkinnedMeshRenderer smr, GizmoType type)
        {
            enable = MorphInspector.editMode && MorphInspector.smr && smr.GetHashCode() == MorphInspector.smr.GetHashCode();
            if (!enable)
                return;
            var mesh = smr.sharedMesh;
            foreach (var pos in mesh.vertices)
            {
                Gizmos.DrawSphere(smr.transform.TransformPoint(pos), 0.02f);
            }
        }
    }
}
