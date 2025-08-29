using UnityEngine;

[CreateAssetMenu (fileName = "NewDialogParticipant", menuName = "ScriptableObjects/DialogParticipant")]
public class DialogParticipantSO : ScriptableObject
{
    public string characterName;
    public Sprite sprite;
    public float typingSpeed = 20f; // in characters per seconds -> 20c per second
}
