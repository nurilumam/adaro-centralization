using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AdaroConnect.Application.AppConsole
{
    public class SAPMetaDataExtractor
    {
        public class SAPTableFieldInfo
        {
            public string Field { get; set; }
            public bool Key { get; set; }
            public string DataElement { get; set; }
            public string Domain { get; set; }
            public string DataType { get; set; }
            public int Length { get; set; }
            public int DecimalPlaces { get; set; }
            public string ShortDescription { get; set; }
            public string CheckTable { get; set; }
        }


        public void GetTableInfo(string tableName)
        {
            string TableName = string.Empty;
            string TableDescription = string.Empty;
            List<string> PropertyFieldHeader = new List<string>();
            string PropertyFieldName = string.Empty;


            var html = @$"https://www.sapdatasheet.org/abap/tabl/{tableName.ToLower()}.html";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var sapds = htmlDoc.DocumentNode.Descendants(0)
        .Where(n => n.HasClass("sapds-card-body")).FirstOrDefault();

            var colomItemTH = sapds.SelectNodes("//table")[4].SelectNodes("//thead/tr")[0].SelectNodes("th");
            foreach (var item1 in colomItemTH)
            {
                Console.Write($"|{item1.InnerText.Trim(),3}");
            }
            Console.Write("|");
            Console.WriteLine();


            List<SAPTableFieldInfo> Fields = new List<SAPTableFieldInfo>();
            var tablesapTr = sapds.SelectNodes("//table")[5].SelectNodes("tbody/tr");
            foreach (var item in tablesapTr)
            {
                var colomItem = item.SelectNodes("td").ToList();
                //foreach (var item1 in colomItem)
                //{
                //    Console.Write($"|{item1.InnerText.Trim(),3}");
                //}


                Fields.Add(new SAPTableFieldInfo()
                {
                    Field = (colomItem[1].InnerText ?? string.Empty).Trim(),
                    Key = (colomItem[2].InnerHtml ?? string.Empty).Contains("checked=\"checked\""),
                    DataElement = (colomItem[3].InnerText ?? string.Empty).Trim(),
                    Domain = (colomItem[4].InnerText ?? string.Empty).Trim(),
                    DataType = (colomItem[5].InnerText ?? string.Empty).Replace("&nbsp;", "").Trim(),
                    Length = int.Parse((colomItem[6].InnerText != null ? colomItem[6].InnerText.Replace("&nbsp;", "").Trim() : "0")),
                    DecimalPlaces = int.Parse((colomItem[7].InnerText != null ? colomItem[7].InnerText.Replace("&nbsp;", "").Trim() : "0")),
                    ShortDescription = (colomItem[8].InnerText ?? string.Empty).Replace("&nbsp;", "").Trim(),
                    CheckTable = (colomItem[9].InnerText ?? string.Empty).Replace("&nbsp;", "").Trim(),
                });

                Console.Write("|");
                Console.WriteLine();
            }


            if (Fields.Count > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var item in Fields)
                {
                    PropertyFieldHeader = new List<string>();
                    PropertyFieldName = string.Empty;

                    string DataType = item.DataType;
                    string PropertyType = "string";

                    switch (item.DataType)
                    {
                    case "CLNT":
                        DataType = "CHAR";
                        break;
                    case "NUMC":
                        DataType = "NUMERIC";
                        PropertyType = "decimal";
                        if(item.Length >= 8)
                            PropertyType = "double";
                        break;
                    case "DEC":
                        DataType = "DECIMAL_COMMAND_SIGN";
                        PropertyType = "decimal";
                        break;
                    case "CURR":
                        DataType = "DECIMAL";
                        PropertyType = "decimal";
                        break;
                    case "INT1":
                        DataType = "INTEGER";
                        PropertyType = "int";
                        break;
                    case "QUAN":
                        DataType = "QUAN_DOUBLE";
                        PropertyType = "double";
                        break;
                    case "DATS":
                    case "DATE_8":
                        DataType = "DATE_8";
                        PropertyType = "DateTime";
                        break;
                    case "TIMS":
                        DataType = "TIME";
                        break;
                    }

                    string LengthStr = string.Empty;
                    if (item.Length > 0)
                        LengthStr = $", Length = {item.Length}";



                    string PropertyField = $"[RfcEntityProperty(\"{item.Field}\", Description = \"{item.ShortDescription}\", SapDataType = RfcDataTypes.{DataType} {LengthStr})]";
                    string PropertyName = $"public {PropertyType} {item.Field} {{ get; set; }}";

                    stringBuilder.AppendLine(PropertyField);
                    stringBuilder.AppendLine(PropertyName);
                    stringBuilder.AppendLine("");




                }

                Console.Write(stringBuilder.ToString());
            }


            Console.Write($"Total Coloum {Fields.Count}");

            //Console.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);
        }
    }
}
