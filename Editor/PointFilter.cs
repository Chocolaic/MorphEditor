using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MorphEditor
{
    public class PointFilter
    {
        public float radius = 0.001f;
        public Vector3[] points;
        public MorphEditor editor;
        public void Enable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }
        public void Disable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }
        void OnSceneGUI(SceneView sceneView)
        {
            Vector3 focus = SceneView.currentDrawingSceneView.camera.transform.position;
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            Vector3 spos = ray.origin;
            Vector3 epos = spos + ray.direction * 100.0f;

            if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && !Event.current.alt)
            {
                CastElement(spos, epos);
                Event.current.Use();
            }
        }
        void CastElement(Vector3 spos, Vector3 epos)
        {
            for (int i = 0; i < editor.mark.Count; i++)
            {
                float d = PointDistance(spos, epos, editor.ms.transform.TransformPoint(points[editor.mark[i]]));
                if (d < radius * 0.5f)
                {
                    Debug.Log(d);
                    editor.mark.RemoveAt(i);
                }
            }
        }
        float PointDistance(Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3 ab = b - a;
            Vector3 ac = c - a;
            Vector3 bc = c - b;
            float e = Vector3.Dot(ac, ab);

            if (e <= 0)
                return Vector3.Dot(ac, ac);
            float f = Vector3.Dot(ab, ab);
            if (e >= f)
                return Vector3.Dot(bc, bc);
            return Vector3.Dot(ac, ac) - e * e / f;
        }
    }
}
