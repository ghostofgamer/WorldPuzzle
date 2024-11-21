using UnityEngine;

public class LevelProgressManager : MonoBehaviour
{
    /*public void SaveLevelProgress(int packIndex, int worldIndex, int levelIndex, bool isCompleted)
    {
        string key = LevelKeyGenerator.GenerateKey(packIndex, worldIndex, levelIndex);
        PlayerPrefs.SetInt(key, isCompleted ? 1 : 0);
        PlayerPrefs.Save();
    }*/
    
    public void SaveLevelProgress(int packIndex, int worldIndex, int levelIndex, bool isCompleted)
    {
        string key = LevelKeyGenerator.GenerateKey(packIndex, worldIndex);
        PlayerPrefs.SetInt(key, isCompleted ? levelIndex : 0);
        PlayerPrefs.Save();
        Debug.Log(key+"      "+levelIndex);
    }

    public bool LoadLevelProgress(int packIndex, int worldIndex, int levelIndex)
    {
        string key = LevelKeyGenerator.GenerateKey(packIndex, worldIndex, levelIndex);
        return PlayerPrefs.GetInt(key, 0) == 1;
    }
    
    public int LoadLevelProgress(int packIndex, int worldIndex)
    {
        string key = LevelKeyGenerator.GenerateKey(packIndex, worldIndex);
        Debug.Log("ЧТо вернул? " + PlayerPrefs.GetInt(key, 0));
        return PlayerPrefs.GetInt(key, 0);
    }
}
