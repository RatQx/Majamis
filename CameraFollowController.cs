using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour {

	public Quaternion cameraRotation;
	public Transform objectToFollow;
	public Vector3 offset;
	private Vector3 originalOffset;
	public float followSpeed = 5;
	public float lookSpeed = 5;
	public float cameraSensitivity = 2;
	private RaycastHit hit;
	float distance;


	public float cameraTimer = 2f;
	private bool notMoving = true;

	private void Start()
	{
		originalOffset = offset;
	}
	public void LookAtTarget()
	{
		Vector3 _lookDirection = objectToFollow.position - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
	}

	public void MoveToTarget()
	{
		if (notMoving)
		{
			Vector3 _targetPos = objectToFollow.position +
							objectToFollow.forward * offset.z +
							objectToFollow.right * offset.x +
							objectToFollow.up * offset.y;
			transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);

			cameraRotation.y = 0;
			cameraRotation.x = gameObject.transform.rotation.eulerAngles.y;
		}
		else
		{
			Quaternion rotation = Quaternion.Euler(cameraRotation.y, cameraRotation.x, 0);
			transform.position = Vector3.Lerp(transform.position, objectToFollow.position  + rotation * offset, followSpeed * Time.deltaTime);
			//transform.position = objectToFollow.position + rotation * offset;
			
			
		}

		
		
	}
	private void Update()
	{
		
		if (Physics.Linecast(transform.position, transform.position + transform.localRotation * offset, out hit))
		{
			//distance = new Vector3(0, 0, Vector3.Distance(transform.position, hit.point));
			distance = Mathf.Clamp((hit.distance * 0.8f), 2f, 3f);
			offset.z += distance*0.05f;
			print("hit");
		}
		else
		{
			offset = originalOffset;
			print("not hit");
		}

		if (cameraRotation.x/10 == (cameraRotation.x + Input.GetAxis("Mouse X"))/10 || cameraRotation.y/10 == (cameraRotation.y + Input.GetAxis("Mouse Y"))/10) // not moving camera
		{
			cameraTimer -= Time.deltaTime;
			
		}
		else // moving
		{
			cameraTimer = 2f;
			notMoving = false;
			
		}

		if(cameraTimer <= 0)
		{
			notMoving = true;
			
		}
			
		cameraRotation.x += Input.GetAxis("Mouse X");
		cameraRotation.y += Input.GetAxis("Mouse Y") * (-1);
		LookAtTarget();
	}


	private void FixedUpdate()
	{
		//LookAtTarget();
		MoveToTarget();
	}

	
}
