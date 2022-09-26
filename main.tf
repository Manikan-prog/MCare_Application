  terraform{
  // required_version =  ">= 0.11"
  required_version = ">= 1.1.0"
  backend "azurerm"{
    storage_account_name = "__terraformstorageaccount__"
    container_name       = "terraform" 
    key                  = "terraform.tfstate"
    access_key           = "__storagekey__"
    features{}
  }
} 


provider "azurerm"{
version = "3.4.0"
 features{}
}

resource "azurerm_resource_group" "example" {
  name     = var.resource_group_name    
  location = var.location
}

resource "azurerm_sql_server" "example" {
  name                         = var.sql_server_name  
  resource_group_name          = azurerm_resource_group.example.name
  location                     = azurerm_resource_group.example.location
  version                      = "12.0"
  administrator_login          = var.server_login     
  administrator_login_password = var.server_password
  

  tags = {
    environment = "production"
  }
}

resource "azurerm_storage_account" "example" {
  name                     = var.sql_storageaccount
  resource_group_name      = azurerm_resource_group.example.name
  location                 = azurerm_resource_group.example.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_sql_firewall_rule" "example" {
  name                = "FirewallRule1"
  resource_group_name = azurerm_resource_group.example.name
  server_name         = azurerm_sql_server.example.name
  start_ip_address    = "0.0.0.0"
  end_ip_address      = "0.0.0.0"
}

resource "azurerm_sql_database" "example" {
  name                         = var.sqldatabase-name  
  resource_group_name          = azurerm_resource_group.example.name
  location                     = azurerm_resource_group.example.location
  server_name                  = azurerm_sql_server.example.name
  
  tags = {
    environment = "production"
  }
}
