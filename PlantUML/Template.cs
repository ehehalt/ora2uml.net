using System;
using System.Collections.Generic;
using Ora2Uml.Objects;

namespace Ora2Uml.PlantUML
{
    public static class Template
    {
        #region Constants

        private const String templatePrefix = @"
@startuml
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
                var tbl = $"Table({table.TableName}, \"{table.TableName}\n(Description)\") {{\n\n";

                uml += tbl;
            }

            uml = templatePrefix + uml + templatePostfix;

            return uml;
        }
    }
}