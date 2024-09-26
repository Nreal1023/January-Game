using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAcess : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (gameObject.CompareTag("RedRazers") && collision.CompareTag("BlueBox"))
        {
            Player target = collision.GetComponent<Player>();
            if (target != null)
            {
                target.TakeDamage(1);
            }
            Destroy(gameObject);  
        }

        
        else if (gameObject.CompareTag("BlueRazers") && collision.CompareTag("RedBox"))
        {
            Player target = collision.GetComponent<Player>();
            if (target != null)
            {
                target.TakeDamage(1);
            }
            Destroy(gameObject); 
        }
    }
}
