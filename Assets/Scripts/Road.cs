using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public RoadConfiguration roadConfig;
    public GameObject roadPrefab;
    public float offset = 1;
    public Vector3 lastPos;
    public float stairHeight = 1f;
    public float stairDepth = 1f;
    private int roadCount = 0;
    private int crystalChange = 5;
    public void StartBuilding(Vector3 startPos)
    {
        lastPos = startPos;
        InvokeRepeating("CreateNewRoadPart", 1f, .05f);
    }
    public void CreateNewRoadPart()
    {
        if (!roadConfig.isInfinite && roadCount >= roadConfig.maxRoads) return;
        Vector3 spawnPos = Vector3.zero;
        if (roadConfig.isMixedLevel)
        {

            switch (Random.Range(0, 5))
            {

                default:
                    switch (Random.Range(0, 2))
                    {

                        case 0:
                            spawnPos = new Vector3((lastPos.x + offset), lastPos.y, lastPos.z);
                            lastPos = spawnPos;
                            break;
                        case 1:
                            spawnPos = new Vector3(lastPos.x, lastPos.y, (lastPos.z + offset));
                            lastPos = spawnPos;
                            break;

                    }
                    lastPos = spawnPos;
                    SpawnRoad(spawnPos);
                    break;
                case 0:
                    switch (Random.Range(0, 2))
                    {

                        case 0:
                            spawnPos = new Vector3((lastPos.x + offset), lastPos.y + stairHeight, lastPos.z);
                            lastPos = new Vector3(spawnPos.x + offset, spawnPos.y, spawnPos.z);
                            break;
                        case 1:
                            spawnPos = new Vector3(lastPos.x, lastPos.y + stairHeight, lastPos.z + stairDepth);
                            lastPos = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z + stairDepth);
                            break;

                    }
                    SpawnRoad(spawnPos);
                    SpawnRoad(lastPos);
                    break;

            }

        }
        else if (!roadConfig.isStairLevel)
        {
            switch (Random.Range(0, 2))
            {

                case 0:
                    spawnPos = new Vector3((lastPos.x + offset), lastPos.y, lastPos.z);
                    break;
                case 1:
                    spawnPos = new Vector3(lastPos.x, lastPos.y, (lastPos.z + offset));
                    break;

            }
            lastPos = spawnPos;
            SpawnRoad(spawnPos);
        }
        else if (roadConfig.isStairLevel)
        {

            switch (Random.Range(0, 2))
            {

                case 0:
                    spawnPos = new Vector3((lastPos.x + offset), lastPos.y + stairHeight, lastPos.z);
                    lastPos = new Vector3(spawnPos.x + offset, spawnPos.y, spawnPos.z);
                    break;
                case 1:
                    spawnPos = new Vector3(lastPos.x, lastPos.y + stairHeight, lastPos.z + stairDepth);
                    lastPos = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z + stairDepth);
                    break;

            }
            SpawnRoad(spawnPos);
            SpawnRoad(lastPos);

        }

    }

    public void SpawnRoad(Vector3 spawnPos)
    {

        roadCount++;
        SpawnCrystal(Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, 0, 0)));

    }

    public void SpawnCrystal(GameObject road)
    {

        if (roadCount % crystalChange == 0)
        {

            road.transform.GetChild(0).gameObject.SetActive(true);

        }

    }


}
