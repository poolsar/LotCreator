using System.Collections.Generic;
using PrestaSharp.Entities;
using auxlanguage = PrestaSharp.Entities.AuxEntities.language;


namespace WebStoreLib
{
    public static class LanguageUtils
    {
        public static auxlanguage CreateAux(this language language, string value)
        {
            return new auxlanguage(language.id.Value, value);
        }

        public static void Write(this language language,List<auxlanguage> prop, string value)
        {
            value = value ?? string.Empty;

            if (prop.Count == 0)
            {
                var aux = language.CreateAux(value);
                prop.Add(aux);
            }
            else
            {
                prop[0].Value = value;
            }

        }

        
    }
}