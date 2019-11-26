using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Burst;

public class MoveJobTest : MonoBehaviour
{
    public class Cube
    {
        public Transform Form;
        public float MoveY;
    }

    public bool isUseJob;

    private List<Cube> _cubeList = new List<Cube>();

    public const float MIN_VALUE = -5f;
    public const float MAX_VALUE = 5f;

    private void Start()
    {
        for(int i = 0 ; i < 1000 ; i++)
        {
            GameObject cubeGO = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubeGO.transform.position = new Vector3(UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE), UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE), 0f);
            cubeGO.transform.localScale = Vector3.one * 0.4f;

            Cube cube = new Cube()
            {
                Form = cubeGO.transform,
                MoveY = UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE)
            };
            _cubeList.Add(cube);
        }
    }

    private void Update()
    {
        if(isUseJob)
        {
            NativeList<float3> positionList = new NativeList<float3>(Allocator.TempJob);
            NativeList<float> moveYList = new NativeList<float>(Allocator.TempJob);

            for (int i = 0; i < _cubeList.Count; i++)
            {
                positionList.Add(_cubeList[i].Form.position);
                moveYList.Add(_cubeList[i].MoveY);
            }

            ParallelMoveJob job = new ParallelMoveJob()
            {
                PositionArray = positionList,
                MoveYArray = moveYList,
                deltaTime = Time.deltaTime
            };

            JobHandle handler = job.Schedule(_cubeList.Count, _cubeList.Count / 10); /// innerloopBatchCount는 잡 워커가 가져갈 반복 횟수??
            handler.Complete();

            for (int i = 0 ; i < _cubeList.Count ; i++)
            {
                _cubeList[i].Form.position = job.PositionArray[i];
            }

            positionList.Dispose();
            moveYList.Dispose();
        }
        else
        {
            foreach(var cube in _cubeList)
            {
                Vector3 temp = cube.Form.position + Vector3.up * cube.MoveY * Time.deltaTime;

                if (temp.y < MIN_VALUE)
                    temp.y = MAX_VALUE;
                else if (temp.y > MAX_VALUE)
                    temp.y = MIN_VALUE;

                cube.Form.position = temp;

                /// cpu를 많이 사용하는 코드
                float v = 0f;
                for(int i = 0 ; i < 1000 ; i++)
                {
                    v = math.exp10(math.sqrt(v));
                }
            }
        }
    }
}

[BurstCompile]
public struct ParallelMoveJob : IJobParallelFor
{
    // 잡 시스템에서는 참조타입을 쓸 수가 없어서 무조건 값형식으로만 쓸 수 있다.
    // 배열을 쓰려면 네이티브 컨테이너 NativeArray를 사용해야한다.

    public NativeArray<float3> PositionArray;
    public NativeArray<float> MoveYArray;
    public float deltaTime;// 잡 시스템에서는 Time.deltaTime에 접근이 불가능 메인쓰레드를 사용한다는 보장이 없기 때문?

    public void Execute(int index)
    {
        float3 up = new float3(0f, 1f, 0f);
        Vector3 temp = PositionArray[index] + up * MoveYArray[index] * deltaTime;

        if (temp.y < MoveJobTest.MIN_VALUE)
            temp.y = MoveJobTest.MAX_VALUE;
        else if (temp.y > MoveJobTest.MAX_VALUE)
            temp.y = MoveJobTest.MIN_VALUE;

        PositionArray[index] = temp;

        /// cpu를 많이 사용하는 코드
        float v = 0f;
        for (int i = 0; i < 1000; i++)
        {
            v = math.exp10(math.sqrt(v));
        }
    }
}


