using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
	private const string OPEN_CLOSE = "OpenClose";
	[SerializeField] ContainerCounter containerCounter;

	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		containerCounter.PlayerInteracted += ConainterCointer_PlayerInteracted;
	}

	private void ConainterCointer_PlayerInteracted(object sender, EventArgs e)
	{
		animator.SetTrigger(OPEN_CLOSE);
	}
}