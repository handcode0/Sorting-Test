using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    #region Singleton
    public static LevelHandler instance;

    private void Awake()
    {
        if (!instance) instance = this;
    }
    #endregion

    private void Start()
    {
        
    }
}


