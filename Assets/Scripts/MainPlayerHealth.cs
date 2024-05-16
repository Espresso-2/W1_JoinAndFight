using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerHealth : MonoBehaviour
{
    public int health = 1;
    public GameObject body;

    public GameObject playerDie;

    bool attack = true;

    public GameObject blood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {

        // Debug.Log("Trigger");

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
                        // Destroy(gameObject);
                        Destroy(body);
                        //Instantiate(playerDie, transform.position, transform.rotation);
                        Instantiate(blood, transform.position, transform.rotation);
                    }
                    Destroy(other.gameObject);
                }
                else
                {
                    // Destroy(gameObject);
                    Destroy(body);
                    Destroy(other.gameObject);
                    //Instantiate(playerDie, transform.position, transform.rotation);
                    Instantiate(blood, transform.position, transform.rotation);
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
                            // Destroy(gameObject);
                            Destroy(body);
                            //Instantiate(playerDie, transform.position, transform.rotation);
                            Instantiate(blood, transform.position, transform.rotation);
                        }
                        attack = false;
                        Attack();
                    }  
                }
                else
                {
                    if(attack)
                    {
                        // Destroy(gameObject);
                        Destroy(body);
                        //Instantiate(playerDie, transform.position, transform.rotation);
                        Instantiate(blood, transform.position, transform.rotation);

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
}
