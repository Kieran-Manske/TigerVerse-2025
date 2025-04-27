using System.Collections.Specialized;
using UnityEngine;
public class Fallen : MonoBehaviour{

    public string targetTag = "Target";
    public string baseTag = "Base";
    public string Brick = "Brick";
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Brick)){
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(Brick);
            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }
        }
    }
}