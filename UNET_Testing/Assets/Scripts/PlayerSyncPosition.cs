using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncPosition : NetworkBehaviour
{

    [SyncVar]
    private Vector3 _syncPos;

    [SerializeField] private Transform _myTransform;
    [SerializeField] private float _lerpRate = 15;

    void FixedUpdate()
    {
        TransmitPosition(); // send my position to server --> then transmitted to all other clients
        LerpPosition(); // sync all other cliens' positions
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
            _myTransform.position = Vector3.Lerp(_myTransform.position,
                _syncPos, _lerpRate * Time.deltaTime);
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos) // method name must include "Cmd"
    {
        _syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer)
            CmdProvidePositionToServer(_myTransform.position);
    }
}