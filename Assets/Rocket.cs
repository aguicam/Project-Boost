
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    Rigidbody rigidBody;
    AudioSource audioRocket;
    
    enum State {Alive, Dying, Transcending}
    State state = State.Alive;

    bool collisionsDisabled = false;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioRocket = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive)
        {
            RespontToThrustInput();
            RespondToRotateInput();
 
        }
        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
        
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionsDisabled) {return;}

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;


        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioRocket.Stop();
        audioRocket.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay); 
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioRocket.Stop();
        audioRocket.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay); 
    }

    private void LoadNextLevel() //TODO Allow for more than 2 levels
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void LoadFirstLevel() //TODO Allow for more than 2 levels
    {
        SceneManager.LoadScene(0);
    }

    private void RespontToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();

        }
        else
        {
            audioRocket.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioRocket.isPlaying)
        {
            audioRocket.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotateInput()
    {
        rigidBody.angularVelocity = Vector3.zero; //remove rotation due to physics
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward*rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
    }


}
