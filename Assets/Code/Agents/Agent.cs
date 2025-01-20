using UnityEngine;

namespace Mr_Sanmi.AI_Agents
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Agent : MonoBehaviour
    {
        #region References

        [SerializeField] public FiniteStateMachine fsm;

        #endregion

        #region Knobs

        #endregion

        #region RuntimeVariables


        #endregion

        #region UnityMethods

        private void OnDrawGizmos()
        {
            if(fsm == null)
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

        #endregion

        #region GettersAndSettters


        #endregion
    }

}