using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class WorldBuilder : MonoBehaviour
{
    public BuildingBuilder BB = null;
    public OverlapWFC OlapWFC = null;        
        
    public Vector2Int cityTileSize = new Vector2Int(50, 50);    

    public int minBuildHeight = 2;
    public int maxBuildHeight = 10;
    public float houseSeperation = 0.1f;

    public int mapSeed = 0;
    public int perlSeed = 0;
    public float perlNoiseStepRes = 0.09f;    
   
    float houseWidth = 1.2f;
    float tileSize = 1.3f;

    private float houseRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {      
        houseWidth = BB.getLargestHouseDim();
        tileSize = houseWidth + houseSeperation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            StartCoroutine(GenerateCityTileFromMap());
        }
        if (Input.GetKeyDown("r"))
        {            
            ClearCity();
        }           
    }

    IEnumerator GenerateCityTileFromMap()
    {

        OlapWFC.setWnD(cityTileSize.x, cityTileSize.y);
        OlapWFC.seed = mapSeed;
        OlapWFC.Generate();
        OlapWFC.Run();
        yield return new WaitForSeconds(0); //Need to wait for CityMap to complete

        BB.setParams(tileSize, perlSeed, minBuildHeight, maxBuildHeight, perlNoiseStepRes);

        GameObject cityMap = GameObject.Find("CityMap");                

        Debug.Log(cityMap.transform.GetChild(0).childCount);
               
        
        foreach(Transform cityObj in cityMap.transform.GetChild(0))
        {         
            Vector2Int objPos = new Vector2Int((int)cityObj.localPosition.x, (int)cityObj.localPosition.y);
            this.transform.position = new Vector3(objPos.x * tileSize, 0.0f, objPos.y * tileSize);

            if (cityObj.gameObject.tag.Contains("House"))
            {
                if (!courtyardCheck(cityMap, objPos))
                {
                    houseRotation = checkHouseRot(cityMap, objPos);
                    BB.GenerateHouse(new Vector2(this.transform.position.x, this.transform.position.z), houseRotation);
                }                
            }            
        }
    }

    bool courtyardCheck(GameObject cityMap, Vector2Int pos)
    {             
        if(pos.x == 0 || pos.x == cityTileSize.x-1 || pos.y == 0 || pos.y == cityTileSize.y-1)
        {
            return false;
        }
        if(getObjTag(cityMap, (pos.x - 1 + pos.y * cityTileSize.x)).Contains("House"))
        {
            if(getObjTag(cityMap, (pos.x + 1 + pos.y * cityTileSize.x)).Contains("House"))
            {
                if(getObjTag(cityMap, (pos.x + (pos.y-1) * cityTileSize.x)).Contains("House"))
                {
                    if(getObjTag(cityMap, (pos.x + (pos.y+1) * cityTileSize.x)).Contains("House"))
                    {
                        return true;
                    }
                }
            }
        } 
        return false;
    }

    string getObjTag(GameObject cityMap, int number)
    {
        return cityMap.transform.GetChild(0).GetChild(number).tag;
    }

    float checkHouseRot(GameObject cityMap, Vector2Int pos)
    {      
        if (pos.x == cityTileSize.x-1 || !getObjTag(cityMap, (pos.x+1 + pos.y*cityTileSize.x)).Contains("House"))
        {
            return 0.0f;
        }
        if (pos.x == 0 || !getObjTag(cityMap, (pos.x-1 + pos.y*cityTileSize.x)).Contains("House"))
        {
            return 180.0f;
        }
        if (pos.y == 0 || !getObjTag(cityMap, (pos.x + (pos.y - 1)*cityTileSize.x)).Contains("House"))
        {
            return 90.0f;
        }

        return 270.0f;        
    }

    void ClearCity()
    {
        GameObject cityObj = GameObject.Find("City");
        foreach (Transform child in cityObj.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        //this.transform.position = new Vector3(orginalPos.x, 0.0f, orginalPos.y);
        this.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
