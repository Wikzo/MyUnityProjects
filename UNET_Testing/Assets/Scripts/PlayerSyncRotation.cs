using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncRotation : NetworkBehaviour
{

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _lerpRate = 15;

    [SyncVar] private Quaternion _syncPlayerRotation;
    [SyncVar] private Quaternion _syncCameraRotation;

    void FixedUpdate()
    {
        TransmitRotations();
        LerpRotations();
    }

    void LerpRotations()
    {
        if (!isLocalPlayer)
        {
            _playerTransform.rotation = Quaternion.Lerp(_playerTransform.rotation,
                _syncPlayerRotation,
                _lerpRate*Time.deltaTime);

            _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation,
                _syncCameraRotation,
                _lerpRate*Time.deltaTime);
        }
    }

    [Command]
    void CmdProvideRotationsToServer(Quaternion playerRotation, Quaternion cameraRotation)
    {
        _syncPlayerRotation = playerRotation;
        _syncCameraRotation = cameraRotation;
    }

    [ClientCallback]
    void TransmitRotations()
    {
        if (isLocalPlayer)
            CmdProvideRotationsToServer(_playerTransform.rotation, _cameraTransform.rotation);
    }
}
