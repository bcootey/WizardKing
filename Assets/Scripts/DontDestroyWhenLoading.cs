using UnityEngine;

public class DontDestroyWhenLoading : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
