az pipelines create --name Hub.Storage.Repository.Entities --folder-path Hub.Shared/Hub.Storage/ --repository rhe89/hub-shared --branch main --repository-type github --yml-path azure-pipelines.yml --skip-first-run true --organization https://rhe89.visualstudio.com --project Hub --service-connection becefc69-726e-404c-b89e-861db500663f
az pipelines variable create --name projectName --value Hub.Storage.Repository.Entities --pipeline-name Hub.Storage.Repository.Entities --organization https://rhe89.visualstudio.com --project Hub
