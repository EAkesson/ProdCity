using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WorldBuilder : MonoBehaviour
{
    public BuildingBuilder BB;
    public bool respawn = false;
    // Start is called before the first frame update
    void Start()
    {
        int height = Random.Range(0, 10);
        BB.GenerateHouse(height);
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
            this.transform.Translate(Vector3.back * h.z);
        }
    }
}
