using UnityEngine;

public class Block_Detection : MonoBehaviour
{
  public Vector3 movementTo = new Vector3(-200,0, 0);
  public string targetTag = "Target";
  public GameObject Tower;
    void Start()
    {
        movementTo.Normalize(); 
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(movementTo);
        //GameObject newTower = GameObject.Instantiate(Tower);
        //newTower.transform.Translate(movementTo);
    }
}
