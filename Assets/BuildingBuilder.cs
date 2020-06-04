﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilder : MonoBehaviour
{
    public int minHeight = 2;
    public int maxHeight = 10;
    static public int houseNumber = 0;
    public GameObject[] groundSegments;
    public GameObject[] middleSegments;
    public GameObject[] topSegments;
    GameObject city;

    // Start is called before the first frame update
    void Start()
    {
        city = new GameObject("City");
    }

    public Vector3 GenerateHouse(int numOfSegments, Vector2 pos)
    {
        houseNumber++;
        GameObject houseGameObj = new GameObject("Building" + houseNumber);
        houseGameObj.transform.position = this.transform.position;
        houseGameObj.transform.rotation = this.transform.rotation;

        houseGameObj.transform.SetParent(city.transform);

        //Mathf.Clamp(numOfSegments, minHeight, maxHeight);    
        numOfSegments = GenHeightFromPNoise(pos);
        float totalHeight = 0.0f;

        //Spawn base
        totalHeight += GenerateSegment(groundSegments, totalHeight, houseGameObj);

        for (int i = 2; i < numOfSegments; i++)
        {
            //Spawn new base part
            totalHeight += GenerateSegment(middleSegments, totalHeight, houseGameObj);
        }

        //Spawn roof
        totalHeight += GenerateSegment(topSegments, totalHeight, houseGameObj);

        Vector3 houseDim = city.gameObject.GetComponentInChildren<MeshFilter>().mesh.bounds.size;
        //this.transform.DetachChildren();
        return houseDim;
    }

    float GenerateSegment(GameObject[] segmentPool, float placementHeight, GameObject houseGo)
    {
        int segmentNum = Random.Range(0, segmentPool.Length-1);
        GameObject segment = Instantiate(segmentPool[segmentNum], this.transform.position + new Vector3(0, placementHeight, 0), this.transform.rotation) as GameObject;
        segment.transform.SetParent(houseGo.transform);

        float segmentHeight = segment.GetComponentInChildren<MeshFilter>().mesh.bounds.size.y;
        return segmentHeight;
    }

    int GenHeightFromPNoise(Vector2 pos)
    {
        int height = 0;
        height = 2 + Mathf.FloorToInt((maxHeight-2) * Mathf.PerlinNoise(pos.x, pos.y));
        return height;
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
