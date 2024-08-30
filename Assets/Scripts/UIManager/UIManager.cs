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
    public Image selectionImg;
    public Text gemCountText;
    


    public void Awake()
    {
        instance = this;
    }

    public void OpenShop(int gemCount)
    {
        playerGemsCount.text = "" + gemCount + "G ";


    }



    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;

    }
    

    public void UpdateShopSelection(int yPosition)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPosition);
    }



}
