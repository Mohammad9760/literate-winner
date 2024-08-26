using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public static int total = 0;
    public static int pickedup = 0;

    private void Awake()
    {
        total++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    // private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            pickedup++;
        }
    }
}
