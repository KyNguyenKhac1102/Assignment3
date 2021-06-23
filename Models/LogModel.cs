using System;

namespace ASP_CORE2.Models
{
    public class LogModel
    {
        public string Schema {get; set;}
        public string Host {get; set;}
        public string Path {get; set;}
        public string QueryString {get; set;}
        public string RequestBody {get; set;}

        public LogModel(string schema, string host, string path, string query, string body){
            Schema = schema;
            Host = host;
            Path = path;
            QueryString = query;
            RequestBody = body;
        }
    }
}
