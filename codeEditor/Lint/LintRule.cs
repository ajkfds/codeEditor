using ajkControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Lint
{
    public class LintRule : ILintRuleItem
    {
        public LintRule(string name, RuleType ruleType, string description)
        {
            this.Name = name;
            this.Type = ruleType;
            this.Description = description;
        }
        public enum RuleType
        {
            Error,
            Warning,
            Notice,
            Hint
        }
        public string Name { get; protected set; }
        public readonly string Description;
        public RuleType Type;

        public void ReadJson(JsonReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteJson(JsonWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
