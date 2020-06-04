using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WorldBuilder : MonoBehaviour
{
    public BuildingBuilder BB;
    public bool genCityTile = false;
    public bool resetCity = false;

    public Vector2Int blockSize = new Vector2Int(3, 3);
    public Vector2Int cityTileSize = new Vector2Int(50, 50);
    Vector2Int currentBlockSize = new Vector2Int(0, 0);
    Vector2Int currentCityTileSize = new Vector2Int(0, 0);

    Vector2 orginalPos = new Vector2(0.0f, 0.0f);

    float roadWidth = 1.5f;
    float houseSeperation = 0.1f;
    float houseWidth = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        orginalPos = new Vector2(this.transform.position.x, this.transform.position.z);
        /*int height = Random.Range(0, 10);
        Vector3 h = BB.GenerateHouse(height);
        this.transform.Translate(Vector3.forward * (h.z + houseSeperation));*/
    }

    // Update is called once per frame
    void Update()
    {
        if (genCityTile)
        {
            genCityTile = false;
            GenerateCityTile();
        }
        if (resetCity)
        {
            resetCity = false;
            ClearCity();
        }
    }

    void GenerateCityTile()
    {        
        Vector3 h = new Vector3(.0f, .0f, .0f); //Constant???
      
        float lastX = this.transform.position.x;
        float lastY = this.transform.position.z;

        while (currentCityTileSize.x < cityTileSize.x)
        {
            float currentMX = this.transform.position.x;
            while (currentCityTileSize.y < cityTileSize.y)
            {
                //Build one block
                for (currentBlockSize.y = 0; currentBlockSize.y < blockSize.y && currentCityTileSize.y < cityTileSize.y; currentBlockSize.y++)
                {
                    for (currentBlockSize.x = 0; currentBlockSize.x < blockSize.x && currentCityTileSize.x < cityTileSize.x; currentBlockSize.x++)
                    {
                        if (!(currentBlockSize.x > 0 && currentBlockSize.x < (blockSize.x - 1) && currentBlockSize.y > 0 && currentBlockSize.y < (blockSize.y - 1)))
                        {
                            int height = Random.Range(0, 10);
                            h = BB.GenerateHouse(height, new Vector2(this.transform.position.x, this.transform.position.z));

                        }
                        this.transform.Translate(Vector3.left * (h.x + houseSeperation));
                    }
                    lastX = this.transform.position.x;
                    currentCityTileSize.y++;

                    this.transform.position = new Vector3(currentMX, this.transform.position.y, this.transform.position.z);
                    this.transform.Translate(Vector3.forward * (h.z + houseSeperation));
                }
                this.transform.Translate(Vector3.forward * (roadWidth));
            }
            currentCityTileSize.y = 0;
            currentCityTileSize.x += blockSize.x; //???? differnet sizes? and not every

            this.transform.position = new Vector3(lastX, this.transform.position.y, lastY);
            this.transform.Translate(Vector3.left * (roadWidth));
        }
        currentCityTileSize.x = 0;
        currentCityTileSize.y = 0;
    }

    void ClearCity()
    {
        GameObject cityP = GameObject.Find("City");
        foreach (Transform child in cityP.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        this.transform.position = new Vector3(orginalPos.x, 0.0f, orginalPos.y);
    }
}
