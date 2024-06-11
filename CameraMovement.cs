using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CharacterController CharacterController;

    private const float camSpeed = 5.0f;
    private const float camSensH = 20.0f;
    private const float camSensV = 10.0f;

    public void FixedUpdate()
    {
        // Camera rotation
        if (Input.GetKey(KeyCode.Mouse1))
            transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y") * camSensV, Input.GetAxis("Mouse X") * camSensH);

        // Camera movement
        Vector3 vector = camSpeed * Time.deltaTime * GetInputVector();
        vector = transform.TransformDirection(vector);
        CharacterController.Move(vector);
    }

    private Vector3 GetInputVector()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            return Vector3.forward;

        if (Input.GetKey(KeyCode.DownArrow))
            return Vector3.back;

        if (Input.GetKey(KeyCode.LeftArrow))
            return Vector3.left;

        if (Input.GetKey(KeyCode.RightArrow))
            return Vector3.right;

        return Vector3.zero;
    }
}
