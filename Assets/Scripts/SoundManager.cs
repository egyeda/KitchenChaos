using System;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[SerializeField] private AudioClipRefsSO audioClipRefsSO;


	private void Start()
	{
		DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
		DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;
	}

	private void DeliveryManager_OnRecipeFail(object sender, EventArgs e)
	{
		PlaySound(audioClipRefsSO.deliveryFail, Camera.main.transform.position);
	}

	private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
	{
		PlaySound(audioClipRefsSO.deliverySuccess, Camera.main.transform.position);
	}

	private void PlaySound(AudioClip[] clips, Vector3 position, float volume = 1f)
	{
		PlaySound(clips[UnityEngine.Random.Range(0, clips.Length)], position, volume);
	}

	private void PlaySound(AudioClip clip, Vector3 position, float volume = 1f)
	{
		AudioSource.PlayClipAtPoint(clip, position, volume);
	}
}
