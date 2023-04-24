using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour
{
    public GameObject tileManager;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("TileManager").GetComponent<DestroyTile>().PlayerFloor++;
            Debug.Log("player");
            // event.
        }
    }
}
