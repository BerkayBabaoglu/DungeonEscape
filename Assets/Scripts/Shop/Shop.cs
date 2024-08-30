using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public GameObject shopPanel;
    private int currentSelectedItem;
    private int currentItemCost;
    private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            _player = collision.GetComponent<Player>();

            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamondAmount);
            }
            
            shopPanel.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectionItem(int item)
    {

        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(90);
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(11);
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-68);
                currentSelectedItem = 2;
                currentItemCost = 100;
                break;


        }

    }

    public void BuyItem()
    {
        if(_player.diamondAmount >= currentItemCost)
        {

            if(currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }


            _player.diamondAmount -= currentItemCost;

            if(_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamondAmount);
            }
        }
        else
        {
            Debug.Log("You do not have enough gems.");
            shopPanel.SetActive(false);
        }
    }

}
