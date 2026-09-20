using System;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Text;
using System.Xml.Linq;

public class ObjectDumper
{
	private StringBuilder sb;

	private TextWriter writer;

	private int pos;

	private int level;

	private int depth;

	public static void Write(object o)
	{
		Write(o, 0);
	}

	public static void Write(object o, int depth)
	{
		ObjectDumper objectDumper = new ObjectDumper(depth);
		objectDumper.WriteObject(null, o);
	}

	public static string GetObjectValue(object o)
	{
		ObjectDumper objectDumper = new ObjectDumper(1, false);
		objectDumper.WriteObject(null, o);
		return objectDumper.sb.ToString();
	}

	private ObjectDumper(int depth)
	{
		sb = new StringBuilder();
		writer = Console.Out;
		this.depth = depth;
	}

	private ObjectDumper(int depth, bool ConsoleOut)
	{
		sb = new StringBuilder();
		writer = ConsoleOut ? Console.Out : null;
		this.depth = depth;
	}

	private void WriterWrite(string s)
	{
		if (writer != null)
		{
			writer.Write(s);
		}
		sb.Append(s);
	}

	private void Write(string s)
	{
		if (s != null)
		{
			WriterWrite(s);
			pos += s.Length;
		}
	}

	private void WriteIndent()
	{
		for (int i = 0; i < level; i++)
		{
			WriteTab();
		}
	}

	private void WriteLine()
	{
		if (writer != null)
		{
			writer.WriteLine();
		}
		sb.Append("\r\n");
		pos = 0;
	}

	private void WriteTab()
	{
		WriterWrite("  ");
		while ((pos & 7) != 0)
		{
			WriterWrite(" ");
			pos++;
		}
	}

	private void WriteObject(string prefix, object o)
	{
		if (o == null || o is string || o is ValueType || o is XElement)
		{
			WriteIndent();
			Write(prefix);
			WriteValue(o);
			WriteLine();
			return;
		}

		IEnumerable enumerable = o as IEnumerable;
		if (enumerable != null)
		{
			foreach (object item in enumerable)
			{
				if (item is IEnumerable && !(item is string))
				{
					WriteIndent();
					Write(prefix);
					Write("...");
					WriteLine();
					if (level < depth)
					{
						level++;
						WriteObject(prefix, item);
						level--;
					}
				}
				else
				{
					WriteObject(prefix, item);
				}
			}
			return;
		}

		MemberInfo[] members = o.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public);
		bool wroteMember = false;
		foreach (MemberInfo member in members)
		{
			FieldInfo field = member as FieldInfo;
			PropertyInfo property = member as PropertyInfo;
			if (field == null && property == null)
			{
				continue;
			}
			Type memberType = field != null ? field.FieldType : property.PropertyType;
			object value = field != null ? field.GetValue(o) : property.GetValue(o, null);
			if (wroteMember)
			{
				WriteTab();
			}
			else
			{
				WriteIndent();
			}
			Write(member.Name + "=");
			if (memberType.IsValueType || memberType == typeof(string))
			{
				WriteValue(value);
			}
			else if (typeof(IEnumerable).IsAssignableFrom(memberType))
			{
				Write("...");
			}
			else
			{
				Write("{ }");
			}
			WriteLine();
			wroteMember = true;
		}

		if (level >= depth)
		{
			return;
		}
		foreach (MemberInfo member in members)
		{
			FieldInfo field = member as FieldInfo;
			PropertyInfo property = member as PropertyInfo;
			if (field == null && property == null)
			{
				continue;
			}
			Type memberType = field != null ? field.FieldType : property.PropertyType;
			if (memberType.IsValueType || memberType == typeof(string) || typeof(IEnumerable).IsAssignableFrom(memberType))
			{
				continue;
			}
			object value = field != null ? field.GetValue(o) : property.GetValue(o, null);
			if (value == null)
			{
				continue;
			}
			level++;
			WriteObject(member.Name + ": ", value);
			level--;
		}
	}

	private void WriteValue(object o)
	{
		if (o == null)
		{
			Write("{ }");
		}
		else if (o is DateTime dateTime)
		{
			Write(dateTime.ToShortDateString());
		}
		else if (o is string || o is ValueType || o is XElement)
		{
			Write(o.ToString());
		}
		else if (o is IEnumerable)
		{
			Write("...");
		}
		else
		{
			Write("{ }");
		}
	}
}
