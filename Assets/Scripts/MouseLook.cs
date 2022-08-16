using Mirror;
using UnityEngine;

public class MouseLook : NetworkBehaviour
{
    [SerializeField] [Range(0f,200f)] private float _mouseSensitivity;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private Camera _playerCamera;

    private float xRotation = 0f;
    private float upAngleLimit = 0f;
    private float downAngleLimit = 30f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (!isLocalPlayer)
        {
            _playerCamera.gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, upAngleLimit, downAngleLimit);

        _playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        _playerBody.Rotate(Vector3.up * mouseX);

    }
}
