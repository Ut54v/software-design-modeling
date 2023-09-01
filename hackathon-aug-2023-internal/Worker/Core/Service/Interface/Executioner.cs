using Worker.Core.Enums;

namespace Worker.Core.Interface
{
    public interface Executioner<T>
    {
         Task<ResponseStatus> Execute(T arg);
    }
}
