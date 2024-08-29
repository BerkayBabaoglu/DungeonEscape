using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public GameObject shopPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                UIManager.Instance.OpenShop(player.diamondAmount);
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
}
