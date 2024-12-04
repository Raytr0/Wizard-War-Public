using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3f;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, life);
    }

    // Update is called once per frame

    //if does not run, comment out if and if-else
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInParent<Character>().takeDamage(15);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponentInParent<Enemy>().takeDamage(15); 
            
        }
        Destroy(gameObject);
    }
}
