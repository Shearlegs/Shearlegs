namespace Shearlegs.Web.API.Utilities.StoredProcedures
{
    public class StoredProcedureResult
    {
        public int ReturnValue { get; set; }
    }

    public class StoredProcedureResult<T> : StoredProcedureResult
    {
        public T Result { get; set; }
        public bool HasResult => Result != null;
    }
}
