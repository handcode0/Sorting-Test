using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Triggers : MonoBehaviour
{
    public Rack RackParent;
    public string Check_Tag = "Bottle";
    public bool AlreadyContainAnObject = false;
    public UnityEvent EnteringEvents, ExitingEvent;
    [SerializeField]bool allSame = false;


    private void OnTriggerEnter(Collider other)
    {
        if (AlreadyContainAnObject) return;


        if (other.gameObject.CompareTag(Check_Tag))
        {
            GenerateBox generate = GenerateBox.instance;
            if (generate && generate.CurrentObject)
            {
                IndexDefined indexDefined = other.GetComponent<IndexDefined>();
                if (indexDefined != null && !RackParent.DefinedIndexes.Contains(indexDefined))
                {
                    RackParent.DefinedIndexes.Add(indexDefined);
                }
                DragableObject drag = generate.CurrentObject.GetComponent<DragableObject>();
                if (drag != null && !AlreadyContainAnObject)
                {
                    drag.Reached = true;
                    drag.Position = transform.position;
                    generate.CurrentObject.transform.SetParent(transform);
                }

                CheckAllSame(generate);

            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Check_Tag))
        {
            AlreadyContainAnObject = true;
            IndexDefined indexDefined = other.GetComponent<IndexDefined>();
            if (indexDefined != null && !RackParent.DefinedIndexes.Contains(indexDefined))
            {
                RackParent.DefinedIndexes.Add(indexDefined);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Check_Tag))
        {
           if(GenerateBox.instance.CurrentObject) GenerateBox.instance.CurrentObject.Reached = false;

            if (RackParent.DefinedIndexes.Contains(other.GetComponent<IndexDefined>()))
                RackParent.DefinedIndexes.Remove(other.GetComponent<IndexDefined>());
            

            if (AlreadyContainAnObject)
            {
                AlreadyContainAnObject = false;
            }
        }
            
    }

    private void CheckAllSame(GenerateBox Generate)
    {
        int level = PlayerPrefs.GetInt("Level");
        if (RackParent.DefinedIndexes.Count < Generate.LevelNeeds.LevelsDetails[level].rackStrength) return;
        bool allSame = true;
        var firstItemIndex = RackParent.DefinedIndexes[0].Index;
        for (int i = 1; i < RackParent.DefinedIndexes.Count; i++)
        {
            if (RackParent.DefinedIndexes[i].Index != firstItemIndex)
            {
                allSame = false;
                break;
            }
        }

        RackParent.GotEveryThing = allSame;

        if (allSame)
        {
            Debug.Log("All indexes are the same!");
        }
        else
        {
            Debug.Log("Indexes are not all the same.");
        }
    }
}
