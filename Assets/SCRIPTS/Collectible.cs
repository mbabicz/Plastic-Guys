using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    public AudioClip coinSound;
    public GameManager GM;

    void FixedUpdate()
    {
        transform.Rotate(0 * Time.deltaTime,100* Time.deltaTime,0* Time.deltaTime);
    }


    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player")){
            GM.points = GM.points + 1;
            AudioSource.PlayClipAtPoint(coinSound,transform.position);
            gameObject.SetActive(false);
        }
        
    }
}