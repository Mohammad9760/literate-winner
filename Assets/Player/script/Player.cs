using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private PlayerController controller;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform launchPoint;

    private float fireTimeout;
    public float fireDelay;

    private Vector2 diePos;


    public float timePlayed = 0.0f;


    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        #region shooting

        if (fireTimeout <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(projectilePrefab, launchPoint.position, transform.rotation);
                fireTimeout = fireDelay;

            }
        }
        else
        {
            fireTimeout -= Time.deltaTime;
        }

        #endregion
       
       UpdateUI();
    }


    public int deathCounter = 0;
    public byte HP = 8;

    public Slider HP_UI;

    public void UpdateUI()
    {
        HP_UI.value = (int)HP;
        // LifeGlass_UI.value = (int)LifeGlass;
    }

    private void Die()
    {
        // diePos = transform.position;
        Time.timeScale = 0;
        GameManagement.instance.OnPlayerDied(deathCounter);
        deathCounter++;
    }

    public void Reborn()
    {
        Time.timeScale = 1;
        HP = 8;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HP--;
            controller.EnemyImpact = collision.gameObject.transform.position;
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HP--;
        }

        if (HP == 0) Die();

        UpdateUI();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            GameManagement.instance.Win();
        }
        UpdateUI();
    }
}
