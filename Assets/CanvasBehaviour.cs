using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasBehaviour : MonoBehaviour
{
    public SceneAsset firstScene;
    public GameObject mainMenu;
    public GameObject creditsMenu; // TODO check if ok to remove

    private GameObject m_currentMenu;

    private void Awake()
    {
        References.canvas = this;
        m_currentMenu = null;
    }

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

    public void ShowMainMenu()
    {
        ShowMenu(mainMenu);
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
