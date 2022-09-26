variable resource_group_name{
    type = string
    # default = "premier-sql-resources"
}

variable location{
    type = string
    # default = "eastus"
}

variable sql_server_name{
    type = string
    # default = "premiersqlserver203"
}


variable server_login{
    type = string
    # default = "Admin203"
}


variable server_password{
    type = string
    # default = "Admin@203"
}

variable sql_storageaccount{
    type = string
    # default = "premierstorage203"
}

variable sqldatabase-name{
    type = string
    # default = "premiersqldatabase203"
}
