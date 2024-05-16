using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Main Player").GetComponent<MainPlayer>().transform;
	}

	void LateUpdate ()
	{
		if(target != null)
		{
			Vector3 desiredPosition = target.position + offset;
			Vector3 smoothedPosition = Vector3.LerpUnclamped(transform.position, desiredPosition, smoothSpeed);
			transform.position = smoothedPosition;

			transform.LookAt(target);
		}
		
	}

}
