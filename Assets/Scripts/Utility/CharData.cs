using UnityEngine;

public enum Character
{
    Dog,
    Cat,
    Rabbit,
    Turtle
}

public class CharData : MonoBehaviour
{
    public static CharData instance;

    public Character currentChar = Character.Dog;

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
