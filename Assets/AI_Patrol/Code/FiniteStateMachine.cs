using System;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;

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
        [SerializeField] public Vector3 _movementDirection;
        [SerializeField] protected float _movementSpeed;
        [SerializeField] protected Transform _transform;

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

        private void FixedUpdate()
        {
            _rb.linearVelocity = _movementDirection * _movementSpeed;
        }

        #endregion

        #region PublicMethods

        public void EnteredState(States value)
        {
            FinalizeState();
            _state = value;
            InitializeState();
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

            InitializeIdleState();
        }

        protected void CleanAnimatorFlags()
        {
            foreach (StateMechanic value in (StateMechanic[])Enum.GetValues(typeof(StateMechanic)))
            {
                _anim.SetBool(value.ToString(), false);
            }
        }

        protected void InitializeState()
        {
            switch (_state)
            {
                case States.IDLE:
                    InitializeIdleState();
                    break;
                case States.MOVING:
                    InitializeMovingState();
                    break;
                case States.TURNING:
                    InitializeTurningState();
                    break;
            }
        }

        protected void FinalizeState()
        {
            switch (_state)
            {
                case States.IDLE:
                    FinalizeIdleState();
                    break;
                case States.MOVING:
                    FinalizeMovingState();
                    break;
                case States.TURNING:
                    FinalizeTurningState();
                    break;
            }
        }

        #endregion

        #region IdleState

        protected void InitializeIdleState()
        {
            //_movementSpeed = 0;
        }

        protected void FinalizeIdleState() 
        {
            
        }

        #endregion

        #region MovingState

        protected void InitializeMovingState()
        {
            switch (_agent)
            {
                case PlayersAvatar:
                    _movementSpeed = 3.0f;
                    break;
                case EnemyNPC:
                    break;
            }
        }

        protected void FinalizeMovingState()
        {

        }

        #endregion

        #region TurningState

        protected void InitializeTurningState()
        {

        }

        protected void FinalizeTurningState()
        {

        }

        #endregion

        #region GettersAndSettters

        public float SetMovementSpeed
        {
            set { _movementSpeed = value; }
        }

        #endregion
    }
}