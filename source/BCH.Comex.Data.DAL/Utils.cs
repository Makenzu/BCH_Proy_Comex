using System;
using System.Data.Common;

namespace BCH.Comex.Data.DAL
{
    public class Utils
    {
        public static String GetStringFromDataReader(DbDataReader reader, int ordinal)
        {
            return (reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal).Trim());
        }

        public static double? GetDoubleFromDataReader(DbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? (double?)null : reader.GetDouble(ordinal);
        }

        public static int? GetIntFromDataReader(DbDataReader reader, int ordinal)
        {
            //(moo 2014-08-24) con GetDecimal se genera una execpción, lo cual se corrigue cambiando a GetInt32
            //(moo 2014-08-24) OldVersion --> return reader.IsDBNull(ordinal) ? (int?)null : (int)reader.GetDecimal(ordinal);
            return reader.IsDBNull(ordinal) ? (int?)null : reader.GetInt32(ordinal);
            
        }

        public static int? GetIntByteFromDataReader(DbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? (int?)null : (int)reader.GetByte(ordinal);
        }

        public static decimal? GetDecimalFromDataReader(DbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? (decimal?)null : reader.GetDecimal(ordinal);
        }

        public static DateTime? GetFechaFromDataReader(DbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? (DateTime?)null : reader.GetDateTime(ordinal);
        }
        public static bool? GetBooleanFromDataReader(DbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? (bool?)null : reader.GetBoolean(ordinal);
        }

    }
}
