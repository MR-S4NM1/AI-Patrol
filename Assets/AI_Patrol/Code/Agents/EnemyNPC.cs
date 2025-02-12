using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mr_Sanmi.AI_Agents
{

    public class EnemyNPC : Agent
    {
        #region Knobs

        [SerializeField] public EnemyNPC_SO enemyNPC_SO;

        #endregion

        #region RuntimeVariables

        [SerializeField] protected PatrolBehaviours _currentEnemyBehaviour;
        protected int _currentEnemyBehaviourIndex;
        [SerializeField] protected Transform _avatarsTransform;
        protected StateMechanics _previousMovementStateMechanic;
        protected RaycastHit _currentRaycastHit;
        protected float _newAngle;
        [SerializeField] protected float _currentVelocity;
        protected float _targetAngle;
        protected Coroutine _currentEnemyRoutine;
        protected float _fDTime;
        protected Quaternion _startRot;
        protected Quaternion _endRot;
        protected bool _isStillRotating;
        protected Vector3 _destinyPos;
        protected float _distanceToDestiny;
        protected float _initialDistance;
        protected Quaternion _rotation;
        protected Vector3 _relativePos;

        #endregion

        #region UnityMethods
        private void OnDrawGizmos()
        {

        }
        void Start()
        {

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
            if (_fsm == null)
            {
                _fsm = GetComponent<FiniteStateMachine>();
            }

            InitializePatrolBehaviour();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Avatar"))
            {
                _avatarsTransform = other.gameObject.transform;;

                if (Physics.Linecast(transform.position, _avatarsTransform.position, out _currentRaycastHit))
                {
                    if (_currentRaycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                    {
                        print("¡No veo nada!");
                    }
                    if (_currentRaycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Avatar"))
                    {
                        print("¡Ya te vi!");
                        GameReferee.instance.ResetPlayersPosition();
                    }
                }
            }
        }

        #endregion

        #region LocalMethods

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
        }

        protected void InitializePatrolBehaviour()
        {
            StopAllCoroutines();
            _currentEnemyBehaviourIndex = 0;

            if (enemyNPC_SO.patrolBehaviours.Length > 0)
            {
                _currentEnemyBehaviour = enemyNPC_SO.patrolBehaviours[0];
            }
            else
            {
                _currentEnemyBehaviour.stateMechanic = StateMechanics.STOP;
                _currentEnemyBehaviour.durationTime = -1;
            }
            InitializeSubState();
        }

        protected virtual void InvokeCorroutine()
        {
            _currentEnemyRoutine = StartCoroutine(TimerForEnemyBehaviour());
        }

        #endregion

        #region SubStateMachineMethods

        #region StopSubStateMachineMethods
        protected virtual void InitializeStopSubStateMachine()
        {
            _fsm.SetMovementSpeed = 0.0f;
            _fsm._movementDirection = Vector3.zero;
            _fsm._angularVelocity = Vector3.zero;
            _fsm.StateMechanic(StateMechanic.STOP);

            if (_currentEnemyBehaviour.durationTime >= 0)
            {
                Invoke("InvokeCorroutine", 0f);
            }
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
            _fsm.StateMechanic(StateMechanic.MOVE);

            _fsm.SetMovementSpeed = _currentEnemyBehaviour.movSpeed;
            _destinyPos = _currentEnemyBehaviour.destinyDirection;

            _isStillRotating = true;
            _fDTime = 0f;
            //_startRot = _fsm.r
            _endRot = Quaternion.Euler(_currentEnemyBehaviour.destinyDirection);
            _initialDistance = Vector3.Distance(gameObject.transform.position, _destinyPos);
        }

        protected virtual void ExecutingMoveSubStateMachine()
        {
            _fsm._movementDirection = (_destinyPos - gameObject.transform.position).normalized;
            _distanceToDestiny = Vector3.Distance(gameObject.transform.position, _destinyPos);

            if (_isStillRotating && _distanceToDestiny > 0.1f)
            {
                _fDTime += Time.fixedDeltaTime;
                float time = _fDTime * _initialDistance;

                //_fsm.RBRotationWhileMoving(_startRot, _endRot, time);

                //transform.Rotate(Vector3.up * Time.fixedDeltaTime * _newAngle);
            }
            else if (_isStillRotating)
            {
                //_newAngle = _currentEnemyBehaviour.destinyRotation.y;
                _fsm.SetRBRotation(_currentEnemyBehaviour.destinyDirection);
                _isStillRotating = false;
            }

            if (_distanceToDestiny <= 0.1f)
            {
                FinalizeSubState();
                GoToNextEnemyBehaviour();
            }
        }

        protected virtual void FinalizeMoveSubStateMachine()
        {

        }

        #endregion MoveSubStateMachineMethods

        #region TurnSubStateMachineMethods

        protected virtual void InitializeTurnSubStateMachine()
        {
            //print("I WILL START ROTATING!");
            ////_newAngle = _currentEnemyBehaviour.destinyRotation.y / _currentEnemyBehaviour.durationTime;
            //print("DestinyRotation" + _currentEnemyBehaviour.destinyRotation);
            //print("DurationTime" + _currentEnemyBehaviour.durationTime);
            _fsm.StateMechanic(StateMechanic.TURN);
            _isStillRotating = true;
            _fDTime = 0f;
            _startRot = _fsm.GetRBRotation();
            _endRot = Quaternion.Euler(_currentEnemyBehaviour.destinyRotation);

            if (_currentEnemyBehaviour.durationTime >= 0)
            {
                Invoke("InvokeCorroutine", 0f);
            }
        }

        protected virtual void ExecutingTurnSubStateMachine()
        {
            //Debug.LogWarning("I'm Rotating, " + transform.rotation.eulerAngles.y + " - New Angle " + _newAngle);
            if (_isStillRotating && _currentEnemyBehaviour.durationTime > 0.1f)
            {
                _fDTime += Time.fixedDeltaTime; 

                float time = _fDTime / _currentEnemyBehaviour.durationTime;

                _fsm.RBRotation(_startRot, _endRot, time);

                //transform.Rotate(Vector3.up * Time.fixedDeltaTime * _newAngle);
            }
            else if(_isStillRotating)
            {
                //_newAngle = _currentEnemyBehaviour.destinyRotation.y;
                _fsm.SetRBRotation(_currentEnemyBehaviour.destinyRotation);
                _isStillRotating = false;
            }
        }

        protected virtual void FinalizeTurnSubStateMachine()
        {

        }

        #endregion TurnSubStateMachineMethods

        #endregion SubStateMachineMethods

    }

}