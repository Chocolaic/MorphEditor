using UnityEngine;

namespace MorphEditor
{
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    [ExecuteInEditMode]
    public class MorphSet : MonoBehaviour
    {
        public MorphData morphData;
        internal SkinnedMeshRenderer smr;
        private void Awake()
        {
            smr = GetComponent<SkinnedMeshRenderer>();
            UpdateBlendShape();
        }
        public void UpdateBlendShape()
        {
            if (!morphData)
                return;
            smr.sharedMesh.ClearBlendShapes();
            for (int shape = 0; shape < morphData.shapeCount; shape++)
            {
                MorphShape morphShape = morphData.shapes[shape];
                if (smr.sharedMesh.GetBlendShapeIndex(morphShape.shapeName) >= 0)
                    continue;
                foreach (MorphFrame morphFrame in morphShape.frames)
                {
                    smr.sharedMesh.AddBlendShapeFrame(morphShape.shapeName, morphFrame.weight, morphFrame.deltaVertices, null, null);
                }
            }
            smr.sharedMesh.RecalculateNormals();
            smr.sharedMesh.RecalculateTangents();
        }
    }
}
