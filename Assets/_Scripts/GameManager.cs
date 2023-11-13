using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject mainMenu;
    

    private bool paused;

    public bool IsPaused => paused;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Pause();
    }

    private void Update()
    {
        if (!paused)
        {
            if(InputManager.Instance.Pause)
            {
                Pause();
            }
        }
    }

    public void Pause(bool dontShowMenu = false)
    {
        InputManager.Instance.enabled = false;

        Time.timeScale = 0f;
        paused = true;

        if(!dontShowMenu)
            mainMenu.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        paused = false;

        mainMenu.SetActive(false);

        InputManager.Instance.enabled = true;
    }
}
