using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Magnet : MonoBehaviour
{
    public float CurrentTime;
    public float startTime;
    public enum Pole
    {
        North,
        South
    }

    public float MagnetForce;
    public Pole MagneticPole;
    public Rigidbody playerRigidbody;

    void Start ()
    {
        CurrentTime = startTime;
        if (playerRigidbody == null)
        {
            playerRigidbody = GetComponent<Rigidbody>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime -= 1 * Time.deltaTime;
        
        if (CurrentTime <= -5)
        {
            CurrentTime = 5;
        }

        if (this.gameObject.CompareTag("Player"))
        {
            if (CurrentTime >= 0)
            {
                this.MagneticPole = Pole.North;
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
                
            }
            else if (CurrentTime <= -1)
            {
                this.MagneticPole = Pole.South;
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
        }

    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("North"))
        {
            MagnetForce = 10f;
        }
        if (other.CompareTag("South"))
        {
            MagnetForce = 10f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (this.gameObject.CompareTag("Player"))
        {
            if (other.CompareTag("South"))
            {
                MagnetForce = 0f;
            }
            if (other.CompareTag("North"))
            {
                MagnetForce = 0f;
            }
            if (CurrentTime >= 0)
            {
                this.MagneticPole = Pole.North;
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            
            }
            else if (CurrentTime <= -1)
            {
                this.MagneticPole = Pole.South;
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
        

        
    }
}
