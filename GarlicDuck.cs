using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicDuck : MonoBehaviour 
{
    public int maxNum = 32; // 31 < maxNum < 44
    public int startNum = 1;

    List<Node> list_square = new List<Node>();

    List<int> list_checkList = new List<int>();
    List<int> list_circle = new List<int>();

    bool isComplete = false;

    void Start()
    {
        ListMaker();
        CircleMaker(startNum);
        CircleViewer();
    }

    void ListMaker()
    {
        for (int i = 1; i < maxNum + 1; i++)
        {
            List<int> list_temp = new List<int>();

            for (int j = 3; j < Mathf.Sqrt(maxNum * 2 - 1); j++)
            {
                int temp = j * j - i;

                if (temp > 0 && temp <= maxNum && temp != i)
                {
                    list_temp.Add(temp);
                }
            }

            list_square.Add(new Node(list_temp));
            list_checkList.Add(i);
        }
    } // 맵 리스트 & 중복 확인 리스트 생성

    void CircleMaker(int number)
    {
        list_checkList.Remove(number);
        list_circle.Add(number);

        foreach(int temp in list_square[number - 1].GetSquare())
        {
            if (list_checkList.Contains(temp))
            {
                CircleMaker(temp);
            }
        }

        if (isComplete)
        {
            return;
        }
        else if (list_checkList.Count == 0)
        {
            foreach (int temp in list_square[list_circle[maxNum - 1] - 1].GetSquare())
            {
                if (temp == list_circle[0])
                {
                    isComplete = true; 
                    return;
                }
            }
        }

        list_circle.Remove(number);
        list_checkList.Add(number);
    } // 수열 리스트 생성

    void CircleViewer()
    {
        string str = "";

        foreach (int materials in list_circle)
        {
            if (str == "")
            {
                str = string.Format("{0}", materials);
            }
            else
            {
                str = string.Format("{0} - {1}", str, materials);
            }
        }

        Debug.Log(str);
    } // 수열 리스트 디버그
}

public class Node
{
    List<int> list_sqrt = new List<int>();

    public Node() { }
    public Node(List<int> list_temp)
    {
        foreach(int materials in list_temp)
        {
            list_sqrt.Add(materials);
        }
    }

    public List<int> GetSquare()
    {
        return list_sqrt;
    }
}