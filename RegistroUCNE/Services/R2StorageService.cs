using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;

namespace RegistroUCNE.Services;

public class R2StorageService
{
    private readonly IAmazonS3 _s3;
    private readonly string _bucket;

    public R2StorageService(IConfiguration config)
    {
        var accessKey = config["R2:AccessKey"];
        var secretKey = config["R2:SecretKey"];
        var accountId = config["R2:AccountId"];
        _bucket = config["R2:BucketName"];

        var credentials = new BasicAWSCredentials(accessKey, secretKey);

        var s3Config = new AmazonS3Config
        {
            ServiceURL = $"https://{accountId}.r2.cloudflarestorage.com",
            ForcePathStyle = true
        };

        _s3 = new AmazonS3Client(credentials, s3Config);
    }

    public async Task<string?> UploadAsync(byte[] bytes, string key, string contentType)
    {
        try
        {
            using var stream = new MemoryStream(bytes);

            var request = new PutObjectRequest
            {
                BucketName = _bucket,
                Key = key,
                InputStream = stream,
                ContentType = contentType,
                DisablePayloadSigning = true
            };

            await _s3.PutObjectAsync(request);
            return key;
        }
        catch (AmazonS3Exception ex)
        {
            Console.WriteLine("=== R2 / S3 ERROR ===");
            Console.WriteLine($"Message: {ex.Message}");
            Console.WriteLine($"ErrorCode: {ex.ErrorCode}");
            Console.WriteLine($"StatusCode: {ex.StatusCode}");
            Console.WriteLine($"RequestId: {ex.RequestId}");
            Console.WriteLine("====================");

            throw;
        }
    }
}
