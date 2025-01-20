using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class PlayerController : MonoBehaviour
{
    CharacterController Controller;
    Animator animator;

    public float Speed;

    public Transform Cam;

    bool forward = false;
    bool left = false;
    bool back = false;
    bool right = false;


    // Start is called before the first frame update
    void Start()
    {

        Controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
        Movement.y = 0f;

        bool dirChanged = false;
        bool tempForward = Input.GetAxis("Vertical") > 0;
        bool tempLeft = Input.GetAxis("Horizontal") < 0;
        bool tempBack = Input.GetAxis("Vertical") < 0;
        bool tempRight = Input.GetAxis("Horizontal") > 0;

        if (forward != tempForward) {
            forward = tempForward;
            dirChanged = true;
        }
        if (left != tempLeft)
        {
            left = tempLeft;
            dirChanged = true;
        }
        if (back != tempBack)
        {
            back = tempBack;
            dirChanged = true;
        }
        if (right != tempRight)
        {
            right = tempRight;
            dirChanged = true;
        }
        animator.SetBool("Forward", forward);
        animator.GetComponent<Animator>().SetBool("Back", back);
        animator.GetComponent<Animator>().SetBool("Left", left);
        animator.GetComponent<Animator>().SetBool("Right", right);

        if (dirChanged)
            animator.SetTrigger("DirChange");


        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("Strike");
        }

        Controller.Move(Movement);

        if (Movement.magnitude != 0f)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Cam.GetComponent<CameraController>().sensivity * Time.deltaTime);


            Quaternion CamRotation = Cam.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);

        }
    }
}
