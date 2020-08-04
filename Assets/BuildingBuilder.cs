using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilder : MonoBehaviour
{
    public int minHeight = 2;
    public int maxHeight = 10;
    public float perlNoiseStepRes = 0.01f;
    public GameObject[] groundSegments;
    public GameObject[] middleSegments;
    public GameObject[] topSegments;    

    float lotWidth;
    static int houseNumber = 0;
    Vector2 houseDimensions;
    GameObject city;

    // Start is called before the first frame update
    void Start()
    {
        city = new GameObject("City");
    }

    public float getLargestHouseDim()
    {
        Vector3 houseDim = groundSegments[0].GetComponentInChildren<Renderer>().bounds.size;
        houseDimensions = new Vector2(houseDim.x, houseDim.z);
        Debug.Log(houseDimensions);
        float largestDim = Mathf.Max(houseDim.x, houseDim.z);        
        return largestDim;
    }

    public void setParams(float lotW)
    {
        lotWidth = lotW;
    }

    public void GenerateHouse(int numOfSegments, Vector2 pos, float houseRot)
    {
        houseNumber++;
        GameObject houseGameObj = new GameObject("Building" + houseNumber);
        houseGameObj.transform.position = this.transform.position;
        houseGameObj.transform.rotation = this.transform.rotation;        

        houseGameObj.transform.SetParent(city.transform);

        //houseGameObj.transform.Rotate(0.0f, houseRot, 0.0f, Space.Self);

        //Mathf.Clamp(numOfSegments, minHeight, maxHeight);    
        numOfSegments = GenHeightFromPNoise(pos);
        float totalHeight = 0.0f;

        //Spawn base
        totalHeight += GenerateSegment(groundSegments, totalHeight, houseGameObj);

        for (int i = 2; i < numOfSegments; i++)
        {
            //Spawn new house part
            totalHeight += GenerateSegment(middleSegments, totalHeight, houseGameObj);
        }

        //Spawn roof
        totalHeight += GenerateSegment(topSegments, totalHeight, houseGameObj);

        houseGameObj.transform.Rotate(0.0f, houseRot, 0.0f, Space.Self);
        //this.transform.DetachChildren();        
    }

    float GenerateSegment(GameObject[] segmentPool, float placementHeight, GameObject houseGo)
    {
        int segmentNum = Random.Range(0, segmentPool.Length-1);
        GameObject segment = Instantiate(segmentPool[segmentNum], this.transform.position + new Vector3(0, placementHeight, 0), this.transform.rotation) as GameObject;
        segment.transform.SetParent(houseGo.transform);

        segment.transform.Translate(houseDimensions.x / 2, 0.0f, houseDimensions.y / 2, Space.Self);        

        float segmentHeight = segment.GetComponentInChildren<MeshFilter>().mesh.bounds.size.y;
        return segmentHeight;
    }

    int GenHeightFromPNoise(Vector2 pos)
    {
        int height = 0;         
        Vector2 posInGrid = pos / lotWidth; //TODO: do i need too round        
        Vector2 posInPelN = posInGrid * perlNoiseStepRes;
        float noiseVal = Mathf.Clamp(Mathf.PerlinNoise(posInPelN.x, posInPelN.y), 0.0f, 1.0f); //Need to clamp due too PerlinNoise return can be slightly less than 0.0f or slightly exceed 1.0f.       
        height = 2 + Mathf.FloorToInt((maxHeight-2) * noiseVal);               
        return height;
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
