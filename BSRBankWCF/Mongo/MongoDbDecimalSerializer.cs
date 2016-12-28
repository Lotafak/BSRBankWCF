using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace BSRBankWCF.Mongo
{
    [BsonSerializer(typeof(MongoDbDecimalSerializer))]
    public class MongoDbDecimalSerializer : SerializerBase<decimal>
    {
        const decimal DECIMAL_PLACE = 100M;

        public override decimal Deserialize(BsonDeserializationContext context, BsonDeserializationArgs e)
        {
            var dbData = context.Reader.ReadInt64();
            return Math.Round((decimal) dbData/DECIMAL_PLACE, 2);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs e, decimal value)
        {
            var realValue = (decimal) value;
            context.Writer.WriteInt64(Convert.ToInt64(realValue * DECIMAL_PLACE));
        }
    }
}
