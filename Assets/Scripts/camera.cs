using UnityEngine;

public class camera : MonoBehaviour {

    float movementSpeed = 10.0f;
    float speed = 3.5f;
    float x;
    float y;

    // Use this for initialization
    void Start () {
		
	}

    void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        }
    }

    // Update is called once per frame
    void Update () {

        Movement();

        if (Input.GetMouseButton(0))
        {
            transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * speed, Input.GetAxis("Mouse X") * speed, 0.0f));
            x = transform.rotation.eulerAngles.x;
            y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(x, y, 0.0f);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
