using UnityEditor;
using UnityEngine;
using System.Linq;

public static class FileIdScanner
{
    [MenuItem("Tools/Scan All MonoScript FileIDs")]
    public static void Scan()
    {
        var scripts = MonoImporter.GetAllRuntimeMonoScripts();

        foreach (var script in scripts)
        {
            if (script == null)
                continue;

            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(
                    script,
                    out string guid,
                    out long fileID))
            {
                var type = script.GetClass();

                Debug.Log(
                    $"{type?.FullName ?? script.name} | " +
                    $"fileID: {fileID} | GUID: {guid} | " +
                    $"path: {AssetDatabase.GetAssetPath(script)}"
                );
            }
        }
    }
}