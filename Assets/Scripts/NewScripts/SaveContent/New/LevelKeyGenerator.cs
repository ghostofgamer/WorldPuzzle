using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelKeyGenerator : MonoBehaviour
{
    public static string GenerateKey(int packIndex, int worldIndex, int levelIndex)
    {
        return $"Pack{packIndex}_World{worldIndex}_Level{levelIndex}";
    }
    
    public static string GenerateKey(int packIndex, int worldIndex)
    {
        return $"Pack{packIndex}_World{worldIndex}";
    }
}
