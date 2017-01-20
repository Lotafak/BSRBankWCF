using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BSRBankWCF.Converters
{
    class DecimalConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handling deserializing decimal variables
        /// </summary>
        /// <param name="reader">Reader is source of token</param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if ( token.Type == JTokenType.Float || token.Type == JTokenType.Integer )
            {
                return token.ToObject<decimal>();
            }
            if ( token.Type == JTokenType.String )
            {
                var s = token.ToString();
                var dot = token.ToString().Split(',').Length;
                if(dot > 1)
                    throw new JsonSerializationException("Wrong number format: " + token);
                if (dot == 1)
                    s = token.ToString().Replace(".", ",");
                // customize this to suit your needs

                return Decimal.Parse(s,
                       System.Globalization.CultureInfo.GetCultureInfo("pl-PL"));
            }
            if ( token.Type == JTokenType.Null && objectType == typeof(decimal?) )
            {
                return null;
            }
            throw new JsonSerializationException("Unexpected token type: " +
                                                  token.Type.ToString());
        }

        /// <summary>
        /// Cheks if variable is of type decimal
        /// </summary>
        /// <param name="objectType">Type of deserializing variable</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal) || objectType == typeof(decimal?);
        }
    }
}
