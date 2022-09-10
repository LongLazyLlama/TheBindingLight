using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class EnergyAbility : MonoBehaviour
{
    public Text EnergyOrbCount;

    public GameObject Player;

    int MaxEnergyOrbs = 0;
    int CurrentEnergyOrbs = 0;

    GameObject _currentInteractible;

    bool IsInteractibleNearby;
    void Update()
    {
        Keyboard _keyboard = Keyboard.current;
        if (_keyboard == null)
        {
            //Keyboard was not found.
            return;
        }

        TransferEnergy(_keyboard);
        RetractEnergy(_keyboard);

        UpdateOrbCount();
    }

    private void UpdateOrbCount()
    {
        EnergyOrbCount.text = CurrentEnergyOrbs + "/" + MaxEnergyOrbs;
    }

    private void RetractEnergy(Keyboard keyboard)
    {
        if (keyboard.rKey.wasPressedThisFrame)
        {
            Debug.Log("R was pressed");
            var _levelPillars = FindObjectsOfType<Pillar>();

            foreach (Pillar pillar in _levelPillars)
            {
                pillar.IsPillarActive = false;
            }
            CurrentEnergyOrbs = MaxEnergyOrbs;
        }
    }

    private void TransferEnergy(Keyboard keyboard)
    {
        if (IsInteractibleNearby && keyboard.eKey.wasPressedThisFrame)
        {
            //NOTE: This script was made to only process ONE INTERACTIBLE at a time.
            //If more interactibles are detected, it will take the last interactible that entered the InteractRange.
            Debug.Log("E was pressed");

            if (_currentInteractible.GetComponent<EnergyOrb>() != null)
            {
                _currentInteractible.GetComponent<EnergyOrb>().TriggerOrb();
                IncreaseMaxOrbCount();

                IsInteractibleNearby = false;
            }

            if (_currentInteractible.GetComponent<Pillar>() != null && CurrentEnergyOrbs >= _currentInteractible.GetComponent<Pillar>().RequiredOrbs)
            {
                if (_currentInteractible.GetComponent<Pillar>().IsPillarActive == false)
                {
                    _currentInteractible.GetComponent<Pillar>().IsPillarActive = true;
                    Debug.Log("Pillar is now active!");

                    CurrentEnergyOrbs -= _currentInteractible.GetComponent<Pillar>().RequiredOrbs;
                }
                else if (_currentInteractible.GetComponent<Pillar>().IsPillarActive)
                {
                    Debug.Log("Pillar is already active!");
                }
            }
            else if (_currentInteractible.GetComponent<Pillar>() != null &&CurrentEnergyOrbs < _currentInteractible.GetComponent<Pillar>().RequiredOrbs)
            {
                Debug.Log("Not Enough orbs ! (" + CurrentEnergyOrbs + "/" + _currentInteractible.GetComponent<Pillar>().RequiredOrbs + ")");
            }

            if (_currentInteractible.GetComponent<Teleporter>() != null)
            {
                _currentInteractible.GetComponent<Teleporter>().Teleport(Player);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactibles"))
        {
            Debug.Log("Interactible Within Range");
            _currentInteractible = other.gameObject;

            IsInteractibleNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactibles"))
        {
            Debug.Log("Interactible Out Of Range");
            _currentInteractible = null;

            IsInteractibleNearby = false;
        }
    }

    void IncreaseMaxOrbCount()
    {
        MaxEnergyOrbs++;

        CurrentEnergyOrbs = MaxEnergyOrbs;
    }
}
