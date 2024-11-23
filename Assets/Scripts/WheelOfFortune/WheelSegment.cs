using UnityEngine;

[System.Serializable]
public class WheelSegment
{
    public string Label;
    public float Value;
    public Color Color;
    [HideInInspector] public SegmentAngles Angles = new SegmentAngles();
}

[System.Serializable]
[HideInInspector]
public class SegmentAngles
{
    public float DeltaAngle;
    public float StartAngle;
}

[System.Serializable]
public class Item
{
    public string Type;
    public int Usage;
    public int Weight;
}