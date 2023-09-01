using System.Text.Json;
using TaskAllocatorCommons.Models;
using Worker.Core.Enums;
using Worker.Core.Interface;
using Worker.Core.Models;

namespace Worker.Core.Impl
{
    public class MemeDownloader : Executioner<MemeTaskModel>
    {
        private WorkerInfo _workerInfo;
        public MemeDownloader(WorkerInfo workerInfo)
        {
            _workerInfo = workerInfo;
        }
        public async Task<ResponseStatus> Execute(MemeTaskModel memeTask)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(memeTask.MemeSourceUrl);
            var data = response.Content.ReadAsStringAsync().Result;
            var memeMetaData = JsonSerializer.Deserialize<MemeModel>(data);
            if (memeMetaData?.nsfw != null && !memeMetaData.nsfw )
            {
                var imageBytes = await client.GetByteArrayAsync(memeMetaData.url);
                await File.WriteAllBytesAsync(Path.Combine(_workerInfo.WorkDir, $"{memeMetaData.title.Trim()}.jpg"), imageBytes);
                return ResponseStatus.Success;
            }
            return ResponseStatus.Failed;
        }
    }
}
