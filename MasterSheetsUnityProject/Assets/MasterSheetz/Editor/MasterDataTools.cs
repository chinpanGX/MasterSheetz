using System;
using UnityEditor;
using UnityEngine;

namespace MasterData.Editor
{
    public static class MasterDataTools
    {
        [MenuItem("Development Tools/MasterData/Open Link GoogleDrive", false, 100)]
        public static void OpenMasterDataDrive()
        {
            var guilds =
                AssetDatabase.FindAssets("t:GoogleDriveLinkAsset");
            if (guilds.Length <= 0)
            {
                EditorUtility.DisplayDialog("Error", "GoogleDriveLinkAsset not found", "OK");
                return;
            }

            var path = AssetDatabase.GUIDToAssetPath(guilds[0]);
            var link = AssetDatabase.LoadAssetAtPath<MasterSheetzConfigAsset>(path).GoogleDriveLink;
            if (string.IsNullOrEmpty(link))
            {
                EditorUtility.DisplayDialog("Error", "Link is not entered", "OK");
                return;
            }

            if (!link.StartsWith("https://drive.google.com"))
            {
                EditorUtility.DisplayDialog("Error", "Link is not a Google Drive link", "OK");
                return;
            }
            Application.OpenURL(link);
        }

        [MenuItem("Development Tools/MasterData/Open MasterData Folder", false, 101)]
        public static void OpenMasterDataFolder()
        {
            var guilds = AssetDatabase.FindAssets("t:MasterSheetzConfigAsset");
            if (guilds.Length <= 0)
            {
                EditorUtility.DisplayDialog("Error", "MasterSheetzConfigAsset not found", "OK");
                return;
            }

            var path = AssetDatabase.GUIDToAssetPath(guilds[0]);
            var config = AssetDatabase.LoadAssetAtPath<MasterSheetzConfigAsset>(path);
            if (string.IsNullOrEmpty(config.MasterDataPath))
            {
                EditorUtility.DisplayDialog("Error", "MasterData path is not set", "OK");
                return;
            }

            EditorUtility.RevealInFinder(config.MasterDataPath);
        }
    }
}