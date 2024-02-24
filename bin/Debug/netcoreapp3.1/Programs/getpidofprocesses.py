import win32serviceutil

service_config = win32serviceutil.GetServiceConfig("Diagnostic Policy Service")

print("DisplayName:", service_config[0])
print("ServiceType:", service_config[1])
print("StartType:", service_config[2])
print("BinaryPathName:", service_config[3])
print("LoadOrderGroup:", service_config[4])
print("TagID:", service_config[5])
print("Dependencies:", service_config[6])