using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBases : MonoBehaviour
{
    public int pieces;
    public float distance;
    public GameObject pBase;
 

    public Vector3 backVec;
    public Vector3 startVec;

    [Header("Trees")]
    public GameObject Tree;
    public Vector2 rangeX;
    public Vector2 rangeZ;


    private void Start()
    {
        backVec = InitializeBases(pieces, distance, pBase);
    }

    public Vector3 InitializeBases(int pieces, float distance, GameObject pBases)
    {
        // Summe der gesamten Distanz
        float sumDist = 0;
        Vector3 pos = new Vector3(0,0,0);
        for (int i = 0; i < pieces; i++)
        {
            sumDist = sumDist + distance;
            pos = new Vector3(0,0,sumDist);
            
            SpawnNewBase(pos);
            
        }
        return pos;

    }

    public void SpawnNewBase(Vector3 pos)
    {
        GameObject cloneBase = Instantiate(pBase, pos, pBase.transform.rotation);
        SpawnTree(pos, rangeX, rangeZ, cloneBase.transform);  //eine Seite
        SpawnTree(pos,-rangeX, rangeZ, cloneBase.transform);    // andere Seite

    }

    public void SpawnTree(Vector3 pos, Vector2 MinMaxX, Vector2 MinMaxZ, Transform parent)
    {
        Vector3 posTree = new Vector3((pos.x + Random.Range(MinMaxX.x, MinMaxX.y)), 0, (pos.z + Random.Range(MinMaxZ.x, MinMaxZ.y)));
        GameObject cloneTree = Instantiate(Tree, posTree, Quaternion.identity);
        cloneTree.transform.SetParent(parent);
    }

    public void SpawnAtStart()
    {
        SpawnNewBase(startVec);
    }

    public void SpawnAtBack()
    {
        SpawnNewBase(backVec);
    }

    /*

    public void SpawnTreeL(Vector3 posL)
    {
        GameObject clone = Instantiate(Tree, posL, Tree.transform.rotation);
        clone.transform.SetParent(this.transform);
    }

    public void SpawnTreeR(Vector3 posR)
    {
        Instantiate(Tree, posR, Tree.transform.rotation);
    }
    */



    /*
    public void SpawnAtStartTreeL()
    {
        SpawnTreeL(startVec);
    }

    public void SpawnAtBackTreeL()
    {
        SpawnTreeL(backVec);
    }

    public void SpawnAtStartTreeR()
    {
        SpawnTreeR(startVec);
    }

    public void SpawnAtBackTreeR()
    {
        SpawnTreeR(backVec);
    }
    */
}
