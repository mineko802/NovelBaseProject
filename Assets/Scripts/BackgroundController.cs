using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] GameObject Room;
    [SerializeField] GameObject LivingRoom;
    [SerializeField] GameObject Kitchen;
    // Start is called before the first frame update

    public void LoadRoom()

    {

        Room.SetActive(true);

    }

    public void LoadLivingRoom()

    {

        LivingRoom.SetActive(true);

    }

    public void LoadKitchen()

    {

        Kitchen.SetActive(true);

    }
}
