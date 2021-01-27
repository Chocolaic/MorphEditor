using System;
using System.Collections.Generic;
using UnityEngine;

public class MorphData : ScriptableObject
{
    [SerializeField]
    public int shapeCount;
    [SerializeField]
    public List<MorphShape> shapes = new List<MorphShape>();
}
[Serializable]
public class MorphShape
{
    public string shapeName;
    public MorphFrame[] frames;
}
[Serializable]
public class MorphFrame
{
    public float weight;
    public Vector3[] deltaVertices;
}


