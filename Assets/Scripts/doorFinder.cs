using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class doorFinder : MonoBehaviour
{
    private static bool doorsOpen;
    private static Tilemap map;
    public static TileBase closedL;
    public static TileBase closedR;
    public static TileBase openL;
    public static TileBase openR;
    
    private static Vector3Int referenceCoordsCL = new Vector3Int(26,-1);
    private static Vector3Int referenceCoordsCR = new Vector3Int(27, -1);

    private static Vector3Int referenceCoordsOL = new Vector3Int(26,-2);
    private static Vector3Int referenceCoordsOR = new Vector3Int(27,-2);

    public static List<Vector3Int> leftDoor = new List<Vector3Int>();
    public static List<Vector3Int> rightDoor = new List<Vector3Int>();    

    private static int zPos;
    // Start is called before the first frame update
    void Start()
    {
        referenceCoordsCL.z = zPos;
        referenceCoordsCR.z = zPos;
        referenceCoordsOL.z = zPos;
        referenceCoordsOR.z = zPos;

        map = GetComponent<Tilemap>();
        closedL = map.GetTile(referenceCoordsCL);
        closedR = map.GetTile(referenceCoordsCR);
        openL = map.GetTile(referenceCoordsOL);
        openR = map.GetTile(referenceCoordsOR);
        
        leftDoor = findTiles(closedL);
        rightDoor = findTiles(closedR);

        zPos = (int) map.transform.position.z;
    }

    public List<Vector3Int> findTiles(TileBase t) {
        List<Vector3Int> answer = new List<Vector3Int>();
        Vector3Int currPos = new Vector3Int(0,0,0);
        for(int i = map.cellBounds.min.x; i < map.cellBounds.max.y; i++) {
            for(int j = map.cellBounds.min.y; j < map.cellBounds.max.y; j++) {
                currPos.Set(i,j,zPos);
                if(map.GetTile(currPos) == t) {
                    Debug.Log("ADDED TILE " + map.GetTile(currPos).name + " AT: " + currPos);
                    answer.Add(currPos);
                }
            }
        }
        return answer;
    }

    public static void Open() {
        Debug.Log("OPEN");
        map.SwapTile(closedL, openL);
        map.SwapTile(closedR, openR);
    }
    public static void Close() {
        Debug.Log("CLOSED");
        map.SwapTile(openL, closedL);
        map.SwapTile(openR, closedR);
    }

    public void UpdateDoors() {
        
    }


    // Update is called once per frame
    void Update()
    {

    }
}
