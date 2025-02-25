﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float distanceBetween;

    public int health = 1;

    public Animator animator;
    public GameObject[] gos;
    public int index;

    public bool toBeAdded = false;

    public GameObject blood;

    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Main Player").Length > 0)
        {
            GameObject closestObject = FindClosestEnemy();
            float step = speed * Time.deltaTime;
            distanceBetween = Vector3.Distance(gameObject.transform.position, closestObject.transform.position);
            if(distanceBetween < 15)
            {
                if(!toBeAdded)
                {
                    index = closestObject.GetComponent<EnemyList>().enemies.Count;
                    closestObject.GetComponent<EnemyList>().enemies.Add(gameObject);
                    toBeAdded = true;
                }
                

                if(distanceBetween < 1.5f)
                {
                    animator.SetBool("Walk", false);
                    animator.SetBool("Idle", false);
                    animator.SetBool("Attack", true);
                }
                else
                {
                    gameObject.isStatic = false;
                    animator.SetBool("Walk", true);
                    animator.SetBool("Idle", false);
                    animator.SetBool("Attack", false);
                    gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                    gameObject.GetComponent<NavMeshAgent>().destination = closestObject.GetComponent<EnemyList>().targetPositionList[index - 1];
                    // gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, closestObject.GetComponent<EnemyList>().targetPositionList[index], step);
                    // transform.LookAt(closestObject.transform);
                }
                
            }
            else
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", true);
                animator.SetBool("Attack", false);
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;

                if(toBeAdded)
                {
                    closestObject.GetComponent<EnemyList>().enemies.Remove(gameObject);
                    toBeAdded = false;
                }
                
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Attack", false);
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }
        
    }

    public GameObject FindClosestEnemy()
	{
		
		gos = GameObject.FindGameObjectsWithTag("Main Player");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

    void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag == "Bullet")
        {
            if(health > 0)
            {
                health -= 1;
                // gameObject.GetComponent<Enemy>().health-= 1;
                
                if(health == 0)
                {
                    Instantiate(blood, transform.position, transform.rotation);
                    GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().CoinInst();
                    // if(coin != null)
                    // {
                    //     int rand = Random.Range(0, 1);
                    //     if(rand == 1)
                    //     {
                    //         Instantiate(coin, new Vector3(transform.position.x, 2f, transform.position.z), coin.transform.rotation);
                    //     }
                        
                    // }
                    if(GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().coinBool)
                    {
                        if(PlayerPrefs.GetInt("Level", 1) > 1)
                        {
                            Instantiate(coin, new Vector3(transform.position.x, 2f, transform.position.z), coin.transform.rotation);
                        }
                        else
                        {
                            Instantiate(coin, new Vector3(transform.position.x, 0.35f, transform.position.z), coin.transform.rotation);
                        }
                        
                    }
                    Destroy(gameObject);
                }
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
                Instantiate(blood, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        
    }
}
