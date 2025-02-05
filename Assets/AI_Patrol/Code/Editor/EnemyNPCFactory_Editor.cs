using UnityEngine;
using UnityEditor;

namespace Mr_Sanmi.AI_Agents
{
    [CustomEditor(typeof(EnemyNPCFactory))]
    public class EnemyNPCFactory_Editor : Editor
    {
        EnemyNPCFactory enemyNPCFactory;

        #region UnityMethods
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if(enemyNPCFactory == null)
            {
                enemyNPCFactory = (EnemyNPCFactory)target;
            }

            if (GUILayout.Button("Create Enemies"))
            {
                enemyNPCFactory.DestroyEnemies();
                enemyNPCFactory.CreateEnemies();
            }
            if (GUILayout.Button("Delete Enemies"))
            {
                enemyNPCFactory.DestroyEnemies();
            }
        }

        #endregion
    }

}