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

    private Quaternion _lastPlayerRotation;
    private Quaternion _lastCameraRotation;
    private float _threshold = 5;

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
        //Debug.Log("Receiving updated network rotation");

        _syncPlayerRotation = playerRotation;
        _syncCameraRotation = cameraRotation;
    }

    [ClientCallback]
    void TransmitRotations()
    {
        if (isLocalPlayer)
        {
            // only transmit network data when player/camera has rotated more than threshold degrees
            if (Quaternion.Angle(_playerTransform.rotation, _lastPlayerRotation) > _threshold ||
                Quaternion.Angle(_cameraTransform.rotation, _lastCameraRotation) > _threshold)
            {
                CmdProvideRotationsToServer(_playerTransform.rotation, _cameraTransform.rotation);
                _lastPlayerRotation = _playerTransform.rotation;
                _lastCameraRotation = _cameraTransform.rotation;
            }
        }
            
    }
}
