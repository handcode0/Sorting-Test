using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rack : MonoBehaviour
{
    public bool GotEveryThing = false;
    public List<Triggers> PositionsForBottles;
    public List<GameObject> BottleInRacks;
    public List<IndexDefined> DefinedIndexes = new List<IndexDefined>();

    private void Start()
    {
        
    }

    public void AllCollidersEnable()
    {
        foreach (var item in PositionsForBottles)
        {
            Collider Col = item.GetComponent<Collider>();
            Col.enabled = true;
        }
        foreach (var item in BottleInRacks)
        {
            Collider Col = item.GetComponent<Collider>();
            Col.enabled = true;
        }
    }
    public void GetBottleInstantiated()
    {
        for (int i = 0; i < BottleInRacks.Count; i++)
        {
            GameObject BottleGotInRacks = Instantiate(BottleInRacks[i], PositionsForBottles[i].transform.localPosition,
                PositionsForBottles[i].transform.localRotation, 
                PositionsForBottles[i].transform);


            PositionsForBottles[i].AlreadyContainAnObject = true;
            BottleGotInRacks.transform.localPosition = Vector3.zero;
            BottleGotInRacks.GetComponent<Collider>().enabled = true;
            IndexDefined Indexs = BottleGotInRacks.GetComponent<IndexDefined>();
            DefinedIndexes.Add(Indexs);
        }
    }

    public void SetEveryFalse()
    {
        if (PositionsForBottles.Count > 0)
        {
            foreach (var item in PositionsForBottles)
            {
                Collider Col = item.GetComponent<Collider>();
                Col.enabled = false;
            }
        }

        if (BottleInRacks.Count > 0)
        {
            foreach (var item in BottleInRacks)
            {
                Collider Col = item.GetComponent<Collider>();
                Col.enabled = false;
            }
        }
    }
}
