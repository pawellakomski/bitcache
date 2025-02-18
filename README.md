**Bitcache** is a solution that allows you to backup your Bitlocker recovery keys from Entra ID (aka Azure AD) to a **local database**. When the device object is removed from Entra ID, they key is lost. In case you need to unlock the Bitlocker-encrypted volume after some time and the computer object in Entra ID does not exist anymore, the key is not available either and it might be impossible to unlock it. The device might not store the key in Active Directory or use Confiuration Manager (MBAM) agent so making a local backup of keys might be important.

**Requirements**:

* Windows 10/11 OS
* .NET 8.0 Desktop Runtime (https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.13-windows-x64-installer)
* Microsoft SQL Server Express (https://www.microsoft.com/en-us/download/details.aspx?id=104781&lc=1033&msockid=3fa1e886c58b622615fafc6bc49963e6)

**Instructions**

The application requires Microsoft SQL Server Express running on the same machine. The database will be create at the first launch of the application.

After the application is run, you will need to sync the keys from Entra ID to Bitcache.
