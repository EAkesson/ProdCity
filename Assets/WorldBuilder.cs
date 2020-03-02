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
    // Start is called before the first frame update
    void Start()
    {
        int height = Random.Range(0, 10);
        Vector3 h = BB.GenerateHouse(height);
        this.transform.Translate(Vector3.forward * (h.z + houseSeperation));
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
            int height = Random.Range(0, 10);
            Vector3 h = BB.GenerateHouse(height);            
            this.transform.Translate(Vector3.forward * (h.z + houseSeperation));
            currentBlockSize.x++;
            currentCityTileSize.x++;

            if (currentBlockSize.x >= blockSize.x)
            {
                this.transform.Translate(Vector3.forward * (roadWidth));
                currentBlockSize.x = 0;
            }
            if (currentBlockSize.y >= blockSize.y)
            {
                //this.transform.Translate(Vector3.left * (roadWidth));
                currentBlockSize.y = 0;
            }
            if (currentCityTileSize.x >= cityTileSize.x)
            {

            }
            //this.transform.Translate(Vector3.back * h.z);
            //this.transform.Translate(Vector3.back * h.z);
        }
    }
}
