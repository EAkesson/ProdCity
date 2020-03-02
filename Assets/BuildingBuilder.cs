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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector3 GenerateHouse(int numOfSegments)
    {
        GameObject houseGameObj = new GameObject("Building" + 56);
        houseGameObj.transform.position = this.transform.position;
        houseGameObj.transform.rotation = this.transform.rotation;

        houseGameObj.transform.SetParent(this.transform);

        Mathf.Clamp(numOfSegments, minHeight, maxHeight);
        //int numOfSegments = Random.Range(minHeight, maxHeight);
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

        Vector3 houseDim = this.gameObject.GetComponentInChildren<MeshFilter>().mesh.bounds.size;
        this.transform.DetachChildren();
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

    // Update is called once per frame
    void Update()
    {
 
    }
}
