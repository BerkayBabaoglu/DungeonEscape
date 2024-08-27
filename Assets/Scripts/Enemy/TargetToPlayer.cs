using UnityEngine;

public class TargetToPlayer : MonoBehaviour
{
    private Enemy enemy;
    private Player player;
    public float speed = 1.3f;
    private bool isPlayerInRange = false;

    private void Start()
    {
        if (enemy == null)
        {
            enemy = GetComponentInParent<Enemy>();
        }
    }

    //private void Update()
    //{
    //    if (isPlayerInRange && player != null)
    //    {
            
    //        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Player entered trigger");
                enemy.OnPlayerInteracted();
                isPlayerInRange = true; //oyuncu alanda
                
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger");
            enemy.OnPlayerExited();
            isPlayerInRange = false; 
            
        }
    }
}
