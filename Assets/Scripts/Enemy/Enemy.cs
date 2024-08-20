using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int speed;
    public int gems;

    public void Attack()
    {
        Debug.Log("My name is: " + this.gameObject.name);
    }
}
