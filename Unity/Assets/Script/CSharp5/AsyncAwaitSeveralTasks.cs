//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using UnityEngine;
//public class DataAsyncController : MonoBehaviour
//{
//	class User
//	{
//		public string Name;
//	}

//	class Todo
//	{
//		public string Title;
//	}

//	readonly string USERS_URL = "https://jsonplaceholder.typicode.com/users";
//	readonly string TODOS_URL = "https://jsonplaceholder.typicode.com/todos";
//	async Task<User[]> FetchUsers()
//	{
//		var www = await new WWW(USERS_URL);
//		if (!string.IsNullOrEmpty(www.error))
//		{
//			throw new Exception();
//		}
//		var json = www.text;
//		var userRaws = JsonHelper.getJsonArray<UserRaw>(json);
//		return userRaws.Select(userRaw => new User(userRaw)).ToArray();
//	}
//	async Task<Todo[]> FetchTodos()
//	{
//		var www = await new WWW(TODOS_URL);
//		if (!string.IsNullOrEmpty(www.error))
//		{
//			throw new Exception();
//		}
//		var json = www.text;
//		var todosRaws = JsonHelper.getJsonArray<TodoRaw>(json);
//		return todosRaws.Select(todoRaw => new Todo(todoRaw)).ToArray();
//	}
//	async void Start()
//	{
//		try
//		{
//			var users = FetchUsers();
//			var todos = FetchTodos();
//			await Task.WhenAll(users, todos);
//			var usersResult = await users;

//			foreach (User user in usersResult)
//			{
//				Debug.Log(user.Name);
//			}
//			foreach (Todo todo in todos)
//			{
//				Debug.Log(todo.Title);
//			}
//		}
//		catch
//		{
//			Debug.Log("An error occurred");
//		}
//	}
//}