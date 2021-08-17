using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Core.JsonModel
{
    [Serializable]
    public class JsonReturnModels
    {
        public bool Ok { get; set; }
        public TypeJsonReturn Type { get; set; }
        public string Message { get; set; }
        public List<string> ListMessage { get; set; }
        public string Title { get; set; }
        public int Time { get; set; }
        public int Height { get; set; }
    }

    [Serializable]
    public enum TypeJsonReturn
    {
        Success = 1,
        Error = 2
    }
}
