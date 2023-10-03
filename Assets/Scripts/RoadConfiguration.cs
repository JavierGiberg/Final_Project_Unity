using UnityEngine;

[CreateAssetMenu(menuName = "Road Configuration")]
public class RoadConfiguration : ScriptableObject
{
    public bool enableSpaces;
    public bool varyHeight;
    public float spaceSize;
    public bool isStairLevel;
    public bool isMixedLevel;
    public int maxRoads = 100;
    public bool isInfinite = false;
}

