using UnityEngine;

public class flock : MonoBehaviour {

    public globalFlock myManager;

    public float speedMult = 1;

    float speed;
    float boostSpeed;
    float rotationSpeed = 20.0f;

    float minSpeed = 0.8f;
    float maxSpeed = 2.0f;

    float run = 0.0f;

    float range = 5.0f;

    Vector3 averageHeading;
    Vector3 averagePosition;

    Vector3 newGoalPos;
    
    bool turn = false;
    bool speedUp = false;

    // Use this for initialization
    void Start ()
    {
        myManager = FindObjectOfType<globalFlock>();
        speed = Random.Range(minSpeed, maxSpeed);
        boostSpeed = 1.0f;
	}
    
    void Rule()
    {
        GameObject[] gos;
        gos = globalFlock.allGO;

        Vector3 vCentre = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;

        float gSpeed = 0.1f;

        Vector3 goalPos = globalFlock.goalPos;

        float dist;

        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != gameObject)
            {
                dist = Vector3.Distance(go.transform.position, transform.position);
                if (dist <= myManager.distValue)
                {
                    vCentre += go.transform.position;
                    groupSize++;

                    if (dist <= 2.0f)
                    {
                        vAvoid = vAvoid + (transform.position - go.transform.position);
                    }

                    flock anotherFlock = go.GetComponent<flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            vCentre = vCentre / groupSize + (goalPos - transform.position);
            speed = gSpeed / groupSize * speedMult;

            Vector3 direction = (vCentre + vAvoid) - transform.position;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
        }
    }

    void Look ()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            if (hit.collider.gameObject.CompareTag("Obstacle"))
            {
                transform.Rotate(new Vector3(0, 45, 0));
            }
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Collider"))
        {
            if (!turn)
            {
                Debug.Log("Collide");
                newGoalPos = transform.position - other.gameObject.transform.position;
            }

            turn = true;
            speedUp = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Collider"))
        {
            turn = false;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        Look();

        Bounds b = new Bounds(myManager.transform.position, myManager.swimLimits * 2);

        if (!b.Contains(transform.position))
        {
            turn = true;
        }
        else
        {
            turn = false;
        }

        //if (Vector3.Distance(transform.position, Vector3.zero) >= globalFlock.size)
        //{
        //    turn = true;
        //}
        //else
        //{
        //    turn = false;
        //}

        if (speedUp)
        {
            boostSpeed = 10.0f;
            run += Time.deltaTime;
        }
        if (run >= 1)
        {
            boostSpeed = 0.0f;
            run = 0.0f;
            speedUp = false;
        }

        if (turn)
        {
            Vector3 direction = newGoalPos - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

            speed = 3.0f * speedMult;
        }
        else
        {
            if (Random.Range(0, 10) < 1)
            {
                Rule();
            }
        }

        transform.Translate(0, 0, Time.deltaTime * (speed + boostSpeed) * speedMult);
	}
}
