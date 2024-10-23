using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateBox : MonoBehaviour
{
    #region Singleton
    public static GenerateBox instance;

    private void Awake()
    {
        if (!instance) instance = this;
    }
    #endregion

    [Header("ScriptAbleHandler")]
    [Space(10)]
    public LevelsNeed LevelNeeds;
    public List<Rack> RackCreated = new List<Rack>();


    [Header("Racks Data")]
    [Space(10)]
    public GameObject rackPrefab; 
    public Vector3 spacing = new Vector3(-4.66f, 1.65f, 0);

    [Header("Levels")]
    public List<Levels> GameplayLevels;
    public DragableObject CurrentObject;


    [Header(" UI ")]
    public GameObject MissionPanel;
    public GameObject LaodingPanel;


    void Start()
    {
        CreateRacks();
        StartTheLevel(PlayerPrefs.GetInt("Level"));
    }

    public void CreateRacks()
    {
        int prefOfLevel = PlayerPrefs.GetInt("Level");
        for (int i = 0; i < LevelNeeds.LevelsDetails[prefOfLevel].RackCount; i++)
        {
            int row = i / LevelNeeds.LevelsDetails[prefOfLevel].rackStrength; 
            int column = i % LevelNeeds.LevelsDetails[prefOfLevel].rackStrength;

            Vector3 position = new Vector3(column * spacing.x, row * spacing.y, 0);
            GameObject Rack_Created = Instantiate(rackPrefab, position, Quaternion.identity);
            if (i < LevelNeeds.LevelsDetails[prefOfLevel].RackCount - 1)
            {
                Rack racks = Rack_Created.GetComponent<Rack>();
                racks.GetBottleInstantiated();
            }

            if (i < LevelNeeds.LevelsDetails[prefOfLevel].RackCount)
            {
                RackCreated.Add(Rack_Created.GetComponent<Rack>());
                RackCreated[i].gameObject.SetActive(true);
            }
            StartCoroutine(AllColliders());
        }
    }

    IEnumerator AllColliders()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < RackCreated.Count; i++)
        {
            RackCreated[i].AllCollidersEnable();
        }
    }


    void StartTheLevel(int Level)
    {
        foreach (var item in GameplayLevels)
        {
            item.gameObject.SetActive(false);
        }

        GameplayLevels[Level].gameObject.SetActive(true);
    }

    public void CheckCompleteLevel()
    {
        int prefOfLevel = PlayerPrefs.GetInt("Level");

        if (prefOfLevel < 0 || prefOfLevel >= LevelNeeds.LevelsDetails.Count) return;
        int requiredRackCount = LevelNeeds.LevelsDetails[prefOfLevel].RackCount;

        if (RackCreated.Count < requiredRackCount) return;
        int completeCount = 0;
        for (int i = 0; i < requiredRackCount; i++)
        {
            if (RackCreated[i].GotEveryThing)
            {
                completeCount++;
            }
        }

        if (completeCount >= requiredRackCount - 1)
        {
            for (int i = 0; i < RackCreated.Count; i++)
            {
                RackCreated[i].SetEveryFalse();
            }
            LevelComplete();
            return;
        }
        else
        {
            return;
        }
    }


    public void LevelComplete()
    {
        MissionPanel.SetActive(true);
        Time.timeScale = 0;
    }


    public void NextLevel()
    {
        Load("Gameplay");
        if(PlayerPrefs.GetInt("Level") < LevelNeeds.LevelsDetails.Count - 1)
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        }
        else
        {
            int RandomLevel = Random.Range(0, LevelNeeds.LevelsDetails.Count - 1);
            PlayerPrefs.SetInt("Level", RandomLevel);
        }
    }
    public void Load(string Name)
    {
        StartCoroutine(CallScene(Name));
    }

    IEnumerator CallScene(string Name)
    {
        LaodingPanel.gameObject.SetActive(true);
        Time.timeScale = 1;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Name);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
