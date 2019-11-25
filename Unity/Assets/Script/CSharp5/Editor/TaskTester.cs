using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

[TestFixture]
public class TaskTester
{
    [Test]
    public void TestTask()
    {
        Task.Factory.StartNew(TestTask2);
    }

    private async Task TestTask2()
    {
        Cookie cookie = new Cookie();
        IceCream iceCream = new IceCream();
        Cola cola = new Cola();
        /// 테스크들 동시 시작
        /// async 함수 실행했을때는 쓰레드를 차단하지 않는다. 그래서 동시에 시작 가능
        var t1 = cookie.Cook();
        var t2 = iceCream.Cook();
        var t3 = cola.Cook();

        List<Task<Food>> list = new List<Task<Food>>();
        list.Add(t1);
        list.Add(t2);
        list.Add(t3);

        Debug.Log("Ready");

        while (true)
        {
            await Task.WhenAll(list).ContinueWith(r =>
            {
                // 모든 테스크 끝나면 할 테스크
                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(1000);
                    Debug.Log(r.Result.GetType().Name);
                });

            });
            break;
        }

        Debug.Log("Done");
    }
}

public abstract class Food
{
    public virtual async Task<Food> Cook()
    {
        await Task.Delay(1000);
        Debug.Log("Cook!");
        return this;
    }

}

public class Cookie : Food
{
    public override async Task<Food> Cook()
    {
        Debug.Log("Ready Cookie");
        Debug.Log("Ready Cookie2");
        await Task.Delay(1000);
        Debug.Log("Cookie!");
        return this;
    }
}

public class IceCream : Food
{
    public override async Task<Food> Cook()
    {
        Debug.Log("Ready IceCream");
        Debug.Log("Ready IceCream2");
        await Task.Delay(2000);
        Debug.Log("IceCream!");
        return this;
    }
}

public class Cola : Food
{
    public override async Task<Food> Cook()
    {
        Debug.Log("Ready Cola");
        Debug.Log("Ready Cola2");
        await Task.Delay(3000);
        Debug.Log("Cola!");
        return this;
    }
}