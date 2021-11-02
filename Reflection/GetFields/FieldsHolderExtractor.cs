namespace GetFields
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class FieldsHolderExtractor
    {
        private readonly StringBuilder sb;

        public FieldsHolderExtractor()
        {
            this.sb = new StringBuilder();
        }

        internal string Run()
        {
            var fields = typeof(FieldsHolder).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            var accModFilters = new Dictionary<string, Func<IEnumerable<FieldInfo>>>()
            {
                { "private", () => fields.Where(f => f.IsPrivate) },
                { "protected", () => fields.Where(f => f.IsFamily) },
                { "public", () => fields.Where(f => f.IsPublic) }
            };

            foreach(var kvp in accModFilters)
            {
                this.sb.AppendLine(kvp.Key.ToUpper());
                this.AppendFields(kvp.Value());
                this.sb.AppendLine();
            }

            return this.sb.ToString().Trim();
        }

        private void AppendFields(IEnumerable<FieldInfo> fieldsCollection)
        {
            foreach (var field in fieldsCollection)
            {
                var accessmodifier = field.Attributes.ToString().ToLower();
                // protected = family
                if (accessmodifier.Equals("family"))
                {
                    accessmodifier = "protected";
                }

                this.sb.AppendLine($"{accessmodifier} {field.FieldType.Name} {field.Name}");
            }
        }
    }
}
