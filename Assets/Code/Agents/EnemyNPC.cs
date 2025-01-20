using UnityEngine;

namespace Mr_Sanmi.AI_Agents
{
    public class EnemyNPC : Agent
    {
        private void OnDrawGizmos()
        {
            if (fsm == null)
            {
                fsm = GetComponent<FiniteStateMachine>();
            }
        }
        void Start()
        {

        }

        void Update()
        {

        }
    }

}