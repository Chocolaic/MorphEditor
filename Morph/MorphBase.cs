using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MorphBase : MonoBehaviour
{
    protected SkinnedMeshRenderer smr;

    protected Mesh mesh;

    protected float vertSize = 0.02f;

    private void Awake()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
        mesh = smr.sharedMesh;
    }

}
