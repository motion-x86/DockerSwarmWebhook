using System;
using WebHooks.Models;

namespace WebHooks.Jobs
{
    public class SwarmServiceUpdateJob:IJob
    {
        private const string SERVICE_UPDATE_SUCCESS = "Service converged";
        public string LoginCommand { get; set; }
        public string UpdateCommand { get; set; }
        public string ServiceName { get; set; }

        public SwarmServiceUpdateJob(ACRConfig config, ACRPayload payload)
        {
            var image = payload.GetImageName();
            ServiceName = config.Services[image];

            LoginCommand = $"docker login -u \"{config.Username}\" -p \"{config.Password}\" {config.Registry}";
            UpdateCommand = $"docker service update --force {ServiceName} --with-registry-auth --image={image}";
        }

        public void Run()
        {
            try
            {
                var loginResult = LoginCommand.ExecuteCommand();
                Console.WriteLine(loginResult);
                var updateResult = UpdateCommand.ExecuteCommand();
                if (updateResult.Contains(SERVICE_UPDATE_SUCCESS))
                {
                    Console.WriteLine($"Service [{ServiceName}] update: SUCCESS!");
                }
                else
                {
                    Console.WriteLine($"Service [{ServiceName}] update: FAILED!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
