using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    public bool isMoving = false;
    public GameObject player;

    public GameObject coin;
    public List<Unit> selectedUnits;
    public List<Vector3> targetPositionList;

    int rand = 0;

    public bool coinBool = false;

    private void Awake() {
        selectedUnits = new List<Unit>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Main Player");
    }

    // Update is called once per frame
    void Update()
    {

        if(player == null)
        {
            player = selectedUnits[0].gameObject;
            selectedUnits.Remove(player.GetComponent<Unit>());
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().target = player.transform;
            player.GetComponent<MainPlayer>().enabled = true;
            player.GetComponent<MainPlayerHealth>().enabled = true;
            player.GetComponent<PlayerSupport>().enabled = false;
            player.GetComponent<Unit>().enabled = false;
            player.GetComponent<NavMeshAgent>().enabled = false;
        }
        if(selectedUnits.Count > 0)
        {
            for(int i = selectedUnits.Count-1; i>=0; i--)
            {
                if(selectedUnits[i] != null)
                {

                }
                else
                {
                    selectedUnits.RemoveAt(i);
                }
            }
        }
        // if(player.GetComponent<MainPlayer>().moving)
        // {
            targetPositionList = GetPositionListAround(player.transform.position, 1f, selectedUnits.Count);
            int targetpositionlistIndex = 0;
            foreach(Unit units in selectedUnits)
            {
                // float step = 12 * Time.deltaTime;
                units.gameObject.GetComponent<NavMeshAgent>().destination = targetPositionList[targetpositionlistIndex];

                // if(!units.gameObject.GetComponent<PlayerSupport>().inPlace)
                // {
                    if(units.gameObject.transform.position != targetPositionList[targetpositionlistIndex])
                    {
                        units.gameObject.GetComponent<PlayerSupport>().inPlace = false;
                    }
                    else
                    {
                        units.gameObject.GetComponent<PlayerSupport>().inPlace = true;
                    }
                // }

                // if(units.gameObject.GetComponent<PlayerSupport>().inPlace)
                // {
                //     units.gameObject.transform.position = Vector3.MoveTowards(units.gameObject.transform.position, targetPositionList[targetpositionlistIndex], step);
                //     units.gameObject.transform.rotation = player.transform.rotation;
                // }
                // units.gameObject.transform.position = Vector3.MoveTowards(units.gameObject.transform.position, targetPositionList[targetpositionlistIndex], step);
                // units.gameObject.transform.rotation = player.transform.rotation;
                targetpositionlistIndex = (targetpositionlistIndex + 1) % targetPositionList.Count;
            }
        // }
    }

    public List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for(int i=0; i<positionCount; i++)
        {
            float angle = i*(360f/positionCount);
            Vector3 dir = ApplyToRotation(new Vector3(1,0,1), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }

    public Vector3 ApplyToRotation(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, angle, 0)*vec;
    }

    public void CoinInst()
    {
        // int rand = Random.Range(0, 1);
        if(rand == 1)
        {
            coinBool = true;
            // Instantiate(coin, new Vector3(transform.position.x, 2f, transform.position.z), coin.transform.rotation);
        }
        else
        {
            coinBool = false;
        }
        rand +=1;
        if(rand > 5)
        {
            rand = 0;
        }
    }
}
