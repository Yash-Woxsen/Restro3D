using UnityEngine;

namespace Player
{
    public class PlayerAnimController : MonoBehaviour
    {
        public Animator animator;
        public CharacterController characterController;
        private void Update()
        {
            if(characterController.velocity.magnitude > 0.1f)
            {
                animator.SetBool("IsIdle", false);
            }
            else
            {
                animator.SetBool("IsIdle", true);
            }
        }
    }
}