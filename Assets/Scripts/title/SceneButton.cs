using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour

{

    public void Loadmain()

    {

        SceneManager.LoadScene("main");

    }

    public void Loadcharacter()

    {

        SceneManager.LoadScene("character");

    }

    public void Loadchapter()

    {

        SceneManager.LoadScene("chapter");

    }

}
