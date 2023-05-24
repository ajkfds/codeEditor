using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeEditor.Lint
{
    public interface ILintRuleItem
    {
        string Name { get; }
        void ReadJson(ajkControls.Json.JsonReader reader);
        void WriteJson(ajkControls.Json.JsonWriter writer);

    }
}
