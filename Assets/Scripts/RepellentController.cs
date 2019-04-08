using UnityEngine;

public class RepellentController : MonoBehaviour {

    float movementSpeed = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.R))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.F))
        {
            transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        }
    }
}
