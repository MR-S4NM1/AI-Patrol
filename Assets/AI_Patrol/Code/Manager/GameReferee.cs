using UnityEngine;

namespace Mr_Sanmi.AI_Agents
{
    public class GameReferee : MonoBehaviour
    {
        #region References

        public static GameReferee instance;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }

        #endregion

        #region PublicMethods

        public void ChangeToVictoryScene() 
        {
            SceneChanger.instance.ChangeSceneTo(1);
        }

        #endregion
    }

}