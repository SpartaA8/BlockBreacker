using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpearHandler : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            //Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Bottom"))
        {
            Destroy(gameObject);
        }
    }
}
