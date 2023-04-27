using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public static BackGroundMusic instance;

    private void Awake()
    {
        // 싱글톤 구현
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}