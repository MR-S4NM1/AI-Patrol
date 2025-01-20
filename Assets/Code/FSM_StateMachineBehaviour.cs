using UnityEngine;

namespace Mr_Sanmi.AI_Agents
{
    public class FSM_StateMachineBehaviour : StateMachineBehaviour
    {
        public States state;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("I entered the state: " + state.ToString() + " :P");
            animator.gameObject.GetComponent<FiniteStateMachine>().EnteredState(state);
        }
    }
}
