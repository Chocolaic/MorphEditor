﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MorphEditor
{
    public class MorphEditor
    {
        public MorphSet ms;
        internal PointFilter pointFilter = new PointFilter();
        internal MorphData data;
        internal Vector3[] dVertices;
        internal List<int> mark = new List<int>();

        private SkinnedMeshRenderer smr;
        public void Init(SkinnedMeshRenderer smr, int shapeIndex)
        {
            GizmoUtilities.editor = this;
            pointFilter.editor = this;
            this.smr = smr;
            if (!(ms = smr.GetComponent<MorphSet>()))
                ms = smr.gameObject.AddComponent<MorphSet>();
            if (ms.morphData != null)
                data = ms.morphData;
            else
                CreateAsset();
            dVertices = data.shapes[shapeIndex].frames[0].deltaVertices;
            pointFilter.points = smr.sharedMesh.vertices;
            mark.Clear();
            smr.sharedMesh.GetBlendShapeFrameVertices(shapeIndex, 0, dVertices, null, null);
            for (int i = 0; i < dVertices.Length; i++)
            {
                if (dVertices[i] != Vector3.zero)
                    mark.Add(i);
            }
        }
        public void Enable()
        {
            pointFilter.Enable();
        }
        public void Save(int shapeIndex)
        {
            Disable();
            for(int i = 0; i < dVertices.Length; i++)
            {
                if (!mark.Contains(i))
                    dVertices[i] = Vector3.zero;
            }
            data.shapes[shapeIndex].frames[0].deltaVertices = dVertices;
            string savePath = EditorUtility.SaveFilePanel("Save Asset", "./", "NewMorphData", "asset");
            if (!string.IsNullOrEmpty(savePath) && savePath.StartsWith(Application.dataPath) && savePath.EndsWith(".asset"))
            {
                int subIndex = Application.dataPath.Length;
                savePath = "Assets" + savePath.Substring(subIndex, savePath.Length - subIndex);
                AssetDatabase.CreateAsset(data, savePath);
                
                ms.morphData = data;
                //EditorPrefs.SetString("morphAssetPath", savePath);
            }
            else
                Debug.LogError("Please Select in " + Application.dataPath + " with correct filename.");
        }
        public void Disable()
        {
            pointFilter.Disable();
        }
        public void CreateAsset()
        {
            data = ScriptableObject.CreateInstance<MorphData>();
            Mesh mesh = smr.sharedMesh;
            data.shapeCount = mesh.blendShapeCount;
            for (int i = 0; i < data.shapeCount; i++)
            {
                MorphShape shapeData = new MorphShape();
                shapeData.shapeName = mesh.GetBlendShapeName(i);
                int frameCount = mesh.GetBlendShapeFrameCount(i);
                MorphFrame[] frames = new MorphFrame[frameCount];
                for (int j = 0; j < frameCount; j++)
                {
                    MorphFrame frameData = new MorphFrame();
                    frameData.deltaVertices = new Vector3[mesh.vertexCount];
                    frameData.weight = mesh.GetBlendShapeFrameWeight(i, j);
                    mesh.GetBlendShapeFrameVertices(i, j, frameData.deltaVertices, null, null);
                    frames[j] = frameData;
                }
                shapeData.frames = frames;
                data.shapes.Add(shapeData);
            }
        }
    }
}
