using UnityEngine;

public class camera : MonoBehaviour
{
    public float mouseSens = 100f;
    public Transform PlayerPos;
    float xRotaiton = 0f;
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        xRotaiton -= mouseY;
        xRotaiton = Mathf.Clamp(xRotaiton, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotaiton, 0f,0f);
        PlayerPos.Rotate(Vector3.up * mouseX);
    }
}
