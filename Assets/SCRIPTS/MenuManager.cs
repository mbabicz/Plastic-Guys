using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private AudioClip clickSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(0)){
            Debug.Log("click");
            if(player.GetComponent<Rigidbody>().velocity.magnitude < 1 && player.GetComponent<Rigidbody>().velocity.magnitude > -1 )
            player.GetComponent<Rigidbody>().velocity = Random.onUnitSphere * 50;
            //player.GetComponent<Rigidbody>().AddForce(new Vector2(1000,1000));
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
