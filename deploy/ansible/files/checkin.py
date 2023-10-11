import json
import requests
import docker
import subprocess
import psutil
import re

client = docker.from_env()
baseUrl = "http://192.168.0.1:8500"
defaultHeaders = {'Content-Type': 'application/json'}
container_list = []

def get_ip_address():

    lines = subprocess.check_output(['ip', 'a'], universal_newlines=True).splitlines()
    hostname = "localhost"
    hostname = subprocess.check_output(['hostname'], universal_newlines=True).strip()
    ip = "0.0.0.0"
    
    for line in lines:
        if not line.startswith(('veth', 'br-')): # Exclude those that start with "veth" or "br-"
            ip_address = re.findall(r"(192\.168\.0\.\d{1,3})", line)

            if len(ip_address) > 0:
                ip = ip_address[0]
    print("ip",ip)
    return ip, hostname

def get_docker_containers():
    containers = []
    for container in client.containers.list():
        if container.name in container_list:            
            containers.append({"containerName":container.name,"status":"OK"})
    return containers

def get_power():
    returnVal = {}
    battery = psutil.sensors_battery()
    try:
        returnVal['Type']  = (1 if battery.power_plugged == True else 0)
        returnVal['batteries'] = {"Name":"Primary", "BatteryLevel": int(battery.percent)}
    finally:
        return returnVal

def post(url, payload):    
    response = requests.request("POST", url, headers=defaultHeaders, data=payload, verify=False)
    print(response)

def get_network():      
    response = requests.request("GET", "https://ipinfo.io", headers=defaultHeaders)
    return json.loads(response.text)

def get_custom_attributes():
    return {
        "vpnStatus": get_nordvpn_status()
    }
    

def get_nordvpn_status():
    returnVal = {}
    for line in subprocess.check_output(['nordvpn', 'status'], universal_newlines=True).splitlines():    
        if "Status" in line:
            status = line.split(":")[1].strip()
            print("status", status)
    
    return status

def post_ping():

    url = f"{baseUrl}/device"
    containers = get_docker_containers()
    power = get_power()
    ip, hostname = get_ip_address()

    payload = json.dumps(            
            {
                "Name":hostname,
                "IpAddress": ip,
                "services":{"dockerServices":containers},
                "power" : power,
                "attributes" : get_custom_attributes()
            }
    )   

    print(json.dumps(json.loads(payload), indent=4))
    post(url, payload)

def get_device(devicename):
    url = f"{baseUrl}/device/search"
    data = {
        'deviceName':devicename
    }
    response = requests.request("GET", url, json=data, headers={}, verify=False)
    print(response.text)
    print(json.dumps(json.loads(response.text), indent=4))

post_ping()
