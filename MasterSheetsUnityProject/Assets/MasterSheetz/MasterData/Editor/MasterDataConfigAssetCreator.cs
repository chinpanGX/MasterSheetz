using UnityEditor;
using UnityEngine;

namespace MasterData.Editor
{
    public static class MasterDataConfigAssetCreator
    {
        /// <summary>
        /// 指定された ScriptableObject 型のアセットをプロジェクト内で検索し、見つからない場合は新しいアセットを作成します。
        /// </summary>
        /// <param name="assetName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateAssetIfNeeded<T>(string assetName) where T : ScriptableObject
        {
            var existingAssets = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            if (existingAssets.Length > 0)
            {
                EditorUtility.DisplayDialog("Error", $"{typeof(T).Name} asset already exists", "OK");
                var path = AssetDatabase.GUIDToAssetPath(existingAssets[0]);
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<T>(path);
                return null;
            }

            var asset = ScriptableObject.CreateInstance<T>();
            
            
            AssetDatabase.CreateAsset(asset, $"Assets/{assetName}.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
            return asset;
        }

        /// <summary>
        /// プロジェクト内で指定された ScriptableObject 型のアセットを検索します。
        /// アセットが見つからない場合、新しいアセットを作成するかどうかをユーザーに確認します。
        /// </summary>
        /// <typeparam name="T">検索または作成する ScriptableObject の型。</typeparam>
        /// <returns>既存または新しく作成された ScriptableObject アセット。ユーザーが作成を選ばなかった場合は null を返します。</returns>
        public static T GetAsset<T>() where T : ScriptableObject
        {
            var typeName = typeof(T).Name;
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            T asset = null;
            if (guids.Length > 0)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                asset = AssetDatabase.LoadAssetAtPath<T>(path);
            }
            
            if (asset != null)
            {
                return asset;
            }

            if (EditorUtility.DisplayDialog(
                    $"{typeName} Not Found", 
                    $"{typeName} was not found. Would you like to create a new one?", 
                    "Yes", 
                    "No"))
            {
                asset = CreateAssetIfNeeded<T>(typeName);
                return asset;
            }
                
            EditorUtility.DisplayDialog($"{typeName} Not Found", $"{typeName} was not found.", "OK");
            return null;

        }
        
        /// <summary>
        /// 指定した ScriptableObject 型のアセットを検索して選択。
        /// 見つからない場合、作成するかどうかをユーザーに確認します。
        /// </summary>
        /// <typeparam name="T">ScriptableObject の型</typeparam>
        /// <param name="assetName">アセットの保存ファイル名</param>
        public static void OpenOrCreateAsset<T>(string assetName) where T : ScriptableObject
        {
            var typeName = typeof(T).Name;
            // 指定した型のアセットを検索
            var guids = AssetDatabase.FindAssets($"t:{typeName}");
            T asset = null;
            if (guids.Length > 0) 
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                asset = AssetDatabase.LoadAssetAtPath<T>(path);

                if (asset != null)
                {
                    SelectAndPingObject(asset);
                    return;
                }
            }

            // アセットが見つからない場合、作成するかを確認
            if (EditorUtility.DisplayDialog(
                    $"{typeName} Not Found", 
                    $"{typeName} was not found. Would you like to create a new one?", 
                    "Yes", 
                    "No"))
            {
                asset = CreateAssetIfNeeded<T>(assetName);
            }

            if (asset == null) return;
            
            // アセットを選択して強調表示
            SelectAndPingObject(asset);
        }
        
        /// <summary>
        /// エディタ上で指定したオブジェクトを選択し、強調表示します。
        /// </summary>
        /// <param name="obj">選択するオブジェクト</param>
        private static void SelectAndPingObject(Object obj)
        {
            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(obj);
        }
    }
}