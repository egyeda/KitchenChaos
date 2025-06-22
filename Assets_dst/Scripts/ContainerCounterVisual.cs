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
		containerCounter.OnCounterOpened += ContainerCounter_OnCounterOpened;
	}

	private void ContainerCounter_OnCounterOpened(object sender, EventArgs e)
	{
		OpenCounter();
	}

	public void OpenCounter()
	{
		animator.SetTrigger(OPEN_CLOSE);
	}
}
