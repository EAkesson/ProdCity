using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WorldBuilder : MonoBehaviour
{
    public BuildingBuilder BB;
    public bool respawn = false;

    public Vector2Int blockSize = new Vector2Int(3, 3);
    public Vector2Int cityTileSize = new Vector2Int(50, 50);
    Vector2Int currentBlockSize = new Vector2Int(0, 0);
    Vector2Int currentCityTileSize = new Vector2Int(0, 0);

    float roadWidth = 1.5f;
    float houseSeperation = 0.1f;
    float houseWidth = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        /*int height = Random.Range(0, 10);
        Vector3 h = BB.GenerateHouse(height);
        this.transform.Translate(Vector3.forward * (h.z + houseSeperation));*/
    }

    void BuildHouse()
    {
        //BB.GenerateHouse();
    }
    // Update is called once per frame
    void Update()
    {
        if (respawn)
        {
            respawn = false;
            Vector3 h = new Vector3(.0f, .0f, .0f); //Constant???

            Vector2 orginalPos = new Vector2(this.transform.position.x, this.transform.position.z);     
            float lastX = orginalPos.x;
            float lastY = orginalPos.y;

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
                                h = BB.GenerateHouse(height);
                                
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
                                
                this.transform.position = new Vector3(lastX, this.transform.position.y, orginalPos.y);
                this.transform.Translate(Vector3.left * (roadWidth));          
            }
            currentCityTileSize.x = 0;
            currentCityTileSize.y = 0;
        }
    }
}
