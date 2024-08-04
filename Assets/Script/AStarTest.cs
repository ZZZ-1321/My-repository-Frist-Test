using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarTest : MonoBehaviour
{
    public int beginX=-3;
    public int beginY=5;

    public int offsetX=2;
    public int offsetY=-2;

    public int mapW=10;
    public int mapH=10;
    List<AStarNode> nodes;
    private Vector2 beginpos = Vector2.right * -1;
    public GameObject player;
    //Dictionary<string, GameObject> dic = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
   
    void Start()
    {
        AStarMgr.Instance.InitMapInfo(mapW,mapH);

        //for (int i = 0; i < mapW; i++)
        //{
        //    for (int j = 0; j < mapH; j++)
        //    {
        //        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //        obj.transform.position = new Vector3(beginX+i*offsetX,beginY+j*offsetY);
        //        obj.name = i + "_" + j;
        //        dic.Add(obj.name,obj);

        //        AStarNode node = AStarMgr.Instance.nodes[i,j];
        //        if (node.type==NodeType.Stop)
        //        {
        //            obj.GetComponent<MeshRenderer>().material.color = Color.red;
        //        }
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit info;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out info,1000))
            {
               
                //if (beginpos==Vector2.right*-1)
                //{
                //    if (nodes != null)
                //    {
                //        for (int i = 0; i < nodes.Count; i++)
                //        {
                //            dic[nodes[i].x + "_" + nodes[i].y].GetComponent<MeshRenderer>().material.color = Color.white;
                //        }
                //    }
                //    string[] strs = info.collider.gameObject.name.Split('_');
                //    beginpos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
                //    info.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                //}
                //else
                //{
                //    string[] strs = info.collider.gameObject.name.Split('_');
                //    Vector2 endPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));

                //   nodes= AStarMgr.Instance.FindPath(beginpos,endPos);
                //    dic[beginpos.x + "_" + beginpos.y].GetComponent<MeshRenderer>().material.color = Color.white;
                //    if (nodes!=null)
                //    {
                //        for (int i = 0; i < nodes.Count; i++)
                //        {
                //            dic[nodes[i].x + "_" + nodes[i].y].GetComponent<MeshRenderer>().material.color = Color.green;
                //        }
                //    }
                  
                //    beginpos = Vector2.right * -1;
                //}
            }
        }
    }
}
