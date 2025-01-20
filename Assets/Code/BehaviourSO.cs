using UnityEngine;

namespace Mr_Sanmi.AI_Agents
{
    public enum EnemyBehaviours
    {
        ROTATE,
        MOVE_FORWARD,
        STAY_STILL
    }

    [System.Serializable]
    public struct BehaviourModes
    {
        public float movSpeed;
        public Vector3 direction;
    }

    [CreateAssetMenu(fileName = "BehaviourSO", menuName = "Scriptable Objects/BehaviourSO")]
    public class BehaviourSO : ScriptableObject
    {
        public EnemyBehaviours[] enemybehaviours;

    }
}