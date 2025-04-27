using UnityEngine;

public class JengaTowerInit : MonoBehaviour
{
    public float offsetX = 2.7F;
    public float offsetY = 2.7F;
    public float offsetZ = 2.7F;
    public GameObject Block;
    public string baseTag = "Base";
    public string brickTag = "Brick";
    void Start()
    {
        // Initialize the tower with the specified offsets
        for (int i = 0; i < 18; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                Vector3 position;
                GameObject newBlock;
                    if(i%2==1)
                    {
                        position = new Vector3(0, i * offsetZ ,(offsetY*j)-(2F*offsetY));
                        newBlock = Instantiate(Block, position, Quaternion.identity);
                        newBlock.transform.Rotate(0, 90, 0);
                        
                    }
                    else{
                        position = new Vector3((offsetY*j)-(2F*offsetY), i * offsetZ, 0);
                        newBlock = Instantiate(Block, position, Quaternion.identity);
                        
                    }
                    if (i==0)
                    {
                        newBlock.gameObject.tag = baseTag;
                    }else
                    {
                        newBlock.gameObject.tag = brickTag;
                    }
            }
        }
    }
}
