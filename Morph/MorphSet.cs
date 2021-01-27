using UnityEngine;

[ExecuteInEditMode]
public class MorphSet : MonoBehaviour
{
    public MorphData morphData;
    private SkinnedMeshRenderer smr;
    private void Awake()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
        UpdateBlendShape();
    }
    public void UpdateBlendShape()
    {
        if (!morphData)
            return;
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
