using System;

namespace NetMock.Rest
{
	public partial class RestRequestSetup
	{
		public void Callback(Action callback)
			=> SetCallback(callback);

		public void Callback(Action<IReceivedRequest> callback)
			=> SetCallback(callback);

		public void Callback<T1>(Action<T1> callback)
			=> SetCallback(callback);

		public void Callback<T1>(Action<IReceivedRequest, T1> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2>(Action<T1, T2> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2>(Action<IReceivedRequest, T1, T2> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3>(Action<T1, T2, T3> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3>(Action<IReceivedRequest, T1, T2, T3> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3, T4>(Action<IReceivedRequest, T1, T2, T3, T4> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3, T4, T5>(Action<IReceivedRequest, T1, T2, T3, T4, T5> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3, T4, T5, T6>(Action<IReceivedRequest, T1, T2, T3, T4, T5, T6> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3, T4, T5, T6, T7>(Action<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
			=> SetCallback(callback);

		public void Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
			=> SetCallback(callback);
	}
}
