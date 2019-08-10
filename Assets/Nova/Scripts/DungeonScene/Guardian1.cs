using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian1 : MonoBehaviour
{
    public float maxSpeed = 10f;
    GameObject PathFind;
    Grid myGrid;
    public List<Node> path;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        PathFind = GameObject.Find("AstarPath");
        myGrid = PathFind.GetComponent<Grid>();
        //path = myGrid.path1;
    }

    // Update is called once per frame
    void Update()
    {
            path = myGrid.path1;
            i = 0;
        
        /*if()              //(待实现)Target不再可用
        {
            ChangeTarget();
        }*/
        if (path != null)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, path[i].Pos, maxSpeed * Time.deltaTime);
            if (myGrid.GetNodeFromPosition(this.transform.position).Equals(path[i])&& i <= path.Count)
            {
                i++;
            }
        }
        
    }

}
