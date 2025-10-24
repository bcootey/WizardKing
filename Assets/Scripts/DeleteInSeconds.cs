using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteInSeconds : MonoBehaviour
{
    public float seconds;
    void Start()
    {
        Destroy(this.gameObject, seconds);
    }
}
