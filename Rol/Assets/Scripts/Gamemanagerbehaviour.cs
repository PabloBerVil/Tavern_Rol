using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Gamemanagerbehaviour : MonoBehaviour
{
    [Header("PlayableCharacter")]
    [SerializeField] List<GameObject> characters;

    [Header("Refs")]
    public Camera cam;
    public GameObject menu;
    public GameObject interfaz;
    public GameObject interactionIcon;
    public GameObject Dialogmenu;
    public GameObject DialogText;
    private int CharacterID;
    public List<GameObject> CharaLeftImg;
    public List<GameObject> CharaRightImg;
    public AudioSource buttonsound;

    //dialogue variables
    private GameObject Speaker1;
    private GameObject Speaker2;
    [SerializeField]
    private int DialogueStep = 0;
    public List<SO_DialogTextes> Dialogues;
    [SerializeField]
    private SO_DialogTextes Dialogue;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        menu.SetActive(true);
        interfaz.SetActive(false);
        interactionIcon.SetActive(false);
        Dialogmenu.SetActive(false);
    }

    public void SetPlayablecharacter(int CharaID)
    {
        CharacterID = CharaID;
        OpenSceneFromMenu();
    }

    public void ToScene()
    {
        cam = Camera.main;
        buttonsound.Play();

        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].GetComponent<PlayerController>().CharacterID = i;

            if (i == CharacterID)
            {
                characters[i].GetComponent<PlayerController>().isPlayableChara = true;
                characters[i].GetComponent<Animator>().SetBool("IsPlayer", true);
                characters[i].GetComponent<Animator>().SetBool("Moving", false);
                characters[i].GetComponent<PlayerController>().InteractChara = null;
                cam.GetComponent<S_CameraBehaviour>().player = characters[i].GetComponent<Transform>().transform;
                characters[i].GetComponent<PlayerController>().InteractCollider.enabled = true;
            }
            else
            {
                characters[i].GetComponent<PlayerController>().InteractCollider.enabled = false;
            }
        }

        interfaz.SetActive(true);
        menu.SetActive(false);
    }

    public void OpenMenuLogic()
    {

        interfaz.SetActive(false);

        buttonsound.Play();

        for (int i = 0; i < characters.Count; i++)
        {
            if (i == CharacterID)
            {
                characters[i].GetComponent<PlayerController>().ResetPosition();
                characters[i].GetComponent<PlayerController>().isPlayableChara = false;
                characters[i].GetComponent<Animator>().SetBool("IsPlayer", false);
                characters[i].GetComponent<Animator>().SetBool("Moving", false);
                characters[i].GetComponent<PlayerController>().InteractCollider.enabled = false;
            }
        }

        HideIconOnChara();
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        OpenMenuLogic();
    }

    public void OpenSceneFromMenu()
    {
        menu.GetComponent<Animator>().SetTrigger("ToScene");
    }

    public void OpenMenuFromScene()
    {
        interfaz.GetComponent<Animator>().SetTrigger("ToMenu");
    }

    public void SetIconOnChara(GameObject IconizableChara)
    {

        interactionIcon.transform.position = IconizableChara.transform.position + Vector3.up * IconizableChara.GetComponent<PlayerController>().IconHeight;
        interactionIcon.SetActive(true);
    }

    public void HideIconOnChara()
    {
        interactionIcon.SetActive(false);
    }

    public void Out()
    {
        Application.Quit();
    }


    //Dialogue

    public void OpenDialog(int CharaID1, int CharaID2)
    {
        //Select the dialogue

        int DialogueID;

        if (CharaID1 < CharaID2)
        {
            DialogueID = CharaID1 * 10 + CharaID2;

        }
        else
        {
            DialogueID = CharaID2 * 10 + CharaID1;
        }

        for (int i = 0; i < Dialogues.Count; i++)
        {
            if (DialogueID == Dialogues[i].DialogID)
            {
                Dialogue = Dialogues[i];
            }
        }

        //Start the dialogue
        DialogueStep = 0;

        for (int i = 0; i < CharaLeftImg.Count; i++)
        {
            if (Dialogue.CharaLeft == i)
            {
                CharaLeftImg[i].SetActive(true);
            }
            else
            {
                CharaLeftImg[i].SetActive(false);
            }
        }

        for (int i = 0; i < CharaRightImg.Count; i++)
        {
            if (Dialogue.CharaRight == i)
            {
                CharaRightImg[i].SetActive(true);

            }
            else
            {
                CharaRightImg[i].SetActive(false);
            }
        }

        Dialogmenu.SetActive(true);
        DialogText.GetComponent<TextMeshProUGUI>().text = Dialogue.Text[DialogueStep];
        Dialogmenu.GetComponent<Animator>().SetInteger("RightLeft", Dialogue.Speaker[DialogueStep]);
    }

    public void CloseDialog()
    {
        Dialogmenu.SetActive(false);
        interfaz.SetActive(true);
        characters[CharacterID].GetComponent<PlayerController>().OnDialog = false;
    }

    public void ChangeDialog()
    {
        DialogueStep = DialogueStep + 1;

        if (Dialogue.Speaker.Count > DialogueStep)
        {
            DialogText.GetComponent<TextMeshProUGUI>().text = Dialogue.Text[DialogueStep];
            Dialogmenu.GetComponent<Animator>().SetInteger("RightLeft", Dialogue.Speaker[DialogueStep]);
        }
        else
        {
            {
                CloseDialog();
            }
        }
    }

}
