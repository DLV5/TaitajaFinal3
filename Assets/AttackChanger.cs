using System.Linq;
using UnityEngine;

public class AttackChanger : StateMachineBehaviour
{
    [SerializeField] private string _attackHitBoxName;
    private Collider _attackHitBox;

    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        _attackHitBox.gameObject.SetActive(false);
    }

    public void EnableHitBox()
    {
        _attackHitBox =
           FindObjectsByType<Collider>(FindObjectsInactive.Include, FindObjectsSortMode.None)
           .FirstOrDefault(s => s.name == _attackHitBoxName);
        _attackHitBox.gameObject.SetActive(true);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
