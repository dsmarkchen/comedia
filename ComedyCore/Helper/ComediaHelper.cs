using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Helper
{
    public class ComediaHelper
    {
        public static string TrimUnecessaryCharacters(string input)
        {
            return input.Trim(new char[] { ',', ' ', '.', '"', '-', ';' });
        }
        public static string CantoReformat(string input, int lineNumber)
        {
            StringBuilder sb = new StringBuilder();
            if (lineNumber % 3 != 1)
            {
                sb.Append("    ");
            }
            sb.AppendLine(input.First().ToString().ToUpper() + String.Join("", input.Skip(1)));
            if (lineNumber % 3 == 0)
            {
                sb.AppendLine("");
            }
            return sb.ToString();
        }
    }

    public static class xxxhelper<T>
    {
        public static DataTable ToDataTable(List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }

    public class DisplayText : Attribute
    {

        public DisplayText(string Text)
        {
            this.text = Text;
        }


        private string text;


        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
    public static class EnumHelper
    {
        public static string ToDescription(this Enum en)
        {

            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {

                object[] attrs = memInfo[0].GetCustomAttributes(
                                              typeof(DisplayText),

                                              false);

                if (attrs != null && attrs.Length > 0)

                    return ((DisplayText)attrs[0]).Text;

            }

            return en.ToString();

        }
    }

}
