using System;
using System.Numerics;
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
				UnityEngine.Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
				transform.LookAt(transform.position + dirFromCamera);
				break;
			case Mode.CameraForward:
				transform.forward = Camera.main.transform.forward;
				break;
			case Mode.CameraForwardInverted:
				transform.forward = -Camera.main.transform.forward;
				break;
		}
	}
}