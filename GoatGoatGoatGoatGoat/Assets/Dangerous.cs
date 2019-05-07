using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dangerous : MonoBehaviour {


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject == Goat.instance.gameObject)
        {
            GameManager.instance.disableMovement = true;
        }
    }
}
