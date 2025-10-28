using UnityEngine;
[System.Serializable]
public class SpawnPointData
{
    public string id;
    public string sceneName;
    public Vector3 position;
    
    public SpawnPointData(string id, string sceneName, Vector3 position)
    {
        this.id = id;
        this.sceneName = sceneName;
        this.position = position;
    }
}
