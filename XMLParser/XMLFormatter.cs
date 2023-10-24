using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Assignment2
{
    public class XMLFormatter
    {
        public static StringBuilder XMLConverter(object o, int indent, bool firstCall=false)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder indentString = new StringBuilder(new string(' ', indent));
            if (o == null)
            {
                return null;
            }
            if (o is DateTime)
            {
                DateTime datetime = (DateTime)o;
                sb.Append($"{indentString}" + datetime.ToString());
            }
            else if (o is Array || o is IList)
            {
                string type = o.GetType().ToString();
                string typeName = type.Substring(type.LastIndexOf('.') + 1).Trim(new char[] { '[', ']' });

                sb.AppendLine($"{indentString}<ArrayOf{typeName}>");
                foreach (var item in o as IList)
                {
                    sb.AppendLine($"{indentString}<{typeName}>");
                    sb.AppendLine(XMLConverter(item, indent + 2).ToString());
                    sb.AppendLine($"{indentString}</{typeName}>");
                }
                sb.Append($"{indentString}</ArrayOf{typeName}>");
            } 
            else if (o is IDictionary)
            {
                
                sb.AppendLine($"{indentString}<Dictionary>");
                var dictionary= o as IDictionary;

                if(dictionary != null )
                {
                    foreach ( var key in dictionary.Keys )
                    {
                        var numberkey="";
                        if (IsNumber(key))
                        {
                            numberkey = "key" + key.ToString();
                        }
                        else
                        {
                            numberkey = key.ToString();
                        }
                        sb.AppendLine($"{indentString}<{numberkey}>");
                        sb.AppendLine(XMLConverter(dictionary[key], indent+2).ToString());
                        sb.AppendLine($"{indentString}</{numberkey}>");

                    }
                }
                sb.AppendLine($"{indentString}</Dictionary>");

            } 
            else if (IsPreemtive(o))
            {
                sb.Append($"{indentString}{o}");
            }
            else
            {
                var name = o.GetType().Name;
                var properties = o.GetType().GetProperties();
                if (firstCall)
                {

                    //sb.AppendLine($"<?xml version=\"1.0\" encoding=\"Codepage-437\"?>");
                    //sb.AppendLine($"{indentString}<{name} xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
                    sb.AppendLine($"{indentString}<{name}>");
                }
               
                foreach (var property in properties)
                {
                    var propValue = property.GetValue(o, null);
                    var list = propValue as IList;
                    var dict = propValue as IDictionary;   
                    if (list != null)
                    {
                        sb.AppendLine($"  {indentString}<{property.Name}>");
                        foreach (var item in list)
                        {
                            sb.AppendLine($"    {indentString}<{item.GetType().Name}>");
                            sb.AppendLine(XMLConverter(item, indent + 4).ToString());
                            sb.AppendLine($"    {indentString}</{item.GetType().Name}>");
                        }
                        sb.AppendLine($"  {indentString}</{property.Name}>");
                    }else if(dict is IDictionary)
                    {

                        sb.AppendLine($"  {indentString}<{property.Name}>");
                        if (dict != null)
                        {
                            foreach (var key in dict.Keys)
                            {
                                var numberkey = "";
                                if (IsNumber(key))
                                {
                                    numberkey = "key" + key.ToString();
                                }
                                else
                                {
                                    numberkey = key.ToString();
                                }
                                sb.AppendLine($"    {indentString}<{numberkey}>");
                                sb.AppendLine(XMLConverter(dict[key], indent + 6).ToString());
                                sb.AppendLine($"    {indentString}</{numberkey}>");

                            }
                        }
                        sb.AppendLine($"  {indentString}</{property.Name}>");
                    }

                    else
                    {
                        sb.AppendLine($"  {indentString}<{property.Name}>");
                        if (propValue != null)
                        {
                            sb.AppendLine(XMLConverter(propValue, indent + 4).ToString());
                        }

                        sb.AppendLine($"  {indentString}</{property.Name}>");
                    }

                }
                if (firstCall)
                {
                    sb.Append($"{indentString}</{name}>");
                }
                
            }

            return sb;
        }
        public static bool IsPreemtive(object o)
        {
            return IsNumber(o)
                || o is string
                || o is StringBuilder
                || o is char;
        }
        public static bool IsNumber(object o)
        {
            return o is sbyte
                || o is byte
                || o is short
                || o is ushort
                || o is int
                || o is uint
                || o is long
                || o is ulong
                || o is float
                || o is double
                || o is decimal
                || o is Single;

        }
        public static string Convert(object o)
        {
            //XmlSerializer xs = new XmlSerializer(o.GetType());
            //xs.Serialize(Console.Out, o);
            StringBuilder sb = new StringBuilder();
            if (o == null) return null;
            if (IsPreemtive(o))
            {
                sb.Append($"<{o.GetType().Name}>");
                sb.Append($"{o}");
                sb.Append($"</{o.GetType().Name}>");
                return sb.ToString();
            }else if(o is DateTime)
            {
                sb.Append($"<{o.GetType().Name}>");
                sb.Append((DateTime)o);
                sb.Append($"</{o.GetType().Name}>");
                return sb.ToString();
            }

            var xml= XMLConverter(o, 0, true);
            
            return xml.ToString();
        }
    }
}
