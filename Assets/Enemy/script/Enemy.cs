using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static int killed = 0;
    public static int count = 0;

    private int HP = 3;
    public Slider HPbar;

    public void TakeDamage(int damage = 1)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
            return;
        }
        HPbar.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, (float)HP / 3);
        HPbar.value = HP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.CompareTag("Bullet"))
            TakeDamage();
    }

    private void Start()
    {
        count++;
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        killed++;
    }
}
