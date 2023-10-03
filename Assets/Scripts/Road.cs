using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

    public RoadConfiguration roadConfig;
    public GameObject roadPrefab;
    public float offset = 0.5f;
    public Vector3 lastPos;
    public float stairHeight = 1f; 
    public float stairDepth = 1f;
    private int roadCount = 0;

    public void StartBuilding()
    {

        lastPos = new Vector3(10, 0, 4); // Needs to be added as paramater to CreateNewRoadPart Func - Last Road Pos
        InvokeRepeating("CreateNewRoadPart", 1f, .05f);

    }


    public void CreateNewRoadPart()
    {
        Debug.Log("Create new road part");

        Vector3 spawnPos;
        float zSpacing = roadConfig.enableSpaces ? roadConfig.spaceSize : 0;

        // Always spawn straight without any offset in the x direction
        spawnPos = new Vector3(lastPos.x, lastPos.y, lastPos.z + 1 + zSpacing);

        if (roadConfig.isStairLevel)
        {
            float newY = roadConfig.isStairLevel ? lastPos.y + stairHeight : lastPos.y;
            float newZ = roadConfig.isStairLevel ? lastPos.z + stairDepth : lastPos.z;
            spawnPos.y = newY;
            spawnPos.z = newZ;
        }

        GameObject g = Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, 0, 0));
        lastPos = spawnPos;
        roadCount++;
        if (roadCount % 5 == 0)
        {
            g.transform.GetChild(0).gameObject.SetActive(true);
        }
    }


}
