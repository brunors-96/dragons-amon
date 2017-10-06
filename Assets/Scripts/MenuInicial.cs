using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
     * O metódo irParaACena recebe o nomeDaCena enviado pelo Unity e carrega em SceneManager.LoadScene a cena com o nome passado.
     */
    public void irParaACena(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
