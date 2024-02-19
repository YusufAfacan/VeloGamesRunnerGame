using UnityEngine;

public class SlideBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RunnerMovement.Instance.IsSliding = false;
    }

}
