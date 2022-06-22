using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRespawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        other.transform.position = new Vector3(95,9f,91);
    }
}
