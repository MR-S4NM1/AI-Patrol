using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.LowLevel;

namespace Mr_Sanmi.AI_Agents
{
    public class PlayersAvatar : Agent
    {
        #region References

        #endregion

        #region

        #endregion

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

        private void FixedUpdate()
        {
            
        }

        #endregion


        #region LocalMethods

        #endregion


        #region PublicMethods


        #endregion

        #region CallbackFunctions

        public void OnMove(InputAction.CallbackContext value)
        {
            if (value.performed) // Update from the input
            {
                fsm.StateMechanic(StateMechanic.MOVE);
                Debug.Log("OnMove: " + value.ReadValue<Vector2>());
                fsm._movementDirection.x = value.ReadValue<Vector2>().x;
                fsm._movementDirection.z = value.ReadValue<Vector2>().y;
            }
            else if (value.canceled) // Release from this input
            {
                fsm._movementDirection.x = value.ReadValue<Vector2>().x;
                fsm._movementDirection.z = value.ReadValue<Vector2>().y;

                if (fsm._movementDirection.magnitude <= 0.05f)
                {
                    fsm.StateMechanic(StateMechanic.STOP);
                    fsm._movementDirection = Vector3.zero;
                }
            }
        }

        #endregion


        #region GettersAndSetters


        #endregion

    }
}
