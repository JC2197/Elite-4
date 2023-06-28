using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Opener : MonoBehaviour
{

    public GameObject Checkpoints;

    public GameObject Player;

    private static Transform pTransform;
    
    public Tilemap DoorMap;

    public Tilemap RefMap;

    private static GameObject grid;
    public BoundsInt refBounds;

    public static TileBase ClosedGateL;
    public static TileBase ClosedGateR;

    public static TileBase OpenGateL;
    public static TileBase OpenGateR;

    public static TileBase ClosedGateT;
    public static TileBase ClosedGateB;

    public static TileBase OpenGateT;
    public static TileBase OpenGateB;

    public static TileBase ClosedExitL;
    public static TileBase ClosedExitR;
    public static TileBase OpenExitL;
    public static TileBase OpenExitR;

    private List<TileBase> closedTiles = new List<TileBase>();
    private List<TileBase> openTiles = new List<TileBase>();

    private List<TileBase> doorTiles = new List<TileBase>();
    private List<Vector3Int> doorTilePositions = new List<Vector3Int>();

    private CheckpointManager manager;


    // Start is called before the first frame update
    void Start()
    {

        manager = Checkpoints.GetComponent<CheckpointManager>();

        grid = DoorMap.gameObject.GetComponentInParent<Grid>().gameObject;

        int i = 0;
        foreach(Vector3Int refPosition in refBounds.allPositionsWithin)
        {
            if(RefMap.HasTile(refPosition))
            {
                i++;
                if(i <= 6) 
                {
                    openTiles.Add(RefMap.GetTile(refPosition));
                } else {
                    closedTiles.Add(RefMap.GetTile(refPosition));
                }
                }
        }

        foreach(TileBase closed in closedTiles)
        {
            //Debug.Log("CLOSED " + closedTiles.IndexOf(closed) + " : " + closed.name);
        }
        foreach(TileBase opened in openTiles)
        {
            //Debug.Log("OPENED : " + opened.name);
        }

        
        foreach(Vector3Int doorsPos in DoorMap.cellBounds.allPositionsWithin) {
            
            if(DoorMap.HasTile(doorsPos))
            {
                doorTiles.Add(DoorMap.GetTile(doorsPos));
                doorTilePositions.Add(doorsPos);

            }
        }
        int x = 0;
        foreach(TileBase doorTile in doorTiles) 
        {
            //Debug.Log("DOOR: " + doorTile.name + " POS: " + doorTilePositions[x]);
            x++;
        }

    }

    private Vector3Int closestTilePos(Transform t)
    {
        Vector3Int converted = new Vector3Int(1000,1000,1000);
        converted.x = (int) t.position.x;
        converted.y = (int) t.position.y;
        converted.z = (int) t.position.z;
        float dist;
        float minDist = 1000f;
        int index = 1000;
        foreach(Vector3Int pos in doorTilePositions)
        {
            dist = Vector3Int.Distance(pos, converted);
            if(dist < minDist)
            {
                minDist = dist;
                index = doorTilePositions.IndexOf(pos);
            }
        }
        return doorTilePositions[index];

    }

    public void open()
    {
        Vector3Int openPos = new Vector3Int(1000,1000,0);
        if(manager.checkClosest())
        {
            Vector3Int targetPos = closestTilePos(manager.getPlayerTransform());
            int targetIndex = doorTilePositions.IndexOf(targetPos);
            TileBase targetTile = doorTiles[targetIndex];

            if(DoorMap.HasTile(targetPos))
            {
                    if(targetTile.name.Contains("GATE")) 
                    {
                        for(int i = targetPos.x-2; i < targetPos.x+2; i++)
                        {
                            for(int j = targetPos.y-2; j < targetPos.y+2; j++)
                            {
                                openPos.x = i;
                                openPos.y = j;     
                                DoorMap.SetTile(openPos, null);
                            }
                        }
                    } 
                    else
                    {
                        for(int i = targetPos.x-2; i < targetPos.x+2; i++)
                        {
                            for(int j = targetPos.y-2; j < targetPos.y+2; j++)
                            {
                                openPos.x = i;
                                openPos.y = j;
                                if(DoorMap.HasTile(openPos)) 
                                {
                                    targetTile = DoorMap.GetTile(openPos);
                                    if(!targetTile.name.Contains("OPEN")) 
                                    {
                                        DoorMap.SetTile(openPos, openTiles[closedTiles.IndexOf(targetTile)]);
                                    }
                                }     
                            }
                        }
                }
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        open();
    }
}
