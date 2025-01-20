using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Mr_Sanmi.AI_Agents
{
    public enum States
    {
        IDLE,
        MOVING,
        TURNING
    }

    public enum StateMechanic // Core Mechanics "Agents"
    {
        STOP,
        MOVE,
        TURN
    }

    [RequireComponent(typeof(Rigidbody), typeof(FiniteStateMachine))]
    public class FiniteStateMachine : MonoBehaviour
    {
        #region References

        [SerializeField] protected Rigidbody _rb;
        [SerializeField] protected float _movementSpeed;
        [SerializeField] protected Animator _anim;
        [SerializeField] protected States _currentState;

        #endregion

        #region Knobs

        #endregion

        #region RuntimeVariables


        #endregion

        #region UnityMethods

        private void OnDrawGizmos()
        {
            if(_rb == null)
            {
                _rb = GetComponent<Rigidbody>();
            }

            if(_anim == null)
            {
                _anim = GetComponent<Animator>();
            }
        }

        void Start()
        {

        }

        void Update()
        {

        }

        #endregion

        #region PublicMethods

        public void EnteredState(States value)
        {
            Debug.Log("FSM - EnteredStated(): Entered the finite state " + value);
            // TODO: Clean all animator parameters with cooldown
            _currentState = value;
            Invoke("CleanAnimatorFlags", 0.1f);
        }

        public void StateMechanic(StateMechanic value)
        {
            _anim.SetBool(value.ToString(), true);
        }

        #endregion

        #region LocalMethods

        protected void CleanAnimatorFlags()
        {
            foreach (StateMechanic value in (StateMechanic[])Enum.GetValues(typeof(StateMechanic)))
            {
                _anim.SetBool(value.ToString(), false);
            }
        }

        #endregion

        #region GettersAndSettters


        #endregion
    }
}