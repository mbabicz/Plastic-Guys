using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class health : MonoBehaviour
{
    public static int health_ = 5;
    public static int maxHealth = 5;

    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {

    }

    void Update()
    {
        for (int i = 0; i < maxHealth; i++){
            if (i < health_){
                hearts[i].sprite = fullHeart;
            }
            else{
                hearts[i].sprite = emptyHeart;
            
             }
        }
    }


}
