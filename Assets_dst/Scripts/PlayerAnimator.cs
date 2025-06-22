using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	const string WALKING_ANIMATION = "isWalking";
	private Animator animator;
	[SerializeField]
	private Player player;
	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		animator.SetBool(WALKING_ANIMATION, player.IsMoving());
	} 
}