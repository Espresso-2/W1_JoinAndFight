using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestingAIController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject joyStick;
    Vector3 move;
    float inputSqrMagnitude;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector3(joyStick.GetComponent<Joystick>().Horizontal, 0f, joyStick.GetComponent<Joystick>().Vertical);
        
        Step();
    }

    public void Step()
    {
        inputSqrMagnitude = move.sqrMagnitude;

        if(inputSqrMagnitude >= 0.01f)
        {
            
            Vector3 newPosition = transform.position + move * Time.deltaTime * speed;
            
            NavMeshHit hit;

            bool isValid = NavMesh.SamplePosition(newPosition, out hit, 0.5f, NavMesh.AllAreas);
            Debug.Log(isValid);
            if(isValid)
            {
                Debug.Log(newPosition);
                if((transform.position - hit.position).magnitude >= 0.02f)
                {
                    transform.position = hit.position;
                    // transform.position = new Vector3(3f, 0f, 3f);
                    Debug.Log(hit.position);
                }
            }
        }
    }
}
