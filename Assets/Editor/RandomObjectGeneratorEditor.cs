using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;


public class RandomObjectGeneratorEditor : Editor
{
    [MenuItem("Tools/Object Placement/Random Placement")]
    public static void GenerateObjectsFromMenu()
    {
        // Find the first RandomObjectPlacer script in the scene
        RandomObjectGenerator placer = FindAnyObjectByType<RandomObjectGenerator>();

        if (placer != null)
        {
            placer.GenerateObjects();
        }
        else
        {
            Debug.LogError("No RandomObjectPlacer script found in the scene. Please attach it to a GameObject.");
        }
    }
}
