using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class BreakerBox : MonoBehaviour, IInteractable
{
    [SerializeField] List<SecurityCamera> cameras;

    private bool _powerOn = true;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public bool CanInteract()
    {
        return true;
    }

    public string Interact()
    {
        _powerOn = !_powerOn;
        _animator.SetBool("isOn", _powerOn);

        foreach (SecurityCamera cam in cameras)
        {
            cam.SetPower(_powerOn);
        }

        if (_powerOn && cameras.Count == 1)
        {
            return "Camera turned on!";
        }
        else if(_powerOn && cameras.Count > 1)
        {
            return "Cameras turned on!";
        }
        else if (!_powerOn && cameras.Count == 1)
        {
            return "Camera turned off!";
        }
        else if(!_powerOn && cameras.Count > 1)
        {
            return "Cameras turned off!";
        }
        else
        {
            return "ERROR, go debug your code...";
        }
    }
}
