using UnityEngine;

namespace Mr_Sanmi.AI_Agents
{
    public class EnemyNPC : Agent
    {
        #region UnityMethods
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

        private void OnTriggerEnter(Collider other)
        {

        }
        #endregion
    }

}