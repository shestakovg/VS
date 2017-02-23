using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace UnicomWeb.ModelForJS
{
    [Serializable()]
    public class JsTreeNode
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set;}
        [JsonProperty(PropertyName = "tags")]
        public string[] Tags { get; set; }
        [JsonProperty(PropertyName = "nodes")]
        public IEnumerable<JsTreeNode> Nodes { get; set; }

        [JsonProperty(PropertyName = "silent")]
        public bool Silent { get; set; }

    }
}