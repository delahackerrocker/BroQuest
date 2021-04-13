using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpace : MonoBehaviour
{
    public Row row;
    public Column column;
    private GameObject tile;
    public int savedTileName = -1;
    public bool tileCreated = false;
    
    void Start()
    {
        //
    }


    void Update()
    {
        if (!tileCreated) {
            if (savedTileName != -1) {
                CreateSavedTile();
            }
        }
    }

    void CreateSavedTile()
    {
        tileCreated = true;
        
        if (savedTileName == 0) tile = (GameObject) Instantiate(Resources.Load("Prefabs/Tiles/Hallway_00"));
        if (savedTileName == 1) tile = (GameObject) Instantiate(Resources.Load("Prefabs/Tiles/Interior_00"));
        if (savedTileName == 2) tile = (GameObject) Instantiate(Resources.Load("Prefabs/Tiles/Interior_E"));
        if (savedTileName == 3) tile = (GameObject) Instantiate(Resources.Load("Prefabs/Tiles/Interior_N"));
        if (savedTileName == 4) tile = (GameObject) Instantiate(Resources.Load("Prefabs/Tiles/Interior_NE"));
        if (savedTileName == 5) tile = (GameObject) Instantiate(Resources.Load("Prefabs/Tiles/Interior_NW"));
        if (savedTileName == 6) tile = (GameObject) Instantiate(Resources.Load("Prefabs/Tiles/Interior_S"));
        if (savedTileName == 7) tile = (GameObject) Instantiate(Resources.Load("Prefabs/Tiles/Interior_SE"));
        if (savedTileName == 8) tile = (GameObject) Instantiate(Resources.Load("Prefabs/Tiles/Interior_SW"));
        if (savedTileName == 9) tile = (GameObject) Instantiate(Resources.Load("Prefabs/Tiles/Interior_W"));
        tile.transform.parent = this.transform;
        tile.transform.localPosition = new Vector3(0, 0, 0);
        tile.transform.transform.Rotate(0.0f, 0f, 0f, Space.Self);
    }
}