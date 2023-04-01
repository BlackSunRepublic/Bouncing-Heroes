using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComicPanel : MonoBehaviour
{
    public string[] _linesOfDialogue;
    public TextMeshProUGUI _dialogueText;
    public GameObject _slide;
    private int _currentLineIndex = 0;

    private void Start() 
    {
        _dialogueText.text = _linesOfDialogue[_currentLineIndex];
        _slide.gameObject.SetActive(true);
    }

    private void Update() 
    {
        ComicsLogic();
    }

    public void ShowNextLine()
    {
        if (_currentLineIndex < _linesOfDialogue.Length - 1) _dialogueText.text = _linesOfDialogue[++_currentLineIndex];
        else if (_currentLineIndex >= _linesOfDialogue.Length - 1) _slide.gameObject.SetActive(false);
    }

    private void ComicsLogic()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShowNextLine();
        }
    }
}
