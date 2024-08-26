using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    private Spider _spider;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
        _spider = GetComponent<Spider>();
    }

    private void Update()
    {
        if(_spider != null)
        {
            if (_spider.Flip(true))
            {
                transform.Translate(Vector3.left * 3 * Time.deltaTime);
            }
            else if (_spider.Flip(false))
            {
                transform.Translate(Vector3.right * 3 * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            IDamagable hit = other.GetComponent<IDamagable>();

            if(hit != null)
            {
                hit.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}
