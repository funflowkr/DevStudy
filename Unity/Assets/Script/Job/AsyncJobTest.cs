//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Jobs;
//using Unity.Jobs;
//using Unity.Collections;
//using Unity.Mathematics;
//using Unity.Entities;
//using Unity.Burst;
//using System.Threading.Tasks;

// 컴파일 오류로 임시로 주석 처리

//public class AsyncJobTest : MonoBehaviour
//{
//    public static int MainThreadID = 0;
//    public class Cube
//    {
//        public Transform Form;
//        public float MoveY;
//    }

//    private List<Cube> _cubeList = new List<Cube>();

//    public const float MIN_VALUE = -5f;
//    public const float MAX_VALUE = 5f;

//    private void Start()
//    {
//        MainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
//        Debug.Log(MainThreadID);
//        for (int i = 0; i < 10; i++)
//        {
//            GameObject cubeGO = GameObject.CreatePrimitive(PrimitiveType.Cube);
//            cubeGO.transform.position = new Vector3(UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE), UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE), 0f);
//            cubeGO.transform.localScale = Vector3.one * 0.4f;

//            Cube cube = new Cube()
//            {
//                Form = cubeGO.transform,
//                MoveY = UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE)
//            };
//            _cubeList.Add(cube);
//        }

//    }

//    private void Update()
//    {
//        foreach(var cube in _cubeList)
//            cube.Form.Rotate(Vector3.forward * Time.deltaTime * 30f);

//        if (Input.GetMouseButtonDown(0))
//        {
//            AsyncParallelMoveJob job = new AsyncParallelMoveJob();

//            JobHandle handler = job.Schedule(_cubeList.Count, _cubeList.Count / 10); /// innerloopBatchCount는 잡 워커가 가져갈 반복 횟수??

//            /*https://forum.unity.com/threads/burst-and-thread-safe-api-outside-unity-jobs-system.522737/
//             * Schedule doesn't actually schedule the jobs immediately, but add them to a queue. 
//             * Jobs are scheduled when you call JobHandle.ScheduleBatchedJobs or JobHandle.Complete. 
//             * This is done for performance reasons since scheduling individual jobs results in expensive Semaphore.Signal calls. 
//             * By scheduling many jobs at the same time delayed this cost will instead be paid only once per ScheduleBatchedJobs calls.*/
//            handler.Complete();
//        }
//    }
//}

//public struct AsyncParallelMoveJob : IJobParallelFor
//{
//    int tempIndex;

//    public void Execute(int index)
//    {
//        tempIndex = index;
//        //Process();
//        Task.Factory.StartNew(AsyncProcess);
//    }

//    async void AsyncProcess()
//    {
//        float v = 0f;
//        int i = 0;
//        Debug.LogFormat("Index : {0} AsyncProcess job Start, ThreadID : {1} ", tempIndex, System.Threading.Thread.CurrentThread.ManagedThreadId);
//        for (i = 0; i < 1000000; i++)
//        {
//            v = math.exp10(math.sqrt(v));
//            if(i != 0 && i % 10000 == 0)
//                await Task.Delay(25);
//        }
//        Debug.LogFormat("Index : {0} AsyncProcess job End, ThreadID : {1} ", tempIndex, System.Threading.Thread.CurrentThread.ManagedThreadId);

//    }

//    void Process()
//    {
//        float v = 0f;
//        int i = 0;
//        Debug.LogFormat("Index : {0} Process job Start, ThreadID : {1} ", tempIndex, System.Threading.Thread.CurrentThread.ManagedThreadId);
//        for (i = 0; i < 1000000; i++)
//        {
//            v = math.exp10(math.sqrt(v));
//        }
//        Debug.LogFormat("Index : {0} Process job End, ThreadID : {1} ", tempIndex, System.Threading.Thread.CurrentThread.ManagedThreadId);
//    }
//}


