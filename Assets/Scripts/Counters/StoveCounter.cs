using System;
using System.Collections;
using System.Data;
using System.Xml.Serialization;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem.Controls;

public class StoveCounter : BaseCounter, IHasProgress
{
	public enum State
	{
		Idle,
		Frying,
		Fried,
		Burnt
	}


	public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

	public class OnStateChangedEventArgs : EventArgs
	{
		public State state;
	}

	[SerializeField] FryingRecipeSO[] fryingRecipeSOs;
	[SerializeField] BurningRecipeSO[] burningRecipeSOs;


	private State currentState;
	private float fryingProgress = 0f;
	private float burningProgress = 0f;
	private FryingRecipeSO fryingRecipeSO;
	private BurningRecipeSO burningRecipeSO;


	private void Start()
	{
		currentState = State.Idle;
	}
	private void Update()
	{
		if (HasKitchenObject())
		{
			switch (currentState)
			{
				case State.Idle:
					break;
				case State.Frying:
					fryingProgress += Time.deltaTime;
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
					{
						progressNormalized = (float)fryingProgress / fryingRecipeSO.fryingTimerMax
					});

					if (fryingRecipeSO.fryingTimerMax <= fryingProgress)
					{
						GetKitchenObject().DestroySelf();

						KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

						burningRecipeSO = GetBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO());
						currentState = State.Fried;
						burningProgress = 0f;
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = currentState });
					}
					break;
				case State.Fried:
					burningProgress += Time.deltaTime;
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
					{
						progressNormalized = (float)burningProgress / burningRecipeSO.burningTimerMax
					});

					if (fryingRecipeSO.fryingTimerMax <= burningProgress)
					{
						GetKitchenObject().DestroySelf();

						KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

						currentState = State.Burnt;
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = currentState });
					}
					break;
				case State.Burnt:
					break;
			}
		}
	}

	public override void Interact(Player player)
	{
		if (!HasKitchenObject())
		{
			//This counter has no kitchen object
			if (player.HasKitchenObject())
			{
				// Check if pplayer has an object iwth recipe and can be cut
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
				{
					//Put it on the counter
					player.GetKitchenObject().SetKitchenObjectParent(this);

					fryingRecipeSO = GetFryingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
					currentState = State.Frying;
					OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{ state = currentState });
					
				}
			}
		}
		else
		{
			//This counter has kitchen object
			if (!player.HasKitchenObject())
			{
				//Player has no object, give this to him.
				GetKitchenObject().SetKitchenObjectParent(player);
				currentState = State.Idle;
				OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = currentState });
				fryingProgress = 0f;
				burningProgress = 0f;
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
				{
					progressNormalized = 0f
				});
			}
		}
	}

	private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
	{
		return GetFryingRecipeSO(kitchenObjectSO) == null ? false : true;
	}

	private FryingRecipeSO GetFryingRecipeSO(KitchenObjectSO input)
	{
		foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOs)
		{
			if (fryingRecipeSO.input == input)
			{
				return fryingRecipeSO;
			}
		}

		return null;
	}
	private BurningRecipeSO GetBurningRecipeSO(KitchenObjectSO input)
	{
		foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOs)
		{
			if (burningRecipeSO.input == input)
			{
				return burningRecipeSO;
			}
		}

		return null;
	}

}