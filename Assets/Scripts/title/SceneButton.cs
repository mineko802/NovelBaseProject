using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour

{

    public void Loadmain()

    {

        SceneManager.LoadScene("Main");

    }

    public void Loadcharacter()

    {

        SceneManager.LoadScene("Character");

    }

    public void Loadchapter()

    {

        SceneManager.LoadScene("Chapter");

    }

    public void Backtitle()

    {

        SceneManager.LoadScene("Title");

    }

}
