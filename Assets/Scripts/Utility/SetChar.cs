using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChar : MonoBehaviour
{
    public Character thisChar;

    private void Awake()
    {
        if (CharData.instance.currentChar != thisChar)
        {
            this.gameObject.SetActive(false);
        }
    }
}