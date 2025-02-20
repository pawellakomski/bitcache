**Bitcache** is a solution that allows you to backup your Bitlocker recovery keys from Entra ID (aka Azure AD) to a **local database**. When the device object is removed from Entra ID, they key is lost. In case you need to unlock the Bitlocker-encrypted volume after some time and the computer object in Entra ID does not exist anymore, the key is not available either and it might be impossible to unlock it. The device might not store the key in Active Directory or use Confiuration Manager (MBAM) agent so making a local backup of keys might be important.

**Requirements**:

* Windows 10/11 OS
  
* .NET 8.0 Desktop Runtime (https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.13-windows-x64-installer)
  
* Microsoft SQL Server Express (https://www.microsoft.com/en-us/download/details.aspx?id=104781&lc=1033&msockid=3fa1e886c58b622615fafc6bc49963e6)
  
* Entra ID App Registration - for Bitcache to use Graph API to extract data:
  
  Go to https://entra.microsoft.com
  
  Under Identity click on "Applications" and select "App Registrations". Create a new registration.
  
  Give it any meaningful name, e.g. Bitcache. Leave "Accounts in this organizational directory only (Contoso only - Single tenant)" under "Supported account types". Click on "Register".
  
  ![image](https://github.com/user-attachments/assets/0db075c0-2856-4e48-ac66-b9882a00583d)
  
  In the confguration go to "Authentication" and under "Platform configurations" click on "Add a platform'. Select "Mobile and desktop applications" and under "Custom redirect URIs" add "http://localhost".

  ![image](https://github.com/user-attachments/assets/f5bfb319-0588-4e38-b4a8-3fa491fd8ae6)

  In the confguration go to "API Permission's and add a permission. Select "Delegated" and add the following permissions: BitlockerKey.Read.All, BitlockerKey.ReadBasic.All, Device.Read.All.

  Make sure to click "Grant admin consent for <your org name>". If you don't hold a Global Addmin, Privileged Role Administrator or a smililar one, you might need to ask your tenant administator to consent. See: https://learn.microsoft.com/en-us/entra/identity/enterprise-apps/grant-admin-consent

  ![image](https://github.com/user-attachments/assets/cac3c686-2268-4eee-b685-e18693ede561)

  Go back to "Overview" and note down "Directory (tenant) ID" and "Application (client) ID":

  <img width="881" alt="image" src="https://github.com/user-attachments/assets/b51f8496-8790-4420-bbc0-8037a6cbbbcd" />


**Instructions**:

The application requires Microsoft SQL Server Express running on the same machine. The database will be create at the first launch of the application. If connection to the SQL Server is not possible, you will get an error message and the application will close.

After the application is run, you will need to sync the keys from Entra ID to Bitcache. to do it, you will need to configure tenant and login. At the first run the configuration status will show "Not configured":

<img width="645" alt="image" src="https://github.com/user-attachments/assets/e25a4d3d-3505-4fba-a2ec-a98dfdd5d62e" />

Click on "Configure".

**Roadmap**:

* zero-knowledge data encrypton in the DB
