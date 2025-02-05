using UnityEngine;
using System.Collections.Generic;

namespace Mr_Sanmi.AI_Agents
{
    public class EnemyNPCFactory : MonoBehaviour
    {
        #region Variables

        [Header("Parameters")]
        [SerializeField] protected GameObject enemyPrefab;
        [SerializeField] protected EnemyNPC_SO[] enemiesScriptableObjects;

        [Header("Runtime Variables")]
        [SerializeField] protected List<GameObject> enemyInstancesGameObject;

        #endregion

        #region RuntimeVariables

        GameObject enemyInstanceGameObject;

        #endregion

        #region UnityMethods
        #endregion

        #region PublicMethods

        public void CreateEnemies()
        {
            foreach(EnemyNPC_SO enemy in enemiesScriptableObjects)
            {
                // Generate the instance of a new Enemy NPC, baser on the prefab.
                enemyInstanceGameObject = Instantiate(enemyPrefab);

                // According to the data from the Scriptable Object,
                // We set the position and rotation of the Enemy.
                enemyInstanceGameObject.transform.position = enemy.spawnParameters.position;
                enemyInstanceGameObject.transform.rotation = Quaternion.Euler(enemy.spawnParameters.rotation);

                // To have a better structure of the scene,
                // every enemy will be adopted by this game object.
                enemyInstanceGameObject.transform.parent = this.gameObject.transform;

                // TODO: Add Patrol Behaviour data.
                // TODO: Generate the vision cone, according to the SO.

                // Add the enemy instance for a future deletion of this enemy.
                enemyInstancesGameObject.Add(enemyInstanceGameObject);
            }
        }

        public void DestroyEnemies()
        {
            for(int i = enemyInstancesGameObject.Count - 1; i >= 0; i--)
            {
                enemyInstanceGameObject = enemyInstancesGameObject[i];
                enemyInstancesGameObject.Remove(enemyInstanceGameObject);
                DestroyImmediate(enemyInstanceGameObject);
            }
            enemyInstancesGameObject.Clear();
        }

        #endregion

    }

}