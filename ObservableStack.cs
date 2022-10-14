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
		
		public void SubscribeOn(IObservable observer)
		{
			observer.Notify += Update;
		}

		public string GetLog()
		{
			return _log.ToString();
		}
	}

	public interface IObservable
	{
		delegate void ObserverHandler(string message);
		event ObserverHandler Notify;
	}

	public class ObservableStack<T> : IObservable
	{
		private readonly Stack<T> _stack = new Stack<T>();

		public event IObservable.ObserverHandler Notify;

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
