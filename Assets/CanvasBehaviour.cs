using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasBehaviour : MonoBehaviour
{
    public SceneAsset firstScene;
    public GameObject mainMenu;
    public GameObject creditsMenu;

    private GameObject m_currentMenu;

    private void Awake()
    {
        References.canvas = gameObject;
        m_currentMenu = null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (m_currentMenu == mainMenu)
            {
                HideMenu();
            }
            else
            {
                ShowMenu(mainMenu);
            }
        }
    }

    // Button actions
    public void StartNewGame()
    {
        if (firstScene)
        {
            SceneManager.LoadScene(firstScene.name);
            HideMenu();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowMenu(GameObject menuToShow)
    {
        HideMenu();
        m_currentMenu = menuToShow;
        if (menuToShow && menuToShow.GetComponent<VerticalLayoutGroup>())
        {
            menuToShow.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void HideMenu()
    {
        if (m_currentMenu && m_currentMenu.GetComponent<VerticalLayoutGroup>())
        {
            m_currentMenu.SetActive(false);
        }
        m_currentMenu = null;
        Time.timeScale = 1;
    }
}
