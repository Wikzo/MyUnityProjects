using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

// from https://youtu.be/NLnzlwCRjgc?list=PLwyZdDTyvucyAeJ_rbu_fbiUtGOVY55BG
// UNET Part 1 - Setup and Movement Syncing

public class PlayerNetworkSetup : NetworkBehaviour
{

    [SerializeField] private Camera _fpsCamera;
    [SerializeField] private AudioListener _audioListener;
    void Start()
    {
        if (isLocalPlayer)
        {
            GameObject.Find("SceneCamera").SetActive(false);
            GetComponent<CharacterController>().enabled = true;
            GetComponent<FirstPersonController>().enabled = true;
            _fpsCamera.enabled = true;
            _audioListener.enabled = true;
        }
    }
}
