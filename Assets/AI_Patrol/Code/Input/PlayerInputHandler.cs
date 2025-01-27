using UnityEngine;
using UnityEngine.InputSystem;

namespace Mr_Sanmi.AI_Agents
{
    public class PlayerInputHandler : MonoBehaviour
    {
        #region References

        [SerializeField] protected PlayerInput _playerInput;
        [SerializeField] protected PlayersAvatar _avatar;

        #endregion

        #region UnityMethods

        void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _avatar = FindAnyObjectByType<PlayersAvatar>();
        }

        #endregion


        #region PlayerInputCallbacks

        public void OnMove(InputAction.CallbackContext value)
        {
            //_avatar.OnMOVE();
        }

        #endregion

    }

}