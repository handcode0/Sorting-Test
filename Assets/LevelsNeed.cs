using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelContainer", menuName = "Levels/LevelContainer", order = 0)]
public class LevelsNeed : ScriptableObject
{
    public List<DataOnLevels> LevelsDetails;
}

[System.Serializable]
public class DataOnLevels
{
    public int LevelNumber;
    public int RackCount = 3;
    public int rackStrength = 3;
}