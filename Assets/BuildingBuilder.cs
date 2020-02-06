using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilder : MonoBehaviour
{
    public int minHeight = 2;
    public int maxHeight = 10;
    public GameObject[] groundSegments;
    public GameObject[] middleSegments;
    public GameObject[] topSegments;
    public bool respawn = false;

    // Start is called before the first frame update
    void Start()
    {
        GenerateHouse();
    }

    void GenerateHouse()
    {
        int numberOfSegments = Random.Range(minHeight, maxHeight);
        float totalHeight = 0.0f;

        //Spawn base
        totalHeight += GenerateSegment(groundSegments, totalHeight);

        for (int i = 2; i < numberOfSegments; i++)
        {
            //Spawn new base part
            totalHeight += GenerateSegment(middleSegments, totalHeight);
        }

        //Spawn roof
        totalHeight += GenerateSegment(topSegments, totalHeight);
    }

    float GenerateSegment(GameObject[] segmentPool, float placementHeight)
    {
        int segmentNum = Random.Range(0, segmentPool.Length-1);
        GameObject segment = Instantiate(segmentPool[segmentNum], this.transform.position + new Vector3(0, placementHeight, 0), transform.rotation) as GameObject;
        segment.transform.SetParent(this.transform);

        float segmentHeight = segment.GetComponentInChildren<MeshFilter>().mesh.bounds.size.y;
        return segmentHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (respawn)
        {
            respawn = false;
            GenerateHouse();
        }
    }
}
