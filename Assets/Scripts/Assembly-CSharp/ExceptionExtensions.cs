using System;
using System.Diagnostics;
using System.Text;

public static class ExceptionExtensions
{
	private const string tab = "   ";

	private const string nl = "\r\n";

	public static string ToLogString(this Exception excep, StackFrame stackFrame, object AdditionalInfo)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (stackFrame != null)
		{
			stringBuilder.AppendFormat("Exception Location:{0}", "\r\n");
			stringBuilder.AppendFormat("{0}Namespace: {1}{2}", tab, stackFrame.GetMethod().DeclaringType.Namespace, "\r\n");
			stringBuilder.AppendFormat("{0}Class Name: {1}{2}", tab, stackFrame.GetMethod().DeclaringType.Name, "\r\n");
			stringBuilder.AppendFormat("{0}Method Name: {1}{2}", tab, stackFrame.GetMethod().Name, "\r\n");
		}
		stringBuilder.AppendFormat("Exception Information:{0}", "\r\n");
		if (excep != null)
		{
			stringBuilder.AppendFormat("{2}Message: {0}{1}", excep.Message, "\r\n", tab);
			stringBuilder.AppendFormat("{2}Source: {0}{1}{1}", excep.Source, "\r\n", tab);
			stringBuilder.AppendFormat("Stack Trace: {1} {0}{1}{1}", excep.StackTrace, "\r\n");
			if (excep.InnerException != null)
			{
				stringBuilder.AppendFormat("{1}{0}Inner Exception Info:{0}", "\r\n", tab);
				stringBuilder.AppendFormat("{2}{2}Message: {0}{1}", excep.InnerException.Message, "\r\n", tab);
				stringBuilder.AppendFormat("{2}{2}Source: {0}{1}", excep.InnerException.Source, "\r\n", tab);
				stringBuilder.AppendFormat("{2}{2}Stack Trace: {0}{1}", excep.InnerException.StackTrace, "\r\n", tab);
			}
		}
		if (AdditionalInfo != null)
		{
			stringBuilder.AppendFormat("Additional Object Info{0}", "\r\n");
			stringBuilder.AppendFormat("{0}{1}{2}", tab, ObjectDumper.GetObjectValue(AdditionalInfo), "\r\n");
		}
		return stringBuilder.ToString();
	}
}
