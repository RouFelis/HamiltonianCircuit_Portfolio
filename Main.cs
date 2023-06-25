using UnityEngine;

using System;

public class Main : MonoBehaviour
{
    // ==================================================================================================== Fields

    [Header("�Է� ����")]
    public int Number = 32;

    // Ž�� ��� �� Ž�� ��Ȳ ���� ������ ������ �迭 ����
    private int[,] Map;

    // ==================================================================================================== Properties

    // ���� Ž�� ���� �� ��� ��
    public int TotalIndex
    {
        get
        {
            return Map[0, 0];
        }

        set
        {
            Map[0, 0] = value;
        }
    }

    // ���� Ž�� ���� �θ� ���
    public int RowIndex
    {
        get
        {
            return Map[TotalIndex, 0];
        }

        set
        {
            Map[TotalIndex, 0] = value;
        }
    }

    // ���� Ž�� ���� �ڽ� ���
    public int ColumnIndex
    {
        get
        {
            return Map[0, TotalIndex];
        }

        set
        {
            Map[0, TotalIndex] = value;
        }
    }

    // ==================================================================================================== Methods

    private void Awake()
    {
        // �迭�� �ε����� �Ϲ����� �ڿ����� ���� �־� ����ȭ
        // 0�� �ε����� �迭�鿡 Ž�� ���� �ε��� ���� ����
        Map = new int[Number + 1, Number + 1];

        SetRoute();
        FindPath();
        PrintPath();
    }

    // =========================================================================== SetRoute

    // �� ��� �� Ž�� ��� ����
    public void SetRoute()
    {
        for (int row = 1; row < Number + 1; row++)
        {
            for (int column = 1; column < Number + 1; column++)
            {
                if (IsSquare(row, column) && row != column)
                {
                    Map[row, column] = 1;
                }
                else
                {
                    Map[row, column] = 0;
                }
            }
        }

        DebugMap();
    }

    // �� ��� �� Ž�� ��� ���
    public void DebugMap()
    {
        string str;

        for (int row = 1; row < Number + 1; row++)
        {
            str = row.ToString();

            for (int column = 1; column < Number + 1; column++)
            {
                if (Map[row, column] is 1)
                {
                    str = $"{str} {column}";
                }
            }

            Debug.Log(str);
        }
    }

    // �� ���� ���� �������̸� true�� ��ȯ
    public bool IsSquare(int a, int b)
    {
        return Math.Sqrt(a + b) % 1 == 0;
    }

    // =========================================================================== FindPath

    // ��� Ž��
    public void FindPath()
    {
        TotalIndex = 1;

        RowIndex = 1;
        ColumnIndex = 1;

        // ��� �Լ� ��� while ���� �̿��� ����
        while (TotalIndex < Number + 1 && TotalIndex > 0)
        {
            if (TotalIndex < Number) // ��� ��带 ��ȸ���� �ʾҴٸ� �Ϲ����� ��� Ž�� ����
            {
                ColumnIndex = MoveNext(RowIndex, ColumnIndex);

                // ���� �ڽ� ��尡 ���ٸ� ���� ��� ����
                if (ColumnIndex > Number)
                {
                    BackTo();

                    continue;
                }

                MoveTo(ColumnIndex);
            }
            else // ��� ��带 ��ȸ�Ͽ��ٸ� ��ȯ ���� Ȯ��
            {
                if (IsSquare(RowIndex, Map[1, 0]))
                {
                    TotalIndex++;
                }
                else
                {
                    BackTo();
                }
            }
        }
    }

    // �ش� ����� ���� �ڽ� ��� ��ȯ
    public int MoveNext(int row, int number)
    {
        for (int column = number + 1; column < Number + 1; column++)
        {
            if (Map[row, column] is 1 && !IsContains(column))
            {
                return column;
            }
        }

        return Number + 1;
    }

    // �ش� ��带 �̹� Ž���ߴٸ� true�� ��ȯ
    public bool IsContains(int number)
    {
        for (int row = 1; row < Number + 1; row++)
        {
            if (Map[row, 0] == number)
            {
                return true;
            }
        }

        return false;
    }

    // ���� ��� Ž��
    public void MoveTo(int number)
    {
        TotalIndex++;

        RowIndex = number;
    }

    // ���� ��� ����
    public void BackTo()
    {
        RowIndex = 0;
        ColumnIndex = 0;

        TotalIndex--;
    }

    // =========================================================================== PrintPath

    // Ž�� ��� ���
    public void PrintPath()
    {
        string messege = Map[1, 0].ToString();

        for (int row = 2; row < Number + 1; row++)
        {
            messege = $"{messege} => {Map[row, 0]}";
        }

        Debug.Log(messege);
    }
}
