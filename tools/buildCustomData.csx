#pragma warning disable RCS1018, RCS1110

using System.Text.Json;

#region Models
class Part
{
    public string Name { get; set; }
    public string Description { get; set; }
}

class CustomData
{
    public string Version { get; set; } = "1.1";
    public IEnumerable<Attribute> GlobalAttributes { get; set; }
}

class Attribute
{
    public Attribute(string name, string description)
    {
        Name = name;
        Description = new AttributeDescription { Value = description };
    }

    public string Name { get; set; }
    public AttributeDescription Description { get; set; }

    public class AttributeDescription
    {
        public string Kind { get; set; } = "markdown";
        public string Value { get; set; }
    }
}
#endregion

var jsonSerializerOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
};

var directives = JsonSerializer.Deserialize<IEnumerable<Part>>(
    File.ReadAllText("directives.json"),
    jsonSerializerOptions);

var onDirective = directives.Single(d => d.Name == "x-on:");
var bindDirective = directives.Single(d => d.Name == "x-bind:");

var events = JsonSerializer.Deserialize<IEnumerable<Part>>(
    File.ReadAllText("events.json"),
    jsonSerializerOptions);

var commonBindableAttributes = JsonSerializer.Deserialize<IEnumerable<Part>>(
    File.ReadAllText("commonBindableAttributes.json"),
    jsonSerializerOptions);

var separator = "\n\n---\n\n";

var attributes =
        directives.Select(d => new Attribute(
            name: d.Name,
            description: d.Description))
    .Concat(
        events.Select(e => new Attribute(
            name: onDirective.Name + e.Name,
            description: string.Join(separator, e.Description, onDirective.Description)))
    .Concat(
        commonBindableAttributes.Select(a => new Attribute(
            name: bindDirective.Name + a.Name,
            description: string.Join(separator, a.Description, bindDirective.Description)))));

var customData = new CustomData { GlobalAttributes = attributes };

File.WriteAllText("../customData/html.json", JsonSerializer.Serialize(customData, jsonSerializerOptions));