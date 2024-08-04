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

        //��ʼ��
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                AStarNode node = new AStarNode(i,j,Random.Range(0,100)<20? NodeType.Stop:NodeType.Walk);
                nodes[i, j] = node;
            }
        }



    }

    //ʵ����Ŀ��Ӧ�ø���ת��Ϊ��Ӧ�ĸ�����Ϣ
    public List<AStarNode> FindPath(Vector2 startPos, Vector2 endPos)
    {
        //�ж�λ���Ƿ�Ϸ� 
        if (startPos.x < 0 || startPos.x >= mapW ||
            startPos.y < 0 || startPos.y >= mapH ||
            endPos.x < 0 || endPos.x >= mapW ||
            endPos.y < 0 || endPos.y >= mapH)
        {
            Debug.Log("��ʼ����߽����㲻�Ϸ�");
            return null;

        }


        AStarNode start = nodes[(int)startPos.x, (int)startPos.y];
        AStarNode end = nodes[(int)endPos.x, (int)endPos.y];

        //�ж��Ƿ��赲
        if (start.type == NodeType.Stop || end.type == NodeType.Stop)
        {
            Debug.Log("��ʼ���߽������赲");
            return null;
        }
        //�����һ����ص����� ��������Ӱ�� ��һ�ε�����
        //��չرպͿ����б�
        closeList.Clear();
        openList.Clear();

        //�ѿ�ʼ�����ر��б���
        start.father = null;
        start.g = 0;
        start.f = 0;
        start.h = 0;
        closeList.Add(start);

        while (true)
        {
            //ѭ���˸���
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
                Debug.Log("��·");
                return null;
            }
               

            //Ѱ·������С�ĵ�
            openList.Sort(SortOpenList);

            //����ر��б��У�Ȼ���ٴӿ����б���
            closeList.Add(openList[0]);
            //�ҵ������ �µ�Ѱ·��� ������һ��Ѱ·����
            start = openList[0];
            //�ӿ����б����Ƴ�
            openList.RemoveAt(0);

            //�������� �����յ� ��ô ����Ѱ·
            if (start == end)
            {
                //����·�� 
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
        //�߽��ж�
        if (x < 0 || x >= mapW ||
            y < 0 || y >= mapH)
            return;
        //�ڷ�Χ�� ��ȥȡ��
            AStarNode node = nodes[x, y];
        //�ж���Щ�� �Ƿ��Ǳ߽� �Ƿ����赲 �Ƿ��ڿ������߹ر��б��� ��������� �ŷ��뿪���б�
            if (node == null || node.type == NodeType.Stop ||
                  closeList.Contains(node) || openList.Contains(node))
                return;
            //��¼������
        node.father = father;
        //����G �������ľ������ ���������ľ��� + ���븸�׵ľ���
        node.g = father.g + g;
        node.h = Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
        node.f = node.g + node.h;
        
        openList.Add(node);

    }
    

}
