using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("GameManager is Null!");
            }

            return instance;
        }
    }

    public bool HasKeyToCastle { get; set; }

    private void Awake()
    {
        instance = this;
    }

}
