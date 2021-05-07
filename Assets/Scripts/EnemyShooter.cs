using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyShooter : MonoBehaviour
{
    public Transform player;
    public LayerMask whatisPlayer;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttack;
    public GameObject projectile;
    public Transform attackPoint;
    int enemyHealth = 100;

    private void Awake()
    {
        player = GameObject.Find("FPS_Controller").transform;
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInAttack = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        if (playerInSight && playerInAttack) AttackPlayer();
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);

        if (!alreadyAttacked)
        {

            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 5f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void takeDamage(int dmgTaken)
    {
        enemyHealth -= dmgTaken;
        Debug.Log(enemyHealth + " health remaining");

        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<Level01Controller>().IncreaseScore();
        }
    }
}
