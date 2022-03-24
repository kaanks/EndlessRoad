using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    public float spawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 5;
    private List<GameObject> activeTile = new List<GameObject>();

    public Transform playerTransform;
    void Start()
    {
      for (int i = 0; i < numberOfTiles; i++)
      {
          if(i==0)
          SpawnTile(0);
          else
          SpawnTile(Random.Range(0,tilePrefabs.Length));
      }
    }

    void Update()
    {
        if(playerTransform.position.z - 35 > spawn-(numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0,tilePrefabs.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex],transform.forward * spawn,transform.rotation);
        activeTile.Add(go);
        spawn+= tileLength;
    }
    private void DeleteTile()
    {
        Destroy(activeTile[0]);
        activeTile.RemoveAt(0);
    }


}
