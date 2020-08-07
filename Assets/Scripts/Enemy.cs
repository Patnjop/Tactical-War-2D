using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float health, damage, attackRate, hitChance, attackRange, movementSpeed, visionRange;

    public GameObject selectedEnemy;

    public bool isAttacking, enemySelected, isMoving;
    public List<GameObject> enemysInRange = new List<GameObject>();

    private GameObject visionBox;
    // Start is called before the first frame update
    void Start()
    {
        createVisionRange();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedEnemy != null)
        {
            if (Vector2.Distance(transform.position, selectedEnemy.transform.position) >= attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, selectedEnemy.transform.position, (Time.deltaTime * movementSpeed));
                isMoving = true;
            }
            else if (Vector2.Distance(transform.position, selectedEnemy.transform.position) < attackRange &&
                     isAttacking == false)
            {
                isMoving = false;
                StartCoroutine(nameof(Attack));
                isAttacking = true;
            }
        }
        
        if (enemysInRange.Count > 0 && enemySelected == false)
        {
            Debug.Log("player selected");
            selectEnemy();
        }

        if (selectedEnemy != null)
        {
            enemySelected = true;
        }

        else if (enemysInRange.Count <= 0)
        {
            enemySelected = false;
            selectedEnemy = null;
        }

        if (health <= 0)
        {
            Die();
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("UnitRange"))
        {
            enemysInRange.Add(other.transform.parent.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("UnitRange"))
        {
            enemysInRange.Remove(other.transform.parent.gameObject);
        }
    }
    
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
        }
    }

    IEnumerator Attack()
    {
        if (isMoving == false)
        {
            int rnd = Random.Range(1, 100);
            if (rnd > hitChance)
            {
                Debug.Log("enemy miss " + rnd);
                yield return new WaitForSeconds(1 / attackRate);
                rnd = Random.Range(1, 100);
                isAttacking = false;
            }
            else if (rnd <= hitChance)
            {
                Debug.Log("enemy hit " + rnd);
                selectedEnemy.GetComponent<BaseUnit>().TakeDamage(damage);
                yield return new WaitForSeconds(1 / attackRate);
                rnd = Random.Range(1, 100);
                isAttacking = false;
            }
        }
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }
}
