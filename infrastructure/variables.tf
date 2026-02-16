variable "subscription_id" {
  description = "Azure Subscription Id"
  type        = string
}

variable "environment_name" {
  description = "Environment name"
  type        = string
  default     = "dev"

  validation {
    condition     = contains(["dev", "staging", "prod"], var.environment_name)
    error_message = "Environment name must be dev, staging or prod"
  }
}

variable "service_plan_sku" {
  description = "Service Plan SKU"
  type        = string
  default     = "F1"

  validation {
    condition     = contains(["F1", "B1", "B2"], var.service_plan_sku)
    error_message = "Not allowed SKU"
  }
}