using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;


/*http://www.csharpstudy.com/CSharp/CSharp-async-await.aspx
 await는 Task와 같은 awaitable 클래스의 객체가 완료되기를 기다리는데, 
 여기서 중요한 점은 UI 쓰레드가 정지되지 않고 메시지 루프를 계속 돌 수 있도록 필요한 코드를 컴파일러가 await 키워드를 만나면 자동으로 추가한다는 점이다. 
 메시지 루프가 계속 돌게 만든다는 것은 마우스 클릭이나 키보드 입력 등과 같은 윈도우 메시지들을 계속 처리할 수 있다는 것을 의미한다. 
 await는 해당 Task가 끝날 때까지 기다렸다가 완료 후, await 바로 다음 실행문부터 실행을 계속한다. 
 await가 기다리는 Task 혹은 실행 메서드는 별도의 Worker Thread에서 돌 수도 있고, 또는 UI Thread에서 돌 수도 있다. 
 즉, await가 항상 비동기 실행을 위한 Background Thread를 필요로 하는 것은 아니다. 
 await가 보장하는 것은, Task가 UI Thread에 돌든지, 
 Worker thread에서 돌든지 상관없이 Task 완료 후 await 이후의 실행문들을 디폴트로 원래 await를 실행하기 전의 Thread에서 실행하도록 보장하는 것이다.*/

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