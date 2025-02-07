using UnityEngine;
using System.Collections.Generic;

namespace Mr_Sanmi.AI_Agents
{
    public class EnemyNPCFactory : MonoBehaviour
    {
        #region Variables

        [Header("Parameters")]
        [SerializeField] protected GameObject enemyPrefab;
        [SerializeField] protected GameObject enemyVisionConePrefab;
        [SerializeField] protected EnemyNPC_SO[] enemiesScriptableObjects;

        [Header("Runtime Variables")]
        [SerializeField] protected List<GameObject> enemyInstancesGameObject;
        [SerializeField] protected List<GameObject> enemyVisionConeInstancesGameObject;

        #endregion

        #region RuntimeVariables

        GameObject enemyInstanceGameObject;
        GameObject enemyVisionConeInstanceGameObject;

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
                enemyVisionConeInstanceGameObject = Instantiate(enemyVisionConePrefab);

                // According to the data from the Scriptable Object,
                // We set the position and rotation of the Enemy.
                enemyInstanceGameObject.transform.position = enemy.spawnParameters.position;
                enemyInstanceGameObject.transform.rotation = Quaternion.Euler(enemy.spawnParameters.rotation);

                // To have a better structure of the scene,
                // every enemy will be adopted by this game object.
                enemyInstanceGameObject.transform.parent = this.gameObject.transform;

                // TODO: Add Patrol Behaviour data.
                enemyInstanceGameObject.GetComponent<EnemyNPC>().enemyNPC_SO = enemy;

                // TODO: Generate the vision cone, according to the SO.
                enemyVisionConeInstanceGameObject.transform.position = enemyInstanceGameObject.transform.position;
                enemyVisionConeInstanceGameObject.transform.rotation = Quaternion.Euler(enemy.spawnParameters.rotation);
                enemyVisionConeInstanceGameObject.transform.localScale = new Vector3(
                    enemy.visionCodeParameters.fieldOfView, 1.0f, enemy.visionCodeParameters.distance);
                enemyVisionConeInstanceGameObject.transform.SetParent(enemyInstanceGameObject.transform);

                // Add the enemy instance for a future deletion of this enemy.
                enemyInstancesGameObject.Add(enemyInstanceGameObject);
                enemyVisionConeInstancesGameObject.Add(enemyVisionConeInstanceGameObject);
            }
        }

        public void DestroyEnemies()
        {
            for(int i = enemyInstancesGameObject.Count - 1; i >= 0; i--)
            {
                enemyInstanceGameObject = enemyInstancesGameObject[i];
                enemyInstancesGameObject.Remove(enemyInstanceGameObject);
                DestroyImmediate(enemyInstanceGameObject);

                enemyVisionConeInstanceGameObject = enemyVisionConeInstancesGameObject[i];
                enemyVisionConeInstancesGameObject.Remove(enemyVisionConeInstanceGameObject);
                DestroyImmediate(enemyVisionConeInstanceGameObject);
            }
            enemyInstancesGameObject.Clear();
            enemyVisionConeInstancesGameObject.Clear();
        }

        #endregion

    }

}