using System;
using System.Collections;
using System.Collections.Generic;
using GG.Infrastructure.Utils.Swipe;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public float moveSpeed;
    public float lerpValue;
    float totalSpeed;
    public Magnet magnet;
    public bool isDoubleTapped;
    private float lastClickTime;
    public static PlayerController Instance {get; private set;}

    private void Awake()
    {
        if (!Instance) 
        {
            Instance=this;
        }
        if (isDoubleTapped == true)
        {
            moveSpeed = 25f;
        }
        else
        {
            moveSpeed = 15f;
        }
        
        
    }

    public void Jump()
    {
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
        Debug.Log("UP is swiped");
    }

    private void Update()
    {
        extraSpeed();
        MovePlayer();
    }

    

    void extraSpeed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float timeSincelastClick = Time.time - lastClickTime;
            if (timeSincelastClick < .2f)
            {
                isDoubleTapped = true;
                StartCoroutine(resetTheClick());
            }
            lastClickTime = Time.time;
        }
        
        if (isDoubleTapped == true)
        {
            moveSpeed = 25f;
        }
        else
        {
            moveSpeed = 15f;
        }
    }
    IEnumerator resetTheClick()
    {
        yield return new WaitForSeconds(3.0f);
        isDoubleTapped = false;
    }

    void MovePlayer()
    {
        if (joystick.IsTouched)
        {
            var joystickVector = new Vector2(joystick.Horizontal, joystick.Vertical);
            var angle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
            
            var localEulerAngles = transform.localEulerAngles;
            var deltaAngle = Mathf.Abs(Mathf.DeltaAngle(localEulerAngles.y, angle)) / 90f;
            deltaAngle = 1 - Mathf.Clamp01(deltaAngle);

            angle = Mathf.LerpAngle(localEulerAngles.y, angle, Time.deltaTime * lerpValue * joystickVector.sqrMagnitude);
            
            var transform1 = transform;
            transform1.localEulerAngles = new Vector3(0f, angle, 0f);

            Vector3 direction = transform1.forward;
            totalSpeed = moveSpeed * deltaAngle * joystickVector.magnitude;
            transform1.position += direction.normalized * (Time.deltaTime * totalSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("YellowPlat"))
        {
            Scene loadedScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedScene.buildIndex);
        }
    }
}
