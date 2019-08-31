using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip levelCompleteSound;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    Rigidbody rigidBody;
    new AudioSource audio;

    enum State { Alive, Dying, Transcending}
    State state = State.Alive;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
            RespondToRotate();
            RespondToThrustInput();
        }
        if(state == State.Dying)
        {

            PlayDeathSound();
        }
        if(state == State.Transcending)
        {

            PlayLevelCompleteSound();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly": 
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", levelLoadDelay);
                break;
            default:
                state = State.Dying;
                Invoke("ReloadCurrentScene", levelLoadDelay);
                break;
        }
    }

    private void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void RespondToRotate()
    {
        rigidBody.freezeRotation = true; //manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {      
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; //physics takes control of rotation
    }

    private void RespondToThrustInput()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust(thrustThisFrame);
        }
        else
        {
            audio.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust(float thrustThisFrame)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);

        if (!audio.isPlaying)
        {
            audio.PlayOneShot(mainEngine);
            mainEngineParticles.Play();
        }
        
    }

    private void PlayDeathSound()
    {
        if(state == State.Dying)
        {
            audio.PlayOneShot(deathSound, .1f);
            deathParticles.Play();
        }
    }

    private void PlayLevelCompleteSound()
    {
        if(state == State.Transcending)
        {
            audio.PlayOneShot(levelCompleteSound, .2f);
            successParticles.Play();
        }
    }
}
