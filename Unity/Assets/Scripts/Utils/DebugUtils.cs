using System;

public class DebugUtils
{
	public static void Assert(bool condition)
	{
		Assert(condition, "");
	}
	
	public static void Assert(bool condition, string message)
	{
		#if UNITY_EDITOR
		if (!condition) throw new Exception(message);
		#endif
	}
}
