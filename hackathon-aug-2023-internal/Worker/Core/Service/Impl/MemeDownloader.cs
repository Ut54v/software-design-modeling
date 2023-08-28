using Worker.Core.Enums;
using Worker.Core.Interface;

namespace Worker.Core.Impl
{
    public class MemeDownloader : Executioner
    {
        public ResponseStatus Execute()
        {
            var client = new HttpClient();
            //var response = await client.GetAsync(Get);
            //will go the execution logic
            return ResponseStatus.Success;
        }
    }
}
