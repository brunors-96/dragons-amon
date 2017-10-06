using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool pausado = false;
    public GameObject menuDePause;
    public GameObject player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Pausar();
        GameOver();
    }

    public void irParaACena(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }

    private void Pausar()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausado = !pausado;
        }
        if (pausado)
        {
            menuDePause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            menuDePause.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        menuDePause.SetActive(false);
        Time.timeScale = 1;
        pausado = !pausado;
    }

    private void GameOver()
    {
        if (player.transform.position.y <= -20)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
