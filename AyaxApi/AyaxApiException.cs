using System;

namespace AyaxApi
{
	/// <summary>
	/// Исключения при работе со службой.
	/// </summary>
	public class AyaxApiException : Exception
	{
		public AyaxApiException(string message, Exception innerException) 
			: base(message, innerException)
		{
		}
	}
}