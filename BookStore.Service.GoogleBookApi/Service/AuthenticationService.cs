using Google.Apis.Auth.OAuth2;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Service.GoogleBookApi
{
    public class AuthenticationService
    {
        public async Task<BooksService> Authenticate()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { BooksService.Scope.Books },
                    "user", CancellationToken.None, new FileDataStore("Books.ListMyLibrary"));
            }

            if (credential.Token == null || credential.Token.IsExpired(SystemClock.Default))
            {
                await GoogleWebAuthorizationBroker.ReauthorizeAsync(credential, CancellationToken.None);
            }

            //await credential.RevokeTokenAsync(CancellationToken.None);

            var service = new BooksService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Books API",
            });

            return service;
        }
    }
}