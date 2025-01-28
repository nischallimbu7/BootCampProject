using UnityEditor;
using UnityEngine;

public class ModifyFBXReadWrite : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Assets/Modify FBX Read/Write To False")]
    static void SetFBXReadWriteToFalse()
    {
        ModifyFBXReadWriteProperty(false);
    }

    [MenuItem("Assets/Modify FBX Read/Write To True")]
    static void SetFBXReadWriteToTrue()
    {
        ModifyFBXReadWriteProperty(true);
    }

    static void ModifyFBXReadWriteProperty(bool enableReadWrite)
    {
        // Get the selected objects in the Unity Editor
        Object[] selectedObjects = Selection.objects;

        if (selectedObjects == null || selectedObjects.Length == 0)
        {
            Debug.LogWarning("No assets selected.");
            return;
        }

        int modifiedCount = 0;

        foreach (Object selectedObject in selectedObjects)
        {
            // Get the asset path of the selected object
            string assetPath = AssetDatabase.GetAssetPath(selectedObject);

            if (string.IsNullOrEmpty(assetPath) || !assetPath.EndsWith(".fbx"))
            {
                Debug.LogWarning($"Skipping non-FBX file: {assetPath}");
                continue;
            }

            // Load the ModelImporter for the FBX file
            ModelImporter modelImporter = AssetImporter.GetAtPath(assetPath) as ModelImporter;

            if (modelImporter == null)
            {
                Debug.LogError($"Could not load ModelImporter for: {assetPath}");
                continue;
            }

            // Check and update the Read/Write Enabled property
            if (modelImporter.isReadable != enableReadWrite)
            {
                modelImporter.isReadable = enableReadWrite;
                modelImporter.SaveAndReimport(); // Apply changes and re-import the asset
                modifiedCount++;
                Debug.Log($"Updated Read/Write for: {assetPath}");
            }
            else
            {
                Debug.Log($"Read/Write already set to {enableReadWrite} for: {assetPath}");
            }
        }

        Debug.Log($"Modified {modifiedCount} FBX files to Read/Write: {enableReadWrite}");
    }
#endif
}
