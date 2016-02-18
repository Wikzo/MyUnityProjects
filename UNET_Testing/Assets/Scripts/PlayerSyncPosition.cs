using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncPosition : NetworkBehaviour
{

    [SyncVar]
    private Vector3 _syncPos;

    [SerializeField] private Transform _myTransform;
    [SerializeField] private float _lerpRate = 15;

    private Vector3 _lastPosition;
    private float _threshold = 0.5f;

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
        //Debug.Log("Receiving updated network position");
        _syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        // only transmit network traffic when moving more than threshold meters
        if (isLocalPlayer && Vector3.Distance(_myTransform.position, _lastPosition) > _threshold)
        {
            CmdProvidePositionToServer(_myTransform.position);
            _lastPosition = _myTransform.position;
        }
    }
}