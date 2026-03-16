using System.Collections.Generic;

public static class DialogueTemplateResolver
{
    public static string Resolve(string template, Dictionary<string, string> variables)
    {
        if (string.IsNullOrEmpty(template)) return "";

        foreach (var pair in variables)
        {
            template = template.Replace($"{{{pair.Key}}}", pair.Value);
        }

        return template;
    }
}