using UnityEngine;

namespace Mr_Sanmi.AI_Agents
{
    public enum EnemyBehaviours
    {
        ROTATE_TO_DIRECTION,
        MOVE_TO_WAYPOINT,
        STAY_STILL
    }

    [System.Serializable]
    public struct EnemyBehaviourModes
    {
        public EnemyBehaviours enemyBehaviour;
        public float movSpeed;
        public float timeDuration;
        public Vector3 direction;
    }

    [CreateAssetMenu(fileName = "BehaviourSO", menuName = "Scriptable Objects/BehaviourSO")]
    public class BehaviourSO : ScriptableObject
    {
        [SerializeField] public EnemyBehaviourModes[] enemybehaviours;
    }
}