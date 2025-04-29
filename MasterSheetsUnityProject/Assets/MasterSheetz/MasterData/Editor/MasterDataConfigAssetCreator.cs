using UnityEditor;
using UnityEngine;

namespace MasterData.Editor
{
    public static class MasterDataConfigAssetCreator
    {
        public static void CreateAssetIfNeeded<T>(string assetName) where T : ScriptableObject
        {
            var existingAssets = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            if (existingAssets.Length > 0)
            {
                EditorUtility.DisplayDialog("Error", $"{typeof(T).Name} asset already exists", "OK");
                var path = AssetDatabase.GUIDToAssetPath(existingAssets[0]);
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<T>(path);
                return;
            }

            var asset = ScriptableObject.CreateInstance<T>();
            
            
            AssetDatabase.CreateAsset(asset, $"Assets/{assetName}.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}