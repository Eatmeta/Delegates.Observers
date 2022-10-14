using System.Collections.Generic;
using System.Text;

namespace Delegates.Observers
{
	public class StackOperationsLogger
	{
		private readonly StringBuilder _log  = new StringBuilder();

		private void Update(string message)
		{
			_log.Append(message);
		}
		
		public void SubscribeOn(Observable observer)
		{
			observer.Notify += Update;
		}

		public string GetLog()
		{
			return _log.ToString();
		}
	}

	public abstract class Observable
	{
		public delegate void ObserverHandler(string message);
		public abstract event ObserverHandler Notify;
	}

	public class ObservableStack<T> : Observable
	{
		private readonly Stack<T> _stack = new Stack<T>();

		public override event ObserverHandler Notify;

		public void Push(T obj)
		{
			_stack.Push(obj);
			Notify?.Invoke("+" + obj);
		}

		public T Pop()
		{
			var item = _stack.Pop();
			Notify?.Invoke("-" + item);
			return item;
		}
	}
}
