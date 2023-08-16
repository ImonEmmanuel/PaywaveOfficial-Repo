using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Paywave.Config;

namespace Paywave
{
    public static class FirebaseService
    {
        public static IServiceCollection AddFirebaseService(this IServiceCollection service, IConfiguration config)
        {
            //Firebase Notification
            service.Configure<FirebaseConfig>(config.GetSection(nameof(FirebaseConfig)));
            string firebaseConfig  = JsonConvert.SerializeObject(config.GetSection("FirebaseConfig").Get<FirebaseConfig>());
            FirebaseApp.Create(new AppOptions()
            {

                Credential = GoogleCredential.FromJson(firebaseConfig),
            });
            return service;
        }
    }
}
