﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

// from https://youtu.be/NLnzlwCRjgc?list=PLwyZdDTyvucyAeJ_rbu_fbiUtGOVY55BG
// UNET Part 1 - Setup and Movement Syncing

public class PlayerNetworkSetup : NetworkBehaviour
{

    [SerializeField] public GameObject FirstPersonCharacterObject;

    void Start()
    {
        if (isLocalPlayer)
        {
            GameObject.Find("SceneCamera").SetActive(false);
            GetComponent<CharacterController>().enabled = true;
            GetComponent<FirstPersonController>().enabled = true;
            FirstPersonCharacterObject.SetActive(true);
        }
    }
}