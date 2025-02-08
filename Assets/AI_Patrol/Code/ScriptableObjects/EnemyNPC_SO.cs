using UnityEngine;

namespace Mr_Sanmi.AI_Agents
{
    #region Enums

    public enum StateMechanics
    {
        STOP,
        MOVE,
        TURN
    }

    #endregion

    #region Structs

    [System.Serializable] //Convertion to bytes which can be saved in the HDD.
    public struct PatrolBehaviours
    {
        public StateMechanics stateMechanic;
        public float movSpeed;
        public float durationTime;
        [SerializeField] public Vector3 destinyDirection;
        [SerializeField] public Vector3 destinyRotation;
    }

    [System.Serializable]
    public struct VisionCodeParameters
    {
        public float distance;
        public float fieldOfView;
    }

    [System.Serializable]
    public struct SpawnParameters
    {
        [SerializeField] public Vector3 position;
        [SerializeField] public Vector3 rotation;
    }

    #endregion

    [CreateAssetMenu(fileName = "EnemyNPC_SO", menuName = "Scriptable Objects/EnemyNPC_SO")]
    public class EnemyNPC_SO : ScriptableObject
    {
        //Patrol
        [SerializeField] public PatrolBehaviours[] patrolBehaviours;

        //Vision Cone
        [SerializeField] public VisionCodeParameters visionCodeParameters;

        //Spawn Transformation
        [SerializeField] public SpawnParameters spawnParameters;
    }
}