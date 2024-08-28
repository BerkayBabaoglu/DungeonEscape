using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    private bool moveLeft;
    private float moveSpeed = 3.0f;

    public void SetDirection(bool isLeft)
    {
        moveLeft = isLeft;
    }

    private void Start()
    {
        Destroy(this.gameObject, 5.0f);  // 5 saniye sonra asit etkisini yok et
    }

    private void Update()
    {
        if (moveLeft)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();  // Player'a hasar ver
            }
            Destroy(this.gameObject);  // Asit etkisini yok et
        }
    }
}

