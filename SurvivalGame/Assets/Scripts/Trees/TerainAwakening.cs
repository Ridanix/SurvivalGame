using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerainAwakening : MonoBehaviour
{
    Terrain terrain;
    public GameObject[] treePrefabs;

    private void Awake()
    {
        this.terrain = GetComponent<Terrain>();
        //Debug.LogWarning(terrain.terrainData.treeInstances.Length);
        //for (int i = 0; i < terrain.terrainData.treeInstances.Length; i++)
        //{
        //    Instantiate(treePrefabs[terrain.terrainData.treeInstances[i].prototypeIndex], terrain.terrainData.treeInstances[i].position, Quaternion.identity, this.transform);
        //}
    }
}
