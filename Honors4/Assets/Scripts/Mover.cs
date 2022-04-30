using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Mover : MonoBehaviour
{
    public float speed = 0;
    public GameObject respawner;
    public GameObject respawn1;
    public GameObject respawn2;
    public GameObject respawn3;
	public GameObject level1;
	public GameObject level2;
	public GameObject level3;
	public static float currentTime = 0.0f;
	public static int blocks = 0;
	[SerializeField] private bool finished = false;
    [SerializeField] private int level = 1;
	[SerializeField] private TextMeshProUGUI timeText;
	[SerializeField] private TextMeshProUGUI blocksText;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
		blocks = 0;
		currentTime = 0.0f;
		finished = false;
		level = 1;
    }
	
	void Update()
	{
		if(!finished)
		{
		currentTime += Time.deltaTime;
		}
		
		double b = System.Math.Round (currentTime, 2);     
		timeText.text = b.ToString ();
		
		blocksText.text = "Blocks: "+blocks;
		
		if(Input.GetKeyDown(KeyCode.P))
		{
			Debug.LogWarning("PURGING WITH FIRE!!!!!!!!!");
			TimeManager.DeleteValues();
		}
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
			blocks++;
        }

        if(other.gameObject.CompareTag("Death"))
        {
            this.transform.position = (new Vector3(respawner.transform.position.x, respawner.transform.position.y, respawner.transform.position.z));
			TimeManager.totalFalls++;
			TimeManager.StoreValues();
        }
		
		if(other.gameObject.CompareTag("Goal") && level == 1)
		{
			level++;
			level1.SetActive(false);
			level2.SetActive(true);
			respawner.transform.position = (new Vector3(respawn2.transform.position.x, respawn2.transform.position.y, respawn2.transform.position.z));
			this.transform.position = (new Vector3(respawner.transform.position.x, respawner.transform.position.y, respawner.transform.position.z));
		}
		else if(other.gameObject.CompareTag("Goal") && level == 2)
		{
			level++;
			level2.SetActive(false);
			level3.SetActive(true);
			respawner.transform.position = (new Vector3(respawn3.transform.position.x, respawn3.transform.position.y, respawn3.transform.position.z));
			this.transform.position = (new Vector3(respawner.transform.position.x, respawner.transform.position.y, respawner.transform.position.z));
		}
		else if(other.gameObject.CompareTag("Goal") && level == 3)
		{
			finished = true;
			TimeManager.totalCompletions++;
			TimeManager.StoreValues();
			SceneManager.LoadScene("Levels1-5");
		}
    }
}
