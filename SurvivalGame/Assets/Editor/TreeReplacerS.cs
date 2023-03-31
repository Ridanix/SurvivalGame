using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
// Replaces Unity terrain trees with prefab GameObject.
// http://answers.unity3d.com/questions/723266/converting-all-terrain-trees-to-gameobjects.html

[ExecuteInEditMode]
public class TreeReplacerS : EditorWindow
{
    [Header("Settings")]
    public GameObject _tree;
    [Header("References")]
    public Terrain _terrain;
    //============================================
    [MenuItem("Window/My/TreeReplacer")]
    static void Init()
    {
        TreeReplacerS window = (TreeReplacerS)GetWindow(typeof(TreeReplacerS));
    }
    void OnGUI()
    {
        _terrain = (Terrain)EditorGUILayout.ObjectField(_terrain, typeof(Terrain), true);
        _tree = (GameObject)EditorGUILayout.ObjectField(_tree, typeof(GameObject), true);
        if (GUILayout.Button("Convert to objects"))
        {
            Convert();
        }
        if (GUILayout.Button("Clear generated trees"))
        {
            Clear();
        }
    }
    //============================================
    public void Convert()
    {
        TerainAwakening terrainTreesPrefs = _terrain.GetComponent<TerainAwakening>();
        TerrainData data = _terrain.terrainData;
        float width = data.size.x;
        float height = data.size.z;
        float y = data.size.y;
        // Create parent
        GameObject parent = GameObject.Find("TREES_GENERATED");
        if (parent == null)
        {
            parent = new GameObject("TREES_GENERATED");
        }
        // Create trees
        foreach (TreeInstance tree in data.treeInstances)
        {
            Vector3 position = new Vector3(tree.position.x * width, tree.position.y * y, tree.position.z * height);
             
            //Vector3 position = new Vector3(tree.position.x * data.detailWidth - (data.size.x / 2), tree.position.y * y - (data.size.y / 2), tree.position.z * data.detailHeight - (data.size.z / 2));
            GameObject treePrefab = Instantiate(terrainTreesPrefs.treePrefabs[tree.prototypeIndex], position, Quaternion.Euler(0, tree.rotation*10f, 0), parent.transform);
            //Instantiate(_tree, position, Quaternion.identity, parent.transform);
        }
    }
    public void Clear()
    {
        DestroyImmediate(GameObject.Find("TREES_GENERATED"));
    }
}
