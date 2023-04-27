using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCollision : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
         GameObject obj = Instantiate(prefab, collision.contacts[0].point, Quaternion.identity);
    }
}
