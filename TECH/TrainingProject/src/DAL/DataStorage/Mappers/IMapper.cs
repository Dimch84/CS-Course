using System.Data.SqlClient;

namespace DataStorage.Mappers
{
    /// <summary>
    /// This interface used to read current row from sql data reader to DTO object
    /// </summary>
    /// <typeparam name="T">type of DTO object</typeparam>
    public interface IMapper<out T>
    {
        T ReadItem(SqlDataReader rd);
    }
}
