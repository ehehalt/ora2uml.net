using System;
using System.Collections.Generic;
using System.Linq;
using Ora2Uml.Objects;

namespace Ora2Uml.PlantUML
{
    public static class Template
    {
        #region Constants

        private const String templatePrefix = @"
@startuml sample

!define Table(name,desc) class name as " + "\"desc\"" + @" << (T,#FFAAAA) >>

!define primary_key(x) <b>x</b>
!define unique(x) <color:green>x</color>
!define not_null(x) <u>x</u>

hide methods
hide stereotypes

";

        private const String templatePostfix = @"

@enduml";

        #endregion Constants

        public static String GeneratePlantUML(IList<Table> tables)
        {
            var uml = "";

            foreach(Table table in tables)
            {
                var tbl = $"Table({table.TableName.ToLower()}, \"{table.TableName.ToLower()}\") {{\n";
                
                foreach(Column column in table.Columns.OrderBy(c => c.ColumnName).OrderBy(c => !c.PrimaryKey))
                {
                    tbl += $"{GetColumnText(column)}\n";
                }

                tbl += "}\n\n";
                uml += tbl;
            }

            uml = templatePrefix + uml + templatePostfix;

            return uml;
        }

        private static String GetColumnText(Column column)
        {
            var result = column.ColumnName.ToLower();

            if (!column.Nullable)
            {
                result = $"not_null({result})";
            }

            if (column.Unique)
            {
                result = $"unique({result})";
            }

            if (column.PrimaryKey)
            {
                result = $"primary_key({result})";
            }

            result += $" {column.DataType.ToUpper()}";
            

            return result;
        }
    }
}