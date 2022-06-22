using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip clickSound;

    void Update()
    {
        if(Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(0)){
            if(player.GetComponent<Rigidbody>().velocity.magnitude < 1 && player.GetComponent<Rigidbody>().velocity.magnitude > -1 )
            player.GetComponent<Rigidbody>().velocity = Random.onUnitSphere * 50;
        }
    }


    public void ClickSound(){
        AudioSource.PlayClipAtPoint(clickSound,transform.position);
    }

   public void Button_Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("mainscene", LoadSceneMode.Single);
    }
}
