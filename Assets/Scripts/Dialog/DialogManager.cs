using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static event Action OnDialogFinished;

    [SerializeField] private AudioClip _spacebarAudio;
    [SerializeField] private GameObject dialogParent;
    [SerializeField] private Image _portraitImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogText;

    [SerializeField] private List<ParticipantSerializable> _participants;
    [SerializeField] private List<LineSerializable> _dialogParts;

    private Dictionary<int, DialogParticipantSO> _participantIDDict = new();

    private void Awake()
    {
        // create characterID to speaker dict
        foreach (ParticipantSerializable participantInfo in _participants)
        {
            _participantIDDict.Add(participantInfo.characterID, participantInfo.participant);
        }
    }

    public void StartDialog()
    {
        GameManager.Instance.PauseMovement();

        dialogParent.SetActive(true);

        StartCoroutine(RunDialog());
    }

    IEnumerator RunDialog()
    {
        // for every dialog part -> for each segment before speaker changes
        foreach (LineSerializable lineInfo in _dialogParts)
        {
            // set character portrait and name to speaker
            DialogParticipantSO speaker = _participantIDDict[lineInfo.characterID];
            _portraitImage.sprite = speaker.sprite;
            _nameText.SetText(speaker.characterName);

            // get wait time between chars and lines
            float waitTime = 1f / speaker.typingSpeed;
            string[] lines = lineInfo.lines;


            foreach (string line in lines)
            {
                yield return StartCoroutine(TypeLines(line, waitTime));

                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                SoundFXManager.Instance.PlaySoundFXClip(_spacebarAudio, transform, 0.2f);
            }
        }

        dialogParent.SetActive(false);

        GameManager.Instance.ResumeMovement();

        OnDialogFinished?.Invoke();
    }

    IEnumerator TypeLines(string line, float waitTime)
    {
        _dialogText.SetText(string.Empty);

        foreach(char c in line)
        {
            _dialogText.text += c;
            yield return new WaitForSeconds(waitTime);
        }
    }


}
