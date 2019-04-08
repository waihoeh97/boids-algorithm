using UnityEngine;
using UnityEngine.UI;

public class globalFlock : MonoBehaviour {

    public globalFlock myFlock;

    public GameObject GO;
    public GameObject goal;

    public static int size = 15;

    static int numGO = 20;
    public static GameObject[] allGO = new GameObject[numGO];

    public static Vector3 goalPos = Vector3.zero;

    float nextAction = 0.0f;
    float period = 5.0f;

    public float distValue = 2f;

    public Vector3 swimLimits = new Vector3(10, 10, 10);

    public Slider distSlider;

    public void FishSpeed (float speedMult)
    {
        for (int i = 0; i < numGO; i++)
        {
            allGO[i].GetComponent<flock>().speedMult = speedMult;
        }
    }

    public void FishDistance ()
    {
        distValue = distSlider.value;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(swimLimits.x * 2, swimLimits.y * 2, swimLimits.z * 2));
        //Gizmos.color = new Color(0, 1, 0, 1);
        //Gizmos.DrawSphere(goalPos, 0.5f);
    }

    // Use this for initialization
    void Start ()
    {
        myFlock = this;
        goalPos = transform.position;
        for (int i = 0; i < numGO; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0, swimLimits.x), Random.Range(0, swimLimits.y), Random.Range(0, swimLimits.z));
            allGO[i] = Instantiate(GO, pos, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > nextAction)
        {
            nextAction = Time.time + period;
            goalPos = new Vector3(Random.Range(0, swimLimits.x), Random.Range(0, swimLimits.y), Random.Range(0, swimLimits.z));
            goal.transform.position = goalPos;
        }
    }
}
