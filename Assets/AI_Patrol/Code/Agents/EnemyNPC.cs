using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mr_Sanmi.AI_Agents
{

    public class EnemyNPC : Agent
    {
        #region Knobs

        [SerializeField] public EnemyNPC_SO enemyNPC_SO;

        #endregion

        #region RuntimeVariables

        protected PatrolBehaviours _currentEnemyBehaviour;
        protected int _currentEnemyBehaviourIndex;
        [SerializeField] protected Transform _avatarsTransform;
        protected StateMechanics _previousMovementStateMechanic;
        protected RaycastHit _currentRaycastHit;

        #endregion

        #region UnityMethods
        private void OnDrawGizmos()
        {

        }
        void Start()
        {
            if (_fsm == null)
            {
                _fsm = GetComponent<FiniteStateMachine>();
            }
        }

        void Update()
        {

        }

        private void FixedUpdate()
        {
            ExecutingSubState();
        }

        private void OnEnable()
        {
            
        }

        private void OnTriggerStay(Collider other)
        {
            //if (other.gameObject.layer == LayerMask.GetMask("Avatar"))
            //{
            //    _playersTransform = other.gameObject.transform;
            //    Debug.Log("¡Te encontré!");
            //}

            if (other.gameObject.layer == LayerMask.NameToLayer("Avatar"))
            {
                _avatarsTransform = other.gameObject.transform;

                Debug.Log("¡Ya te vi!");

                if (Physics.Raycast(this.gameObject.transform.position, _avatarsTransform.position,
                    out _currentRaycastHit, 50, LayerMask.GetMask("Obstacle")))
                {
                    print("YESSSSSSSSSSSS");
                    return;
                }
                if (Physics.Raycast(this.gameObject.transform.position, _avatarsTransform.position,
                    out _currentRaycastHit, 50, LayerMask.GetMask("Avatar")))
                {
                    print("NOOOOOOOOOOOOOO");
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            //if (other.gameObject.layer == LayerMask.GetMask("Avatar"))
            //{
            //    _playersTransform = other.gameObject.transform;
            //    Debug.Log("¡Te encontré!");
            //}

            if (other.gameObject.layer == LayerMask.NameToLayer("Avatar"))
            {
                _avatarsTransform = other.gameObject.transform;

                Debug.Log("¡Ya te vi!");

                if (Physics.Raycast(this.gameObject.transform.position, _avatarsTransform.position,
                    out _currentRaycastHit, 5, LayerMask.GetMask("Obstacle")))
                {
                    print("YESSSSSSSSSSSS");
                    return;
                }
                if (Physics.Raycast(this.gameObject.transform.position, _avatarsTransform.position,
                    out _currentRaycastHit, 5, LayerMask.GetMask("Avatar")))
                {
                    print("NOOOOOOOOOOOOOO");
                }
            }
        }

        #endregion

        #region LocalMethods

        protected virtual void InvokeStateMechanic()
        {
            switch (_currentEnemyBehaviour.stateMechanic)
            {
                case StateMechanics.STOP:
                    _fsm.StateMechanic(StateMechanic.STOP);
                    break;
                case StateMechanics.MOVE:
                    _fsm.StateMechanic(StateMechanic.MOVE);
                    break;
                case StateMechanics.TURN:
                    _fsm.StateMechanic(StateMechanic.TURN);
                    break;
            }
        }

        protected virtual void InitializeSubState()
        {
            switch (_currentEnemyBehaviour.stateMechanic)
            {
                case StateMechanics.STOP:
                    InitializeStopSubStateMachine();
                    break;
                case StateMechanics.MOVE:
                    InitializeMoveSubStateMachine();
                    break;
                case StateMechanics.TURN:
                    InitializeTurnSubStateMachine();
                    break;
            }
        }

        protected virtual void ExecutingSubState()
        {
            switch (_currentEnemyBehaviour.stateMechanic)
            {
                case StateMechanics.STOP:
                    ExecutingStopSubStateMachine();
                    break;
                case StateMechanics.MOVE:
                    ExecutingMoveSubStateMachine();
                    break;
                case StateMechanics.TURN:
                    ExecutingTurnSubStateMachine();
                    break;
            }
        }

        protected virtual void FinalizeSubState()
        {
            switch (_currentEnemyBehaviour.stateMechanic)
            {
                case StateMechanics.STOP:
                    FinalizeStopSubStateMachine();
                    break;
                case StateMechanics.MOVE:
                    FinalizeMoveSubStateMachine();
                    break;
                case StateMechanics.TURN:
                    FinalizeTurnSubStateMachine();
                    break;
            }
        }

        protected virtual IEnumerator TimerForEnemyBehaviour()
        {
            yield return new WaitForSeconds(_currentEnemyBehaviour.durationTime);
            FinalizeSubState();
            GoToNextEnemyBehaviour();
        }

        protected virtual void GoToNextEnemyBehaviour()
        {
            _currentEnemyBehaviourIndex++;

            if (_currentEnemyBehaviourIndex >= enemyNPC_SO.patrolBehaviours.Length)
            {
                _currentEnemyBehaviourIndex = 0;
            }
            _currentEnemyBehaviour = enemyNPC_SO.patrolBehaviours[_currentEnemyBehaviourIndex];

            InitializeSubState();
            //CalculateStateMechanicDirection();
            InvokeStateMechanic();

            if (_currentEnemyBehaviour.durationTime >= 0)
            {
                StartCoroutine(TimerForEnemyBehaviour());
            }
        }

        #endregion

        #region SubStateMachineMethods

        #region StopSubStateMachineMethods
        protected virtual void InitializeStopSubStateMachine()
        {
            _fsm.SetMovementSpeed = 0.0f;
            _fsm._movementDirection = Vector3.zero;
        }

        protected virtual void ExecutingStopSubStateMachine()
        {
            // NOTHING
        }

        protected virtual void FinalizeStopSubStateMachine()
        {
            // NOTHING
        }

        #endregion StopSubStateMachineMethods

        #region MoveSubStateMachineMethods

        protected virtual void InitializeMoveSubStateMachine()
        {

        }

        protected virtual void ExecutingMoveSubStateMachine()
        {

        }

        protected virtual void FinalizeMoveSubStateMachine()
        {

        }

        #endregion MoveSubStateMachineMethods

        #region TurnSubStateMachineMethods

        protected virtual void InitializeTurnSubStateMachine()
        {
            print("I'M ROTATING!");
        }

        protected virtual void ExecutingTurnSubStateMachine()
        {

        }

        protected virtual void FinalizeTurnSubStateMachine()
        {

        }

        #endregion TurnSubStateMachineMethods

        #endregion SubStateMachineMethods

    }

}