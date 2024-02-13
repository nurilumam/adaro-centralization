using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdaroConnect.Application.Core.Models;

namespace AdaroConnect.Application.AppConsole
{
    public class TableGenerator
    {
        public static void Exec()
        {
            List<TableClass> tables = new List<TableClass>();

            //// Pass assembly name via argument
            Type type = typeof(PurchasingDocumentHeader);

            TableClass tc = new TableClass(type);
            tables.Add(tc);

            // Create SQL for each table
            foreach (TableClass table in tables)
            {
                Console.WriteLine(table.CreateTableScript());
                Console.WriteLine();
            }

            // Total Hacked way to find FK relationships! Too lazy to fix right now
            foreach (TableClass table in tables)
            {
                foreach (KeyValuePair<string, Type> field in table.Fields)
                {
                    foreach (TableClass t2 in tables)
                    {
                        if (field.Value.Name == t2.ClassName)
                        {
                            // We have a FK Relationship!
                            Console.WriteLine("GO");
                            Console.WriteLine("ALTER TABLE " + table.ClassName + " WITH NOCHECK");
                            Console.WriteLine("ADD CONSTRAINT FK_" + field.Key + " FOREIGN KEY (" + field.Key + ") REFERENCES " + t2.ClassName + "(ID)");
                            Console.WriteLine("GO");

                        }
                    }
                }
            }
        }
    }

    public class TableClass
    {
        private static Dictionary<Type, string> DataMapper
        {
            get
            {
                // Add the rest of your CLR Types to SQL Types mapping here
                Dictionary<Type, string> dataMapper = new Dictionary<Type, string>();
                dataMapper.Add(typeof(int), "BIGINT");
                dataMapper.Add(typeof(string), "NVARCHAR(500)");
                dataMapper.Add(typeof(bool), "BIT");
                dataMapper.Add(typeof(DateTime), "DATETIME");
                dataMapper.Add(typeof(float), "FLOAT");
                dataMapper.Add(typeof(decimal), "DECIMAL(18,0)");
                dataMapper.Add(typeof(Guid), "UNIQUEIDENTIFIER");

                return dataMapper;
            }
        }

        public List<KeyValuePair<string, Type>> Fields { get; set; } = new List<KeyValuePair<string, Type>>();

        public string ClassName { get; set; } = string.Empty;

        public TableClass(Type t)
        {
            ClassName = t.Name;

            foreach (PropertyInfo p in t.GetProperties())
            {
                KeyValuePair<string, Type> field = new KeyValuePair<string, Type>(p.Name, p.PropertyType);

                Fields.Add(field);
            }
        }

        public string CreateTableScript()
        {
            StringBuilder script = new StringBuilder();

            script.AppendLine("CREATE TABLE " + ClassName);
            script.AppendLine("(");
            script.AppendLine("\t ID BIGINT,");
            for (int i = 0; i < Fields.Count; i++)
            {
                KeyValuePair<string, Type> field = Fields[i];

                if (DataMapper.ContainsKey(field.Value))
                {
                    script.Append("\t " + field.Key + " " + DataMapper[field.Value]);
                }
                else
                {
                    // Complex Type? 
                    script.Append("\t " + field.Key + " BIGINT");
                }

                if (i != Fields.Count - 1)
                {
                    script.Append(",");
                }

                script.Append(Environment.NewLine);
            }

            script.AppendLine(")");

            return script.ToString();
        }

    }
}
