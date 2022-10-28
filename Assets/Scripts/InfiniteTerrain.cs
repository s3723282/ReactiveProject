using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTerrain : MonoBehaviour
{
    [Range(2, 100)] public int length = 5;
    public GameObject cube;
    public GameObject player;
    public int detailScale = 8;
    public int noiseHeight = 3;
    private Vector3 startPos = Vector3.zero;
    private Hashtable cubePos;

    private int XPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZPlayerMove => (int)(player.transform.position.z - startPos.z);

    private int XPlayerLocation => (int)Mathf.Floor(player.transform.position.x);
    private int ZPlayerLocation => (int)Mathf.Floor(player.transform.position.z);

    // Start is called before the first frame update
    void Start()
    {
        cubePos = new Hashtable();
        GenerateTerrain(length);
    }

    private void Update()
    {
        if (Mathf.Abs(XPlayerMove) >= 1 || Mathf.Abs(ZPlayerMove) >= 1)
        {
            GenerateTerrain(length);
        }
    }

    private void GenerateTerrain(int length)
    {
        Hashtable newTiles = new Hashtable();
        float cTime = Time.realtimeSinceStartup;

        for (int x = -length; x < length; x++)
        {
            for (int z = -length; z < length; z++)
            {

                Vector3 pos = new Vector3(x + XPlayerLocation,
                yNoise(x + XPlayerLocation, z + ZPlayerLocation, detailScale) * noiseHeight,
                z + ZPlayerLocation);

                if (!cubePos.ContainsKey(pos))
                {
                    GameObject cubeInstance = Instantiate(cube, pos, Quaternion.identity, transform);
                    Tile t = new Tile(cTime, cubeInstance);
                    cubePos.Add(pos, t);
                }
                else
                {
                    ((Tile)cubePos[pos]).cTimestamp = cTime;
                }
            }
        }

        foreach (Tile t in cubePos.Values)
        {
            if (!t.cTimestamp.Equals(cTime))
            {
                Destroy(t.tileObject);
            }
            else
            {
                newTiles.Add(t.tileObject, t);
            }
        }

        cubePos = newTiles;
        startPos = player.transform.position;
    }

    private class Tile
    {
        public float cTimestamp;
        public GameObject tileObject;

        public Tile(float cTimestamp, GameObject tileObject)
        {
            this.tileObject = tileObject;
            this.cTimestamp = cTimestamp;
        }
    }

    private float yNoise(int x, int z, float detailScale)
    {
        float xNoise = (x + transform.position.x) / detailScale;
        float zNoise = (z + transform.position.y) / detailScale;
        return Mathf.PerlinNoise(xNoise, zNoise);
    }
}
