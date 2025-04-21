using UnityEngine;

public class Eventsmenu : MonoBehaviour
{
    public GameObject GameManager;

    public void EventToScene()
    {
        GameManager.GetComponent<Gamemanagerbehaviour>().ToScene();
    }

    public void EventToMenu()
    {
        GameManager.GetComponent<Gamemanagerbehaviour>().OpenMenu();
    }
}
