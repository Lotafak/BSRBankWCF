using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace BSRBankWCF.Mongo
{
    /// <summary>
    /// Decimal number type custom serializer class
    /// </summary>
    [BsonSerializer(typeof(MongoDbDecimalSerializer))]
    public class MongoDbDecimalSerializer : SerializerBase<decimal>
    {
        const decimal DECIMAL_PLACE = 100M;

        /// <summary>
        /// Deserialize decimal number. Database store Int-like number f.e. 12000 in database is 120,00 in decimal representation
        /// </summary>
        /// <param name="context"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public override decimal Deserialize(BsonDeserializationContext context, BsonDeserializationArgs e)
        {
            var dbData = context.Reader.ReadInt64();
            return Math.Round(dbData/DECIMAL_PLACE, 2);
        }

        /// <summary>
        /// Serialize decimal number. Database store Int-like number f.e. 12000 in database is 120,00 in decimal representation
        /// </summary>
        /// <param name="context"></param>
        /// <param name="e"></param>
        /// <param name="value"></param>
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs e, decimal value)
        {
            var realValue = value;
            context.Writer.WriteInt64(Convert.ToInt64(realValue * DECIMAL_PLACE));
        }
    }
}
