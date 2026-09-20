#if UNITY_2021_1_OR_NEWER||USE_UNITY_UIBUILDER

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fix.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UxmlFix : FixEditorBase
{
    [MenuItem("Assets/" + nameof(Fix) + "/" + nameof(VisualTreeAssetFix), true)]
    public static bool IsValidateAsset()
    {
        return Selection.activeObject is VisualTreeAsset;
    }

    [MenuItem("Assets/" + nameof(Fix) + "/" + nameof(VisualTreeAssetFix))]
    [MenuItem(FixRoot + "VisualTreeAssetFix")]
    public static void Fix()
    {
        if (!(Selection.activeObject is VisualTreeAsset visualTreeAsset)) return;
        VisualTreeAssetFix(visualTreeAsset);
    }

    internal static void VisualTreeAssetFix(VisualTreeAsset visualTreeAsset)
    {
        if (visualTreeAsset == null) throw new Exception("VisualTreeAsset Is Null");
        var assetPath = AssetDatabase.GetAssetPath(visualTreeAsset);
        if (assetPath.EndsWith(".uxml"))
        {
            Debug.Log("资源已经是uxml文件");
            return;
        }

        var type = Type.GetType("Unity.UI.Builder.VisualTreeAssetToUXML,UnityEditor.UIBuilderModule");
        if (type == null) throw new Exception("Find VisualTreeAssetToUXML Failure");

        string content = (string) type.GetMethod("GenerateUXML", new Type[]
            {
                typeof(VisualTreeAsset),
                typeof(string),
                typeof(bool),
            })
            .Invoke(null, new object[]
            {
                visualTreeAsset,
                assetPath,
                true,
            });
        var path = $"{Path.GetDirectoryName(assetPath)}/{Path.GetFileNameWithoutExtension(assetPath)}.uxml";

        File.WriteAllText(path, content);
        var assetMetaPath = $"{assetPath}.meta";
        if (File.Exists(assetMetaPath))
            File.Move(assetMetaPath, $"{path}.meta");
    }
}
#endif