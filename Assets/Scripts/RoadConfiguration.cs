using UnityEngine;

[CreateAssetMenu(menuName = "Road Configuration")]
public class RoadConfiguration : ScriptableObject
{
    public bool enableSpaces; 
    public bool varyHeight; 
    public float spaceSize;
    public bool isStairLevel;
}

