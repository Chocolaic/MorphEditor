using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MorphEditor
{
    public class PointFilter
    {
        public float radius = 0.02f;
        public Vector3[] points;
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

            }
        }
        void CastElement(Ray ray)
        {
            for (int i = 0; i < points.Length; i++)
            {

            }
        }
    }
}
