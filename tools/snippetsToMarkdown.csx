#pragma warning disable RCS1018, RCS1110

#r "nuget: DotMarkdown, 0.1.0"

using System.Text.Json;
using DotMarkdown;

class Snippet
{
    public string Prefix { get; set; }
    public IEnumerable<string> Body { get; set; }
}

var snippets = JsonSerializer.Deserialize<Dictionary<string, Snippet>>(
    File.ReadAllText("../snippets/html.json"),
    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

var stringBuilder = new StringBuilder();

using (var markdownWriter = MarkdownWriter.Create(stringBuilder))
{
    markdownWriter.WriteStartTable(2);

    markdownWriter.WriteStartTableRow();
    markdownWriter.WriteTableCell("Prefix");
    markdownWriter.WriteTableCell("Body");
    markdownWriter.WriteEndTableRow();

    markdownWriter.WriteTableHeaderSeparator();

    foreach (var snippet in snippets)
    {
        markdownWriter.WriteStartTableRow();

        markdownWriter.WriteTableCell(snippet.Value.Prefix);

        var humanizedBody = string.Join(" ", snippet.Value.Body.Select(l => l.Trim()))
            .Replace(@"\$", "$")
            .Replace(@"\}", "}");

        markdownWriter.WriteStartTableCell();
        markdownWriter.WriteInlineCode(humanizedBody);
        markdownWriter.WriteEndTableCell();

        markdownWriter.WriteEndTableRow();
    }

    markdownWriter.WriteEndTable();
}

File.WriteAllText("snippets.md", stringBuilder.ToString().Trim());