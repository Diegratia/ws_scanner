using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ws_scanner.Application.Dtos
{
    public record WsRequest(
     [property: JsonPropertyName("cmd")]
    string Cmd,

     [property: JsonPropertyName("doc_type")]
    string DocType,

     [property: JsonPropertyName("action_type")]
    string ActionType,

     [property: JsonPropertyName("action_source")]
    string ActionSource,

    [property: JsonPropertyName("images")]
    List<string>? Images
 );
}
