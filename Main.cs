using UnityEngine;

using System;

public class Main : MonoBehaviour
{
    // ==================================================================================================== Fields

    [Header("입력 숫자")]
    public int Number = 32;

    // 탐색 경로 및 탐색 현황 등을 저장할 이차원 배열 생성
    private int[,] Map;

    // ==================================================================================================== Properties

    // 현재 탐색 중인 총 노드 수
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

    // 현재 탐색 중인 부모 노드
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

    // 현재 탐색 중인 자식 노드
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
        // 배열의 인덱싱을 일반적인 자연수로 맞춰 주어 직관화
        // 0번 인덱스의 배열들에 탐색 중인 인덱싱 정보 저장
        Map = new int[Number + 1, Number + 1];

        SetRoute();
        FindPath();
        PrintPath();
    }

    // =========================================================================== SetRoute

    // 각 노드 별 탐색 경로 설정
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

    // 각 노드 별 탐색 경로 출력
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

    // 두 수의 합이 제곱수이면 true를 반환
    public bool IsSquare(int a, int b)
    {
        return Math.Sqrt(a + b) % 1 == 0;
    }

    // =========================================================================== FindPath

    // 경로 탐색
    public void FindPath()
    {
        TotalIndex = 1;

        RowIndex = 1;
        ColumnIndex = 1;

        // 재귀 함수 대신 while 문을 이용해 구현
        while (TotalIndex < Number + 1 && TotalIndex > 0)
        {
            if (TotalIndex < Number) // 모든 노드를 순회하지 않았다면 일반적인 경로 탐색 실행
            {
                ColumnIndex = MoveNext(RowIndex, ColumnIndex);

                // 다음 자식 노드가 없다면 이전 노드 복귀
                if (ColumnIndex > Number)
                {
                    BackTo();

                    continue;
                }

                MoveTo(ColumnIndex);
            }
            else // 모든 노드를 순회하였다면 순환 조건 확인
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

    // 해당 노드의 다음 자식 노드 반환
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

    // 해당 노드를 이미 탐색했다면 true를 반환
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

    // 다음 노드 탐색
    public void MoveTo(int number)
    {
        TotalIndex++;

        RowIndex = number;
    }

    // 이전 노드 복귀
    public void BackTo()
    {
        RowIndex = 0;
        ColumnIndex = 0;

        TotalIndex--;
    }

    // =========================================================================== PrintPath

    // 탐색 경로 출력
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
