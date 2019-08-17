using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian2 : MonoBehaviour
{
    public float maxSpeed = 3f;
    GameObject PathFind;
    Grid myGrid;
    List<Node> path;
    // Start is called before the first frame update
    void Start()
    {
        PathFind = GameObject.Find("AstarPath");
        myGrid = PathFind.GetComponent<Grid>();

    }

    // Update is called once per frame
    void Update()
    {
            path = myGrid.path2;
        
        /*if()              //(待实现)Target不再可用
        {
            ChangeTarget();
        }*/
        if (path != null)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, path[0].Pos, maxSpeed * Time.deltaTime);
        }
        
    }
    public void ChangeTarget()
    {

    }
}
