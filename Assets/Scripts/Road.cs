using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

    public RoadConfiguration roadConfig;
    public GameObject roadPrefab;
    public float offset = 0.707f;
    public Vector3 lastPos;
    public float stairHeight = 0.5f; 
    public float stairDepth = 0.5f;
    private int roadCount = 0;

    public void StartBuilding()
    {
        InvokeRepeating("CreateNewRoadPart", 1f, .25f);
    }

    public void CreateNewRoadPart()
    {
        Debug.Log("Create new road part");

        Vector3 spawnPos = Vector3.zero;
        float chance = Random.Range(0, 100);

        float zSpacing = roadConfig.enableSpaces ? roadConfig.spaceSize : 0; 
        float newY = roadConfig.isStairLevel ? lastPos.y + stairHeight : lastPos.y;
        float newZ = roadConfig.isStairLevel ? lastPos.z + stairDepth : lastPos.z + offset;

        if (chance < 50)
        {
            spawnPos = new Vector3(lastPos.x + offset, lastPos.y, lastPos.z + offset + zSpacing);
        }
        else
        {
            spawnPos = new Vector3(lastPos.x - offset, lastPos.y, lastPos.z + offset + zSpacing);
        }

        GameObject g = Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, 45, 0));
        lastPos = g.transform.position;

        roadCount++;
        if (roadCount % 5 == 0)
        {
            g.transform.GetChild(0).gameObject.SetActive(true);
        }
    }


}
