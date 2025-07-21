using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[SerializeField] private AudioClipRefsSO audioClipRefsSO;


	private void Start()
	{
		DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
		DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;
		CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
		Player.Instance.OnPickedSomething += Player_OnPickedSomething;
		BaseCounter.OnObjectPlaced += BaseCounter_OnObjectPlaced;
		TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
	}

	private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
	{
		TrashCounter trashCounter = sender as TrashCounter;
		PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
	}

	private void BaseCounter_OnObjectPlaced(object sender, EventArgs e)
	{
		BaseCounter baseCounter = sender as BaseCounter;
		PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
	}

	private void Player_OnPickedSomething(object sender, EventArgs e)
	{
		PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
	}

	private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
	{
		CuttingCounter cuttingCounter = sender as CuttingCounter;
		PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
	}

	private void DeliveryManager_OnRecipeFail(object sender, EventArgs e)
	{
		PlaySound(audioClipRefsSO.deliveryFail, DeliveryCounter.Instance.transform.position);
	}

	private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
	{
		PlaySound(audioClipRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
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
