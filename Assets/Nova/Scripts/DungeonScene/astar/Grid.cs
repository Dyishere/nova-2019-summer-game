using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject G1, T1;//, G2, T2, G3, T3, G4, T4;
    public Transform Gd1, Target1;//, Gb2, Target2, Gb3, Target3, Gb4, Target4;
    Node playerNode, targetNode;
    public LayerMask unwalkableMask;
    public Vector2 gridSize;
    public float nodeRadius,nodeDiameter;
    public Node[,] grid;
    public List<Node> path1;//,path2,path3,path4;
    public int nodeNumX,nodeNumY;
    // Start is called before the first frame update
    void Start()
    {
        G1 = GameObject.Find("Guardian1");
        //G2 = GameObject.Find("Guard2"); G3 = GameObject.Find("Guard3"); G4 = GameObject.Find("Guard4");
        T1 = GameObject.Find("Player1");
        nodeDiameter = nodeRadius * 2;
        nodeNumX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        nodeNumY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
    }

    private void Update()
    {
        Gd1 = G1.transform;
        Target1 = T1.transform;
    }
    void CreateGrid()
    {
        grid = new Node[nodeNumX, nodeNumY];
        Vector3 startPos = transform.position - new Vector3(gridSize.x / 2,gridSize.y/2,0);
        for(int x= 0;x<nodeNumX; x++)
            for(int y = 0; y < nodeNumY; y++)
            {
                Vector3 currentPos = startPos + new Vector3(x * nodeDiameter + nodeRadius, y * nodeDiameter + nodeRadius, 0);
                bool walkable = !Physics.CheckSphere(currentPos, nodeRadius,unwalkableMask);   //在每个方块位置处检查在范围内的物品是否为unwalkableMask
                grid[x, y] = new Node(x, y, walkable, currentPos);

            }
    }


    public Node GetNodeFromPosition(Vector3 pos)
    {
        float percentX = (pos.x + gridSize.x / 2) / gridSize.x;
        percentX = Mathf.Clamp01(percentX);
        float percentY = (pos.y + gridSize.y / 2) / gridSize.y;
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt(percentX * (nodeNumX - 1));
        int y = Mathf.RoundToInt(percentY * (nodeNumY - 1));
        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, new Vector3(gridSize.x, gridSize.y, 1));
        if(grid!=null)
            foreach(Node n in grid)
            {
                if(n.walkable)
                {
                    Gizmos.color = Color.gray;
                    if (GetNodeFromPosition(Gd1.position) == n)
                        Gizmos.color = Color.red;
                    if (GetNodeFromPosition(Target1.position) == n)
                        Gizmos.color = Color.white;
                    if ((path1 != null && path1.Contains(n)))
                        Gizmos.color = Color.blue;
                    /*if (GetNodeFromPosition(Gd1.position) == n|| GetNodeFromPosition(Gd2.position) == n || GetNodeFromPosition(Gd3.position) == n || GetNodeFromPosition(Gd4.position) == n )
                        Gizmos.color = Color.red;
                    if (GetNodeFromPosition(Target1.position) == n|| GetNodeFromPosition(Target2.position) == n || GetNodeFromPosition(Target3.position) == n || GetNodeFromPosition(Target4.position) == n )
                        Gizmos.color = Color.white;
                    if ((path1 != null&&path1.Contains(n)) || (path2 != null && path2.Contains(n)) || (path3 != null && path3.Contains(n)) || (path4 != null && path4.Contains(n)))
                            Gizmos.color = Color.blue;*/
                    Gizmos.DrawCube(n.Pos, new Vector3(nodeDiameter * 0.9f, nodeDiameter * 0.9f, 1));
                }
            }
    }

    void ChangeTarget()      //待实现方法
    {
        /*for()           //检查所有Target可用情况
        {
        if(new Target is available)
           this.Target= new Target               //从myGrid里调取new Target的Transform，赋值给本地Target
        }*/


    }
}
