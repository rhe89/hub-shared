az pipelines create --name Hub.Extensions --folder-path Hub.Shared// --repository rhe89/hub-shared --branch main --repository-type github --yml-path azure-pipelines.yml --skip-first-run true --organization https://rhe89.visualstudio.com --project Hub --service-connection becefc69-726e-404c-b89e-861db500663f
az pipelines variable create --name projectName --value Hub.Extensions --pipeline-name Hub.Extensions --organization https://rhe89.visualstudio.com --project Hub
