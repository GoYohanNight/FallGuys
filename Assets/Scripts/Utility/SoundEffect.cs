using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public static SoundEffect instance;

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