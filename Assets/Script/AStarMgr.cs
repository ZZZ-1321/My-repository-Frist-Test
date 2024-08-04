using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarMgr
{
    private static AStarMgr _instance;
    public static AStarMgr Instance
    {
        get
        {
            if (_instance==null)
            {
                _instance = new AStarMgr();
            }
            return _instance;
        }
    }

    private int mapW;
    private int mapH;

    public AStarNode[,] nodes;

    private List<AStarNode> openList;
    private List<AStarNode> closeList;
    

    public void InitMapInfo(int w, int h)
    {
        openList = new List<AStarNode>();
        closeList = new List<AStarNode>();
        nodes = new AStarNode[w, h];
        mapW = w;
        mapH = h;

        //初始化
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                AStarNode node = new AStarNode(i,j,Random.Range(0,100)<20? NodeType.Stop:NodeType.Walk);
                nodes[i, j] = node;
            }
        }



    }

    //实际项目中应该给他转换为对应的格子信息
    public List<AStarNode> FindPath(Vector2 startPos, Vector2 endPos)
    {
        //判断位置是否合法 
        if (startPos.x < 0 || startPos.x >= mapW ||
            startPos.y < 0 || startPos.y >= mapH ||
            endPos.x < 0 || endPos.x >= mapW ||
            endPos.y < 0 || endPos.y >= mapH)
        {
            Debug.Log("开始点或者结束点不合法");
            return null;

        }


        AStarNode start = nodes[(int)startPos.x, (int)startPos.y];
        AStarNode end = nodes[(int)endPos.x, (int)endPos.y];

        //判断是否阻挡
        if (start.type == NodeType.Stop || end.type == NodeType.Stop)
        {
            Debug.Log("开始或者结束是阻挡");
            return null;
        }
        //清空上一次相关的数据 避免他们影响 这一次的数据
        //清空关闭和开启列表
        closeList.Clear();
        openList.Clear();

        //把开始点放入关闭列表内
        start.father = null;
        start.g = 0;
        start.f = 0;
        start.h = 0;
        closeList.Add(start);

        while (true)
        {
            //循环八个点
            FindNearlyNodeToOpenList(start.x - 1, start.y - 1, 1.4f, start, end);
            FindNearlyNodeToOpenList(start.x, start.y - 1, 1f, start, end);
            FindNearlyNodeToOpenList(start.x + 1, start.y - 1, 1.4f, start, end);
            FindNearlyNodeToOpenList(start.x - 1, start.y, 1f, start, end);
            FindNearlyNodeToOpenList(start.x + 1, start.y, 1f, start, end);
            FindNearlyNodeToOpenList(start.x, start.y + 1, 1f, start, end);
            FindNearlyNodeToOpenList(start.x + 1, start.y + 1, 1.4f, start, end);
            FindNearlyNodeToOpenList(start.x - 1, start.y + 1, 1.4f, start, end);

            if (openList.Count == 0)
            {
                Debug.Log("死路");
                return null;
            }
               

            //寻路消耗最小的点
            openList.Sort(SortOpenList);

            //放入关闭列表中，然后再从开启列表中
            closeList.Add(openList[0]);
            //找到这个点 新的寻路起点 进行下一次寻路计算
            start = openList[0];
            //从开启列表中移除
            openList.RemoveAt(0);

            //如果这个点 不是终点 那么 继续寻路
            if (start == end)
            {
                //返回路径 
                List<AStarNode> path = new List<AStarNode>();
                path.Add(end);
                while (end.father!=null)
                {
                    path.Add(end.father);
                    end = end.father;
                }
                path.Reverse();
                return path;
            }
        }
    }

    private int SortOpenList(AStarNode a , AStarNode b)
    {
        if (a.f > b.f)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }


    private void FindNearlyNodeToOpenList(int x,int y,float g,AStarNode father,AStarNode end)
    {
        //边界判断
        if (x < 0 || x >= mapW ||
            y < 0 || y >= mapH)
            return;
        //在范围内 再去取点
            AStarNode node = nodes[x, y];
        //判断这些点 是否是边界 是否是阻挡 是否在开启或者关闭列表中 如果都不是 才放入开启列表
            if (node == null || node.type == NodeType.Stop ||
                  closeList.Contains(node) || openList.Contains(node))
                return;
            //记录父对象
        node.father = father;
        //计算G 我离起点的距离等于 父亲离起点的距离 + 我离父亲的距离
        node.g = father.g + g;
        node.h = Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
        node.f = node.g + node.h;
        
        openList.Add(node);

    }
    

}
