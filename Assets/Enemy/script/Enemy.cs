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


    public GameObject LightLook, DarkLook;

    private void Start()
    {
        count++;
        LightLook.SetActive(GameManagement.instance.CurrentTeam == GameManagement.Team.TeamLight);
        DarkLook.SetActive(GameManagement.instance.CurrentTeam == GameManagement.Team.TeamDark);
    }


    private void OnDestroy()
    {
        killed++;
        AskQuestions();
    }

    private void AskQuestions()
    {
        for (int i = 1; i < 5; i++)
        {
            print("did you kill " + i * 5 + " enemies? " + (killed == i * 5));
            if (killed == i * 5)
                GameManagement.instance.OnKilledEnemies(i - 1);
        }
    }
    [SerializeField] private GameObject darkBullet, lightBullet;
    private GameObject bullet => GameManagement.instance.CurrentTeam == GameManagement.Team.TeamLight? darkBullet: lightBullet;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private float attackDistance = 10;
    private float fireTimeout;
    public float fireDelay;
    private bool canShoot => (fireTimeout <= 0) && Vector2.Distance(GameManagement.instance.selectedPlayer.transform.position, transform.position) <= attackDistance;

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            Flip();
            if(currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime*speed);

        #region shooting

        if (canShoot)
        {
            Instantiate(bullet, launchPoint.position, transform.rotation);
            fireTimeout = fireDelay;
        }
        fireTimeout -= Time.deltaTime;

        #endregion

    }
    private void Flip()
    {
        transform.Rotate (0f, 180f, 0f);

    }

}
