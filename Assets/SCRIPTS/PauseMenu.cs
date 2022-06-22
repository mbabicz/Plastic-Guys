using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused;
    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] private GameObject[] UIcomponents;

    [SerializeField] private Slider red;
    [SerializeField] private Slider green;
    [SerializeField] private Slider blue;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject player;
    private static readonly int UnscaledTime = Shader.PropertyToID("_UnscaledTime");
    private Renderer rend;
    private Color color;


    [SerializeField] private AudioClip clickSound;
    void Start()
    {
        rend = playerUI.GetComponent<Renderer>();
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(false);


        UIcomponents = GameObject.FindGameObjectsWithTag("UIcomponent");
        for(int i=0;i<UIcomponents.Length; i++) UIcomponents[i].SetActive(true);

        

    }

    void Update()
    {
        if (GameIsPaused) ColorController();



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused == true)
            {
                Resume();
            }
            else if (GameIsPaused == false)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(false);
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        for(int i=0;i<UIcomponents.Length; i++) UIcomponents[i].SetActive(true);
    }
    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(true);
        GameIsPaused = true;
        for(int i=0;i<UIcomponents.Length; i++) UIcomponents[i].SetActive(false);
    }


    public void Button_Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("mainscene", LoadSceneMode.Single);
        Resume();
    }


    //materials
    public void ColorController(){

        color = rend.material.color;
        color.r = red.value;
        color.g = green.value;
        color.b = blue.value;
        if (rend.material.HasProperty(UnscaledTime)) rend.material.SetFloat(UnscaledTime, Time.unscaledTime);
        rend.material.color = color;
        rend.material.SetColor("_EmissionColor", color);
    }

    public void ApplyColor(){
        player.GetComponent<Renderer>().material.color = color;
        player.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);

    }

    public void ClickSound(){
        AudioSource.PlayClipAtPoint(clickSound,transform.position);
    }

}