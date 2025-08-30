using TMPro;
using UnityEngine;

public class LootBox : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _lootPrefab;
    [SerializeField] private GameObject _lootSpawnLocation;
    [SerializeField] private Sprite _emptyLootBox;

    [SerializeField] private AudioClip _lootSpawnSound;
    [SerializeField] private float _volume = 0.2f;

    private bool _closed = true;

    public bool CanInteract()
    {
        return _closed;
    }

    public string Interact()
    {
        _closed = false;

        SoundFXManager.Instance.PlaySoundFXClip(_lootSpawnSound, transform, _volume);

        GameObject loot = Instantiate(_lootPrefab, transform);
        loot.transform.position = transform.position + (_lootSpawnLocation.transform.position - transform.position);

        GetComponent<SpriteRenderer>().sprite = _emptyLootBox;

        return "Opened a Chest!";
    }
}
