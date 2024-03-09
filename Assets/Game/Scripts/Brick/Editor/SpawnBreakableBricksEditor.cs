using Brick.Runtime;
using UnityEditor;
using UnityEngine;

namespace Brick.Editor
{
    [CustomEditor(typeof(SpawnBreakableBricks))]
    public class SpawnBreakableBricksEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {                
            SpawnBreakableBricks spawnBreakableBricks = (SpawnBreakableBricks) target;
            DrawDefaultInspector();
            
            if (GUILayout.Button("Generate Breakable Bricks"))
            {
                spawnBreakableBricks.GenerateBricks();
            }
        }
    }
}