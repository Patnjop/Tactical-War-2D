using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField]
    protected float health, movementSpeed, damage, hitChance, visionRange, attackRange, attackRate, idleWaitTime;
    protected Vector2 direction;
    public bool selected, enemySelected, moving, isAttacking;

    private GameObject visionBox, selectedEnemy;
    [SerializeField]
    private List<GameObject> enemysInRange = new List<GameObject>();
    private float proximity = 0.2f, idleTimer;

    private void Awake()
    {
        createVisionRange();
        direction = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, direction, (Time.deltaTime * movementSpeed));
            if (Vector2.Distance(transform.position, direction) <= proximity)
            {
                moving = false;
            }
        }

        if (moving == false)
        {
            idleTimer += Time.deltaTime;
        }

        if (enemysInRange.Count > 0 && enemySelected == false && moving == false)
        {
            selectEnemy();
        }
        else if (enemysInRange.Count <= 0)
        {
            enemySelected = false;
            selectedEnemy = null;
        }

        if (Vector2.Distance(transform.position, selectedEnemy.transform.position) >= attackRange && idleTimer >= idleWaitTime)
        {
            ApproachEnemy();
            idleTimer = 0f;
        }
        else if (Vector2.Distance(transform.position, selectedEnemy.transform.position) < attackRange && isAttacking == false)
        {
            StartCoroutine(nameof(Attack));
            isAttacking = true;
        }
    }

    public void moveToPoint(Vector2 targetPos)
    {
        //moves unit to mouse right click
        direction = targetPos;
        moving = true;
        proximity = 0.2f;
        enemySelected = false;
        idleTimer = 0f;
    }

    public void createVisionRange()
    {
        visionBox = new GameObject();
        visionBox.transform.position = transform.position;
        visionBox.transform.parent = transform;
        visionBox.AddComponent<BoxCollider2D>();
        visionBox.GetComponent<BoxCollider2D>().isTrigger = true;
        visionBox.GetComponent<BoxCollider2D>().size = new Vector2(visionRange, visionRange);
    }

    //selects nearest enemy from within the units visionrange
    public void selectEnemy()
    {
        float shortestDistance = visionRange;
        if (enemySelected == false)
        {
            foreach (GameObject e in enemysInRange)
            {
                float distanceTemp = Vector2.Distance(e.transform.position, transform.position);
                if (distanceTemp < shortestDistance)
                {
                    shortestDistance = distanceTemp;
                    selectedEnemy = e.gameObject;
                }
            }
            enemySelected = true;
        }
    }

    public void ApproachEnemy()
    {
        if (moving == false)
        {
            proximity = attackRange + 0.2f;
            direction = selectedEnemy.transform.position;
            moving = true;
        }
    }

    IEnumerator Attack()
    {
        Debug.Log("aaaaa");
        yield return new WaitForSeconds(1 / attackRate);
        isAttacking = false;
    }
    
    //adds enemies to a list when they enter vision range of a unit
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemysInRange.Add(other.gameObject);
        }
    }
    
    //removes enemies from a list when they exit vision range of a unit
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemysInRange.Remove(other.gameObject);
        }
    }
}
