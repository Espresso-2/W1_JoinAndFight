using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSupport : MonoBehaviour
{
    Animator animator;
    public int health = 1;

    public GameObject bullet;

    public float time = 0;

    public GameObject gunPos;

    public bool inPlace = false; 

    public float distance;

    bool attack = true;

    public GameObject playerDie;

    public GameObject[] gos;

    public GameObject blood;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        if(PlayerPrefs.GetInt("Health", 1) == 1)
        {
            health = 1;
        }
        else if(PlayerPrefs.GetInt("Health", 1) == 2)
        {
            health = 2;
        }
        else if(PlayerPrefs.GetInt("Health", 1) == 3)
        {
            health = 3;
        }
        else if(PlayerPrefs.GetInt("Health", 1) == 4)
        {
            health = 4;
        }
        else if(PlayerPrefs.GetInt("Health", 1) == 5)
        {
            health = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {

        GameObject closeEnemy = FindClosestEnemy();

         if(closeEnemy != null)
        {
            distance = Vector3.Distance(gameObject.transform.position, closeEnemy.transform.position);
        }
        else
        {
            distance = 20;
        }

        if(gos.Length != 0)
        {
            if(distance < 12)
            {
                transform.LookAt(closeEnemy.transform.position);
                if(!inPlace)
                {
                    animator.SetBool("run", true);
                    animator.SetBool("idle", false);
                    animator.SetBool("shoot", false);
                    animator.SetBool("hit", false);

                    if(time == 0)
                    {
                        Instantiate(bullet, gunPos.transform.position, gameObject.transform.rotation);
                    }

                    time+= Time.deltaTime;

                    if(time > 1)
                    {
                        time = 0;
                    }
                }
                else
                {
                    animator.SetBool("run", false);
                    animator.SetBool("idle", false);
                    animator.SetBool("shoot", true);
                    animator.SetBool("hit", false);

                    if(time == 0)
                    {
                        Instantiate(bullet, gunPos.transform.position, gameObject.transform.rotation);
                    }

                    time+= Time.deltaTime;

                    if(time > 3)
                    {
                        time = 0;
                    }
                }

            }
            else
            {
                if(!inPlace)
                {
                    animator.SetBool("run", true);
                    animator.SetBool("idle", false);
                    animator.SetBool("shoot", false);
                    animator.SetBool("hit", false);
                }
                else
                {
                    animator.SetBool("run", false);
                    animator.SetBool("idle", true);
                    animator.SetBool("shoot", false);
                    animator.SetBool("hit", false);
                }
            }
        }else
        {
            if(!inPlace)
            {
                animator.SetBool("run", true);
                animator.SetBool("idle", false);
                animator.SetBool("shoot", false);
                animator.SetBool("hit", false);
            }
            else
            {
                animator.SetBool("run", false);
                animator.SetBool("idle", true);
                animator.SetBool("shoot", false);
                animator.SetBool("hit", false);
            }
        }

        

        
    }

    void OnTriggerEnter(Collider other) {

        if(GameObject.FindGameObjectWithTag("Game Manager").GetComponent<Gamemanager>().guard == false)
        {
            if(other.gameObject.tag == "Bullet Enemy")
            {
                if(health > 0)
                {
                    health -= 1;
                    GameObject.Find("Player").GetComponent<MainPlayer>().health-= 1;
                    if(health == 0)
                    {
                        Instantiate(blood, transform.position, transform.rotation);
                        Destroy(gameObject);
                        // Instantiate(playerDie, transform.position, transform.rotation);
                    }
                    Destroy(other.gameObject);
                }
                else
                {
                    Instantiate(blood, transform.position, transform.rotation);
                    Destroy(gameObject);
                    Destroy(other.gameObject);
                    // Instantiate(playerDie, transform.position, transform.rotation);
                }
            }

            if(other.gameObject.tag == "Enemy Hand")
            {
                
                if(health > 0)
                {
                    if(attack)
                    {
                        health -= 1;
                        GameObject.Find("Player").GetComponent<MainPlayer>().health-= 1;
                        if(health == 0)
                        {
                            Instantiate(blood, transform.position, transform.rotation);
                            Destroy(gameObject);
                            // Instantiate(playerDie, transform.position, transform.rotation);
                        }
                        attack = false;
                        Attack();
                    }  
                }
                else
                {
                    if(attack)
                    {
                        Instantiate(blood, transform.position, transform.rotation);
                        Destroy(gameObject);
                        // Instantiate(playerDie, transform.position, transform.rotation);

                        attack = false;
                        Attack();
                    }
                    
                }
            }
        }

        if(other.gameObject.tag == "Coin")
        {
            GameObject.FindGameObjectWithTag("Game Manager").GetComponent<Gamemanager>().coins += 1;
            Destroy(other.gameObject);
        }
        
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        attack = true;
    }

    public GameObject FindClosestEnemy()
	{
		
		gos = GameObject.FindGameObjectsWithTag("Enemy");
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
}
