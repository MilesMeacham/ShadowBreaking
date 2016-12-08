using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealerAI : MonoBehaviour {


    Rigidbody2D rbody;
    Animator anim;

    // AI VARIABLES:
    public float moveSpeed = 0.5f;
    public float knockbackSpeed = 0.8f;
    public float chaseDistance = 3.0f;
    public float attackDistance = 1.0f;
    private float distanceToPlayer;
    private float xDiff;
    private float yDiff;
    private float xLoc;
    private float yLoc;

    public bool knockback = false;
    public bool stunned = false;
    private bool walking = false;

    private Vector2 spawnedLocation;
    private Vector2 currentEnemyPos;
    private Vector2 prevEnemyPos;
    private Vector2 velocity;

    private GameObject player;


    // Healer Stuff
    private GameObject closestAlly;
    private GameObject[] allies;
    public bool moveTowardsAlly = false;
    private bool stopped = false;
    private bool healing = false;
    public List<GameObject> alliesNearBy;
    public float healingDelay = 1f;
    public int healingAmount = 10;




    // START:
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    } // END START()

    // This is called any time the enemy spawns (becomes active)
    void OnEnable()
    {
        knockback = false;
        stunned = false;
        spawnedLocation = transform.position;
    }

    // UPDATE:
    void FixedUpdate()
    {
        if (knockback)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -knockbackSpeed * Time.deltaTime);
        }
        else
        {
            if (closestAlly == this.gameObject)
            {
                FindClosestAlly();
            }

            if (moveTowardsAlly == false && alliesNearBy.Count <= 0)
            {
                GameObject myEnemy = FindClosestAlly();

                print(myEnemy);
            }

            if (moveTowardsAlly == true)
            {
                MoveTowardsPlayer();
            }
            else if (!stopped)
            {
                Stop();
            }
            else
            {
                if (healing != true)
                    StartCoroutine(CastHealing());


                Stop();
            }


        }

        } // END UPDATE()

    public IEnumerator CastHealing()
    {
        healing = true;
        anim.SetBool("CastSpell", true);


        foreach (GameObject obj in alliesNearBy)
        {
            obj.GetComponent<EnemyHealth>().AddHealth(healingAmount);

            print("Healing " + obj);
        }

        yield return new WaitForSeconds(healingDelay);

        anim.SetBool("CastSpell", false);
        healing = false;
    }




    /// <summary>
    /// Finds and returns the closest Ally to this character
    /// </summary>
    /// <returns>GameObject closestAlly</returns>
    public GameObject FindClosestAlly()
    {

        float distance = 0;
        float closestDistance = 5000;

        allies = GameObject.FindGameObjectsWithTag("Enemy");

        if (allies.Length <= 0)
            closestAlly = this.gameObject;

        foreach (GameObject obj in allies)
        {
            distance = Vector2.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestAlly = obj;
            }
        }

        moveTowardsAlly = true;

        return closestAlly;
            
    }

    public void MoveTowardsPlayer()
    {
        stopped = false;

        distanceToPlayer = Vector2.Distance(transform.position, closestAlly.transform.position);

        //  If the distance between the mob and the player
        //  is less than or equal to the ChaseDistance,
        //  and greater than the AttackDistance,
        //  the mob moves towards the player.
        velocity = new Vector2((transform.position.x - closestAlly.transform.position.x) * moveSpeed,
            (transform.position.y - closestAlly.transform.position.y) * moveSpeed);

        rbody.velocity = -velocity;
        currentEnemyPos = rbody.position;
        walking = true;



        // obtain difference between the current position and the past position to send to animator
        xDiff = currentEnemyPos.x - prevEnemyPos.x;
        yDiff = currentEnemyPos.y - prevEnemyPos.y;

        if (walking == true)
        {
            anim.SetBool("IsWalking", true);
            anim.SetFloat("Input_X", xDiff);
            anim.SetFloat("Input_Y", yDiff);


        }
        else
        {
            anim.SetBool("IsWalking", false);
            xLoc = player.transform.position.x - transform.position.x;
            yLoc = player.transform.position.y - transform.position.y;
            anim.SetFloat("Input_X", xLoc);
            anim.SetFloat("Input_Y", yLoc);
        }

        prevEnemyPos = currentEnemyPos;

        walking = false;

    }


    public void Stop()
    {
        rbody.velocity = new Vector2(0, 0);

        anim.SetBool("IsWalking", false);
        xLoc = player.transform.position.x - transform.position.x;
        yLoc = player.transform.position.y - transform.position.y;
        anim.SetFloat("Input_X", xLoc);
        anim.SetFloat("Input_Y", yLoc);

        moveTowardsAlly = false;
        stopped = true;
    }
} 