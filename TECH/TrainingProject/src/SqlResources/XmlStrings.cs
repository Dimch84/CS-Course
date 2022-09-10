using System;
using System.Reflection;
using System.Xml;

namespace SqlHelper
{
	public class XmlStrings
	{
		private static readonly XmlDocument xmlDocument;

		static XmlStrings()
		{
			xmlDocument = loadXml();
		}

		private static XmlDocument loadXml()
		{
			Assembly resourceAssembly = Assembly.GetExecutingAssembly();
			var xmlDoc = new XmlDocument();
			xmlDoc.Load(resourceAssembly.GetManifestResourceStream("SqlHelper.Queries.xml"));
			return xmlDoc;
		}

		public static string GetString(string commandName)
		{
			return GetString(string.Empty, commandName);
		}

		public static string GetString(string tableName, string commandName)
		{
			if (tableName == null)
				throw new ArgumentNullException("tableName");
			if (commandName == null)
				throw new ArgumentNullException("commandName");
			if (tableName == string.Empty)
				tableName = "CUSTOM_COMMANDS";

			return loadCommand(tableName, commandName);
		}

		private static string loadCommand(string tableName, string commandName)
		{
			XmlNode xNode = getXmlNode(tableName, commandName);
			
			string commandText = (xNode != null) ? xNode.InnerText.Trim(' ', '\r', '\n', '\t') : string.Empty;

			return commandText;
		}

		private static XmlNode getXmlNode(string tableName, string commandName)
		{
			if (xmlDocument == null)
				return null;

			return xmlDocument.DocumentElement.SelectSingleNode(string.Format("Table[@Name=\"{0}\"]/Command[@Name=\"{1}\"]", tableName, commandName));
		}
	}
}
