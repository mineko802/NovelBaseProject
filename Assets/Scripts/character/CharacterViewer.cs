using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterViewer : MonoBehaviour
{
    [System.Serializable]
    public class CharacterData
    {
        public Sprite image;
        public string name;
        [TextArea] public string description;
    }

    public CharacterData[] characters;
    public Image characterImage;
    public TMP_Text nameText;
    public TMP_Text descText;

    private int currentIndex = 0;

    void Start()
    {
        ShowCharacter(currentIndex);
    }

    public void NextCharacter()
    {
        currentIndex++;
        if (currentIndex >= characters.Length) currentIndex = 0; // ループ
        ShowCharacter(currentIndex);
    }

    public void PrevCharacter()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = characters.Length - 1; // ループ
        ShowCharacter(currentIndex);
    }

    void ShowCharacter(int index)
    {
        CharacterData c = characters[index];
        characterImage.sprite = c.image;
        nameText.text = c.name;
        descText.text = c.description;
    }

    
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("title");
    }
}