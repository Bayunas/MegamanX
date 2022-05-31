using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIContoller : MonoBehaviour
{

    Button msgBtn;
    Button startBtn;
    Label msgText;
    // Start is called before the first frame update
    void Start()
    {
       VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        msgBtn = root.Q<Button>("msg-btn");
        msgText = root.Q<Label>("msg-txt");
        startBtn = root.Q<Button>("start-btn");
        msgBtn.clicked += ShowMSG;
        startBtn.clicked += StartGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowMSG()
    {
        msgText.text = "Daniel Bayona\t Santiago Dorado";
        msgText.style.display = DisplayStyle.Flex;
    }

    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
