az pipelines create --name Hub.HostedServices.Commands.Logging.Core --folder-path Hub.Shared/Hub.HostedServices/ --repository rhe89/hub-shared --repository-type github --yml-path azure-pipelines.yml --skip-first-run true --organization https://rhe89.visualstudio.com --project Hub --service-connection becefc69-726e-404c-b89e-861db500663f
az pipelines variable create --name projectName --value Hub.HostedServices.Commands.Logging.Core --pipeline-name Hub.HostedServices.Commands.Logging.Core --organization https://rhe89.visualstudio.com --project Hub
