using UnityEngine;

public class CharSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject dog;
    [SerializeField]
    private GameObject cat;
    [SerializeField]
    private GameObject rabbit;
    [SerializeField]
    private GameObject turtle;

    public static CharSelect instance;

    public void RightBtn()
    {
        switch (CharData.instance.currentChar)
        {
            case Character.Dog:
                CharData.instance.currentChar = Character.Cat;
                break;
            case Character.Cat:
                CharData.instance.currentChar = Character.Rabbit;
                break;
            case Character.Rabbit:
                CharData.instance.currentChar = Character.Turtle;
                break;
            case Character.Turtle:
                CharData.instance.currentChar = Character.Dog;
                break;
        }
    }
    public void LeftBtn()
    {
        switch (CharData.instance.currentChar)
        {
            case Character.Dog:
                CharData.instance.currentChar = Character.Turtle;
                break;
            case Character.Cat:
                CharData.instance.currentChar = Character.Dog;
                break;
            case Character.Rabbit:
                CharData.instance.currentChar = Character.Cat;
                break;
            case Character.Turtle:
                CharData.instance.currentChar = Character.Rabbit;
                break;
        }
    }

    private void Update()
    {
        switch (CharData.instance.currentChar)
        {
            case Character.Dog:
                dog.SetActive(true);
                cat.SetActive(false);
                rabbit.SetActive(false);
                turtle.SetActive(false);
                break;
            case Character.Cat:
                dog.SetActive(false);
                cat.SetActive(true);
                rabbit.SetActive(false);
                turtle.SetActive(false);
                break;
            case Character.Rabbit:
                dog.SetActive(false);
                cat.SetActive(false);
                rabbit.SetActive(true);
                turtle.SetActive(false);
                break;
            case Character.Turtle:
                dog.SetActive(false);
                cat.SetActive(false);
                rabbit.SetActive(false);
                turtle.SetActive(true);
                break;
        }
    }
}