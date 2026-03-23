terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~>4.60.0"
    }
  }

  backend "azurerm" {}
}

provider "azurerm" {
  subscription_id = var.subscription_id
  features {}
}

data "azurerm_client_config" "current" {}

resource "azurerm_resource_group" "rg" {
  name     = "yaecommerce-rg-${var.environment_name}"
  location = "West Europe"
}

resource "azurerm_service_plan" "asp" {
  name                = "yaecommerce-asp-${var.environment_name}"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  os_type             = "Linux"
  sku_name            = var.service_plan_sku
}

resource "azurerm_linux_web_app" "web" {
  name                = "yaecommerce-web-${var.environment_name}"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_service_plan.asp.location
  service_plan_id     = azurerm_service_plan.asp.id

  identity {
    type = "SystemAssigned"
  }

  site_config {
    always_on = false
    application_stack {
      dotnet_version = "10.0"
    }
  }
}

resource "azurerm_static_web_app" "swa" {
  name                = "yeaecommerce-swa${var.environment_name}"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  sku_tier            = "Free"
}

resource "azurerm_key_vault" "kv" {
  name                = "yaecommerce-kv-${var.environment_name}"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  tenant_id           = data.azurerm_client_config.current.tenant_id
  sku_name            = "standard"
}

resource "azurerm_key_vault_access_policy" "webapp_kv_policy" {
  key_vault_id = azurerm_key_vault.kv.id
  tenant_id    = data.azurerm_client_config.current.tenant_id
  object_id    = azurerm_linux_web_app.web.identity[0].principal_id

  secret_permissions = [
    "Get", "List"
  ]
}

resource "azurerm_postgresql_flexible_server" "postgres" {
  name                = "yeacommerce-sql-${var.environment_name}"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  sku_name            = "B_Standard_B1ms"
  version             = 18
  zone                = 1

  administrator_login    = var.sql_administrator_login
  administrator_password = var.sql_administrator_password
}

resource "azurerm_servicebus_namespace" "servicebus" {
  name                = "yeacommerce-sb-${var.environment_name}"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  sku                 = "Standard"
}

resource "azurerm_servicebus_topic" "identity_topic" {
  name         = "identity.events"
  namespace_id = azurerm_servicebus_namespace.servicebus.id
}

resource "azurerm_servicebus_topic" "users_topic" {
  name         = "users.events"
  namespace_id = azurerm_servicebus_namespace.servicebus.id
}

resource "azurerm_servicebus_topic" "products_topic" {
  name         = "products.events"
  namespace_id = azurerm_servicebus_namespace.servicebus.id
}

resource "azurerm_servicebus_topic" "orders_topic" {
  name         = "orders.events"
  namespace_id = azurerm_servicebus_namespace.servicebus.id
}

resource "azurerm_servicebus_topic" "carts_topic" {
  name         = "carts.events"
  namespace_id = azurerm_servicebus_namespace.servicebus.id
}

resource "azurerm_servicebus_subscription" "orders_carts_subscription" {
  name               = "orders"
  topic_id           = azurerm_servicebus_topic.carts_topic.id
  max_delivery_count = 5
}

resource "azurerm_servicebus_subscription_rule" "orders_carts_subscription_rule" {
  name            = "event-type-filter"
  subscription_id = azurerm_servicebus_subscription.orders_carts_subscription.id
  filter_type     = "SqlFilter"
  sql_filter      = "eventType IN ('order.placed')"
}

resource "azurerm_servicebus_subscription" "products_orders_subscription" {
  name               = "products"
  topic_id           = azurerm_servicebus_topic.orders_topic.id
  max_delivery_count = 5
}

resource "azurerm_servicebus_subscription_rule" "products_orders_subscription_rule" {
  name            = "event-type-filter"
  subscription_id = azurerm_servicebus_subscription.products_orders_subscription.id
  filter_type     = "SqlFilter"
  sql_filter      = "eventType IN ('order.canceled', 'order.revoked', 'order.created')"
}

resource "azurerm_servicebus_subscription" "orders_products_subscription" {
  name               = "orders"
  topic_id           = azurerm_servicebus_topic.products_topic.id
  max_delivery_count = 5
}

resource "azurerm_servicebus_subscription_rule" "orders_products_subscription_rule" {
  name            = "event-type-filter"
  subscription_id = azurerm_servicebus_subscription.orders_products_subscription.id
  filter_type     = "SqlFilter"
  sql_filter      = "eventType IN ('order.accepted', 'order.rejected')"
}

resource "azurerm_servicebus_subscription" "carts_products_subscription" {
  name               = "carts"
  topic_id           = azurerm_servicebus_topic.products_topic.id
  max_delivery_count = 5
}

resource "azurerm_servicebus_subscription_rule" "carts_products_subscription_rule" {
  name            = "event-type-filter"
  subscription_id = azurerm_servicebus_subscription.carts_products_subscription.id
  filter_type     = "SqlFilter"
  sql_filter      = "eventType IN ('product.added.to.cart')"
}

resource "azurerm_servicebus_subscription" "orders_users_subscription" {
  name               = "orders"
  topic_id           = azurerm_servicebus_topic.users_topic.id
  max_delivery_count = 5
}

resource "azurerm_servicebus_subscription_rule" "orders_users_subscription_rule" {
  name            = "event-type-filter"
  subscription_id = azurerm_servicebus_subscription.orders_users_subscription.id
  filter_type     = "SqlFilter"
  sql_filter      = "eventType IN ('registration.completed')"
}

resource "azurerm_servicebus_subscription" "users_identity_subscription" {
  name               = "users"
  topic_id           = azurerm_servicebus_topic.identity_topic.id
  max_delivery_count = 5
}

resource "azurerm_servicebus_subscription_rule" "users_identity_subscription_rule" {
  name            = "event-type-filter"
  subscription_id = azurerm_servicebus_subscription.users_identity_subscription.id
  filter_type     = "SqlFilter"
  sql_filter      = "eventType IN ('user.registered')"
}