using System;
using System.Globalization;
using System.Xml;

namespace Common
{
	public static class XmlNodeExtension
	{
		public static XmlElement Append(this XmlNode node, string nodeName)
		{
			return (XmlElement)node.AppendChild(node.OwnerDocument.CreateElement(nodeName));
		}

		public static XmlElement AppendText(this XmlNode node, string nodeName, string text)
		{
			XmlElement result = node.Append(nodeName);
			result.AppendChild(node.OwnerDocument.CreateTextNode(text));
			return result;
		}

		/// <summary>
		/// ���������� �������� ��������� ����.
		/// </summary>
		/// <param name="root">���������� ����.</param>
		/// <param name="nodeName">��� ��������� ����.</param>
		/// <param name="raiseIfNull">true, ���� ����� �������� �� ������������� ����������.</param>
		public static string GetText(this XmlNode root, string nodeName, bool raiseIfNull = false, XmlNamespaceManager xnm = null)
		{
			XmlText attr = (XmlText)root.SelectSingleNode(nodeName + "/text()", xnm);
			if (attr == null || attr.Value == null || attr.Value.Length == 0)
			{
				if (raiseIfNull)
				{
					throw new NullReferenceException(string.Format("�� ������ '{0}'.", nodeName));
				}
				return null;
			}

			return attr.Value;
		}

		public static DateTime? GetNullableDate(this XmlNode root, string nodeName, string format = null, bool raiseIfNull = false, XmlNamespaceManager xnm = null)
		{
			string text = GetText(root, nodeName, raiseIfNull, xnm);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}

			if (format == null)
			{
				format = "dd.MM.yyyy";
			}

			try
			{
				return DateTime.ParseExact(text, format, DateTimeFormatInfo.InvariantInfo);
			}
			catch (Exception ex)
			{
				throw new FormatException(string.Format("����������� ������ ���� � ���� '{0}': {1}.", nodeName, text), ex);
			}
		}

		public static DateTime GetDate(this XmlNode root, string nodeName, string format = null, XmlNamespaceManager xnm = null)
		{
			DateTime? result = GetNullableDate(root, nodeName, format, true, xnm);
			if (result == null) throw new NullReferenceException();
			return result.Value;
		}

		public static decimal? GetNullableDecimal(this XmlNode root, string nodeName, bool raiseIfNull = false, XmlNamespaceManager xnm = null)
		{
			string text = root.GetText(nodeName, raiseIfNull, xnm);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}

			try
			{
				return text.ToDecimal();
			}
			catch (Exception ex)
			{
				throw new FormatException(string.Format("����������� ������ ����� � ���� '{0}': {1}.", nodeName, text), ex);
			}
		}

		public static decimal GetDecimal(this XmlNode root, string nodeName, XmlNamespaceManager xnm = null)
		{
			decimal? result = GetNullableDecimal(root, nodeName, true, xnm);
			if (result == null) throw new NullReferenceException();
			return result.Value;
		}

		public static int? GetNullableInt(this XmlNode root, string nodeName, bool raiseIfNull = false, XmlNamespaceManager xnm = null)
		{
			string text = GetText(root, nodeName, raiseIfNull, xnm);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}

			try
			{
				return int.Parse(text);
			}
			catch (Exception ex)
			{
				throw new FormatException(string.Format("����������� ������ ������ � ���� '{0}': {1}.", nodeName, text), ex);
			}
		}

		public static int GetInt(this XmlNode root, string nodeName, XmlNamespaceManager xnm = null)
		{
			int? result = GetNullableInt(root, nodeName, true, xnm);
			if (result == null) throw new NullReferenceException();
			return result.Value;
		}
	}
}