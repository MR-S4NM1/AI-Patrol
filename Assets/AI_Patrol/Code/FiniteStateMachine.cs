using System;
using UnityEditorInternal;
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
        [SerializeField] protected Animator _anim;
        [SerializeField] protected Agent _agent;

        #endregion

        #region Knobs

        #endregion

        #region RuntimeVariables

        [SerializeField] protected States _state;
        [SerializeField] protected Vector3 _movementDirection;
        [SerializeField] protected float _movementSpeed;

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
            InitializeFiniteStateMachine();
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
            _state = value;
            Invoke("CleanAnimatorFlags", 0.1f);
        }

        public void StateMechanic(StateMechanic value)
        {
            _anim.SetBool(value.ToString(), true);
        }

        #endregion

        #region LocalMethods

        protected void InitializeFiniteStateMachine()
        {
            if (_agent == null)
            {
                _agent = GetComponent<Agent>();
            }
        }

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