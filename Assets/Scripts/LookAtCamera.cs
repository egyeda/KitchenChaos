using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	private enum Mode
	{
		LookAt,
		LookAtInverted,
		CameraForward,
		CameraForwardInverted
	}

	[SerializeField] private Mode mode;
	void LateUpdate()
	{
		switch (mode)
		{
			case Mode.LookAt:
				transform.LookAt(Camera.main.transform);
				break;
				
			case Mode.LookAtInverted:
				Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
				transform.LookAt(transform.position + dirFromCamera);
				break;
			case Mode.CameraForward:
				transform.LookAt(Camera.main.transform.position);
				break;
			case Mode.CameraForwardInverted:
				transform.LookAt(-Camera.main.transform.position);
				break;
		}
	}
}