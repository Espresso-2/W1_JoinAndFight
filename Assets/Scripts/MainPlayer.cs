using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainPlayer : MonoBehaviour
{

    public bool isMoving = false;
    public bool moving = false;
    public float speed = 10f;
    public GameObject joyStick;
    public CharacterController characterController;
    Vector3 move;
    Animator playerAnimator;

    public int health = 3;

    public GameObject gunPos;

    public GameObject bullet;
    float time = 0;

    public float distance;

     public GameObject[] gos;

     float inputSqrMagnitude;

     Vector3 rotate; 
     public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        // characterController = gameObject.GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        joyStick = GameObject.FindGameObjectWithTag("Joystick");

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

        PlayerPrefs.SetInt("Squad", 1);

        if(PlayerPrefs.GetInt("Squad", 1) == 2)
        {
            GameObject playerInitiated = Instantiate(player, transform.position, transform.rotation);
                    
            GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated.GetComponent<Unit>());
            GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
        }
        else if(PlayerPrefs.GetInt("Squad", 1) == 3)
        {
            GameObject playerInitiated = Instantiate(player, transform.position, transform.rotation);
            GameObject playerInitiated1 = Instantiate(player, transform.position, transform.rotation);
                    
            GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated.GetComponent<Unit>());
            GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
            GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated1.GetComponent<Unit>());
            GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
        }
        else if(PlayerPrefs.GetInt("Squad", 1) == 4)
        {
            GameObject playerInitiated = Instantiate(player, transform.position, transform.rotation);
            GameObject playerInitiated1 = Instantiate(player, transform.position, transform.rotation);
            GameObject playerInitiated2 = Instantiate(player, transform.position, transform.rotation);
                    
            GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated.GetComponent<Unit>());
            GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated1.GetComponent<Unit>());
            GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated2.GetComponent<Unit>());
            GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
            GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
            GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
        }
        else if(PlayerPrefs.GetInt("Squad", 1) == 5)
        {
            GameObject playerInitiated = Instantiate(player, transform.position, transform.rotation);
            GameObject playerInitiated1 = Instantiate(player, transform.position, transform.rotation);
            GameObject playerInitiated2 = Instantiate(player, transform.position, transform.rotation);
            GameObject playerInitiated3 = Instantiate(player, transform.position, transform.rotation);
                    
            GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated.GetComponent<Unit>());
            GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated1.GetComponent<Unit>());
            GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated2.GetComponent<Unit>());
            GameObject.FindGameObjectWithTag("Player Controller").GetComponent<PlayerController>().selectedUnits.Add(playerInitiated3.GetComponent<Unit>());
            GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
            GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
            GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
            GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().health+= health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        GameObject closeEnemy = FindClosestEnemy();

        if(health < 1)
        {
            // GameObject.Find("Game Manager").GetComponent<Gamemanager>().gameOver = true;
        }
        
        if(isMoving)
        {
            if(joyStick.GetComponent<Joystick>().Horizontal != 0 || joyStick.GetComponent<Joystick>().Vertical != 0)
            {
                moving = true;
            }
            else
            {
                moving = false;
            }
            move = -1 * new Vector3(joyStick.GetComponent<Joystick>().Horizontal, 0f, joyStick.GetComponent<Joystick>().Vertical);
            rotate = -1 * new Vector3(joyStick.GetComponent<Joystick>().Horizontal, 0f, joyStick.GetComponent<Joystick>().Vertical)*Time.deltaTime*2f;

            // characterController.Move(move * speed * Time.deltaTime);
            
            Step();

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
                    if(joyStick.GetComponent<Joystick>().Horizontal != 0f || joyStick.GetComponent<Joystick>().Vertical !=0f)
                    {
                        playerAnimator.SetBool("run", true);
                        playerAnimator.SetBool("idle", false);
                        playerAnimator.SetBool("shoot", false);
                        playerAnimator.SetBool("hit", false);

                        if(health > 0)
                        {
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

                        
                    }
                    else
                    {
                        playerAnimator.SetBool("run", false);
                        playerAnimator.SetBool("idle", false);
                        playerAnimator.SetBool("shoot", true);
                        playerAnimator.SetBool("hit", false);

                        if(health > 0)
                        {
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
                }
                else 
                {    
                    if(move.x>0 || move.x<0 || move.z>0 || move.z<0)
                    {
                        transform.rotation = Quaternion.LookRotation(rotate);
                    }
                    if(joyStick.GetComponent<Joystick>().Horizontal != 0f || joyStick.GetComponent<Joystick>().Vertical !=0f)
                    {
                        playerAnimator.SetBool("run", true);
                        playerAnimator.SetBool("idle", false);
                        playerAnimator.SetBool("shoot", false);
                        playerAnimator.SetBool("hit", false);
                    }
                    else
                    {
                        playerAnimator.SetBool("run", false);
                        playerAnimator.SetBool("idle", true);
                        playerAnimator.SetBool("shoot", false);
                        playerAnimator.SetBool("hit", false);
                    }   
                    
                }
            }
            else
            {
                 if(move.x>0 || move.x<0 || move.z>0 || move.z<0)
                    {
                        transform.rotation = Quaternion.LookRotation(rotate);
                    }
                if(joyStick.GetComponent<Joystick>().Horizontal != 0f || joyStick.GetComponent<Joystick>().Vertical !=0f)
                    {
                        playerAnimator.SetBool("run", true);
                        playerAnimator.SetBool("idle", false);
                        playerAnimator.SetBool("shoot", false);
                        playerAnimator.SetBool("hit", false);
                    }
                    else
                    {
                        playerAnimator.SetBool("run", false);
                        playerAnimator.SetBool("idle", true);
                        playerAnimator.SetBool("shoot", false);
                        playerAnimator.SetBool("hit", false);
                    }   
            }

            
            
        }
        
    }

    public void Step()
    {
        inputSqrMagnitude = move.sqrMagnitude;

        if(inputSqrMagnitude >= 0.01f)
        {
            Vector3 newPosition = transform.position + move * Time.deltaTime * speed;
            NavMeshHit hit;

            bool isValid = NavMesh.SamplePosition(newPosition, out hit, 0.3f, NavMesh.AllAreas);
            if(isValid)
            {
                if((transform.position - hit.position).magnitude >= 0.02f)
                {
                    transform.position = hit.position;
                }
            }
        }
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
