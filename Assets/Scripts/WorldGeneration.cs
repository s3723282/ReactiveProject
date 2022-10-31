using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public GameObject objectToSpawn;
    public static List<GameObject> GeneratedTiles = new List<GameObject>();
    private List<Vector3> TileGenerator = new List<Vector3>();

    [SerializeField] private GameObject tilePrefab;

    private int radius = 100;

    // Start is called before the first frame update
    void Start()
    {
        Path pathGenerator = new Path(radius);

        for (int x = 0; x < radius; x++)
        {
            for (int z = 0; z < radius; z++)
            {
                GameObject tile = Instantiate(tilePrefab,
                    new Vector3(x * 1f, 0, z * 1f),
                    Quaternion.identity);

                tile.transform.SetParent(this.transform);

                GeneratedTiles.Add(tile);
                TileGenerator.Add(tile.transform.position);
                pathGenerator.AssignTopAndBottomTiles(z, tile);
            }
        }
        SpawnObject();

        //World Generated
        pathGenerator.GeneratePath();

        foreach (var pObject in pathGenerator.GetGeneratedPath)
        {
            pObject.SetActive(false);
        }
    }

    private void SpawnObject()
    {
        for (int c = 0; c < 20; c++)
        {
            GameObject toPlaceObject = Instantiate(objectToSpawn,
            ObjectSpawnLocation(),
            Quaternion.identity);
        }
    }

    private Vector3 ObjectSpawnLocation()
    {
        int rndIndex = Random.Range(0, TileGenerator.Count);

        Vector3 newPos = new Vector3(
            TileGenerator[rndIndex].x,
            TileGenerator[rndIndex].y + 0.5f,
            TileGenerator[rndIndex].z
            );

        TileGenerator.RemoveAt(rndIndex);
        return newPos;

    }
    
}