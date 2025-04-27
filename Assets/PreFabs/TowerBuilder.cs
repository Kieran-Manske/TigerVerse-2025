using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


public class TowerBuilder : MonoBehaviour
{
    [Header("Tower Settings")]
    [Tooltip("Stack height of the tower - Default:18")]
    [Range(1, 100)] // Creates a slider in the Inspector
    [SerializeField] private int _towerHeight = 18;
    public Vector3 blockScale = Vector3.one;
    public float ySize = 1.5f;
    public float xSize = 2.3f;
    public float zSize = 7.5f;
    public ScaleController scaleController;

    public GameObject blockPrefab;

    void Start()
    {
        StartCoroutine(CreateTower());
    }

    IEnumerator CreateTower() {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < _towerHeight; i++) {
            if (i%2 == 0) {
                LayerZ(i);
            } else {
                LayerX(i);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    void LayerZ(int offset) {
        for (int i = 0; i < 3; i++) {
            Vector3 position = new Vector3(
                transform.position.x + i * xSize,
                transform.position.y + offset * ySize,
                transform.position.z
            );
            Instantiate(blockPrefab, position, Quaternion.identity, transform);
        }
    }
    void LayerX(int offset){
        for (int i = 0; i < 3; i++) {
            Vector3 position = new Vector3(
                transform.position.x + xSize,
                transform.position.y + offset * ySize,
                transform.position.z + i * xSize - xSize
            );
            Quaternion rotation = Quaternion.Euler(0, 90, 0);
            Instantiate(blockPrefab, position, rotation, transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (scaleController != null){
            transform.localScale = Vector3.one * scaleController.scaleFactor;
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.mass = scaleController.scaleFactor; 
            }

        }
    }
 }
