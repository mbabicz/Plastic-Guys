using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused;
    [SerializeField] private GameObject pauseMenuUI;


    [SerializeField] private GameObject[] UIcomponents;

    [SerializeField] private Slider red;
    [SerializeField] private Slider green;
    [SerializeField] private Slider blue;    

    [SerializeField] private MeshRenderer playerUI;

    void Start()
    {
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);

        UIcomponents = GameObject.FindGameObjectsWithTag("UIcomponent");
        for(int i=0;i<UIcomponents.Length; i++) UIcomponents[i].SetActive(true);

    }

    void Update()
    {
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

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        for(int i=0;i<UIcomponents.Length; i++) UIcomponents[i].SetActive(true);
    }
    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        for(int i=0;i<UIcomponents.Length; i++) UIcomponents[i].SetActive(false);
    }


    public void Button_Quit()
    {
        Application.Quit();
    }

    //materials
    private void OnEdit(){
        Color color = playerUI.material.color;
        color.r = red.value;
        color.g = green.value;
        color.b = blue.value;
        playerUI.material.color = color;
        playerUI.material.SetColor("_EmissionColor", color);
    }
}