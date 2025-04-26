using UnityEngine;
public class Fallen : MonoBehaviour
{
  public Vector3 movementTo = new Vector3(-200,0, 0);

  public GameObject Tower;
  public string targetTag = "Target";
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
        void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            transform.Translate(movementTo);
            GameObject newTower = GameObject.Instantiate(Tower);
            newTower.transform.Translate(movementTo);
        }

    }
}
