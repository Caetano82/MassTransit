param location string = 'eastus'
param appName string = 'mass-transit-app'
param imageName string = 'mass-transit-api:latest'

resource rg 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: '${appName}-rg'
  location: location
}

resource acr 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' = {
  name: '${appName}acr'
  location: location
  sku: {
    name: 'Basic'
  }
  properties: {
    adminUserEnabled: true
  }
}

resource appPlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: '${appName}-plan'
  location: location
  sku: {
    name: 'B1'
    tier: 'Basic'
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

resource webApp 'Microsoft.Web/sites@2022-03-01' = {
  name: appName
  location: location
  kind: 'app,linux,container'
  properties: {
    serverFarmId: appPlan.id
    siteConfig: {
      appSettings: [
        {
          name: 'WEBSITES_ENABLE_APP_SERVICE_STORAGE'
          value: 'false'
        }
        {
          name: 'DOCKER_REGISTRY_SERVER_URL'
          value: 'https://${acr.name}.azurecr.io'
        }
        {
          name: 'DOCKER_REGISTRY_SERVER_USERNAME'
          value: acr.listCredentials().username
        }
        {
          name: 'DOCKER_REGISTRY_SERVER_PASSWORD'
          value: acr.listCredentials().passwords[0].value
        }
        {
          name: 'WEBSITES_PORT'
          value: '80'
        }
      ]
      linuxFxVersion: 'DOCKER|${acr.name}.azurecr.io/${imageName}'
    }
  }
  dependsOn: [
    acr
  ]
}
