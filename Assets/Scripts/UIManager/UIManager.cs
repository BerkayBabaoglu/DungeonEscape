using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("UIManager is NULL!");
            }
            
            return instance;
        }
    }

    public Text playerGemsCount;


    public void OpenShop(int gemCount)
    {

    }


    public void Awake()
    {
        instance = this;
    }


}
